#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
 __title__ = ''
 __author__ = 'HUANGWEI45'
 __mtime__ = '2019/03/11'
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
['EQ_ID', 'EVT_TIME', 'LAYER', 'LOT_ID', '记录数', 'PART', 'RECIPE_ID',
       'SLOT_ID', 'STAGE', 'VALUE_NUM']
 """
import pandas as pd
df = pd.read_csv('y:/maz_all.csv')
key = df['RECIPE_ID'].unique()
result =[]

for  k,i in enumerate(key[0:]):
	print(k,i)
	tmp= df[df['RECIPE_ID']==i]
	if tmp.shape[0]>250:
		bigNo =90
		data=tmp[['LOT_ID','VALUE_NUM']]
		lotQty = len(tmp['LOT_ID'].unique())  # rework lot missed
		wfrQty = tmp.shape[0]

		tmp = tmp[tmp['VALUE_NUM'] < bigNo]      # extra large data is excluded
		lotQty1 = len(tmp['LOT_ID'].unique())  # rework lot missed
		wfrQty1 = tmp.shape[0]

		tmp=list(tmp['VALUE_NUM'])
		x= [ abs(tmp[n]-tmp[n-1]) for n in range(1,len(tmp)) ]
		mavg=sum(tmp)/len(tmp)
		ravg=sum(x)/len(x)

		spec=[]
		spec.append(i)
		spec.extend([lotQty,wfrQty])
		print(spec)
		for j in [2.66,3,4,5,6,7,8,9,10]:
			s= round(mavg + j*ravg,1)
			spec.append(s)
			data['OOS']=[ (list(data['VALUE_NUM'])[k] - s)>0 for k in range(wfrQty)]
			OOS=data[data['OOS']==True]
			if OOS.shape[0]>0:
				spec.extend([len(OOS['LOT_ID'].unique()),OOS.shape[0]])
			else:
				spec.extend([0,0])
		result.append(spec)
pd.DataFrame(result).to_csv('c:/temp/result.csv')


