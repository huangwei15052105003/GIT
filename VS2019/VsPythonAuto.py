#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
 __title__ = ''
 __author__ = 'HUANGWEI45'
 __mtime__ = '2019/04/10'
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

import os
import pandas as pd
import sqlite3
import tarfile
from sklearn.linear_model import LinearRegression
import numpy as np
import shutil
import datetime
import gc
from dateutil import parser
from PIL import Image,ImageDraw,ImageFont













#import ftplib
#
#
#
#import win32com.client
#from dateutil import parser
#from PIL import Image,ImageDraw,ImageFont
#from math import isnan
# from matplotlib.gridspec import GridSpec
#import gc
#import pandas as pd
# import matplotlib.pyplot as plt
#import matplotlib.dates as mdates
#import matplotlib.lines as mlines
# import re
# import matplotlib.ticker as ticker
# r'D:\HuangWeiScript\PyTaskCode\R2R_New_Part.xlsm'
# 'Z:\\_DailyCheck\\NikonShot\\transfer\\move.xls'
# os.system('z:\\_dailycheck\\ESF\\ESF.xlsm')

#below for wafer map
#from matplotlib.patches import Circle, Rectangle,Polygon
#import matplotlib.pyplot as plt
# from math import sqrt as sqrt
#import win32com.client
# import threading
# from multiprocessing import Process
# import math
#import time
#import datetime


#import tarfile


"""
    def Sqlite3Demo(self):
        #http://club.excelhome.net/thread-1381302-1-1.html?tdsourcetag=s_pctim_aiomsg
        #VBA和SQLite数据库交互

        #https: // www.jb51.net / article / 116556.htm
        # 事物处理


        df = pd.read_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\index.csv')
        conn = sqlite3.connect('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\sqlite3Awe.db')
        df.to_sql('tbl_index',conn,if_exists='append',index=None)

        '''
        if_exists: {'fail', 'replace', 'append'},default  'fail'
        - fail: If table exists, do   nothing.
        - replace: If  table   exists, drop   it, recreate     it, and insert      data.
        - append: If  table   exists, insert  data.Create if does  not exist.
        '''
        cursor = conn.cursor()
        query = cursor.execute("select * from sqlite_master where type = 'table'")#查看所有表的表结构
        query = cursor.execute('select * from sqlite_master where name = "tbl_index"')#查看某个表的表结构
        query = cursor.execute('select * from tbl_index')







        no=0
        for row in query:
            print([i for i in row])
            no += 1
            if no>10:
                break


        conn.close()


        #t1=datetime.datetime.now()
        conn = sqlite3.connect('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\sqlite3Awe.db')
        conn.execute('delete from tbl_index')
        conn.execute("COMMIT;")
        conn.execute("VACUUM")
        cursor = conn.cursor()
        # conn.execute("BEGIN TRANSACTION;")  # 关键点
        for i in range(1000):
            cursor.execute("insert into tbl_index values (?, ?,?,?,?,?,?,?,?)", ('1','2','3','4','5','6','7','8','9'))
        conn.execute("COMMIT;")  # 关键点
       # t2=datetime.datetime.now()
        #print(str(t2-t1))
        conn.close()

        conn = sqlite3.connect('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\sqlite3Awe.db')
        cursor = conn.cursor()
        query = cursor.execute('select * from tbl_index')
        l=query.fetchall()
        print(len(l))
        conn.close()

        conn = sqlite3.connect('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\sqlite3Awe.db')
        cursor = conn.cursor()
        query = cursor.execute('select max(cast(substr(No,3,10) as int)) from tbl_index')
        l = query.fetchall()
        print(len(l))
        conn.close()

"""

class Sqlite3Databas:
    def __init__(self):
        pass
    # 新建NikonEga数据，需将原数据库全部删除
    def NikonParameter(self): #import data from 10.4.3.130 and convert them to Sqlite3
        if 1==1: # modeling data
            filenamelist = []
            rootpath = '\\\\10.4.3.130\\ftpdata\\litho\\ExcelCsvFile\\NikonEga\\'
            for root, dirs, files in os.walk(rootpath, False):
                for names in files:
                    if 'parameter.csv' in names:
                        filenamelist.append(root + '\\' +names)

        df = pd.read_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\Index.csv')
        df["No"] = [i[2:] for i in df["No"]]
        df["No"] = df["No"].astype('int32')
        df['Date']=[i[-17:-5] for i in df['File']]
        df["Date"]=df["Date"].astype('int64')
        df = df.drop(['File'],axis=1)

        conn=sqlite3.connect('//10.4.72.74/asml/_sqlite/NikonEgaPara.db')
        df.to_sql('tbl_index', conn, if_exists='append', index=None)

        for n,file in enumerate(filenamelist[:]):


            if "\\2020" in file:
                print(n, len(filenamelist), file)

                df = pd.read_csv(file)
                df=df[['wfrNo', 'No', 'scalingx', 'scalingy', 'rot', 'ort']]
                df["No"] = [i[2:] for i in df["No"]]
                df["No"] = df["No"].astype('int32')
                df =df .fillna('')

                df.to_sql('tbl_parameter', conn, if_exists='append', index=None)
        conn.close()

        if 2==2:# vector data
            filenamelist=[]
            for root, dirs, files in os.walk(rootpath, False):
                for names in files:
                    if 'vector.csv' in names:
                        filenamelist.append(root + '\\' +names)
        conn = sqlite3.connect('//10.4.72.74/asml/_sqlite/NikonEgaPara.db')
        for n, file in enumerate(filenamelist[:]):

            if 1 == 1 and  "\\2020" in file:
                print(n, len(filenamelist), file)
                df = pd.read_csv(file)
                df = df.fillna('')
                coordinate = df[['x', 'y', 'No', 'shot','type']]
                coordinate=coordinate[coordinate["type"]=="coordinate"]
                coordinate=coordinate.drop_duplicates()
                coordinate=coordinate[['No','x','y','shot']]
                coordinate["No"] = [i[2:] for i in coordinate["No"]]
                coordinate["No"] = coordinate["No"].astype('int32')

                residual= df[['No','wfr','shot','x', 'y','type']]
                residual=residual[residual["type"]=="residual"]
                residual = residual.drop_duplicates()
                residual = residual[['No','wfr','shot','x', 'y']]
                residual["No"] = [i[2:] for i in residual["No"]]
                residual["No"] = residual["No"].astype('int32')



                residual.to_sql('tbl_'+file.split('\\')[-3], conn, if_exists='append', index=None)
                coordinate.to_sql('tbl_coordinate', conn, if_exists='append', index=None)

        conn.close()

    def AsmlBatchReport(self):
        if 1==1:
            conn = sqlite3.connect('//10.4.72.74/asml/_sqlite/AsmlBatchreport.db')
            rootpath = '\\\\10.4.72.74\\litho\\ASML_BATCH_REPORT\\'
            for tool in ['82','83','85','86','87','89','8A','8B','8C','7D','08']:
                file = rootpath + tool + '_batchreport.csv'
                df = pd.read_csv(file).drop(['path'],axis=1)
                df =df.fillna("")
                df.to_sql('tbl_asmlbatchreport', conn, if_exists='append', index=None)
            conn.close()

    def ReduceAweFileSize(self):
        root= "//10.4.3.130/ftpdata/Litho/ExcelCsvFile/AsmlAwe/"
        tools=["ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD7D", "BLSD08"]
        for tool in tools:
            file=root + tool+"/2020_01/vector.csv"
            print(file)
            df=pd.read_csv(file)
            df["tmp"]=[i[2:] for i in df["No"]]
            df["tmp"]=df["tmp"].astype("int64")
            tmpIndex=df.loc[int(df.shape[0]/3*2)]['tmp']
            df=df[df["tmp"]>tmpIndex]
            df=df.drop(["tmp"],axis=1)
            df.to_csv(root + tool+"/2020_01/activeVector.csv",index=None)

