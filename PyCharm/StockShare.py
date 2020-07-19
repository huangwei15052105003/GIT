import numpy as np
import pandas as pd
import csv


# https://www.cnblogs.com/sanduzxcvbnm/p/10250222.html
for i in range(1,178):  # 爬取全部177页数据
	url = 'http://s.askci.com/stock/a/?reportTime=2017-12-31&pageNum=%s' % (str(i))
	url="http://quote.eastmoney.com/center/gridlist.html#fund_lof"
	tb = pd.read_html(url)[3] #经观察发现所需表格是网页中第4个表格，故为[3]
	tb.to_csv(r'1.csv', mode='a', encoding='utf_8_sig', header=1, index=0)
	print('第'+str(i)+'页抓取完成')
