# -*- coding: utf-8 -*-
"""
Created on Tue Sep 18 10:22:11 2018

@author: huangwei45
"""


import pandas as pd
import os
import datetime
import win32com.client

def compare(flow):
        
    ## merge bias table ,get OVL measurment sequence
    tmp = pd.read_csv('Y:/overlay/biastable.csv')[['PPID', 'Part', 'OVL-PPID']]
    #some products share identical OVL_PPID,can not merge by OVL_PPID
    #df = pd.merge(flow,tmp,on=['Part','OVL-PPID'],how='left')
    # merge by Layer 
    tmp['Layer'] = [ i.split('-')[-1].strip() for i in tmp['OVL-PPID']]
    flow['Layer'] = [ i.split('-')[-1].strip() for i in flow['OVL-PPID']]
    df = pd.merge(flow,tmp,on=['Part','Layer'],how='left').fillna('')
    df = df.drop('OVL-PPID_y',axis=1)
    df.columns = ['Part','Stage','OVL-PPID','ToolType','Layer','PPID']
    
    
    ## merge step size
    tmp = pd.read_csv('Y:/overlay/stepsize.csv')[['Part', 'StepX', 'StepY']]
    df = pd.merge(df,tmp,on=['Part'],how='left').fillna('')


    ## merge standard coordinate
    tmp = pd.read_csv('Y:/overlay/reference.csv')#[['Part', 'StepX', 'StepY']]
    tmp = tmp[tmp['PPID'].str[-3:] != '3UM']
    tmp['Part'] = [i.split('\\')[3].split('.')[0] for i in list(tmp['Path']) ]
    tmp = tmp[['LDx', 'LDy', 'RDx', 'RDy','RUx', 'RUy' ,'LUx', 'LUy', 'PPID','Part']]
    df = pd.merge(df,tmp,on=['Part','PPID'],how='left').fillna('')

    
    ##merge jobfile coordinates
    tmp = pd.read_csv('Y:/overlay/zuobiao.csv')[[ 'Path', 'Count', '6', '7', '8', '9',
       '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21',
       'OVL-PPID']].fillna('')
    tmp.columns = [ 'Path', 'Count', 'm11', 'm12', 'm21', 'm22',
       'm31', 'm32', 'm41', 'm42', 'm51', 'm52', 'm61', 'm62', 'm71', 'm72', 'm81', 'm82',
       'OVL-PPID']
    
    tmpQ200 = pd.read_csv('y:/overlay/q200.csv').fillna('')
    tmpQ200.columns = ['OVL-PPID', 'm11', 'm12', 'm21', 'm22', 'm31', 'm32', 'm41', 'm42', 'Path']
    tmpQ200['m11']=tmpQ200['m11']/1000
    tmpQ200['m12']=tmpQ200['m12']/1000    
    tmpQ200['m21']=tmpQ200['m21']/1000    
    tmpQ200['m22']=tmpQ200['m22']/1000    
    tmpQ200['m31']=tmpQ200['m31']/1000
    tmpQ200['m32']=tmpQ200['m32']/1000
    tmpQ200['m41']=tmpQ200['m41']/1000  
    tmpQ200['m42']=tmpQ200['m42']/1000        
    tmpQ200['Count']=8        
    tmp = pd.concat([tmp,tmpQ200],axis=0).fillna('')

      
    df = pd.merge(df,tmp,on=['OVL-PPID'],how='left').fillna('')

    
    #string -->numer
    
    
    
        
    
    df[['StepX', 'StepY', 'LDx', 'LDy', 'RDx', 'RDy', 'RUx', 'RUy', 'LUx', 'LUy', 'Count',
       'm11', 'm12', 'm21', 'm22', 'm31', 'm32', 'm41', 'm42', 'm51', 'm52',
       'm61', 'm62', 'm71', 'm72', 'm81', 'm82']]   = df[['StepX', 'StepY', 'LDx', 'LDy', 'RDx', 'RDy', 'RUx', 'RUy', 'LUx', 'LUy', 'Count',
       'm11', 'm12', 'm21', 'm22', 'm31', 'm32', 'm41', 'm42', 'm51', 'm52',
       'm61', 'm62', 'm71', 'm72', 'm81', 'm82']].apply(pd.to_numeric,errors='ignore')
    bak = df.copy()   
 
    
    df = df[df['RDx']==df['RDx']]
    df = df[df['m42']==df['m42']]  
    #Nikon large field
    df1 = df[df['Part'].str.endswith('-L')]
    df1 = df1[df1['ToolType']=='LII']
    df1 = df1[df1['Count']==8]
    
    df1['t11'] = (df1['LDx']/1000 + df1['StepY']/4)  * 1000 
    df1['t12'] = (df1['LDy']/1000 + df1['StepX']/2) * 1000 
    
    df1['t21'] = (df1['RDx']/1000 + df1['StepY']/4)  * 1000 
    df1['t22'] = (df1['RDy']/1000 + df1['StepX']/2)   * 1000 
    
    df1['t31'] = (df1['RUx']/1000 + df1['StepY']/4)  * 1000 
    df1['t32'] = (df1['RUy']/1000 + df1['StepX']/2)   * 1000 
    
    df1['t41'] = (df1['LUx']/1000 + df1['StepY']/4)   * 1000 
    df1['t42'] = (df1['LUy']/1000 + df1['StepX']/2)     * 1000 
    
    
    
    df1['d11'] = ((df1['LDx']/1000 + df1['StepY']/4) - df1['m11'])  * 1000 
    df1['d12'] = ((df1['LDy']/1000 + df1['StepX']/2) - df1['m12'])  * 1000 
    
    df1['d21'] = ((df1['RDx']/1000 + df1['StepY']/4) - df1['m21'])  * 1000 
    df1['d22'] = ((df1['RDy']/1000 + df1['StepX']/2) - df1['m22'])  * 1000 
    
    df1['d31'] = ((df1['RUx']/1000 + df1['StepY']/4) - df1['m31'])  * 1000 
    df1['d32'] = ((df1['RUy']/1000 + df1['StepX']/2) - df1['m32'])  * 1000 
    
    df1['d41'] = ((df1['LUx']/1000 + df1['StepY']/4) - df1['m41'])  * 1000 
    df1['d42'] = ((df1['LUy']/1000 + df1['StepX']/2) - df1['m42'])   * 1000   
    
    
    
    

    df1['c11'] = abs((df1['LDx']/1000 + df1['StepY']/4) - df1['m11']) <0.020
    df1['c12'] = abs((df1['LDy']/1000 + df1['StepX']/2) - df1['m12']) <0.020
    
    df1['c21'] = abs((df1['RDx']/1000 + df1['StepY']/4) - df1['m21']) <0.020
    df1['c22'] = abs((df1['RDy']/1000 + df1['StepX']/2) - df1['m22']) <0.020
    
    df1['c31'] = abs((df1['RUx']/1000 + df1['StepY']/4) - df1['m31']) <0.020
    df1['c32'] = abs((df1['RUy']/1000 + df1['StepX']/2) - df1['m32']) <0.020
    
    df1['c41'] = abs((df1['LUx']/1000 + df1['StepY']/4) - df1['m41']) <0.020
    df1['c42'] = abs((df1['LUy']/1000 + df1['StepX']/2) - df1['m42']) <0.020  
    
    df1['check'] = [ df1['c11'][i] and df1['c12'][i] and df1['c21'][i] and df1['c22'][i] and 
                     df1['c31'][i] and df1['c32'][i] and df1['c41'][i] and df1['c42'][i]     
                    for i in df1.index]
    
    
    xx=df1[['d11','d21','d31','d41']].T.describe().T
    yy=df1[['d12','d22','d32','d42']].T.describe().T   
    df1['X-Range']=xx['max']-xx['min']
    df1['Y-Range']=yy['max']-yy['min']
    
    


    #Asml large filed
    df2 = df[df['Part'].str.endswith('-L')]
    df2 = df2[df2['ToolType']=='LDI']
    df2 = df2[df2['Count']==16]
    
    
    df2['t11'] = (df2['RDy']/1000 + df2['StepX']/2)  * 1000 
    df2['t12'] = (-df2['RDx']/1000 + df2['StepY']/4)  * 1000 
    
    df2['t21'] = (df2['RUy']/1000 + df2['StepX']/2)  * 1000 
    df2['t22'] = (-df2['RUx']/1000 + df2['StepY']/4) * 1000 
    
    df2['t31'] = (df2['LUy']/1000 + df2['StepX']/2)  * 1000 
    df2['t32'] = (-df2['LUx']/1000 + df2['StepY']*3/4)  * 1000 
    
    df2['t41'] = (df2['LDy']/1000 + df2['StepX']/2)  * 1000 
    df2['t42'] = (-df2['LDx']/1000 + df2['StepY']*3/4)  * 1000 
    
    df2['t51'] = (df2['RDy']/1000 + df2['StepX']/2)  * 1000 
    df2['t52'] = (-df2['RDx']/1000 + df2['StepY']*3/4)  * 1000 
    
    df2['t61'] = (df2['RUy']/1000 + df2['StepX']/2)  * 1000 
    df2['t62'] = (-df2['RUx']/1000 + df2['StepY']*3/4)  * 1000 
    
    df2['t71'] = (df2['LUy']/1000 + df2['StepX']/2) * 1000 
    df2['t72'] = (-df2['LUx']/1000 + df2['StepY']/4) * 1000 
    
    df2['t81'] = (df2['LDy']/1000 + df2['StepX']/2)  * 1000 
    df2['t82'] = (-df2['LDx']/1000 + df2['StepY']/4)  * 1000 





    
    
    df2['d11'] = ((df2['RDy']/1000 + df2['StepX']/2) - df2['m11'])  * 1000 
    df2['d12'] = ((-df2['RDx']/1000 + df2['StepY']/4) - df2['m12'])  * 1000 
    
    df2['d21'] = ((df2['RUy']/1000 + df2['StepX']/2) - df2['m21'])  * 1000 
    df2['d22'] = ((-df2['RUx']/1000 + df2['StepY']/4) - df2['m22']) * 1000 
    
    df2['d31'] = ((df2['LUy']/1000 + df2['StepX']/2) - df2['m31'])  * 1000 
    df2['d32'] = ((-df2['LUx']/1000 + df2['StepY']*3/4) - df2['m32'])  * 1000 
    
    df2['d41'] = ((df2['LDy']/1000 + df2['StepX']/2) - df2['m41'])  * 1000 
    df2['d42'] = ((-df2['LDx']/1000 + df2['StepY']*3/4) - df2['m42'])  * 1000 
    
    df2['d51'] = ((df2['RDy']/1000 + df2['StepX']/2) - df2['m51'])  * 1000 
    df2['d52'] = ((-df2['RDx']/1000 + df2['StepY']*3/4) - df2['m52'])  * 1000 
    
    df2['d61'] = ((df2['RUy']/1000 + df2['StepX']/2) - df2['m61']) * 1000 
    df2['d62'] = ((-df2['RUx']/1000 + df2['StepY']*3/4) - df2['m62'])  * 1000 
    
    df2['d71'] = ((df2['LUy']/1000 + df2['StepX']/2) - df2['m71'])  * 1000 
    df2['d72'] = ((-df2['LUx']/1000 + df2['StepY']/4) - df2['m72'])  * 1000 
    
    df2['d81'] = ((df2['LDy']/1000 + df2['StepX']/2) - df2['m81'])  * 1000 
    df2['d82'] = ((-df2['LDx']/1000 + df2['StepY']/4) - df2['m82'])  * 1000 
    
    
    
    
    
    
    
    
    
    
    df2['c11'] = abs((df2['RDy']/1000 + df2['StepX']/2) - df2['m11']) <0.020
    df2['c12'] = abs((-df2['RDx']/1000 + df2['StepY']/4) - df2['m12']) <0.020
    
    df2['c21'] = abs((df2['RUy']/1000 + df2['StepX']/2) - df2['m21']) <0.020
    df2['c22'] = abs((-df2['RUx']/1000 + df2['StepY']/4) - df2['m22']) <0.020
    
    df2['c31'] = abs((df2['LUy']/1000 + df2['StepX']/2) - df2['m31']) <0.020
    df2['c32'] = abs((-df2['LUx']/1000 + df2['StepY']*3/4) - df2['m32']) <0.020
    
    df2['c41'] = abs((df2['LDy']/1000 + df2['StepX']/2) - df2['m41']) <0.020
    df2['c42'] = abs((-df2['LDx']/1000 + df2['StepY']*3/4) - df2['m42']) <0.020
    
    df2['c51'] = abs((df2['RDy']/1000 + df2['StepX']/2) - df2['m51']) <0.020
    df2['c52'] = abs((-df2['RDx']/1000 + df2['StepY']*3/4) - df2['m52']) <0.020
    
    df2['c61'] = abs((df2['RUy']/1000 + df2['StepX']/2) - df2['m61']) <0.020
    df2['c62'] = abs((-df2['RUx']/1000 + df2['StepY']*3/4) - df2['m62']) <0.020
    
    df2['c71'] = abs((df2['LUy']/1000 + df2['StepX']/2) - df2['m71']) <0.020
    df2['c72'] = abs((-df2['LUx']/1000 + df2['StepY']/4) - df2['m72']) <0.020
    
    df2['c81'] = abs((df2['LDy']/1000 + df2['StepX']/2) - df2['m81']) <0.020
    df2['c82'] = abs((-df2['LDx']/1000 + df2['StepY']/4) - df2['m82']) <0.020 
    
    df2['check'] = [ df2['c11'][i] and df2['c12'][i] and df2['c21'][i] and df2['c22'][i] and 
                     df2['c31'][i] and df2['c32'][i] and df2['c41'][i] and df2['c42'][i] and
                     df2['c51'][i] and df2['c52'][i] and df2['c61'][i] and df2['c62'][i] and 
                     df2['c71'][i] and df2['c72'][i] and df2['c81'][i] and df2['c82'][i] 
                    for i in df2.index]
    
    xx=df2[['d11','d21','d31','d41','d51','d61','d71','d81']].T.describe().T
    yy=df2[['d12','d22','d32','d42','d52','d62','d72','d82']].T.describe().T   
    df2['X-Range']=xx['max']-xx['min']
    df2['Y-Range']=yy['max']-yy['min']
        
    
    
    
    

    #standard field
    df3 = df[df['Part'].str[-2:] != '-L']
    
    df3['t11'] = (df3['LDx']/1000 + df3['StepX']/2) * 1000
    df3['t12'] = (df3['LDy']/1000 + df3['StepY']/2) * 1000 
    
    df3['t21'] = (df3['RDx']/1000 + df3['StepX']/2) * 1000  
    df3['t22'] = (df3['RDy']/1000 + df3['StepY']/2) * 1000   
    
    df3['t31'] = (df3['RUx']/1000 + df3['StepX']/2) * 1000  
    df3['t32'] = (df3['RUy']/1000 + df3['StepY']/2) * 1000   
    
    df3['t41'] = (df3['LUx']/1000 + df3['StepX']/2) * 1000  
    df3['t42'] = (df3['LUy']/1000 + df3['StepY']/2) * 1000   


    
    df3['d11'] = ((df3['LDx']/1000 + df3['StepX']/2) - df3['m11']) * 1000 
    df3['d12'] = ((df3['LDy']/1000 + df3['StepY']/2) - df3['m12']) * 1000 
    
    df3['d21'] = ((df3['RDx']/1000 + df3['StepX']/2) - df3['m21'])  * 1000 
    df3['d22'] = ((df3['RDy']/1000 + df3['StepY']/2) - df3['m22'])  * 1000 
    
    df3['d31'] = ((df3['RUx']/1000 + df3['StepX']/2) - df3['m31'])  * 1000 
    df3['d32'] = ((df3['RUy']/1000 + df3['StepY']/2) - df3['m32']) * 1000  
    
    df3['d41'] = ((df3['LUx']/1000 + df3['StepX']/2) - df3['m41'])  * 1000 
    df3['d42'] = ((df3['LUy']/1000 + df3['StepY']/2) - df3['m42'])  * 1000 
    
    
    
    
    df3['c11'] = abs((df3['LDx']/1000 + df3['StepX']/2) - df3['m11']) <0.020
    df3['c12'] = abs((df3['LDy']/1000 + df3['StepY']/2) - df3['m12']) <0.020
    
    df3['c21'] = abs((df3['RDx']/1000 + df3['StepX']/2) - df3['m21']) <0.020
    df3['c22'] = abs((df3['RDy']/1000 + df3['StepY']/2) - df3['m22']) <0.020
    
    df3['c31'] = abs((df3['RUx']/1000 + df3['StepX']/2) - df3['m31']) <0.020
    df3['c32'] = abs((df3['RUy']/1000 + df3['StepY']/2) - df3['m32']) <0.020
    
    df3['c41'] = abs((df3['LUx']/1000 + df3['StepX']/2) - df3['m41']) <0.020
    df3['c42'] = abs((df3['LUy']/1000 + df3['StepY']/2) - df3['m42']) <0.020
    
    df3['check']  = [ df3['c11'][i] and df3['c12'][i] and df3['c21'][i] and df3['c22'][i] and 
                     df3['c31'][i] and df3['c32'][i] and df3['c41'][i] and df3['c42'][i]     
                    for i in df3.index]
    
    xx=df3[['d11','d21','d31','d41']].T.describe().T
    yy=df3[['d12','d22','d32','d42']].T.describe().T   
    df3['X-Range']=xx['max']-xx['min']
    df3['Y-Range']=yy['max']-yy['min']
    
    
    
    df = pd.concat([df1,df2,df3])[[
            't11', 't12', 't21', 't22', 't31', 't32', 't41', 't42', 
            't51', 't52', 't61', 't62', 't71', 't72', 't81', 't82',
            'd11', 'd12', 'd21', 'd22', 'd31', 'd32', 'd41', 'd42', 
            'd51', 'd52', 'd61', 'd62', 'd71', 'd72', 'd81', 'd82',
            'c11', 'c12', 'c21', 'c22', 'c31', 'c32', 'c41', 'c42', 
            'c51', 'c52', 'c61', 'c62', 'c71', 'c72', 'c81', 'c82', 
            'check','X-Range','Y-Range']].fillna('')
    
    
    df['range-check'] = [(list(df['X-Range']<10)[i] and list(df['Y-Range']<10)[i]) for i in range(df.shape[0])]
    
    
    df = pd.concat([bak,df],axis=1).fillna('')
    df['No'] = df['Part'].str[2:6]
    df=df.sort_values(by='No')
    df = df[df['No'].str.isdigit()==True]
    

    
    
    df.to_csv('Y:/overlay/check.csv')
    
    log = open('y:/overlay/log.txt','a')
    log.write("\n\n" + str(datetime.datetime.now())[0:19] +'_  '+ str(df.shape[0]) +'  _Total Recipes Actually')
    log.write("\n" + str(datetime.datetime.now())[0:19] +'_  '+ str(df[df['check']!=''].shape[0]) +'  _OVL Recipes Are Checked')
    log.write("\n" + str(datetime.datetime.now())[0:19] +'_  '+ str(df[df['check'] ==True].shape[0]) +'  _OVL Recipes Are Correct')
    log.write("\n" + str(datetime.datetime.now())[0:19] +'_  '+ str(df[df['check'] == False].shape[0]) +'  _OVL Recipes Are Wrong')
    log.close()
    
    return df