class AWE_ANALYSIS_NEW_TAR:
    def __init__(self):
        pass

    def linearfit(self, XYin):

        linreg = LinearRegression()
        lot = pd.DataFrame()
        para = []
        for i in XYin['WaferNr'].unique():
            wfr = XYin[XYin['WaferNr'] == i].dropna()
            if XYin.columns[-1][1] == 'X':
                input_y = wfr[XYin.columns[-1]] - wfr['NomPosX']
            else:
                input_y = wfr[XYin.columns[-1]] - wfr['NomPosY']

            input_x = wfr[['NomPosX', 'NomPosY']]
            model = linreg.fit(input_x, input_y)
            tmp = pd.DataFrame(linreg.intercept_ + linreg.coef_[0] * input_x['NomPosX'] + linreg.coef_[1] * input_x[
                'NomPosY'] - input_y)
            tmp.columns = [XYin.columns[-1][:-3] + 'Residual']
            tmp = -1 * tmp
            wfr = pd.concat([wfr, tmp], axis=1)
            lot = pd.concat([lot, wfr], axis=0)
            para.append([linreg.intercept_, linreg.coef_[0], linreg.coef_[1]])
        lot[['NomPosX', 'NomPosY']] = lot[['NomPosX', 'NomPosY']].apply(
            lambda x: round(x * 1000, 4))  # 标准坐标转换为mm，四位有效数字
        lot[list(lot.columns[-2:])] = lot[list(lot.columns[-2:])].astype('float64')
        lot[list(lot.columns[-2:])] = lot[list(lot.columns[-2:])].apply(lambda x: round(x * 1000, 6))
        para = pd.DataFrame(para).apply(lambda x: round(x * 1000000, 3))
        if 'VectorZoom' == 'VectorZoom':
            position = lot.columns[-2];
            residual = lot.columns[-1]
            if 'X' in position:
                lot[position] = [round(x - (x - y) * 1000000, 4) for x, y in zip(lot['NomPosX'], lot[position])]
                lot[residual] = [round(x + y * 1000000, 4) for x, y in zip(lot['NomPosX'], lot[residual])]
            else:
                lot[position] = [round(x - (x - y) * 1000000, 4) for x, y in zip(lot['NomPosY'], lot[position])]
                lot[residual] = [round(x + y * 1000000, 4) for x, y in zip(lot['NomPosY'], lot[residual])]

        return lot, para

    def read_single_awe(self, name):
        try:
            tmp = open(("C:"+name))
            # f = [i.strip() for i in open(name) if i.strip('\n') != ""]
            f = [i.strip() for i in tmp if i.strip('\n') != ""]
            tmp.close()
        except:
            tmp.close()
        if eval(f[-1].split('\t')[3]) >= 1:  # lot with wfr qty >=20
            # basic information================================================

            riqi = f[[i for i, k in enumerate(f) if k[0:17] == 'Date(YYYY/MM/DD)='][0]].split("=")[1]
            shijian = f[[i for i, k in enumerate(f) if k[0:15] == 'Time(HR:MM:SS)='][0]].split("=")[1]
            part = f[[i for i, k in enumerate(f) if k[0:6] == 'JobID='][0]].split("/")[1]
            layer = f[[i for i, k in enumerate(f) if k[0:8] == 'LayerID='][0]].split("=")[1]
            tool = f[[i for i, k in enumerate(f) if k[0:14] == 'MachineNumber='][0]].split("=")[1]
            lot = f[[i for i, k in enumerate(f) if k[0:8] == 'BatchID='][0]].split("=")[1]

            # measurement data
            n = [i for i, k in enumerate(f) if k[0:17] == 'AlignmentStrategy'][0]  # flag for inline data
            df = pd.DataFrame([i.split('\t') for i in f])
            tmp = df.loc[n + 1:, ]
            tmp.columns = df.loc[n:n].T[n]
            df = tmp.copy()
            mark = tmp['MarkVariant'].iloc[0]
            align = tmp['AlignmentStrategy'].iloc[0]
            basic = [riqi, shijian, part, layer, tool, lot, mark, align, name]
            tmp, riqi, shijian, part, layer, tool, lot, mark, align, name = None, None, None, None, None, None, None, None, None, None

            # =========================================================================================
            validNo = df[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', 'BasicMarkType']].drop_duplicates()
            validX = validNo[
                validNo['BasicMarkType'].str.contains('-X')]  # [['MarkNr','NomPosX','NomPosY']].drop_duplicates()
            validY = validNo[
                validNo['BasicMarkType'].str.contains('-Y')]  # [['MarkNr','NomPosX','NomPosY']].drop_duplicates()
            validNo = [validX.shape[0]]
            validNo.append(validY.shape[0])
            # validX = validX[['MarkNr','NomPosX','NomPosY']].drop_duplicates()
            # validY = validY[['MarkNr','NomPosX','NomPosY']].drop_duplicates()

            valid = ['1XRedValid', '3XRedValid', '5XRedValid', '7XRedValid', '88XRedValid', '1XGreenValid',
                     '3XGreenValid', '5XGreenValid', '7XGreenValid', '88XGreenValid', '1YRedValid', '3YRedValid',
                     '5YRedValid', '7YRedValid', '88YRedValid', '1YGreenValid', '3YGreenValid', '5YGreenValid',
                     '7YGreenValid', '88YGreenValid']

            # dela shift
            for n, order in enumerate(valid[0:20]):
                if '88' in order:
                    tmp = order[0:3]
                else:
                    tmp = order[0:2]

                if tmp[0:2] == '88':
                    # print(order,tmp)
                    tmp1 = df[df[valid[n - 4]] == 'T']
                    tmp1 = tmp1[tmp1[valid[n]] == 'T'][
                        ['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', tmp + order[3:-5] + 'Pos',
                         tmp + order[3:-5] + 'MCC', tmp + order[3:-5] + 'WQ']]
                    validNo.append(tmp1.shape[0])
                else:
                    # print(order,tmp)
                    tmp1 = df[df[valid[n]] == 'T'][
                        ['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', tmp + order[2:-5] + 'Pos',
                         tmp + order[2:-5] + 'MCC', tmp + order[2:-5] + 'WQ']]
                    validNo.append(tmp1.shape[0])
                if "X" in order:
                    validX = pd.merge(validX, tmp1, how='left', on=['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY'])
                else:
                    validY = pd.merge(validY, tmp1, how='left', on=['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY'])
            validX[
                ['NomPosX', 'NomPosY', '1XRedPos', '1XRedMCC', '1XRedWQ', '3XRedPos', '3XRedMCC', '3XRedWQ', '5XRedPos',
                 '5XRedMCC', '5XRedWQ', '7XRedPos', '7XRedMCC', '7XRedWQ', '88XRedPos', '88XRedMCC', '88XRedWQ',
                 '1XGreenPos', '1XGreenMCC', '1XGreenWQ', '3XGreenPos', '3XGreenMCC', '3XGreenWQ', '5XGreenPos',
                 '5XGreenMCC', '5XGreenWQ', '7XGreenPos', '7XGreenMCC', '7XGreenWQ', '88XGreenPos', '88XGreenMCC',
                 '88XGreenWQ']] = validX[
                ['NomPosX', 'NomPosY', '1XRedPos', '1XRedMCC', '1XRedWQ', '3XRedPos', '3XRedMCC', '3XRedWQ', '5XRedPos',
                 '5XRedMCC', '5XRedWQ', '7XRedPos', '7XRedMCC', '7XRedWQ', '88XRedPos', '88XRedMCC', '88XRedWQ',
                 '1XGreenPos', '1XGreenMCC', '1XGreenWQ', '3XGreenPos', '3XGreenMCC', '3XGreenWQ', '5XGreenPos',
                 '5XGreenMCC', '5XGreenWQ', '7XGreenPos', '7XGreenMCC', '7XGreenWQ', '88XGreenPos', '88XGreenMCC',
                 '88XGreenWQ']].astype(float)
            validY[
                ['NomPosX', 'NomPosY', '1YRedPos', '1YRedMCC', '1YRedWQ', '3YRedPos', '3YRedMCC', '3YRedWQ', '5YRedPos',
                 '5YRedMCC', '5YRedWQ', '7YRedPos', '7YRedMCC', '7YRedWQ', '88YRedPos', '88YRedMCC', '88YRedWQ',
                 '1YGreenPos', '1YGreenMCC', '1YGreenWQ', '3YGreenPos', '3YGreenMCC', '3YGreenWQ', '5YGreenPos',
                 '5YGreenMCC', '5YGreenWQ', '7YGreenPos', '7YGreenMCC', '7YGreenWQ', '88YGreenPos', '88YGreenMCC',
                 '88YGreenWQ']] = validY[
                ['NomPosX', 'NomPosY', '1YRedPos', '1YRedMCC', '1YRedWQ', '3YRedPos', '3YRedMCC', '3YRedWQ', '5YRedPos',
                 '5YRedMCC', '5YRedWQ', '7YRedPos', '7YRedMCC', '7YRedWQ', '88YRedPos', '88YRedMCC', '88YRedWQ',
                 '1YGreenPos', '1YGreenMCC', '1YGreenWQ', '3YGreenPos', '3YGreenMCC', '3YGreenWQ', '5YGreenPos',
                 '5YGreenMCC', '5YGreenWQ', '7YGreenPos', '7YGreenMCC', '7YGreenWQ', '88YGreenPos', '88YGreenMCC',
                 '88YGreenWQ']].astype(float)

            validX['XRedDelta'] = validX['1XRedPos'] - validX['88XRedPos']
            validX['XGreenDelta'] = validX['1XGreenPos'] - validX['88XGreenPos']
            validY['YRedDelta'] = validY['1YRedPos'] - validY['88YRedPos']
            validY['YGreenDelta'] = validY['1YGreenPos'] - validY['88YGreenPos']
            return basic, validNo, validX, validY

    def MainFunction(self):
        dbPath="P:\\_SQLite\\AsmlAwe.db"
        if 'dict' == 'dict':
            toolid = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                      '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                      '3527': 'BLSD08'}
        if "GetFileList" == "GetFileList":  # new file 'Z:\\AsmlAweFile\\RawData\\'
            filelist = []
            toollist = ['7D', '08', '82', '83', '85', '86', '87', '89', '8A', '8B', '8C']
            for tool in toollist[:]:
                path = 'P:\\_AsmlDownload\\AWE\\' + tool
                for root, dirs, files in os.walk(path, False):
                    for names in files:
                        filelist.append(root + '\\' + names)
            filelist = [file for file in filelist if file.split('\\')[4][0:8] != 'FINISHED']
        if "getIndex" == "getIndex":
            conn= sqlite3.connect(dbPath)
            cursor=conn.cursor()
            sql = " select max(id) from tbl_index"
            cursor.execute(sql)
            No=cursor.fetchall()
            No=No[0][0]
            cursor.close()
            conn.close()
            if No is None:
                No=0


        if 'ReadFile' == 'ReadFile':
            for i, zipfile in enumerate(filelist[:]):
                allVector = {};
                allResidual = {};
                allParameter = {};
                allX_WQ_MCC_DELTA = {};
                allY_WQ_MCC_DELTA = {};
                index = {}
                print(No, i, zipfile, len(filelist))
                if os.path.isdir('c:/usr'):
                    try:
                        shutil.rmtree('c:/usr')
                    except:
                        pass
                tar = tarfile.open(zipfile)

                for aaa, fileinfo in enumerate(tar.getmembers()[:]):

                    file = fileinfo.name;
                    print(No, aaa, len(tar.getmembers()), file)
                    tar.extract(file, 'c:/')
                    #######################################################################################
                    #######################################################################################

                    basic, validNo, validX, validY = None, None, None, None
                    try:
                        if 'getdata' == 'getdata':
                            basic, validNo, validX, validY = self.read_single_awe(file)
                            # validNo = [validNo[2],validNo[4],validNo[7],validNo[9],validNo[12],validNo[14],validNo[17],validNo[19]] #['1XRedValid',  '5XRedValid',  '1XGreenValid', '5XGreenValid', '1YRedValid','5YRedValid', '1YGreenValid',  '5YGreenValid'] 有效点数
                            if 'generate smooth color dynamic data' == 'generate smooth color dynamic data':  # NaN data for WQ/Position??
                                try:
                                    validX['5XSmPos'] = ''
                                    for i in range(validX.shape[0]):
                                        if validX.iloc[i, 28] < 0.00001 or validX.iloc[i, 13] / validX.iloc[
                                            i, 28] >= 10:  # red/green>10
                                            validX.iloc[i, 37] = validX.iloc[i, 11]
                                        elif validX.iloc[i, 13] < 0.00001 or validX.iloc[i, 13] / validX.iloc[
                                            i, 28] <= 0.1:  # red/green<0.1
                                            validX.iloc[i, 37] = validX.iloc[i, 26]
                                        else:
                                            validX.iloc[i, 37] = validX.iloc[i, 11] * validX.iloc[i, 13] / (
                                                        validX.iloc[i, 13] + validX.iloc[i, 28]) + validX.iloc[i, 26] * \
                                                                 validX.iloc[i, 28] / (
                                                                             validX.iloc[i, 13] + validX.iloc[i, 28])
                                    validY['5YSmPos'] = ''
                                    for i in range(validY.shape[0]):
                                        if validY.iloc[i, 28] < 0.00001 or validY.iloc[i, 13] / validY.iloc[
                                            i, 28] >= 10:  # red/green>10
                                            validY.iloc[i, 37] = validY.iloc[i, 11]
                                        elif validY.iloc[i, 13] < 0.00001 or validY.iloc[i, 13] / validY.iloc[
                                            i, 28] <= 0.1:  # red/green<0.1
                                            validY.iloc[i, 37] = validY.iloc[i, 26]
                                        else:
                                            validY.iloc[i, 37] = validY.iloc[i, 11] * validY.iloc[i, 13] / (
                                                    validY.iloc[i, 13] + validY.iloc[i, 28]) + validY.iloc[i, 26] * \
                                                                 validY.iloc[i, 28] / (
                                                                             validY.iloc[i, 13] + validY.iloc[i, 28])
                                    validX = validX[
                                        ['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', 'BasicMarkType', '1XRedPos',
                                         '1XRedMCC', '1XRedWQ', '5XRedPos', '5XRedMCC', '5XRedWQ', '1XGreenPos',
                                         '1XGreenMCC', '1XGreenWQ', '5XGreenPos', '5XGreenMCC', '5XGreenWQ',
                                         'XRedDelta', 'XGreenDelta', '5XSmPos']]
                                    validY = validY[
                                        ['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', 'BasicMarkType', '1YRedPos',
                                         '1YRedMCC', '1YRedWQ', '5YRedPos', '5YRedMCC', '5YRedWQ', '1YGreenPos',
                                         '1YGreenMCC', '1YGreenWQ', '5YGreenPos', '5YGreenMCC', '5YGreenWQ',
                                         'YRedDelta', 'YGreenDelta', '5YSmPos']]
                                except:
                                    print('calcualte smooth-color-dynamic data error, script stopped')
                                    print(file)
                        if 'basic' == 'basic':
                            if 'compileData' == 'compileData':
                                basic[0] = basic[0].replace('/', '_')
                                basic[1] = basic[1].replace(':', '_')
                                basic[4] = toolid[basic[4]]
                                b = pd.DataFrame(basic[:-1]).T
                                b.columns = ['date', 'time', 'part', 'layer', 'tool', 'lot', 'mark', 'recipe']
                                No += 1
                                b['No'] = 'No' + str(No)
                                # src = basic[-1]
                                # dst = '\\\\10.4.72.74\\litho\\ASML_AWE\\RawData\\' + basic[4] +'\\'+ basic[0][0:7] + '\\' +basic[0]+basic[1]+'#'+basic[2]+"#"+basic[3]+'#'+basic[5]+"#" + basic[6] + "#" + basic[7]
                                # if os.path.exists('\\\\10.4.72.74\\litho\\ASML_AWE\\RawData\\' + basic[4] + '\\'+ basic[0][0:7] + '\\'):
                                #     pass
                                # else:
                                #     os.makedirs('\\\\10.4.72.74\\litho\\ASML_AWE\\RawData\\' + basic[4] + '\\'+ basic[0][0:7] + '\\')
                                # shutil.move(src,dst)

                                if len(index) == 0:
                                    index['index'] = b.copy()
                                else:
                                    index['index'] = pd.concat([index['index'], b.copy()], axis=0)

                                # if os.path.exists(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv'):  #     b.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv',index=None,header=None,mode='a')  # else:  #     b.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv', index=None)
                            if 'makedir' == 'makedir':
                                if os.path.exists(
                                        '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '\\' +
                                        basic[0][:7] + '\\'):
                                    pass
                                else:
                                    os.makedirs(
                                        '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '\\' +
                                        basic[0][:7] + '\\')
                        if 'WQ_MCC_delta' == 'WQ_MCC_delta':
                            tmp = validX[
                                ['WaferNr', 'MarkNr', '1XRedMCC', '1XRedWQ', '5XRedMCC', '5XRedWQ', '1XGreenMCC',
                                 '1XGreenWQ', '5XGreenMCC', '5XGreenWQ', 'XRedDelta', 'XGreenDelta']].copy()
                            tmp['No'] = 'No' + str(No)
                            tmp = tmp.fillna('')
                            if (basic[4] + basic[0][:7]) in allX_WQ_MCC_DELTA.keys():
                                allX_WQ_MCC_DELTA[basic[4] + basic[0][:7]] = pd.concat(
                                    [allX_WQ_MCC_DELTA[basic[4] + basic[0][:7]], tmp.copy()], axis=0)
                            else:
                                allX_WQ_MCC_DELTA[basic[4] + basic[0][:7]] = tmp.copy()
                            tmp = validY[
                                ['WaferNr', 'MarkNr', '1YRedMCC', '1YRedWQ', '5YRedMCC', '5YRedWQ', '1YGreenMCC',
                                 '1YGreenWQ', '5YGreenMCC', '5YGreenWQ', 'YRedDelta', 'YGreenDelta']].copy()
                            tmp['No'] = 'No' + str(No)
                            if (basic[4] + basic[0][:7]) in allY_WQ_MCC_DELTA.keys():
                                allY_WQ_MCC_DELTA[basic[4] + basic[0][:7]] = pd.concat(
                                    [allY_WQ_MCC_DELTA[basic[4] + basic[0][:7]], tmp.copy()], axis=0)
                            else:
                                allY_WQ_MCC_DELTA[basic[4] + basic[0][:7]] = tmp.copy()

                            # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'X_WQ_MCC_DELTA.csv'):  #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'X_WQ_MCC_DELTA.csv',index=None,header=None,mode='a')  # else:  #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + 'X_WQ_MCC_DELTA.csv', index=None)  # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'Y_WQ_MCC_DELTA.csv'):  #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'Y_WQ_MCC_DELTA.csv',index=None,header=None,mode='a')  # else:  #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'Y_WQ_MCC_DELTA.csv',index=None)
                        if 'fit' == 'fit':
                            if 'normalizeStandardCoordinate' == 'normalizeStandardCoordinate':
                                X = validX[['MarkNr', 'NomPosX', 'NomPosY']].drop_duplicates().copy()
                                Y = validY[['MarkNr', 'NomPosX', 'NomPosY']].drop_duplicates().copy()

                                count = 0
                                for i in X['NomPosX']:
                                    for j in Y['NomPosX']:
                                        if count == 0:
                                            delta = i - j
                                            count += 1
                                        else:
                                            if abs(i - j) < abs(delta):
                                                delta = i - j
                                                count += 1
                                validY['NomPosX'] = [i + delta for i in validY['NomPosX']]

                                count = 0
                                for i in Y['NomPosY']:
                                    for j in X['NomPosY']:
                                        if count == 0:
                                            delta = i - j
                                            count += 1
                                        else:
                                            if abs(i - j) < abs(delta):
                                                delta = i - j
                                                count += 1
                                validX['NomPosY'] = [i + delta for i in validX['NomPosY']]
                            if 'runFit' == 'runFit':
                                if 'fit' == 'fit':
                                    XYin = validX[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', '5XRedPos']]
                                    red5x = self.linearfit(XYin)

                                    XYin = validY[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', '5YRedPos']]
                                    red5y = self.linearfit(XYin)

                                    XYin = validX[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', '5XGreenPos']]
                                    green5x = self.linearfit(XYin)

                                    XYin = validY[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', '5YGreenPos']]
                                    green5y = self.linearfit(XYin)

                                    XYin = validX[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', '5XSmPos']]
                                    sm5x = self.linearfit(XYin)

                                    XYin = validY[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', '5YSmPos']]
                                    sm5y = self.linearfit(XYin)

                                    lot = pd.merge(red5x[0], green5x[0], how='outer',
                                                   on=['WaferNr', 'NomPosX', 'NomPosY'])
                                    lot = pd.merge(lot, red5y[0], how='outer', on=['WaferNr', 'NomPosX', 'NomPosY'])
                                    lot = pd.merge(lot, green5y[0], how='outer', on=['WaferNr', 'NomPosX', 'NomPosY'])
                                    lot = pd.merge(lot, sm5x[0], how='outer', on=['WaferNr', 'NomPosX', 'NomPosY'])
                                    lot = pd.merge(lot, sm5y[0], how='outer', on=['WaferNr', 'NomPosX', 'NomPosY'])
                                if 'ShotLocationMisMatch' == 'ShotLocationMisMatch':
                                    for i in lot.index:
                                        if lot.iloc[i, 4] != lot.iloc[i, 4]:  # NaN
                                            lot.iloc[i, 4] = lot.iloc[i, 5] = lot.iloc[i, 7] = lot.iloc[i, 8] = \
                                            lot.iloc[i, 16] = lot.iloc[i, 17] = lot.iloc[i, 2]
                                            if lot.iloc[i, 1] != lot.iloc[i, 1]:
                                                lot.iloc[i, 1] = lot.iloc[i, 9]
                                        else:
                                            if lot.iloc[i, 10] != lot.iloc[i, 10]:
                                                lot.iloc[i, 10] = lot.iloc[i, 11] = lot.iloc[i, 13] = lot.iloc[i, 14] = \
                                                lot.iloc[i, 19] = lot.iloc[i, 20] = lot.iloc[i, 3]
                                if 'residual' == 'residual':
                                    residual = lot[['WaferNr', 'MarkNr_x']].fillna('')
                                    residual.columns = ['wfrNo', 'markNo', '1', '2']
                                    for i in range(residual.shape[0]):
                                        if residual.iloc[i, 1] == "":
                                            residual.iloc[i, 1] = residual.iloc[i, 2]
                                    residual = residual.drop(['1', '2'], axis=1)
                                    residual['5XRedResidual'] = lot['5XRedResidual'] - lot['NomPosX']
                                    residual['5YRedResidual'] = lot['5YRedResidual'] - lot['NomPosY']
                                    residual['5XGreenResidual'] = lot['5XGreenResidual'] - lot['NomPosX']
                                    residual['5YGreenResidual'] = lot['5YGreenResidual'] - lot['NomPosY']
                                    residual['5XSmResidual'] = lot['5XSmResidual'] - lot['NomPosX']
                                    residual['5YSmResidual'] = lot['5YSmResidual'] - lot['NomPosY']
                                    residual['No'] = 'No' + str(No)
                                    if (basic[4] + basic[0][:7]) in allResidual.keys():
                                        allResidual[basic[4] + basic[0][:7]] = pd.concat(
                                            [allResidual[basic[4] + basic[0][:7]], residual.copy()], axis=0)
                                    else:
                                        allResidual[basic[4] + basic[0][:7]] = residual.copy()

                                    # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + '_Residual.csv'):  #     residual.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + '_Residual.csv',  #                 header=None, mode='a',index=None)  # else:  #     residual.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + '_Residual.csv',index=None)
                                if 'parameter' == 'parameter':
                                    red5x[1].columns = ['r5x_tr', 'r5x_exp', 'r5x_rotort']
                                    red5y[1].columns = ['r5y_tr', 'r5y_rotort', 'r5y_exp']
                                    green5x[1].columns = ['g5x_tr', 'g5x_exp', 'g5x_rotort']
                                    green5y[1].columns = ['g5y_tr', 'g5y_rotort', 'g5y_exp']
                                    sm5x[1].columns = ['sm5x_tr', 'sm5x_exp', 'sm5x_rotort']
                                    sm5y[1].columns = ['sm5y_tr', 'sm5y_rotort', 'sm5y_exp']

                                    para = pd.concat([red5x[1], red5y[1]], axis=1)
                                    para = pd.concat([para, green5x[1]], axis=1)
                                    para = pd.concat([para, green5y[1]], axis=1)
                                    para = pd.concat([para, sm5x[1]], axis=1)
                                    para = pd.concat([para, sm5y[1]], axis=1)

                                    para['No'] = 'No' + str(No)
                                    para = para.fillna('')
                                    para = para.reset_index()
                                    para.columns = ['wfrid', 'r5x_tr', 'r5x_exp', 'r5x_rotort', 'r5y_tr', 'r5y_rotort',
                                                    'r5y_exp', 'g5x_tr', 'g5x_exp', 'g5x_rotort', 'g5y_tr',
                                                    'g5y_rotort', 'g5y_exp', 'sm5x_tr', 'sm5x_exp', 'sm5x_rotort',
                                                    'sm5y_tr', 'sm5y_rotort', 'sm5y_exp', 'No']

                                    if (basic[4] + basic[0][:7]) in allParameter.keys():
                                        allParameter[basic[4] + basic[0][:7]] = pd.concat(
                                            [allParameter[basic[4] + basic[0][:7]], para.copy()], axis=0)
                                    else:
                                        allParameter[basic[4] + basic[0][:7]] = para.copy()

                                    # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '_parameter.csv'):  #     para.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '_parameter.csv',index=None,header=None,mode='a')  # else:  #     para.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '_parameter.csv',index=None)
                                if 'opas' == 'opas':
                                    lot = lot.fillna('')
                                    tmp = lot[['WaferNr', 'MarkNr_x', 'NomPosX', 'NomPosY']]
                                    tmp.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp = tmp.drop('toDelete', axis=1)
                                    tmp1 = lot[['WaferNr', 'MarkNr_x', '5XRedPos', '5YRedPos']]
                                    tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp1 = tmp1.drop('toDelete', axis=1)
                                    tmp1 = pd.concat([tmp, tmp1], axis=0)
                                    tmp1['type'] = 'R5_Measured'
                                    tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                                   zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                    output = tmp1.copy()

                                    tmp1 = lot[['WaferNr', 'MarkNr_x', '5XGreenPos', '5YGreenPos']]
                                    tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp1 = tmp1.drop('toDelete', axis=1)
                                    tmp1 = pd.concat([tmp, tmp1], axis=0)
                                    tmp1['type'] = 'G5_Measured'
                                    tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                                   zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                    output = pd.concat([output, tmp1], axis=0)

                                    tmp1 = lot[['WaferNr', 'MarkNr_x', '5XSmPos', '5YSmPos']]
                                    tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp1 = tmp1.drop('toDelete', axis=1)
                                    tmp1 = pd.concat([tmp, tmp1], axis=0)
                                    tmp1['type'] = 'Sm_Measured'
                                    tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                                   zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                    output = pd.concat([output, tmp1], axis=0)

                                    tmp1 = lot[['WaferNr', 'MarkNr_x', '5XRedResidual', '5YRedResidual']]
                                    tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp1 = tmp1.drop('toDelete', axis=1)
                                    tmp1 = pd.concat([tmp, tmp1], axis=0)
                                    tmp1['type'] = 'R5_Residual'
                                    tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                                   zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                    output = pd.concat([output, tmp1], axis=0)

                                    tmp1 = lot[['WaferNr', 'MarkNr_x', '5XGreenResidual', '5YGreenResidual']]
                                    tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp1 = tmp1.drop('toDelete', axis=1)
                                    tmp1 = pd.concat([tmp, tmp1], axis=0)
                                    tmp1['type'] = 'G5_Residual'
                                    tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                                   zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                    output = pd.concat([output, tmp1], axis=0)

                                    tmp1 = lot[['WaferNr', 'MarkNr_x', '5XSmResidual', '5YSmResidual']]
                                    tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete', 'X', 'Y']
                                    tmp1 = tmp1.drop('toDelete', axis=1)
                                    tmp1 = pd.concat([tmp, tmp1], axis=0)
                                    tmp1['type'] = 'Sm_Residual'
                                    tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                                   zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                    output = pd.concat([output, tmp1], axis=0)

                                    output['No'] = 'No' + str(No)
                                    output = output.fillna('')
                                    if (basic[4] + basic[0][:7]) in allVector.keys():
                                        allVector[basic[4] + basic[0][:7]] = pd.concat(
                                            [allVector[basic[4] + basic[0][:7]], output.copy()], axis=0)
                                    else:
                                        allVector[basic[4] + basic[0][:7]] = output.copy()

                                    # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + "_" + basic[0][:7]+ '_5th.csv'):  #     output.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + "_" + basic[0][:7]+ '_5th.csv',index=None,header=None,mode='a')  # else:  #     output.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + "_" + basic[0][:7]+ '_5th.csv',index=None)
                    except:
                        print('==file errro or small wfr qty===')

                    #######################################################################################
                    ########################################################################################
                    try:
                        os.remove(file)
                    except:
                        pass

                tar.close()

                try:
                    print('==========rename================')
                    print(zipfile)
                    print(zipfile[:24] + 'Finished_' + zipfile[24:])
                    os.rename(zipfile, zipfile[:24] + 'FINISHED_' + zipfile[24:])
                    print('===============succeeed')
                except:
                    pass

                for key in allVector.keys():
                    print(key)
                try: #部分压缩文件是空的
                    #########################
                    tmp=index["index"]
                    tmp['id']=[i[2:] for i in tmp['No']]
                    tmp['date']=[ i.replace('_', '') for i in tmp['date']]
                    tmp=tmp.drop(["No"],axis=1)
                    # tmp.to_csv("D:/temp/index.csv",index=None)
                    conn = sqlite3.connect(dbPath)
                    tmp=tmp.fillna("")
                    tmp.to_sql('tbl_index', conn, if_exists='append', index=None)
                    conn.close()



                    ###############################
                    for key in allVector.keys():
                        tmp = allVector[key]
                        tmp['id'] = [i[2:] for i in tmp['No']]
                        tmp=tmp.drop(["No","key"], axis=1)

                        #generate referece coordinate
                        tmp1=tmp[tmp["type"]=="R5_Measured"]
                        tmp1=tmp1[tmp1["WaferNr"] == "1"]
                        reference=pd.DataFrame(columns=tmp.columns)

                        id=list(tmp1['id'].unique())
                        for x in id:
                            refTmp=tmp1[tmp1["id"]==x]
                            refTmp=refTmp.reset_index().drop("index",axis=1)
                            refTmp=refTmp.loc[0:refTmp.shape[0]/2-1]
                            reference = pd.concat([reference, refTmp], axis=0)
                        reference = reference.drop(['MarkNrY','type'],axis=1)
                        # reference.to_csv("D:/temp/"+key+"_reference.csv",index=None)
                        conn = sqlite3.connect(dbPath)
                        reference = reference .fillna("")
                        reference.to_sql('tbl_reference', conn, if_exists='append', index=None)
                        conn.close()


                        #generate measured vecotr
                        measured=pd.DataFrame(columns=tmp.columns)
                        for measureType in ["R5_Measured", "R5_Residual", "G5_Measured", "G5_Residual", "Sm_Measured", "Sm_Residual"]:
                            tmp1=tmp[tmp["type"]==measureType]
                            for x in id:
                                tmp2=tmp1[tmp1["id"]==x]
                                for y in list(tmp2['WaferNr'].unique()):
                                    tmp3=tmp2[tmp2["WaferNr"]==y]
                                    tmp3=tmp3.reset_index().drop("index",axis=1)
                                    tmp3=tmp3.loc[tmp3.shape[0]/2:tmp3.shape[0]]
                                    measured=pd.concat([measured,tmp3],axis=0)

                        # measured.to_csv("D:/temp/"+key+"_measured.csv",index=None)
                        conn = sqlite3.connect(dbPath)
                        measured=measured.fillna("")
                        measured.to_sql('tbl_'+key[0:6]+'_measured', conn, if_exists='append', index=None)
                        conn.close()
                        # tmp.to_csv("D:/temp/"+key+"_vector.csv",index=None)
                    ################################################
                    # for key in allResidual.keys():
                    #     tmp = allResidual[key]
                    #     tmp['id'] = [i[2:] for i in tmp['No']]
                    #     tmp=tmp.drop(["No"], axis=1)
                    #     tmp.to_csv("D:/temp/"+key+"_residual.csv",index=None)
                    #######################################################
                    print(str(datetime.datetime.now())+"_Vector")
                    for key in allX_WQ_MCC_DELTA.keys():
                        tmp = allX_WQ_MCC_DELTA[key]
                        tmp["XY"]="X"
                        tmp['id'] = [i[2:] for i in tmp['No']]
                        tmp = tmp.drop(["No"], axis=1)
                        tmp.columns=["WaferNr","MarkNr","RedMCC1","RedWQ1","RedMCC5","RedWQ5",
                                     "GreenMCC1","GreenWQ1","GreenMCC5","GreenWQ5","RedDelta","GreenDelta","XY","id"]
                        conn = sqlite3.connect(dbPath)
                        tmp=tmp.fillna("")
                        tmp.to_sql('tbl_' + key[0:6] + '_WqMccDelta', conn, if_exists='append', index=None)
                        conn.close()
                        # tmp.to_csv("D:/temp/"+key+"_allX_WQ_MCC_DELTA.csv",index=None)
                    print(str(datetime.datetime.now()) + "_allX_WQ_MCC_DELTA")
                    ###################################################################
                    for key in allY_WQ_MCC_DELTA.keys():
                        tmp = allY_WQ_MCC_DELTA[key]
                        tmp["XY"]="Y"
                        tmp['id'] = [i[2:] for i in tmp['No']]
                        tmp=tmp.drop(["No"], axis=1)
                        tmp.columns = ["WaferNr", "MarkNr", "RedMCC1", "RedWQ1", "RedMCC5", "RedWQ5", "GreenMCC1",
                                       "GreenWQ1", "GreenMCC5", "GreenWQ5", "RedDelta", "GreenDelta", "XY", "id"]
                        conn = sqlite3.connect(dbPath)
                        tmp=tmp.fillna("")
                        tmp.to_sql('tbl_' + key[0:6] + '_WqMccDelta', conn, if_exists='append', index=None)
                        conn.close()
                        # tmp.to_csv("D:/temp/"+key+"_allY_WQ_MCC_DELTA.csv",index=None)
                    print(str(datetime.datetime.now()) + "_allX_WQ_MCC_DELTA")
                    ###################################################################
                    for key in  allParameter.keys():
                        tmp = allParameter[key]
                        tmp['id'] = [i[2:] for i in tmp['No']]
                        tmp=tmp.drop(["No"], axis=1)
                        conn = sqlite3.connect(dbPath)
                        tmp=tmp.fillna("")
                        tmp.to_sql('tbl_' + key[0:6] + '_para', conn, if_exists='append', index=None)
                        conn.close()
                        # tmp.to_csv("D:/temp/"+key+"_ allParameter.csv",index=None)
                    print(str(datetime.datetime.now()) + "_allX_WQ_MCC_DELTA")
                except:
                    pass
        print("All Done")

class AsmlBatchReport_TAR:#
    def __init__(self):
        pass
    def get_batch_report_path(self,dir_batch_report):
        # list batchreport filename in specified directory
        filenamelist = []
        for root, dirs, files in os.walk(dir_batch_report,False):
            for names in files:
                filenamelist.append(root + '\\' + names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def sec_delta(self,start, end):
        s = parser.parse(start)
        e = parser.parse(end)
        sec = (e - s).seconds
        return sec
    def replace_list(self,tmp, new):
        for index, value in enumerate(tmp):
            if len(value.strip()) == 0:
                tmp[index] = new
        return tmp
    def singlefile(self,path):
        tool = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                '3527': 'BLSD08'}
        Flag1, Flag2, Flag3, Flag4, Flag5 = False, False, False, False, False
        Flag6, Flag7, Flag8, Flag9, Flag10 = False, False, False, False, False
        Flag11, Flag12, Flag13, Flag14, Flag15 = False, False, False, False, False
        Flag16, Flag17, Flag18, Flag19, Flag20 = False, False, False, False, False
        Flag21, Flag22 = False, False

        end_count = 0

        throughput = []
        data = ['888' for i in range(218)]
        align = []
        mark_residual = []
        new = '888'
        err_arr = []

        try:
            input = open(path)
        except:
            pass  # sys.exit(0)

        try:
            for line in input:

                # Q above P
                if 'INLINE Q ABOVE P CALIBRATION RESULTS' in line:
                    Flag21 = True
                if (Flag21 == True) & ('Q above P Offset Ave [nm]' in line):
                    data[212] = line.split(':')[1].strip()
                if (Flag21 == True) & ('Q above P Offset Sdv [nm]:' in line):
                    data[213] = line.split(':')[1].strip()
                    Flag21 = False

                if 'REPORT GENERATED AFTER ABNORMAL TERMINATION OF BATCH' in line:
                    end_count = 0

                if 'Operator:' in line:
                    end_count += 1
                    if end_count > 1:  # appended data will not be analyzed
                        input.close
                        break
                    tmp = line.split()

                    try:
                        data[0] = (tool[tmp[1].split(':')[1]])
                    except:
                        pass
                    try:
                        data[1] = (tmp[3].split(':')[1])
                    except:
                        pass
                    try:
                        data[2] = (tmp[4][5:])
                    except:
                        pass

                if 'Batch ID' in line:
                    data[3] = (line.split(':')[1].strip('\n').strip())
                if 'Job Name' in line:
                    data[4] = (line.split(':')[1].strip('\n').strip())
                if 'Job Modified' in line:
                    data[5] = (line[31:].strip())
                if 'Layer ID' in line:
                    data[6] = (line[31:46].strip())

                if 'Alignment Strategy ID' in line:
                    data[21] = (line.split(':')[1].strip('\n').strip())

                if 'Number of Marks n Required' in line:
                    data[22] = (line.split(':')[1].strip('\n').strip())

                if 'SPM Mark Scan' in line:
                    data[23] = (line.split(':')[1].strip('\n').strip())

                if 'Maximum Error Count Dynamic Performance' in line:
                    data[24] = (line.split(':')[1].strip('\n').strip())

                if 'Maximum Error Count Dose' in line:
                    data[25] = (line.split(':')[1].strip('\n').strip())

                if 'Batch started at' in line:
                    data[30] = (line[28:].strip('\n').strip())

                if 'Batch finished at' in line:
                    data[31] = (line[28:].strip('\n').strip())

                if 'No. of Wafers out Batch' in line:
                    data[32] = (line[28:].strip('\n').strip())

                if 'No. of Wafers Accepted' in line:
                    data[33] = (line[28:].strip('\n').strip())
                if 'No. of Wafers Rejected' in line:
                    data[34] = (line[28:].strip('\n').strip())

                    # reading dose/focus data
                if '+=================+==========+==========+========+========+' in line:
                    Flag1 = True
                if (Flag1 == True) & (len(line.split('|')) == 7):
                    data[7] = (line.split('|')[2].strip())
                    data[8] = (line.split('|')[3].strip())
                    data[9] = (line.split('|')[4].strip())
                    data[10] = (line.split('|')[5].strip())
                    Flag1 = False

                # read focus tilt
                if '+=================+========+========+========+========+' in line:
                    Flag2 = True
                if (Flag2 == True) & (len(line.split('|')) == 7):
                    data[11] = (line.split('|')[2].strip())
                    data[12] = (line.split('|')[3].strip())
                    data[13] = (line.split('|')[4].strip())
                    data[14] = (line.split('|')[5].strip())
                    Flag2 = False

                # read illumination NA
                if '+=================+=================+=========+===================+' in line:
                    Flag3 = True
                if (Flag3 == True) & (('conventional' in line) | ('annular' in line)):
                    data[15] = (line.split('|')[3].strip())
                    data[16] = (line.split('|')[4].strip())
                    Flag3 = False

                # read illumination sigma
                if '+=================+=======+=======+=======+=======+' in line:
                    Flag4 = True
                if (Flag4 == True) & (len(line.split('|')) == 7):
                    data[17] = (line.split('|')[2].strip())
                    data[18] = (line.split('|')[3].strip())
                    data[19] = (line.split('|')[4].strip())
                    data[20] = (line.split('|')[5].strip())
                    Flag4 = False

                # Read Delta Shift Offset
                if '8.0 to 8.8 Shift Corrections' in line:
                    Flag5 = True
                if (Flag5 == True) & ('Red [um]' in line):
                    data[26] = (line.split()[4].strip())
                    data[27] = (line.split()[7].strip())
                if (Flag5 == True) & ('Green [um]' in line):
                    data[28] = (line.split()[4].strip())
                    data[29] = (line.split()[7].strip())
                    Flag5 == False

                    # read throughput data
                if '+=======+=========+==========+=========+==========+==========+==========+' in line:
                    Flag6 = True
                if (Flag6 == True) & ('Accept  | Complete' in line):
                    start = line.split('|')[6]
                    end = line.split('|')[7]
                    throughput.append(self.sec_delta(start, end))  # process time of each wafer
                if '+-------+---------+----------+---------+----------+----------+----------+' in line:
                    Flag6 = False
                    if len(throughput) > 1:
                        tmp = np.array(throughput[1:])
                        data[35] = (throughput[0])
                        data[36] = (tmp.max())
                        data[37] = (tmp.min())

                if '+=================+============================================+=============+' == line.strip():
                    Flag8 = True
                if (Flag8 == True) and ('|' in line):
                    data[38] = line.split('|')[2]
                    data[217] = line.split('|')[1]
                    Flag8 = False

                if line.strip() == 'ALIGNMENT STATISTICS I':  # Alignment Statistics I
                    Flag9 = True

                if (Flag9 == True) & ('| AVE.  |' in line):
                    tmp = line.split('|')[2:8]
                    tmp = self.replace_list(tmp, new)
                    data[39:45] = (list(np.array(tmp).astype('float16')))
                if (Flag9 == True) & ('| S.D.  |' in line):
                    tmp = line.split('|')[2:8]
                    tmp = self.replace_list(tmp, new)

                    data[45:51] = (list(np.array(tmp).astype('float16')))
                    Flag9 = False

                # Alignment Statistics II
                if line.strip() == 'ALIGNMENT STATISTICS II':
                    Flag10 = True
                if (Flag10 == True) & ('| AVE. |' in line):
                    tmp = line.split('|')[2:8]
                    tmp = self.replace_list(tmp, new)
                    data[51:57] = (list(np.array(tmp).astype('float16')))
                if (Flag10 == True) & ('| S.D. | ' in line):
                    tmp = line.split('|')[2:8]
                    tmp = self.replace_list(tmp, new)
                    data[57:63] = (list(np.array(tmp).astype('float16')))
                    Flag10 = False

                # LEVEL STATISTICS I
                if line.strip() == 'LEVEL STATISTICS I':
                    Flag11 = True
                if (Flag11 == True) & ('| MIN. |' in line):
                    tmp = line.split('|')[2:7]
                    tmp = self.replace_list(tmp, new)
                    data[215] = eval(tmp[3])

                if (Flag11 == True) & ('| MAX. |' in line):
                    tmp = line.split('|')[2:7]
                    tmp = self.replace_list(tmp, new)
                    data[214] = eval(tmp[3])
                    data[216] = data[214] - data[215]

                if (Flag11 == True) & ('| AVE. |' in line):
                    tmp = line.split('|')[2:7]
                    tmp = self.replace_list(tmp, new)

                    data[63:68] = (list(np.array(tmp).astype('float16')))
                if (Flag11 == True) & ('| S.D. |' in line):
                    tmp = line.split('|')[2:7]
                    tmp =  self.replace_list(tmp, new)
                    data[68:73] = (list(np.array(tmp).astype('float16')))
                    Flag11 = False

                # LEVEL STATISTICS II
                if line.strip() == 'LEVEL STATISTICS II':
                    Flag12 = True
                if (Flag12 == True) & ('| AVE. |' in line):
                    tmp = line.split('|')[2:8]
                    tmp =  self.replace_list(tmp, new)

                    data[73:79] = (list(np.array(tmp).astype('float16')))
                if (Flag12 == True) & ('| S.D. |' in line):
                    tmp = line.split('|')[2:8]
                    tmp =  self.replace_list(tmp, new)
                    data[79:85] = (list(np.array(tmp).astype('float16')))
                    Flag12 = False

                # LEVEL STATISTICS III
                if line.strip() == 'LEVEL STATISTICS III':
                    Flag13 = True
                if (Flag13 == True) & ('| AVE. |' in line):
                    tmp = line.split('|')[2:8]
                    tmp =  self.replace_list(tmp, new)

                    data[85:91] = (list(np.array(tmp).astype('float16')))
                if (Flag13 == True) & ('| S.D. |' in line):
                    tmp = line.split('|')[2:8]
                    tmp =  self.replace_list(tmp, new)

                    data[91:97] = (list(np.array(tmp).astype('float16')))
                    Flag13 = False

                    # ALIGNMENT RESULTS V
                if '+=======+=================+=======+====+===+========+====+===+====+====+' in line:
                    Flag14 = True
                if (Flag14 == True) & ('|' in line):
                    align.append(line.split('|')[1:11])
                if '+-------+-----------------+-------+----+---+--------+----+---+----+----+' in line:
                    Flag14 = False

                if '+=======+=================+=======+=================+=======+=================+' in line:
                    Flag15 = True
                if (Flag15 == True) & ('|' in line):
                    mark_residual.append(line.split('|')[1:7])
                if '+-------+-----------------+-------+-----------------+-------+-----------------+' in line:
                    Flag15 = False

                # DYNAMIC PERFORMANCE STATISTICS
                if line.strip() == 'DYNAMIC PERFORMANCE STATISTICS':
                    Flag16 = True
                if (Flag16 == True) & ('| MIN. |' in line):
                    data[97:105] = (line.split('|')[2:10])
                if (Flag16 == True) & ('| MAX. | ' in line):
                    data[105:113] = (line.split('|')[2:10])
                if (Flag16 == True) & ('| AVE. |  ' in line):
                    data[113:121] = (line.split('|')[2:10])
                if (Flag16 == True) & ('| S.D. |' in line):
                    data[121:129] = (line.split('|')[2:10])
                    Flag16 = False

                # LEVEL STATISTICS IV
                if line.strip() == 'LEVEL STATISTICS IV':
                    Flag17 = True
                if (Flag17 == True) & ('AVE.' in line):
                    data[129:137] = (line.split('|')[2:10])
                if (Flag17 == True) & ('S.D.' in line):
                    data[137:145] = (line.split('|')[2:10])
                    Flag17 = False

                # LEVEL STATISTICS V
                if line.strip() == 'LEVEL STATISTICS V':
                    Flag18 = True
                if (Flag18 == True) & ('AVE.' in line):
                    data[145:151] = (line.split('|')[2:8])
                if (Flag18 == True) & ('S.D.' in line):
                    data[151:157] = (line.split('|')[2:8])
                    Flag18 = False

                    # DOSE MONITORING DATA
                if line.strip() == 'DOSE MONITORING DATA':
                    Flag19 = True
                    i = 0
                if Flag19 == True:
                    i += 1
                    if (i == 3) & (line.strip() == 'NO ERRORS'):
                        Flag19 = False
                        data[157] = ('False')

                # Reticle Inspection
                if line.strip() == 'Reticle Inspection':
                    Flag20 = True
                if (Flag20 == True) & ('Reticle ID' in line):
                    data[158] = (line[20:].strip())
                if (Flag20 == True) & ('Inside Analysis Area' in line):
                    data[159:162] = (line.split('|')[2:5])
                if (Flag20 == True) & ('Outside Analysis Area' in line):
                    data[162:165] = (line.split('|')[2:5])
                if (Flag20 == True) & ('| FULL ' in line):
                    data[165:168] = (line.split('|')[2:5])
                    Flag20 = False

                # confirm Focus/Dyn/Dose error
                # |       |  Alignment   |Focus | Dyn  | Dose |Chuck |                 |
                # |       |  Alignment   |Focus | Dyn  | Dose | BQ   |Chuck |                 |
                # it depends on different tool, not identical

                if '| Wafer | Pre  |Global |' in line:
                    err_arr = []
                    Flag7 = True
                if (Flag7 == True) & ('|' in line):
                    if len(line) > 71:
                        err_arr.append(line.split('|')[4:9])
                    else:
                        err_arr.append(line.split('|')[4:8])

                if line.strip() == 'ALIGNMENT RESULTS I':

                    err_arr.remove(err_arr[0])
                    err_df = pd.DataFrame(err_arr).astype('int32')
                    err_df.loc['sum'] = err_df.apply(lambda x: x.sum(), axis=0)
                    for index, err_count in enumerate(err_df.loc['sum']):
                        index = index + 168
                        data[index] = err_count

                    Flag7 = False

                if 'Batch Type' in line:
                    # data[211] = data.append( line.split(':')[1]   )
                    data[211] = line[31:].strip()

















        except:
            pass

        input.close
        return data, align, mark_residual
    def aligndata(self,data, align):
        df = pd.DataFrame(align)
        ####Color used
        # X mark
        i = 0
        for x in df[8]:
            if x.strip() == 'RG':
                i += 1
        data[173] = i

        i = 0
        for x in df[8]:
            if x.strip() == 'R':
                i += 1
        data[174] = i

        i = 0
        for x in df[8]:
            if x.strip() == 'G':
                i += 1
        data[175] = i

        # y Mark
        i = 0
        for x in df[9]:
            if x.strip() == 'RG':
                i += 1
        data[176] = i

        i = 0
        for x in df[9]:
            if x.strip() == 'R':
                i += 1
        data[177] = i

        i = 0
        for x in df[9]:
            if x.strip() == 'G':
                i += 1
        data[178] = i

        if 'XPA' in list(df[1])[0]:
            # red largest order deviation

            data[179] = (df[df[4].str.contains('R')][2].astype('int16').max())
            data[180] = (df[df[4].str.contains('R')][2].astype('int16').min())

            # Green largest order deviation

            data[181] = (df[df[4].str.contains('G')][2].astype('int16').max())
            data[182] = (df[df[4].str.contains('G')][2].astype('int16').min())

            # red worst quality
            data[183] = (df[df[7].str.contains('R')][5].astype('float64').max())
            data[184] = (df[df[7].str.contains('R')][5].astype('float64').min())

            # Green worst quality
            data[185] = (df[df[7].str.contains('G')][5].astype('float64').max())
            data[186] = (df[df[7].str.contains('G')][5].astype('float64').min())
        else:

            # X red largest order deviation
            data[187] = (df[df[1].str.contains('-X')][df[4].str.contains('R')][2].astype('int16').max())
            data[188] = (df[df[1].str.contains('-X')][df[4].str.contains('R')][2].astype('int16').min())

            # X Green largest order deviation

            data[189] = (df[df[1].str.contains('-X')][df[4].str.contains('G')][2].astype('int16').max())
            data[190] = (df[df[1].str.contains('-X')][df[4].str.contains('G')][2].astype('int16').min())

            # X red worst quality
            data[191] = (df[df[1].str.contains('-X')][df[7].str.contains('R')][5].astype('float64').max())
            data[192] = (df[df[1].str.contains('-X')][df[7].str.contains('R')][5].astype('float64').min())

            # X Green worst quality
            data[193] = (df[df[1].str.contains('-X')][df[7].str.contains('G')][5].astype('float64').max())
            data[194] = (df[df[1].str.contains('-X')][df[7].str.contains('G')][5].astype('float64').min())

            # Y red largest order deviation
            data[195] = (df[df[1].str.contains('-Y')][df[4].str.contains('R')][2].astype('int16').max())
            data[196] = (df[df[1].str.contains('-Y')][df[4].str.contains('R')][2].astype('int16').min())

            # Y Green largest order deviation

            data[197] = (df[df[1].str.contains('-Y')][df[4].str.contains('G')][2].astype('int16').max())
            data[198] = (df[df[1].str.contains('-Y')][df[4].str.contains('G')][2].astype('int16').min())

            # Y red worst quality
            data[199] = (df[df[1].str.contains('-Y')][df[7].str.contains('R')][5].astype('float64').max())
            data[200] = (df[df[1].str.contains('-Y')][df[7].str.contains('R')][5].astype('float64').min())

            # Y Green worst quality
            data[201] = (df[df[1].str.contains('-Y')][df[7].str.contains('G')][5].astype('float64').max())
            data[202] = (df[df[1].str.contains('-Y')][df[7].str.contains('G')][5].astype('float64').min())
    def residualdata(self,mark_residual, data):
        df = pd.DataFrame(mark_residual)
        data[203] = df[df[1].str.contains('-X')][2].astype('int32').max()
        data[204] = df[df[1].str.contains('-X')][2].astype('int32').min()
        data[205] = df[df[1].str.contains('-X')][2].astype('int32').mean()
        data[206] = df[df[1].str.contains('-X')][2].astype('int32').std()

        data[207] = df[df[1].str.contains('-Y')][4].astype('int32').max()
        data[208] = df[df[1].str.contains('-Y')][4].astype('int32').min()
        data[209] = df[df[1].str.contains('-Y')][4].astype('int32').mean()
        data[210] = df[df[1].str.contains('-Y')][4].astype('int32').std()
    def MainFunction(self):
        if 1==1:
            dbPath="P:\\_SQLite\\AsmlBatchreport.db"
            col=['tool','date','time','lotid','jobName','jobModified','layer','doseActual','doseJob','focusActual','focusJob','focusRxActual','focusRxJob','focusRyActual','focusRyJob','aperture','illuminationMode','sigmaOutActual','sigmaOutJob','sigmaInActual','sigmaInJob','alignStrategy','markRequired','spmMarkScan','MaxDynErrCount','MaxDoseErrCount','deltaRedX','deltaRedY','deltaGreenX','deltaGreenY','batchStart','batchFinish','wfrBatchOut','wfrAccept','wfrReject','throughputWfrFirst','throughputWfrMax','throughputWfrMin','alignRecipe','tranXave','tranYave','expXave','expYave','wfrRotAve','wfrOrtAve','tranXdev','tranYdev','expXdev','expYdev','wfrRotDev','wfrOrtDev','reticleMagAve','reticleRotAve','redXave','redYave','greenXave','greenYave','reticleMagDev','reticleRotDev','redXdev','redYdev','greenXdev','greenYdev','globalLevelDzAve','globalLevelPhixAve','globalLevelPhiyAve','blueFocusAve','blueRyAve','globalLevelDzDev','globalLevelPhixDev','globalLevelPhiyDev','blueFocusDev','blueRyDev','fieldLevelDzMinAve','fieldLevelPhixMinAve','fieldLevelPhiyMinAve','fieldLevelDzMaxAve','fieldLevelPhixMaxAve','fieldLevelPhiyMaxAve','fieldLevelDzMinDev','fieldLevelPhixMinDev','fieldLevelPhiyMinDev','fieldLevelDzMaxDev','fieldLevelPhixMaxDev','fieldLevelPhiyMaxDev','fieldLevelDzMeanAve','fieldLevelPhixMeanAve','fieldLevelPhiyMeanAve','fieldLevelDzSigmaAve','fieldLevelPhixSigmaAve','fieldLevelPhiySigmaAve','fieldLevelDzMeanDev','fieldLevelPhixMeanDev','fieldLevelPhiyMeanDev','fieldLevelDzSigmaDev','fieldLevelPhixSigmaDev','fieldLevelPhiySigmaDev','MaXmin','MaYmin','MaRzMin','MaZmin','MsdXmin','MsdYmin','MsdRzMin','MsdZmin','MaXmax','MaYmax','MaRzMax','MaZmax','MsdXmax','MsdYmax','MsdRzMax','MsdZmax','MaXave','MaYave','MaRzAve','MaZave','MsdXave','MsdYave','MsdRzAve','MsdZave','MaXdev','MaYdev','MaRzDev','MaZdev','MsdXdev','MsdYdev','MsdRzDev','MsdZdev','intraFieldRxMinAve','intraFieldRxMaxAve','intraFieldRxAveAve','intraFieldRxDevAve','intraFieldRyMinAve','intraFieldRyMaxAve','intraFieldRyAveAve','intraFieldRyDevAve','intraFieldRxMinDev','intraFieldRxMaxDev','intraFieldRxAveDev','intraFieldRxDevDev','intraFieldRyMinDev','intraFieldRyMaxDev','intraFieldRyAveDev','intraFieldRyDevDev','TiltZplaneDzAve','TiltZplanePhixAve','TiltZplanePhiyAve','TiltZplaneMccAve','TiltResidualRxAve','TiltResidualRyAve','TiltZplaneDzDev','TiltZplanePhixDev','TiltZplanePhiyDev','TiltZplaneMccDev','TiltResidualRxDev','TiltResidualRyDev','doseError','Reticle','Inside_S','Inside_M','Inside_L','Outside_S','Outside_M','Outside_L','Full_S','Full_M','Full_L','focusErr','dynErr','doseErr','bqErr','chuckErr','xColorRG','xColorR','xColorG','yColorRG','yColorR','yColorG','xpaLargestOrderRedMax','xpaLargestOrderRedMin','xpaLargestOrderGreenMax','xpaLargestOrderGreenMin','xpaWorstRedWqMax','xpaWorstRedWqMin','xpaWorstGreenWqMax','xpaWorstGreenWqMin','xLargestOrderRedMax','xLargestOrderRedMin','xLargestOrderGreenMax','xLargestOrderGreenMin','xWorstRedWqMax','xWorstRedWqMin','xWorstGreenWqMax','xWorstGreenWqMin','yLargestOrderRedMax','yLargestOrderRedMin','yLargestOrderGreenMax','yLargestOrderGreenMin','yWorstRedWqMax','yWorstRedWqMin','yWorstGreenWqMax','yWorstGreenWqMin','xResidualMax','xResidualMin','xResidualAve','xResidualDev','yResidualMax','yResidualMin','yResidualAve','yResidualDev','batchType','QabovePoffsetAve','QabovePoffsetDev','blueFocusMax','blueFocusMin','blueFocusRange','markType','path']
        root = 'P:/_AsmlDownload/BatchReport/'
        alltools = ['85','86','7D','08','8A','8B','8C','82','83','87','89']
        tool = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                '3527': 'BLSD08'}
        for id in alltools[:]:
            summary= pd.DataFrame(columns=col[0:-1])

            filelist = [ root + id + '/' + i for i in os.listdir(root + id) if i[0:8]!='Finished' and i[-7:]=='.tar.gz']

            filelist=filelist[:]


            if len(filelist)>0:
                for zipfile in filelist[0:]:
                    try:
                        shutil.rmtree('c:/usr')
                    except:
                        pass
                    tar = tarfile.open(zipfile)
                    newset = None;filename = None; oldset = None;newset = None; df = None;activeset = None; data = None;align = None; mark_resiudal = None;gc.collect()

                    for file in tar.getmembers()[:]:
                        tar.extract(file,'c:/')
                        filepath = 'c:'+file.name
                        print(filepath)

                        if os.path.getsize(filepath) > 8888 and 'timestamp' not in filepath:
                            data, align, mark_residual = self.singlefile(filepath)
                            if len(align) > 0:
                                self.aligndata(data, align)  # process array of largest order deviation and worst wafer quality
                            if len(mark_residual) > 0:
                                self.residualdata(mark_residual, data)  # process array of mark residual
                            for index, value in enumerate(data):
                                if (value == '888') | (value == 888) | (value == 'Actual') | (
                                        value == 'Job'):  # (  value == 'Actual') | ( value == 'Job' )FEM wafer, dose/focus with step,wrong extraction
                                    data[index] = ''
                            data.append(filepath)
                            df = pd.DataFrame(data).T
                            df.columns=col
                            df=df.drop("path",axis=1)
                            summary=pd.concat([summary,df],axis=0)
                            # conn = sqlite3.connect(dbPath)
                            # df = df.fillna("")
                            # df.to_sql('tbl_asmlbatchreport', conn, if_exists='append', index=None)



                    tar.close()
                    print(zipfile + '--> Extraction & Check Done')
                    print( "Renaming...." + zipfile)

                    os.rename(zipfile,zipfile[0:32]+"Finished_"+zipfile[32:])

            if summary.shape[0]>0:
                conn = sqlite3.connect(dbPath)  #
                summary = summary.fillna("")  #
                summary.to_sql('tbl_asmlbatchreport', conn, if_exists='append', index=None)
                conn.close()

class NikonVector_NEW: #原数据有误：X值需取负；residual取负；改为CSV格式
    def __init__(self):
        pass
    def read_log(self,path):
        key,flag = [],True
        if 1==1:
            if 11==11: #open data
                tmp = []
                f = open(path)
                for i in f:
                    tmp.append(i.strip().split(':='))
                f.close()

                try:
                    df = pd.DataFrame(tmp)

                    Measure = df[df[0].str.contains("MEAS_DATA")]

                    Measure = Measure[1].apply(lambda x: pd.Series([eval(i) for i in x.split(",")]))
                    df = df.reset_index().set_index(0).drop(columns='index', axis=1)
                except:
                    flag = False  # no content in egam file
                    print('no content in egam file')
                    return key,key

            if flag == True and int(df.loc['MESP_NUM'][1].strip()) > 1:
                try:
                    if 'ExtractData'=='ExtractData':
                        if 'basicData'=='basicData':
                            StepX = eval(df.loc['STEP_PITCH'][1].split(',')[0]) ; StepY = eval(df.loc['STEP_PITCH'][1].split(',')[1])
                            OffsetX = eval(df.loc['MAP_OFFSET'][1].split(',')[0]) ; OffsetY = eval(df.loc['MAP_OFFSET'][1].split(',')[1])
                            WfrNo = int(df.loc['WAFER_NUM'][1]) ; AlignNo = int(df.loc['SHOT_NUM'][1])

                            if  'X' in df.loc['MESP(1)'][1].strip().split(',')[-2]:
                                zbX = eval( df.loc['MESP(1)'][1].strip().split(',')[1].strip() )
                                zbY = eval( df.loc['MESP(2)'][1].strip().split(',')[2].strip() )
                            else:
                                zbX = eval(df.loc['MESP(2)'][1].split(',')[1].strip())
                                zbY = eval(df.loc['MESP(1)'][1].split(',')[2].strip())

                            AlignPosition = []
                            for i in range(AlignNo):
                                tmp = 'SHOT(' + str(i + 1) + ')'
                                # tmp = eval(df.loc[[tmp], :].iloc[0, 0])
                                tmp = eval(df.loc[tmp][1])
                                AlignPosition.append((i + 1, (tmp[1] - 1) * StepX + OffsetX - StepX / 2 + zbX,
                                                      ((tmp[2] - 1) * StepY + OffsetY - StepY / 2 + zbY)))  #实际未加入测量坐标位置
                            AlignPosition = pd.DataFrame(AlignPosition).drop(columns=0, axis=1)

                        ##'\\\\10.4.72.74\\litho\\NikonEgaLog\\ALII07\\ega_201904171421.egam'


                        tmp = Measure[Measure[1]==1.0][[3,4]].describe().loc['max']#不能用Measure[2]判断X,Y坐标
                        if abs(tmp.loc[3]-0.0)>0.001: # 1-->X,2-->Y      #异常lot没有 Measure[1]==2.0
                            X = Measure[Measure[1] == 1.0][3]
                            Y = Measure[Measure[1] == 2.0][4]
                        else:
                            Y = Measure[Measure[1] == 1.0][4]
                            X = Measure[Measure[1] == 2.0][3]
                        X = pd.DataFrame(X.values.reshape(WfrNo, AlignNo)).T
                        X.columns = [i + 1 for i in range(X.shape[1])]
                        Y = pd.DataFrame(Y.values.reshape(WfrNo, AlignNo)).T
                        Y.columns = [i + 1 for i in range(Y.shape[1])]





                    if "LinearRegression"=="LinearRegression":
                        tmpx,tmpy,magrotortx,magrotorty = [],[],[],[] #残差，拟合系数
                        linreg = LinearRegression()
                        ref = pd.DataFrame([0 for i in range(AlignNo)])

                        for i in range(1,X.shape[1]+1):
                            input_y = X[X[i] != 0][i]
                            input_x = AlignPosition.iloc[input_y.index, :]
                            model = linreg.fit(input_x, input_y)
                            residual = pd.DataFrame( input_y - (linreg.intercept_ + linreg.coef_[0] * input_x[1] + linreg.coef_[1] * input_x[2]) )
                            tmpx.append(((residual + ref).fillna(0))[0])
                            magrotortx.append([linreg.coef_[0] * 1000000, linreg.coef_[1] * 1000000])

                            input_y = Y[Y[i] != 0][i]
                            input_x = AlignPosition.iloc[input_y.index, :]
                            model = linreg.fit(input_x, input_y)
                            residual = pd.DataFrame(input_y - (linreg.intercept_ + linreg.coef_[0] * input_x[1] + linreg.coef_[1] * input_x[2] ) )
                            tmpy.append(((residual + ref).fillna(0))[0])
                            magrotorty.append([linreg.coef_[1] * 1000000, linreg.coef_[0] * 1000000])

                        magrotortx = pd.DataFrame(magrotortx)
                        magrotorty = pd.DataFrame(magrotorty)

                        tmpx = pd.DataFrame(tmpx).T
                        tmpx.columns = [i + 1 for i in range(tmpx.shape[1])]
                        tmpy = pd.DataFrame(tmpy).T
                        tmpy.columns = [i + 1 for i in range(tmpy.shape[1])]
                    if 'key'=='key':
                        key = []
                        key.append( path)
                        # key.append(  df.loc['FILE_NAME'][1].strip()[1:-1].split(':')[1] ) #filename
                        key.append(df.loc['MEAS_SENS'][1].strip()[1:-1]) #EgaSensor
                        key.append( df.loc['EXPS_MACHINE'][1].strip()[1:-1] )
                        key.append(df.loc['MEAS_PDF'][1].strip()[1:-1]) #Process.Prgogram
                        key.extend([StepX,StepY,OffsetX,OffsetY])
                        key.append( int(df.loc['LSA_REQ_SHOT'][1]) )
                        key.append( int(df.loc['FIA_REQ_SHOT'][1]) )
                        key.append( eval(df.loc['FM_ROTATION'][1]) )
                        key.extend(df.loc['CORR_W_OFF'][1].strip().split(',') )
                        key.extend(df.loc['CORR_W_SCL'][1].strip().split(',') )
                        key.append(df.loc['CORR_W_ORT'][1].strip() )
                        key.append(df.loc['CORR_W_ROT'][1].strip() )
                        key.extend(df.loc['CORR_C_SCL'][1].strip().split(',')  )
                        key.append(df.loc['CORR_C_ROT'][1].strip() )
                        key.extend([WfrNo,AlignNo])
                        key.append( df.loc['SENSOR(1)'][1].split(',')[1].strip()[1:-1] ) #search
                        key.append( df.loc['SENSOR(2)'][1].split(',')[1].strip()[1:-1] ) #ega
                        key.append( eval(df.loc['UPPER_LIMIT'][1]) )
                        key.append( eval(df.loc['LOWER_LIMIT'][1]) )
                    AlignPosition[1] = AlignPosition[1] / 1000
                    AlignPosition[2] = AlignPosition[2] / 1000
                    return key,X,Y,tmpx,tmpy,magrotortx,magrotorty,AlignPosition
                except:
                    return key,key
            else:
                return key,key
    def MainFunction(self):
        ##'\\\\10.4.72.74\\litho\\NikonEgaLog\\ALII07\\ega_201904171421.egam'  -->EGA不完整
        dbPath = "P:\\_SQLite\\NikonEgaPara.db"

        # if "MoveFile"=="MoveFile":
        #     FileDir = 'P:\\EgaLog\\'
        #     filenamelist = []
        #     for root, dirs, files in os.walk(FileDir, False):
        #         for names in files:
        #             if '.egam' in names:
        #                 filenamelist.append(root + '\\' + names)
        #     filenamelist.sort(reverse=False)
        #     if len(filenamelist)>0:
        #         for k, path in enumerate(filenamelist[0:]):
        #             newpath = "Y:\\NikonEgaLog\\" + path[10:]
        #             shutil.move(path, newpath)
        if 'AllTool'=='AllTool':
            toollist = ['ALII01','ALII02','ALII03','ALII04','ALII05','ALII06','ALII07','ALII08',
                        'ALII09','ALII10','ALII11','ALII12','ALII13','ALII14','ALII15','ALII16',
                        'ALII17','ALII18','ALII20','ALII21','ALII22','ALII23']
            tooldic={'ALII01':"ALSIB1",'ALII02':"ALSIB2",'ALII03':"ALSIB3",'ALII04':"ALSIB4",'ALII05':"ALSIB5",'ALII06':"ALSIB6",
                     'ALII07':"ALSIB7",'ALII08':"ALSIB8",'ALII09':"ALSIB9",'ALII10':"ALSIBA",'ALII11':"ALSIBB",'ALII12':"ALSIBC",
                     'ALII13':"ALSIBD",'ALII14':"ALSIBE",'ALII15':"ALSIBF",'ALII16':"ALSIBG",'ALII17':"ALSIBH",'ALII18':"ALSIBI",
                     'ALII20':"BLSIBK",'ALII21':"BLSIBL",'ALII22':"BLSIE1",'ALII23':"BLSIE2"}
            for tool in toollist[0:]:
                # d1={};d2={};d0={}
                if 'generateIndexDummyMerge'=='generateIndexDummyMerge':
                    indexSummary = pd.DataFrame(columns=['File', 'MEAS_SENS', 'Tool', 'Ppid', 'StepX', 'StepY', 'OffsetX',
           'OffsetY', 'LsaReqShot', 'FiaReqShot', 'FM_ROT', 'TranX', 'TranY',
           'ScalX', 'ScalY', 'Ort', 'Rot', 'MagX', 'MagY', 'SRot', 'WfrNo',
           'AlignNo', 'Search', 'EGA', 'ULimit', 'Llimit', 'No'])
                    merge = pd.DataFrame(columns=['x', 'y', 'No', 'wfr', 'shot'])
                    reference = pd.DataFrame(columns=["No","x","y","shot"])
                    paraSummary=pd.DataFrame(columns=['scalingx','ort','scalingy','rot','No','wfrNo'])

                if 'GetFileList' == 'GetFileList':
                    pathDir = r'\\10.4.72.74\litho\NikonEgaLog' + '\\' + tool + '\\'
                    pathDir = r'P:\\EGALOG\\' + tool + '\\'
                    filenamelist = []
                    for root, dirs, files in os.walk(pathDir,False):
                        for names in files:
                            if '.egam' in names:
                                filenamelist.append(root  + names)
                    # 读入数据库最大日期
                    conn = sqlite3.connect('P:\\_SQLite\\NikonEgaPara.db')
                    cursor = conn.cursor()
                    sql = "Select max(Date) from tbl_index where tool='" + tooldic[tool] + "'"
                    query = cursor.execute(sql)
                    maxDate = query.fetchall()[0][0]
                    conn.close()
                    if maxDate == None:
                        filenamelist = [x for x in filenamelist if x[-17:-5] > '202000000000']
                        pass
                    else:
                        maxDate=str(maxDate)
                        print(len(filenamelist))
                        # 列出新文件
                        filenamelist = [ x for x in filenamelist if x[-17:-5]>maxDate]


                    filenamelist.sort(reverse=False)
                    filenamelist = [ i for i in filenamelist if i[-4:]=='egam']

                    #读入最大编号
                    conn = sqlite3.connect('P:\\_SQLite\\NikonEgaPara.db')
                    cursor = conn.cursor()
                    sql = "Select max(No) from tbl_index"
                    query = cursor.execute(sql)
                    No = query.fetchall()[0][0]
                    conn.close()
                    if No==None:
                        No=0

                if 'readData'=='readData' and len(filenamelist)>0:
                    for k, path in enumerate(filenamelist[0:]):
                        print(k, len(filenamelist), path)
                        data = self.read_log(path)
                        try:#NoneType
                            if len(data[0])>0:
                                if 'generateindex' =='generateindex':
                                    AlignPosition = data[7]
                                    tmpx,tmpy = data[3] / 1000,data[4] /1000
                                    tmp = pd.DataFrame(data[0]).T
                                    tmp.columns = ['File','MEAS_SENS','Tool','Ppid','StepX','StepY','OffsetX','OffsetY','LsaReqShot',
                                                   'FiaReqShot','FM_ROT','TranX','TranY','ScalX','ScalY','Ort','Rot','MagX','MagY','SRot',
                                                   'WfrNo','AlignNo','Search','EGA','ULimit','Llimit' ]
                                    No += 1
                                    tmp['No']=No
                                    indexSummary=pd.concat([indexSummary,tmp],axis=0)

                                if 'generate_merge'=='generate_merge':

                                    for i in tmpx.columns:
                                        tx = (tmpx[i] * 1000000 + AlignPosition[1])
                                        ty = (-tmpy[i] * 1000000 + AlignPosition[2])
                                        xy = pd.DataFrame([tx,ty]).T
                                        xy['No']=No; xy['wfr']=i; xy['shot'] = [ 'A' + str(i) for i in range(xy.shape[0])]
                                        # xy['type'] = 'residual'
                                        xy.columns=['x','y','No','wfr','shot']
                                        xy['y']=[200-i for i in xy['y']]



                                        # merge = pd.concat([merge,ref])
                                        merge = pd.concat([merge,xy],axis=0)
                                if 'Generate_reference'=='Generate_reference':
                                    ref = AlignPosition.copy()
                                    ref['No'] = No;
                                    # ref['wfr'] = i;
                                    ref['shot'] = ['A' + str(i) for i in range(ref.shape[0])]
                                    # ref['type'] = 'coordinate'
                                    ref.columns = ['x', 'y', 'No',  'shot',]
                                    ref['y']=[200-i for i in ref['y']]
                                    ref= ref[["No","x","y","shot"]]
                                    reference = pd.concat([reference,ref],axis=0)

                                    # merge['index1'] = [ str(x)+"_"+str(y)+"_"+z  for x,y,z in zip(merge['No'],merge['wfr'],merge['shot'])]
                                    # merge['index2'] = [ str(x)+"_"+ y  for x,y in zip(merge['wfr'],merge['shot'])]
                                    # merge['No']=['No' + str(i) for i in merge['No']]



                                    # if path.split('ega_')[1][:6] in d1.keys():
                                    #     d1[path.split('ega_')[1][:6]] = pd.concat([d1[path.split('ega_')[1][:6]],merge],axis=0)
                                    # else:
                                    #     d1[path.split('ega_')[1][:6]] = merge.copy()
                                if 'generate para'=='generate para':
                                    para = pd.concat([data[5],data[6]],axis=1)
                                    para.columns = ['scalingx','ort','scalingy','rot']
                                    para['ort'] = para['ort']-para['rot']
                                    para['scalingy']= -1 * para['scalingy']
                                    para['No']=No
                                    para['wfrNo'] = [str(i + 1) for i in para.index]
                                    paraSummary=pd.concat([paraSummary,para],axis=0)

                            else:
                                try:
                                    os.rename(path, path + '_bad')
                                except:
                                    os.remove(path) #重复下载时会出错

                        except:
                            os.rename(path,path+'_bad')
                    # print(indexSummary.shape)
                    # print(merge.shape)
                    # print(para.shape)
                    # print(reference.shape)



                    indexSummary["Date"]=[ x.split("ega_")[1].split(".egam")[0] for x in indexSummary["File"]]
                    indexSummary=indexSummary.drop("File",axis=1)
                    indexSummary=indexSummary.fillna("")
                    # indexSummary.to_csv("c:/temp/001.csv",index=None)
                    conn = sqlite3.connect(dbPath)
                    indexSummary = indexSummary.fillna("")
                    indexSummary.to_sql('tbl_index', conn, if_exists='append', index=None)
                    conn.close()


                    merge=merge[["No","wfr","shot","x","y"]]
                    # merge.to_csv("c:/temp/002_"+tool+"_.csv",index=None)
                    conn = sqlite3.connect(dbPath)
                    merge = merge.fillna("")
                    merge.to_sql('tbl_'+tool, conn, if_exists='append', index=None)
                    conn.close()





                    paraSummary=paraSummary[["wfrNo","No","scalingx","scalingy","rot","ort"]]
                    # para.to_csv("c:/temp/003.csv",index=None)
                    conn = sqlite3.connect(dbPath)
                    paraSummary = paraSummary.fillna("")
                    paraSummary.to_sql('tbl_parameter', conn, if_exists='append', index=None)
                    conn.close()



                    # reference.to_csv("c:/temp/004.csv",index=None)
                    conn = sqlite3.connect(dbPath)
                    reference = reference.fillna("")
                    reference.to_sql('tbl_coordinate', conn, if_exists='append', index=None)
                    conn.close()


class NikonProductMetalImage:
    def __init__(self):
        pass
    def YE_folderlist(self):  # n天内的图片目录
        print('=== folder lists of YE images server being extracted.........===')
        folderlist = []
        str1 = datetime.datetime.today().strftime("%Y%m%d")
        foldername = '\\\\10.4.72.182\\d\\ftpimages\\' + str1

        for root, dirs, files in os.walk(foldername, False):
            for name in dirs:
                if '-LN_' in name and (
                        '-A1-' in name or '-A2-' in name or '-A3-' in name or '-AT-' in name or '-TT-' in name):
                    print(name)
                    folderlist.append(root + '\\' + name + '\\')
        return folderlist
    def nikon_merge_image(self,folderlist):  # 图片合并程序

        for path in folderlist:
            filelist = [lists for lists in os.listdir(path) if os.path.isfile(os.path.join(path, lists))]

            n = len(filelist)
            if n % 9 != 0:
                k = n // 9 + 1
            try:
                toImage = Image.new('RGB', (9 * 540, k * 540 + 96), (255, 255, 255))

                for i, name in enumerate(filelist):
                    print(path, "  ", i, "  ", name)
                    tmp = Image.open(path + name)
                    toImage.paste(tmp, (i * 540, i // 9 * 540 + 96))
                # toImage.save('c:/temp/a.jpg')

                setfont = ImageFont.truetype('simsun.ttc', 96)
                fillcolor = (255, 0, 0)
                draw = ImageDraw.Draw(toImage)
                draw.text((100, 0), path.split('\\')[7], fill=fillcolor, font=setfont)

                toImage.save(
                    'c:\\temp\\' + path.split('\\')[7] + '_' + path.split('\\')[6] + '.jpg')
            except:
                pass
    def MainFunction(self):


        try:
            shutil.rmtree(r'c:\temp')
            os.mkdir(r'c:\temp')
        except:
            pass
        try:
            os.mkdir(r'c:\temp')
        except:
            pass
        folderlist = self.YE_folderlist()
        self.nikon_merge_image(folderlist)










if __name__ == "__main__":


    try:
        AWE_ANALYSIS_NEW_TAR().MainFunction()
    except:
        pass
    try:
        AsmlBatchReport_TAR().MainFunction()
    except:
        pass
    try:
        NikonVector_NEW().MainFunction()
    except:
        pass

