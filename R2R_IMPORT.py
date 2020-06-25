#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
 __title__ = ''
 __author__ = 'HUANGWEI45'
 __mtime__ = '2019/04/16'
 # code is far away from bugs with the god animal protecting
     I love animals. They taste delicious.
               ┏┓      ┏┓
             ┏┛┻━━━┛┻┓
             ┃      ☃      ┃
             ┃  ┳┛  ┗┳  ┃
             ┃      ┻      ┃
             ┗━┓      ┏━┛
                 ┃      ┗━━━┓
                 ┃  神兽保佑    ┣┓
                 ┃　永无BUG！   ┏┛
                 ┗┓┓┏━┳┓┏┛
                   ┃┫┫  ┃┫┫
                   ┗┻┛  ┗┻┛
 """

import pandas as pd

nikon=pd.DataFrame(columns=['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part',
           'Layer', 'EqId', 'CD', 'DOSE', 'FOCUS'])
asml=pd.DataFrame(columns=['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part',
           'Layer', 'EqId', 'CD', 'DOSE', 'FOCUS'])

try:
    nikon = pd.read_excel('c:/temp/nikon.xls')
    nikon = nikon[['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part',
           'Layer', 'EqId', 'CD', 'DOSE', 'FOCUS']]
except:
    pass
try:
    asml = pd.read_excel('c:/temp/asml.xls')
    asml = asml [['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part',
           'Layer', 'EqId', 'CD', 'DOSE', 'FOCUS']]
except:
    pass
output = pd.read_csv("c:/temp/R2R.csv")
focus = pd.concat([nikon,asml],axis=0)
print(focus.shape)
max=pd.DataFrame(focus.groupby(['Part','Layer','EqId'])['FOCUS'].max())
min=pd.DataFrame(focus.groupby(['Part','Layer','EqId'])['FOCUS'].min())

tmp = pd.concat([max,min],axis=1)
tmp.columns=['max','min']
tmp['Flag']=tmp['max']==tmp['min']
focus=tmp[tmp['Flag']==True][['max']].reset_index()
nikon,asml,max,min,tmp=None,None,None,None,None
focus.columns=['PART','LAYER','TOOL','FOCUS']
r2r = pd.merge(output,focus,how='left',on=['PART','LAYER','TOOL'])
r2r.columns=['DCOLL_TIME', 'PART', 'LAYER', 'TOOL', 'TYPE', 'tran-x', 'tran-y',
       'exp-x', 'exp-y', 'non-ort', 'w-rot', 'mag', 'rot',
       'asym-mag', 'asym-rot', 'DCOLL_TIME-1', 'JI_TIME', 'PART-1',
       'LAYER-1', 'TOOL-1', 'TYPE-1', 'AVG', 'JOBIN', 'OPT', 'FB', 'RN',
       'QTY-1','FOCUS']



dummy=pd.DataFrame(['' for i in range(r2r.shape[0])])
datain= r2r[['TYPE','PART','LAYER']]
for i in range(13):
    datain=pd.concat([datain,dummy],axis=1)



datain['EqId']=r2r['TOOL']
datain=pd.concat([datain,dummy],axis=1)
datain['Value']=r2r['OPT']
datain['Up']=r2r['OPT']*1.1
datain['Down']=r2r['OPT']*0.9
datain['Value1']=r2r['FOCUS']
datain['Up1']=r2r['FOCUS'] + [0.01 for i in range(r2r.shape[0])]
datain['Down1']=r2r['FOCUS'] + [-0.01 for i in range(r2r.shape[0])]
datain = pd.concat([datain,pd.DataFrame(r2r['tran-x'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['tran-y'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['exp-x'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)

datain = pd.concat([datain,pd.DataFrame(r2r['exp-y'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['non-ort'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['w-rot'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['mag'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['rot'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['asym-mag'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain = pd.concat([datain,pd.DataFrame(r2r['asym-rot'])],axis=1)
for i in range(2):
    datain=pd.concat([datain,dummy],axis=1)
datain['Lock']='PPCS IMPORT,PLS PILOT RUN'
datain['Fix']='N'
datain['Constrain']='N'
datain['TrackRecipe']=' '
datain.columns= [ i for i in range(1,59)]
tmp=[i for i in range(1,49)]
tmp.extend([55,56,57,58])

datain[datain[1]=='ASML'].to_csv('c:/temp/asml_import.csv',index=None,header=None)
datain = datain[tmp]
datain[datain[1]=='NIKON'].to_csv('c:/temp/nikon_import.csv',index=None,header=None)