def list_measurement_files_to_be_download(df):


    
    df = df[df['LUy'] != '']
    
    tmp = pd.read_excel('Z:/_DailyCheck/CD_SEM_Recipe/check/_PPID.xlsm',sheet_name='Move')
    tmp = tmp['Extracted'].str.split('$',expand=True)
    tmp['Flag1']=True
    tmp.columns = ['Part','Stage','Flag1']
    
  
    
    df = pd.merge(df,tmp,on= ['Part','Stage'],how='left')

    
    
    df  = df [['No','Part', 'Stage', 'OVL-PPID', 'ToolType', 'PPID', 'StepX', 'StepY',
       'LDx', 'LDy', 'RDx', 'RDy', 'RUx', 'RUy', 'LUx', 'LUy', 'Count', 'Path',
       'm11', 'm12', 'm21', 'm22', 'm31', 'm32', 'm41', 'm42', 'm51', 'm52',
       'm61', 'm62', 'm71', 'm72', 'm81', 'm82', 't11', 't12', 't21', 't22',
       't31', 't32', 't41', 't42', 't51', 't52', 't61', 't62', 't71', 't72',
       't81', 't82', 'd11', 'd12', 'd21', 'd22', 'd31', 'd32', 'd41', 'd42',
       'd51', 'd52', 'd61', 'd62', 'd71', 'd72', 'd81', 'd82', 'c11', 'c12',
       'c21', 'c22', 'c31', 'c32', 'c41', 'c42', 'c51', 'c52', 'c61', 'c62',
       'c71', 'c72', 'c81', 'c82',  'X-Range', 'Y-Range','check',
       'range-check',  'Flag1']]
    df.columns = ['No','Part', 'Stage', 'OVL-PPID', 'ToolType', 'PPID', 'StepX', 'StepY',
       'LDx', 'LDy', 'RDx', 'RDy', 'RUx', 'RUy', 'LUx', 'LUy', 'Count', 'Path',
       'm11', 'm12', 'm21', 'm22', 'm31', 'm32', 'm41', 'm42', 'm51', 'm52',
       'm61', 'm62', 'm71', 'm72', 'm81', 'm82', 't11', 't12', 't21', 't22',
       't31', 't32', 't41', 't42', 't51', 't52', 't61', 't62', 't71', 't72',
       't81', 't82', 'd11', 'd12', 'd21', 'd22', 'd31', 'd32', 'd41', 'd42',
       'd51', 'd52', 'd61', 'd62', 'd71', 'd72', 'd81', 'd82', 'c11', 'c12',
       'c21', 'c22', 'c31', 'c32', 'c41', 'c42', 'c51', 'c52', 'c61', 'c62',
       'c71', 'c72', 'c81', 'c82',  'X-Range', 'Y-Range','absolute-check',
       'range-check',  'run']
    

    df = df.sort_values(by='No') 
    

    
    
    
    df.to_csv('y:/overlay/Result.csv',index=False)   
    #log = open('y:/overlay/log.txt','a')
    #log.write("\n" + str(datetime.datetime.now())[0:19] +'_  '+ str(df[df['PendingDownload']==True].shape[0]) +'  _recipes to be downloaded for check\n')
    #log.close()
def New_Part():
    p1 = 'P:/_NewBiasTable/'
    p2 = 'P:/Recipe/Coordinate/'
    newpart = [  i[:-8] for i in os.listdir(p1) if i[-7:-4].upper()=='NEW']

    for k,part in enumerate( newpart ):
        part = part.upper()
        try:
            f = [ i for i in  open(p2 + part + '.txt') if 'BAR IN BAR' in i] 
            if len(f) > 0:
                newpart[k]=[part,True,True]
            else:
                newpart[k]=[part,False,True]
        except:
            newpart[k]=[part,False,False]
    df = pd.DataFrame(newpart,columns=['Part','Coordinate','ToolingForm'])

    
    df = df[df['ToolingForm']==True]
    df = df[df['Coordinate']==False]
    df['No'] = df['Part'].str[2:6]
    df = df.sort_values(by='No')

class OvlCheck:

    def __init__(self):
        pass
    def openDB(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        return conn
    def get_path(self,FileDir=r'P:\OVLdata\GOL'):
        filenamelist = []
        for root, dirs, files in os.walk(FileDir,
                                         False):  # root:所有目录名-->字符串 #dirs: root下的子目录名-->列表 #files：root下的文件名-->列表 # name.endswith(ext)-->文件名筛选
            for names in files:
                filenamelist.append(root + '\\' + names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def read_raw_data(self):
        filelist = set(self.get_path(FileDir=r'P:\OVLdata\Old'))
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select distinct Path from ZB"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        old=[]
        for i in range(rs.recordcount):
            old.append(rs.fields('Path').value)
            rs.movenext
        filelist = filelist - set(old)  #to remove [0:-2]
        result,fail = [],[]
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = 'select * from zb'
        rs.Open(sql, conn, 1, 3)
        for n, file in enumerate(filelist):
            print(n,file)
            transfer = []
            try:
                f = [i.strip('\n') for i in open(file).readlines()]
                RiQi = f[0].split('\t')[0]
                Folder = f[2].split(' ')[1][6:-1]
                Part = Folder.split('\\')[0]
                Ppid =Folder.split('\\')[1]
                tmp = [i for i in f if i[0:8] == ('Location')]
                zuobiao = [(eval(i.split(' ')[1][2:]), eval(i.split(' ')[3][2:])) for i in tmp]
                if  len(zuobiao)>0:
                    tmp = pd.DataFrame(zuobiao).drop_duplicates()

                    zb = tmp.values.reshape(tmp.shape[0] * tmp.shape[1], order="C")
                    # multiwafers measured, delete duplicates of coordinate
                    # to keep ID sequence,swithched to DataFrame ,otherwise use "SET"
                    # zuobiao =list(  zip (tmp[0],tmp[1]) )
                    tmp = tmp.describe().loc['max']
                    Max_x, Max_y,Path,Count = tmp[0], tmp[1],file,len(zb)
                    tmp=['' for i in range(16)]
                    if Count//8>0 and Count%8==0:

                        for i in range(Count):
                            tmp[i]=zb[i]
                        rs.AddNew()  # 添加一条新记录
                        rs.Fields('Path').Value = Path
                        rs.Fields('Folder').Value = Folder
                        rs.Fields('Part').Value = Part
                        rs.Fields('Ppid').Value = Ppid
                        rs.Fields('RiQi').Value = RiQi
                        rs.Fields('Max_x').Value = Max_x
                        rs.Fields('Max_y').Value = Max_y
                        rs.Fields('Count').Value = Count
                        rs.Fields('A1').Value = tmp[0]
                        rs.Fields('A2').Value = tmp[1]
                        rs.Fields('A3').Value = tmp[2]
                        rs.Fields('A4').Value = tmp[3]
                        rs.Fields('A5').Value = tmp[4]
                        rs.Fields('A6').Value = tmp[5]
                        rs.Fields('A7').Value = tmp[6]
                        rs.Fields('A8').Value = tmp[7]
                        rs.Fields('A9').Value = tmp[8]
                        rs.Fields('A10').Value = tmp[9]
                        rs.Fields('A11').Value = tmp[10]
                        rs.Fields('A12').Value = tmp[11]
                        rs.Fields('A13').Value = tmp[12]
                        rs.Fields('A14').Value = tmp[13]
                        rs.Fields('A15').Value = tmp[14]
                        rs.Fields('A16').Value = tmp[15]
                        rs.Update()  # 更新
                else:
                    fail.append(file)
            except:
                fail.append(file)
        # conn.close

        for i in fail:
            os.remove(i)

        # get Q200 coordinate
        FileDir = 'Z:\\_DailyCheck\\Q200_LD\\'
        filenamelist=set(self.get_path(FileDir))
        filenamelist= [i for i in list(filenamelist) if i[-2:]=='ld']
        filenamelist = list( set(filenamelist) - set(old) )
        filenamelist.sort(reverse=False)
        Q200 = []

        for file in filenamelist[:]:
            print(file)
            data = []
            try:
                f = [i.strip('\n') for i in open(file).readlines()]
                for n, tmp in enumerate(f):
                    if tmp == '\t\t\tlocations=4,':
                        Path=file
                        Ppid=file.split('\\')[5][:-3]
                        data.extend([eval(f[n + 2][10:].split(',')[0]), eval(f[n + 2][10:].split(',')[1][0:-1])])
                        data.extend([eval(f[n + 4][10:].split(',')[0]), eval(f[n + 4][10:].split(',')[1][0:-1])])
                        data.extend([eval(f[n + 6][10:].split(',')[0]), eval(f[n + 6][10:].split(',')[1][0:-1])])
                        data.extend([eval(f[n + 8][10:].split(',')[0]), eval(f[n + 8][10:].split(',')[1][0:-1])])
                if len(data) == 8:
                    rs.AddNew()  # 添加一条新记录
                    rs.Fields('Path').Value = Path
                    rs.Fields('Ppid').Value = Ppid
                    rs.Fields('Count').Value = 8
                    rs.Fields('A1').Value = data[0]/1000
                    rs.Fields('A2').Value = data[1]/1000
                    rs.Fields('A3').Value = data[2]/1000
                    rs.Fields('A4').Value = data[3]/1000
                    rs.Fields('A5').Value = data[4]/1000
                    rs.Fields('A6').Value = data[5]/1000
                    rs.Fields('A7').Value = data[6]/1000
                    rs.Fields('A8').Value = data[7]/1000
                    rs.Update()  # 更新
            except:
                pass
        conn.close
    def read_standard_coordinate(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select distinct Path from OVL_STANDARD"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        old = []
        if rs.recordcount>0:
            for i in range(rs.recordcount):
                old.append(rs.fields('Path').value)
                rs.movenext

        filelist = []
        path = r'P:\Recipe\Coordinate'
        filelist = self.get_path(path)
        filelist = [ i for i in filelist if i[-3:]=='txt']

        filelist =list( set(filelist) - set(old))
        fail = []
        sql = "select * from OVL_STANDARD"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        for n, file in enumerate(filelist):
            print(n,len(filelist))
            try:
                f = [i.strip('\n') for i in open(file).readlines() if (i.strip('\n') != '' and 'BAR ' in i)]
                if len(f) > 4:
                    f = [i.replace('\t\t\t', '_') for i in f]
                    f = [i.replace('\t\t', '_') for i in f]

                    ld = f.index('BAR IN BAR\tLD\tbar_bar')
                    rd = f.index('BAR IN BAR\tRD\tbar_bar')
                    lu = f.index('BAR IN BAR\tLU\tbar_bar')
                    ru = f.index('BAR IN BAR\tRU\tbar_bar')

                    LD = [i.split(' ')[1] for i in f[ld + 1:rd]]
                    LD = pd.DataFrame([i.split('_') for i in LD]).reset_index().set_index(0)
                    LD = LD.drop(columns='index')
                    LD.columns = ['LDx', 'LDy']

                    RD = [i.split(' ')[1] for i in f[rd + 1:lu]]
                    RD = pd.DataFrame([i.split('_') for i in RD]).reset_index().set_index(0)
                    RD = RD.drop(columns='index')
                    RD.columns = ['RDx', 'RDy']

                    LU = [i.split(' ')[1] for i in f[lu + 1:ru]]
                    LU = pd.DataFrame([i.split('_') for i in LU]).reset_index().set_index(0)
                    LU = LU.drop(columns='index')
                    LU.columns = ['LUx', 'LUy']

                    RU = [i.split(' ')[1] for i in f[ru + 1:]]
                    RU = pd.DataFrame([i.split('_') for i in RU]).reset_index().set_index(0)
                    RU = RU.drop(columns='index')
                    RU.columns = ['RUx', 'RUy']
                    tmp = pd.concat([LD, RD, RU, LU], axis=1)
                    tmp = tmp.reset_index()
                    tmp.columns = ['PPID', 'LDx', 'LDy', 'RDx', 'RDy', 'RUx', 'RUy', 'LUx', 'LUy']
                    Path = file
                    Part = file.split('\\')[3][:-4]
                    for i in range(tmp.shape[0]):
                        rs.AddNew()  # 添加一条新记录
                        rs.Fields('Path').Value = Path
                        rs.Fields('Part').Value = Part
                        rs.Fields('Ppid').Value = tmp.iloc[i,0]
                        rs.Fields('LDx').Value = tmp.iloc[i,1]
                        rs.Fields('LDy').Value = tmp.iloc[i,2]
                        rs.Fields('RDx').Value = tmp.iloc[i,3]
                        rs.Fields('RDy').Value = tmp.iloc[i,4]
                        rs.Fields('RUx').Value = tmp.iloc[i,5]
                        rs.Fields('RUy').Value = tmp.iloc[i,6]
                        rs.Fields('LUx').Value = tmp.iloc[i,7]
                        rs.Fields('LUy').Value = tmp.iloc[i,8]
                        rs.Update()  # 更新


            except:
                pass
                fail.append(file)
        conn.close
    def read_asml_step_size(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select distinct Path from STEP_SIZE"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        old = []
        if rs.recordcount>0:
            for i in range(rs.recordcount):
                old.append(rs.fields('Path').value)
                rs.movenext


        asmlfilepath = 'P:/Recipe/ASMLBACKUP/'  # only latest files
        asmlfilepathnew = 'P:/Recipe/recipe/'
        filelist = []
        for file in os.listdir(asmlfilepath):
            filelist.append(os.path.join(asmlfilepath, file))
        for file in os.listdir(asmlfilepathnew):
            filelist.append(os.path.join(asmlfilepathnew, file))

        filelist =list( set(filelist) - set(old))

        stepSize = []
        fail = []
        sql = 'select * from STEP_SIZE'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        for n, file in enumerate(filelist):
            print(n, '--', file)
            try:
                tmp = [i.strip('\n') for i in open(file).readlines() if i.strip()[0:22] == 'Cell Size [mm]       X'][
                    0].strip().split(':')
                Path=file
                Part = file.split('/')[3]
                StepX=tmp[1].strip().split(' ')[0]
                StepY=tmp[2].strip()
                if StepX.strip()!='':
                    rs.AddNew()  # 添加一条新记录
                    rs.Fields('Path').Value = Path
                    rs.Fields('Part').Value = Part
                    rs.Fields('StepX').Value = StepX
                    rs.Fields('StepY').Value = StepY
                    rs.Update()  # 更新
            except:
                fail.append(file)
        conn.close
    def read_bias_table(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select distinct Path from BT"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        old = []
        if rs.recordcount > 0:
            for i in range(rs.recordcount):
                old.append(rs.fields('Path').value)
                rs.movenext

        path = 'P:/recipe/biastable/'
        filelist = []
        for file in os.listdir(path):
            filelist.append(os.path.join(path, file))

        filelist =list( set(filelist) -set(old))

        tmp1 = pd.DataFrame(columns=['Path', 'Part', 'Ppid', 'Ovl_Ppid'])
        fail = []

        sql = "select * from BT"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        for n, file in enumerate(filelist[:]):
            print(n, '===', len(filelist))
            try:
                BiasTable = pd.read_excel(file)
                BiasTable.columns = [i for i in range(len(BiasTable.columns))]
                BiasTable = BiasTable[[6, 19]].dropna()
                tmp = []
                for i in list(BiasTable[6]):
                    if len(i) > 3:
                        tmp.append(i[-3:-1])
                    else:
                        tmp.append(i)

                BiasTable['Ppid'] = [tmp[i] + '-' + list(BiasTable[19])[i] for i in range(len(tmp))]
                BiasTable['Part'] = file.split('/')[3].split('.')[0]
                BiasTable['Ovl-Ppid'] = BiasTable['Part'] + '-' + tmp  # BiasTable[6]
                BiasTable = BiasTable.drop(columns=[6, 19])
                BiasTable['Path'] = file
                BiasTable=BiasTable.reset_index().drop('index',axis=1)
                if BiasTable.shape[0]>0:
                    for i in range(BiasTable.shape[0]):
                        rs.AddNew()  # 添加一条新记录
                        rs.Fields('Ppid').Value = BiasTable.iloc[i,0]
                        rs.Fields('Part').Value = file.split('/')[3].split('.')[0]
                        rs.Fields('Ovl_Ppid').Value = BiasTable.iloc[i,2]
                        rs.Fields('Path').Value = file
                        rs.Update()  # 更新
            except:
                fail.append(file)
        conn.close
    def refresh_ppid_from_xls(self):

        # Part,OVL PPID, scanner/stepper type -->standard naming rule from MFG DB
        # referencepath = 'Y:/overlay/reference.csv'


        mfg = pd.read_excel('Z:/_DailyCheck/CD_SEM_Recipe/check/_PPID.xlsm', sheet_name='PPID')
        tmp1 = mfg[['PART.1', 'STAGE.1', 'PPID.1']].dropna()
        tmp1.columns = ['Part', 'Stage', 'OVL-PPID']
        tmp2 = mfg[['TECH', 'PART.2', 'STAGE.2', 'ToolType']].dropna()
        tmp2.columns = ['TECH', 'Part', 'Stage', 'ToolType']
        flow = pd.merge(tmp1, tmp2, how='left', on=['Part', 'Stage'])
        tmp = list(flow['ToolType'])
        for i, value in enumerate(tmp):
            if value == 'LSI':
                tmp[i] = 'LII'
        flow['ToolType'] = tmp
        flow = flow.drop_duplicates()
        flow = flow[['Part', 'Stage', 'OVL-PPID', 'ToolType']]
        #=======================================================
        tmp = mfg[['PART', 'STAGE', 'PPID']].dropna()
        tmp.columns = ['PART','STAGE','CD']
        tmp1 = flow[['Part','Stage','ToolType','OVL-PPID']]
        tmp1.columns=['PART','STAGE','ToolType','OVL']
        tmp = pd.merge(tmp,tmp1,how='outer',on=['PART','STAGE'])
        tmp = tmp.fillna('')
        tmp=tmp.reset_index().drop('index',axis=1)

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        for i in range(tmp.shape[0]):
            sql = "select * from PPID where PART='" + tmp.iloc[i,0] + "' and STAGE='" + tmp.iloc[i,1] + "'"
            print(i,tmp.shape[0])
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)
            if rs.recordcount==0:
                rs.AddNew()  # 添加一条新记录
                rs.Fields('PART').Value = tmp.iloc[i,0]
                rs.Fields('STAGE').Value = tmp.iloc[i,1]
                rs.Fields('CD').Value = tmp.iloc[i,2]
                rs.Fields('ToolType').Value = tmp.iloc[i,3]
                rs.Fields('OVL').Value = tmp.iloc[i,4]
                rs.Update()  # 更新
            else:
                pass




        return flow
    def ppid_flag(self):
        #f1 是否有标准坐标
        #f2 bias table是否有测量对准顺序
        #f3 是否有asml stepping size
        #f4 是否有程序坐标
        #AutoCheck 自检结果
        #f5 是否已自检


        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)

        ######################################################################################
        #get part list with ovl coordinate
        #确认是否有标准坐标数据，f1
        sql = "select distinct Part  from OVL_STANDARD"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp=[]
        for i in range(rs.recordcount):
            tmp.append(rs.fields(0).value)
            rs.movenext
        # table 'PPID'--> 'f1'-->part with coordiante
        for part in tmp:
            try:
                sql = "update PPID set f1=True where PART='" + part + "'"
                conn.Execute(sql)
            except:
                pass

        ################################################################################################3
        # 'f3'-->asml step size ready
        sql = "select distinct PART from PPID WHERE f1=True"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp = []
        for i in range(rs.recordcount):
            tmp.append(rs.fields(0).value)
            rs.movenext
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        for part in tmp:
            sql = "select part from STEP_SIZE where Part='" + part + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount > 0:
                sql = "update PPID set f3=True where PART='" + part + "'"
                conn.Execute(sql)
            else:
                pass
############################################################################################
        # 'f2'-->bias table alignment tree
        sql = "select distinct OVL from PPID where f1=True"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp = []
        for i in range(rs.recordcount):
            tmp.append(rs.fields(0).value)
            rs.movenext
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        for ovl in tmp:
            sql = "select Ovl_Ppid from BT where Ovl_Ppid='" + ovl + "'"
            tmprs=win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount>0:
                sql = "update PPID set f2=True where OVL='" + ovl + "'"
                conn.Execute(sql)
            else:
                pass
        #f4-->rcipe coordinate downloaded
            sql = "select Ppid from ZB where Ppid='" + ovl + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount > 0:
                sql = "update PPID set f4=True where OVL='" + ovl + "'"
                conn.Execute(sql)
            else:
                pass
        conn.close
    def auto_check(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)

        sql = "select PART,ToolType,OVL from PPID where f4=True and f3=True and f2=True and f1=True"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp=[]

        for i in range(rs.recordcount):
            tmp.append([rs.fields('PART').value,rs.fields('ToolType').value,rs.fields('OVL').value])
            rs.movenext

        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        count = 0
        for i in range(len(tmp)):
            print(i,len(tmp),tmp[i])


            stepx,stepy,ovlto,recipeZb,refZb='','','','',''
            part = tmp[i][0]
            sql = "select StepX,StepY from STEP_SIZE where Part='"+part+"'"
            tmprs= win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount>0:
                stepx,stepy=eval(tmprs.fields('StepX').value),eval(tmprs.fields('StepY').value)

            ppid=tmp[i][2]
            sql = "select Ppid from BT Where Ovl_Ppid='" + ppid + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount>0:
                ovlto=tmprs.fields('Ppid').value.replace(' ','')

            sql = "Select A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,Count from ZB where Ppid='" + ppid + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            recipeZb=[eval(tmprs.fields(k).value) for k in range(int(eval(tmprs.fields('Count').value)))]



            sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='"+part + "' and Ppid='"+ ovlto + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount>0:
                refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value),
                         eval(tmprs.fields('RDx').value), eval(tmprs.fields('RDY').value),
                         eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
                         eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)] #类似MIM层次，CE命名不规范，部分ovlto下有多组坐标

            ToolType=tmp[i][1]

            if stepx!='' and stepy!='' and ovlto!='' and recipeZb!='' and refZb!='':
                if part[-2:].upper()=="-L" and ToolType.upper()=='LII':
                    c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
                    c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020

                    c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
                    c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020

                    c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
                    c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020

                    c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
                    c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020

                    if  c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
                        AutoCheck='Correct'
                    else:
                        AutoCheck='Wrong'

                    sql = "update PPID set AutoCheck='" + AutoCheck +   "' where PART='"+part + "' and OVL='"+ppid + "'"
                    conn.Execute(sql)
                elif part[-2:].upper()=="-L" and ToolType.upper()=='LDI':
                    if len(recipeZb)==16:
                        c11 = abs((refZb[3] / 1000  + stepx / 2) - recipeZb[0]) < 0.020
                        c12 = abs((-refZb[2] / 1000  + stepy / 4) - recipeZb[1]) < 0.020

                        c21 = abs((refZb[5] / 1000  + stepx / 2) - recipeZb[2]) < 0.020
                        c22 = abs((-refZb[4] / 1000  + stepy / 4) - recipeZb[3]) < 0.020

                        c31 = abs((refZb[7] / 1000  + stepx / 2) - recipeZb[4]) < 0.020
                        c32 = abs((-refZb[6] / 1000  + stepy * 3 / 4) - recipeZb[5]) < 0.020

                        c41 = abs((refZb[1] / 1000  + stepx / 2) - recipeZb[6]) < 0.020
                        c42 = abs((-refZb[0] / 1000  + stepy * 3 / 4) - recipeZb[7]) < 0.020

                        c51 = abs((refZb[3] / 1000  + stepx / 2) - recipeZb[8]) < 0.020
                        c52 = abs((-refZb[2] / 1000  + stepy * 3 / 4) - recipeZb[9]) < 0.020

                        c61 = abs((refZb[5] / 1000  + stepx / 2) - recipeZb[10]) < 0.020
                        c62 = abs((-refZb[4] / 1000  + stepy * 3 / 4) - recipeZb[11]) < 0.020

                        c71 = abs((refZb[7] / 1000  + stepx / 2) - recipeZb[12]) < 0.020
                        c72 = abs((-refZb[6] / 1000  + stepy / 4) - recipeZb[13]) < 0.020

                        c81 = abs((refZb[1] / 1000  + stepx / 2) - recipeZb[14]) < 0.020
                        c82 = abs((-refZb[0] / 1000  + stepy / 4) - recipeZb[15]) < 0.020
                        if False in [c11,c12,c21,c22,c31,c32,c41,c42,c51,c52,c61,c62,c71,c72,c81,c82]:
                            AutoCheck='Wrong'
                        else:
                            AutoCheck = 'Correct'
                        sql = "update PPID set AutoCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                        conn.Execute(sql)
                    else:
                        sql = "update PPID set AutoCheck='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
                        conn.Execute(sql)
                else:
                    c11 = abs((refZb[0] / 1000 + stepx  / 2) - recipeZb[0]) < 0.020
                    c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020

                    c21 = abs((refZb[2] / 1000 + stepx  / 2) - recipeZb[2]) < 0.020
                    c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020

                    c31 = abs((refZb[4] / 1000 + stepx  / 2) - recipeZb[4]) < 0.020
                    c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020

                    c41 = abs((refZb[6] / 1000 + stepx  / 2) - recipeZb[6]) < 0.020
                    c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
                    if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
                        AutoCheck = 'Wrong'
                    else:
                        AutoCheck = 'Correct'
                    sql = "update PPID set AutoCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                    conn.Execute(sql)
            else:
                sql = "update PPID set AutoCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
                conn.Execute(sql)
            sql = "update PPID set f5=True,RiQi='" +str(datetime.datetime.now())[:10].replace('-','') + "' where PART='" + part + "' and OVL='" + ppid + "'"
            conn.Execute(sql)
        conn.close






if __name__ == "__main__":
    pass
    # OvlCheck().read_raw_data()
    # OvlCheck().read_standard_coordinate()
    # OvlCheck().read_asml_step_size()
    # OvlCheck().read_bias_table()
    # OvlCheck().ppid_flag()
    OvlCheck().auto_check()






    # list_measurement_files_to_be_download(df)