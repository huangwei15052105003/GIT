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
import ftplib
import numpy as np
from sklearn.linear_model import LinearRegression
import shutil
import win32com.client
from dateutil import parser
from PIL import Image,ImageDraw,ImageFont
from math import isnan
# from matplotlib.gridspec import GridSpec
import gc
import pandas as pd
# import matplotlib.pyplot as plt
import matplotlib.dates as mdates
import matplotlib.lines as mlines
# import re
# import matplotlib.ticker as ticker
# r'D:\HuangWeiScript\PyTaskCode\R2R_New_Part.xlsm'
# 'Z:\\_DailyCheck\\NikonShot\\transfer\\move.xls'
# os.system('z:\\_dailycheck\\ESF\\ESF.xlsm')

#below for wafer map
from matplotlib.patches import Circle, Rectangle,Polygon
import matplotlib.pyplot as plt
# from math import sqrt as sqrt
import win32com.client
# import threading
# from multiprocessing import Process
# import math
import time
import datetime

import sqlite3
import tarfile


global selection

class Tools:
    def __init__(self):
        pass

    def JobinImport(self):
        nikon = pd.DataFrame(
            columns=['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part', 'Layer', 'EqId', 'CD', 'DOSE',
                     'FOCUS'])
        asml = pd.DataFrame(
            columns=['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part', 'Layer', 'EqId', 'CD', 'DOSE',
                     'FOCUS'])

        try:
            nikon = pd.read_excel('c:/temp/nikon.xls')
            nikon = nikon[
                ['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part', 'Layer', 'EqId', 'CD', 'DOSE',
                 'FOCUS']]
        except:
            pass
        try:
            asml = pd.read_excel('c:/temp/asml.xls')
            asml = asml[
                ['JI Time', 'Lot ID', 'WaferCount', 'Tech', 'Track Recipe', 'Part', 'Layer', 'EqId', 'CD', 'DOSE',
                 'FOCUS']]
        except:
            pass
        output = pd.read_csv("c:/temp/R2R.csv")
        focus = pd.concat([nikon, asml], axis=0)
        print(focus.shape)
        max = pd.DataFrame(focus.groupby(['Part', 'Layer', 'EqId'])['FOCUS'].max())
        min = pd.DataFrame(focus.groupby(['Part', 'Layer', 'EqId'])['FOCUS'].min())

        tmp = pd.concat([max, min], axis=1)
        tmp.columns = ['max', 'min']
        tmp['Flag'] = tmp['max'] == tmp['min']
        focus = tmp[tmp['Flag'] == True][['max']].reset_index()
        nikon, asml, max, min, tmp = None, None, None, None, None
        focus.columns = ['PART', 'LAYER', 'TOOL', 'FOCUS']
        r2r = pd.merge(output, focus, how='left', on=['PART', 'LAYER', 'TOOL'])
        r2r.columns = ['DCOLL_TIME', 'PART', 'LAYER', 'TOOL', 'TYPE', 'tran-x', 'tran-y', 'exp-x', 'exp-y', 'non-ort',
                       'w-rot', 'mag', 'rot', 'asym-mag', 'asym-rot', 'DCOLL_TIME-1', 'JI_TIME', 'PART-1', 'LAYER-1',
                       'TOOL-1', 'TYPE-1', 'AVG', 'JOBIN', 'OPT', 'FB', 'RN', 'QTY-1', 'FOCUS']

        dummy = pd.DataFrame(['' for i in range(r2r.shape[0])])
        datain = r2r[['TYPE', 'PART', 'LAYER']]
        for i in range(13):
            datain = pd.concat([datain, dummy], axis=1)

        datain['EqId'] = r2r['TOOL']
        datain = pd.concat([datain, dummy], axis=1)
        datain['Value'] = r2r['OPT']
        datain['Up'] = r2r['OPT'] * 1.1
        datain['Down'] = r2r['OPT'] * 0.9
        datain['Value1'] = r2r['FOCUS']
        datain['Up1'] = r2r['FOCUS'] + [0.01 for i in range(r2r.shape[0])]
        datain['Down1'] = r2r['FOCUS'] + [-0.01 for i in range(r2r.shape[0])]
        datain = pd.concat([datain, pd.DataFrame(r2r['tran-x'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['tran-y'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['exp-x'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)

        datain = pd.concat([datain, pd.DataFrame(r2r['exp-y'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['non-ort'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['w-rot'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['mag'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['rot'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['asym-mag'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain = pd.concat([datain, pd.DataFrame(r2r['asym-rot'])], axis=1)
        for i in range(2):
            datain = pd.concat([datain, dummy], axis=1)
        datain['Lock'] = 'PPCS IMPORT,PLS PILOT RUN'
        datain['Fix'] = 'N'
        datain['Constrain'] = 'N'
        datain['TrackRecipe'] = ' '
        datain.columns = [i for i in range(1, 59)]
        tmp = [i for i in range(1, 49)]
        tmp.extend([55, 56, 57, 58])

        datain[datain[1] == 'ASML'].to_csv('c:/temp/asml_import.csv', index=None, header=None)
        datain = datain[tmp]
        datain[datain[1] == 'NIKON'].to_csv('c:/temp/nikon_import.csv', index=None, header=None)
class RunExcel:
    def __init_(self):
        pass
    def MainFunction(self):
        pass
        t1=datetime.datetime.now()
        os.system('P:\\_Script\\ExcelFile\\ESF.xlsm')
        print('001_ ESF.xlsm',datetime.datetime.now()-t1)
        t1 = datetime.datetime.now()
        os.system('P:\\_Script\\ExcelFile\\_PPID.xlsm')
        print('002_ _PPID.xlsm',datetime.datetime.now()-t1)
        t1 = datetime.datetime.now()
        os.system('z:\\_dailycheck\\WeeklyRework\\2019Rework.xlsm')
        print('003_ 2019Rework.xlsm',datetime.datetime.now()-t1)
        t1 = datetime.datetime.now()
        os.system(r'\\10.4.72.74\litho\PyTaskCode\NikonLotReportRevised.xls')
        print('004_ NikonLotReportRevised.xls',datetime.datetime.now()-t1)
        t1 = datetime.datetime.now()
        os.system('P:/_Script/ExcelFile/SPC99.xlsm')
        print('005_ SPC99.xlsm',datetime.datetime.now()-t1)
        t1 = datetime.datetime.now()
        os.system(r'P:\_Script\ExcelFile\R2R_New_Part.xlsm')  ##refreshed again in other function
        print('006_ R2R_New_Part.xlsm',datetime.datetime.now()-t1)
        t1 = datetime.datetime.now()
        os.system('P:\\_Script\\ExcelFile\\MoveCdu.xls')   ##refreshed again in other function
        print('007_ MoveCDU.xls', datetime.datetime.now() - t1)
        t1 = datetime.datetime.now()
        os.system('P:\\_Script\\ExcelFile\\move.xlsm')
        print('008_ move.xlsm',datetime.datetime.now() - t1)
        t1 = datetime.datetime.now()
        os.system('P:\\_Script\\ExcelFile\\FLOW.xlsm')
        print('009_ FLOW.xlsm', datetime.datetime.now() - t1)  ## For ESF CONSTRAINTS
        t1 = datetime.datetime.now()
        os.system('P:\\_Script\\ExcelFile\\FLOW_FOR_OPAS.xlsm')
        print('010_ FLOW_FOR_OPAS.xlsm', datetime.datetime.now() - t1)  ## For R2R TRACK RECIPE


        # os.system(r'Z:\_DailyCheck\WeeklyRework\RWK_Refresh_Modified_Rwk_Code_@7PM.xlsm')
        # print('007',datetime.datetime.now()-t1)
class MakeVmsFtp:
    def __init__(self):
        pass
    def MainFunction(self):
        no = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '16', '17', '18',
              '19', '20', '21', '22', '23', '24', '25']
        tmp = str(datetime.datetime.now().date() - datetime.timedelta(days=1)).split('-')
        tmp = tmp[0] + tmp[1] + tmp[2]
        for tool in no:
            if tool in ['22', '23']:
                if os.path.exists(r'P:\SEQLOG\EGAftp\EGAftp' + tool + '.ftp'):
                    os.remove(r'P:\SEQLOG\EGAftp\EGAftp' + tool + '.ftp')
                file = open(r'P:\SEQLOG\EGAftp\EGAftp' + tool + '.ftp', 'w')
                tmpstr = 'set def dka100:[mcsvlog.msr]\n'
                tmpstr = tmpstr + 'lcd dka300:[ega' + tool + ']\n'
                tmpstr = tmpstr + 'asc\n'
                tmpstr = tmpstr + 'get ega_' + tmp + '*.egam;*\n'
                tmpstr = tmpstr + 'bye\n'
                file.write(tmpstr)
                file.close
            else:
                if os.path.exists(r'P:\SEQLOG\EGAftp\EGAftp' + tool + '.ftp'):
                    os.remove(r'P:\SEQLOG\EGAftp\EGAftp' + tool + '.ftp')
                file = open(r'P:\SEQLOG\EGAftp\EGAftp' + tool + '.ftp', 'w')
                tmpstr = 'set def dka300:[mcsvlog.msr]\n'
                tmpstr = tmpstr + 'lcd dka300:[ega' + tool + ']\n'
                tmpstr = tmpstr + 'asc\n'
                tmpstr = tmpstr + 'get ega_' + tmp + '*.egam;*\n'
                tmpstr = tmpstr + 'bye\n'
                file.write(tmpstr)
                file.close
class NikonRecipeDate:
    def __init__(self):
        pass
    def get_path(self,FileDir=r'P:\Nikondir'):
        filenamelist = []
        for root, dirs, files in os.walk(FileDir,False):
            # root:所有目录名-->字符串 #dirs: root下的子目录名-->列表 #files：root下的文件名-->列表 # name.endswith(ext)-->文件名筛选
            for names in files:
                if 'ALII' in names and '.txt' in names:
                    tmpname = root + '\\' + names
                    x = time.localtime((int(os.stat(tmpname).st_mtime)))
                    tmpdatetime = datetime.datetime.strptime(
                        str(x.tm_year) + '-' + str(x.tm_mon) + '-' + str(x.tm_mday), '%Y-%m-%d')
                    filenamelist.append([tmpname, (tmpdatetime.date())])
        filenamelist.sort(reverse=False)
        return filenamelist
    def MainFunction(self):
        filelist = self.get_path(FileDir=r'P:\Nikondir')
        tool = []
        missing = []
        for i, x in enumerate(filelist):
            print(x)
            key = datetime.datetime.now().date()
            if x[1] < key:
                tool.append([x[0], str(x[1])])
                missing.append(x[0].split("\\")[2].split('.')[0])
                print(x[0], str(x[1]))
            tmp = pd.read_csv(x[0])
            tmp = tmp.dropna()
            tmp = tmp.reset_index()
            tmp.columns = [1, 2, 3, 4]
            tmp = tmp[tmp[1].str.contains('.PRV;')]
            tmp['Tool'] = x[0][-10:-4]
            if i == 0:
                df = tmp.copy()
            else:
                df = pd.concat([df, tmp], axis=0)
        tmp = df[1].str.split(';')
        name = [i[0][:-4] for i in tmp]  # part name
        date = [i[1].split('-') for i in tmp]
        date1 = [i[0][-2:] for i in date]  # date
        date2 = [i[1] for i in date]  # month
        date3 = [i[2][0:4] for i in date]  # year
        tool = list(df['Tool'])
        df = pd.DataFrame(np.array([tool, name, date1, date2, date3]).T)
        df['date'] = pd.to_datetime(df[2] + '-' + df[3] + '-' + df[4])
        df = df.drop([2, 3, 4], axis=1)
        df.columns = ['Tool', 'Part', 'Date']
        df = df[df['Date'] > (datetime.datetime.now().date() - datetime.timedelta(days=2))]
        df = df.drop_duplicates()
        result = df.pivot(index='Part', columns='Tool', values='Part')
        pd.concat([result, pd.DataFrame(missing)], axis=0).to_csv(
            'Z:/_DailyCheck/NikonRecipe/' + str(datetime.datetime.now().date()) + '.csv')
class NikonPara:
    def __init__(self):
        pass
    def get_path(self,FileDir=r'P:\SEQLOG\macconst'):
        filenamelist = []
        for root, dirs, files in os.walk(FileDir,False):
            for names in files:
                filenamelist.append(FileDir + '\\' + names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def MacAdjust(self):
        filelist = self.get_path(FileDir=r'P:\SEQLOG\macadjust')
        data11 = []
        data14 = []
        for file in filelist:
            print(file)
            try:
                input = open(file)
                tmp = [eval(line.split('=')[1].strip()) for line in input if "=" in line and "sas_name" not in line]
                tmp.append(file.split('.')[1])
                tmp.append(str(datetime.date.today()))
                if file[31:34] == '230':
                    data11.append(tmp)
                else:
                    data14.append(tmp)
            except:
                pass
            input.close()
            os.remove(file)
        pd.DataFrame(data11).to_csv('Y:/NikonPara/Adjust11.csv', index=False, header=False, mode='a')
        pd.DataFrame(data14).to_csv('Y:/NikonPara/Adjust14.csv', index=False, header=False, mode='a')

    def MacConst(self):
        filelist = self.get_path(FileDir=r'P:\SEQLOG\macconst')
        data11 = []
        data14 = []
        for file in filelist:
            print(file)
            try:
                input = open(file)
                tmp = [(line.split('=')[1].strip()) for line in input if "=" in line and '!' not in line]

                tmp.append(file.split('.')[1])
                tmp.append(str(datetime.date.today()))
                if file[29:32] == '241':
                    data11.append(tmp)
                else:
                    data14.append(tmp)
            except:
                pass
            input.close()
            os.remove(file)
        pd.DataFrame(data11).to_csv('Y:/NikonPara/Const11.csv', index=False, header=False, mode='a')
        pd.DataFrame(data14).to_csv('Y:/NikonPara/Const14.csv', index=False, header=False, mode='a')
    def SYSparam(self):
        filelist = self.get_path(FileDir=r'P:\SEQLOG\SYSparam')
        data11 = []
        data11B = []
        data14 = []
        for file in filelist:
            print(file)
            try:
                input = open(file)
                tmp = [(line.split('=')[1].strip()) for line in input if "=" in line and '!' not in line]
                tmp.append(file.split('.')[1])
                tmp.append(str(datetime.date.today()))

                if file[29:33] == '242.':
                    data11.append(tmp)
                elif file[29:33] == '242B':
                    data11B.append(tmp)
                else:
                    data14.append(tmp)
            except:
                pass
            input.close()
            os.remove(file)
        pd.DataFrame(data11).to_csv('Y:/NikonPara/SysParam11.csv', index=False, header=False, mode='a')
        pd.DataFrame(data11B).to_csv('Y:/NikonPara/SysParam11B.csv', index=False, header=False, mode='a')
        pd.DataFrame(data14).to_csv('Y:/NikonPara/SysParam14.csv', index=False, header=False, mode='a')
    def MainFunction(self):
        self.MacAdjust()
        self.MacConst()
        self.SYSparam()
class NikonVector: #部分设备无数据；数据有误：X值需取负；residual取负;计算都有问题，作废；
    def __init__(self):
        pass
    def get_path(self,FileDir='f:\\temp\\ALII01'):
        filenamelist = []
        for root, dirs, files in os.walk(FileDir,False):
            for names in files:
                if '.egam' in names:
                    filenamelist.append(root + '\\' + names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def connect_db(self,databasepath, rs_name):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(rs_name, conn, 1, 3)
        return conn, rs
    def dataframe_to_string(self,dataframe, shape0, shape1):
        tmp = list((np.array(dataframe)).reshape(shape0 * shape1))
        tmpstr = ''
        for i in tmp:
            # tmpstr = tmpstr + str(i) + ','
            tmpstr = tmpstr + str("%.3f" % i) + ','
        return tmpstr
    def MainFunction(self):
        databasepath = 'Y:/NikonEgaLog/NikonEgaLog.mdb'
        rs_name = 'NikonEga'
        conn, rs = self.connect_db(databasepath, rs_name)
        filenamelist = self.get_path(FileDir='P:\\EgaLog\\')
        for k, path in enumerate(filenamelist[0:]):
            flag = True
            print(k, len(filenamelist), path, flag)
            tmp = []

            f = open(path)
            for i in f:
                tmp.append(i.strip().split(':='))

            try:
                df = pd.DataFrame(tmp)

                Measure = df[df[0].str.contains("MEAS_DATA")]

                Measure = Measure[1].apply(lambda x: pd.Series([eval(i) for i in x.split(",")]))
                df = df.reset_index().set_index(0).drop(columns='index', axis=1)
            except:
                flag = False  # no content in egam file
                print('\n\n=====', k, len(filenamelist), path, flag, '\n\n====')

            if flag == True and eval(df.loc[['MESP_NUM'], :].iloc[0, 0]) > 1:
                try:

                    StepX = eval(df.loc[['STEP_PITCH'], :].iloc[0, 0].split(',')[0])
                    StepY = eval(df.loc[['STEP_PITCH'], :].iloc[0, 0].split(',')[1])
                    OffsetX = eval(df.loc[['MAP_OFFSET'], :].iloc[0, 0].split(',')[0])
                    OffsetY = eval(df.loc[['MAP_OFFSET'], :].iloc[0, 0].split(',')[1])


                    WfrNo = eval(df.loc[['WAFER_NUM'], :].iloc[0, 0])
                    AlignNo = eval(df.loc[['SHOT_NUM'], :].iloc[0, 0])
                    AlignPosition = []
                    for i in range(AlignNo):
                        tmp = 'SHOT(' + str(i + 1) + ')'
                        tmp = eval(df.loc[[tmp], :].iloc[0, 0])
                        AlignPosition.append((i + 1, (tmp[1] - 1) * StepX + OffsetX - StepX / 2,
                                              ((tmp[2] - 1) * StepY + OffsetY - StepY / 2)))
                    Y = Measure[Measure[1] == 1.0][4]
                    Y = pd.DataFrame(Y.values.reshape(WfrNo, AlignNo)).T
                    Y.columns = [i + 1 for i in range(Y.shape[1])]
                    X = Measure[Measure[1] == 2.0][3]
                    X = pd.DataFrame(X.values.reshape(WfrNo, AlignNo)).T
                    X.columns = [i + 1 for i in range(X.shape[1])]
                    AlignPosition = pd.DataFrame(AlignPosition).drop(columns=0, axis=1)

                    tmpx = []
                    tmpy = []

                    magrotortx = []
                    magrotorty = []

                    linreg = LinearRegression()

                    ref = pd.DataFrame([0 for i in range(AlignNo)])

                    for i in range(X.shape[1]):
                        i = i + 1

                        input_y = X[X[i] != 0][i]
                        input_x = AlignPosition.iloc[input_y.index, :]
                        model = linreg.fit(input_x, input_y)
                        residual = pd.DataFrame(
                            linreg.intercept_ + linreg.coef_[0] * input_x[1] + linreg.coef_[1] * input_x[2] - input_y)
                        tmpx.append(((residual + ref).fillna(0))[0])
                        magrotortx.append([linreg.coef_[0] * 1000000, linreg.coef_[1] * 1000000])

                        input_y = Y[Y[i] != 0][i]
                        input_x = AlignPosition.iloc[input_y.index, :]
                        model = linreg.fit(input_x, input_y)
                        residual = pd.DataFrame(
                            linreg.intercept_ + linreg.coef_[0] * input_x[1] + linreg.coef_[1] * input_x[2] - input_y)
                        tmpy.append(((residual + ref).fillna(0))[0])
                        magrotorty.append([linreg.coef_[1] * 1000000, linreg.coef_[0] * 1000000])

                    magrotortx = pd.DataFrame(magrotortx)
                    magrotorty = pd.DataFrame(magrotorty)

                    tmpx = pd.DataFrame(tmpx).T
                    tmpx.columns = [i + 1 for i in range(tmpx.shape[1])]
                    tmpy = pd.DataFrame(tmpy).T
                    tmpy.columns = [i + 1 for i in range(tmpy.shape[1])]

                    rs.AddNew()

                    rs.Fields.Item("FILE_NAME").Value = df.loc[['FILE_NAME'], :].iloc[0, 0].strip()[1:-1]
                    rs.Fields.Item("MEAS_SENS").Value = df.loc[['MEAS_SENS'], :].iloc[0, 0].strip()[1:-1]
                    rs.Fields.Item('EXPS_MACHINE').Value = df.loc[['EXPS_MACHINE'], :].iloc[0, 0].strip()[1:-1]
                    rs.Fields.Item('MEAS_DATE').Value = df.loc[['MEAS_DATE'], :].iloc[0, 0].strip()[1:-1]
                    rs.Fields.Item('MEAS_PDF').Value = df.loc[['MEAS_PDF'], :].iloc[0, 0].strip()[1:-1]
                    rs.Fields.Item('StepX').Value = StepX
                    rs.Fields.Item('StepY').Value = StepY
                    rs.Fields.Item('OffsetX').Value = OffsetX
                    rs.Fields.Item('OffsetY').Value = OffsetY
                    rs.Fields.Item('LSA_REQ_SHOT').Value = eval(df.loc[['LSA_REQ_SHOT'], :].iloc[0, 0])
                    rs.Fields.Item('FIA_REQ_SHOT').Value = eval(df.loc[['FIA_REQ_SHOT'], :].iloc[0, 0])
                    rs.Fields.Item('FM_ROTATION').Value = eval(df.loc[['FM_ROTATION'], :].iloc[0, 0])
                    rs.Fields.Item('CORR_W_OFF').Value = (df.loc[['CORR_W_OFF'], :].iloc[0, 0])
                    rs.Fields.Item('CORR_W_SCL').Value = (df.loc[['CORR_W_SCL'], :].iloc[0, 0])
                    rs.Fields.Item('CORR_W_ORT').Value = (df.loc[['CORR_W_ORT'], :].iloc[0, 0])
                    rs.Fields.Item('CORR_W_ROT').Value = (df.loc[['CORR_W_ROT'], :].iloc[0, 0])
                    rs.Fields.Item('CORR_C_SCL').Value = (df.loc[['CORR_C_SCL'], :].iloc[0, 0])
                    rs.Fields.Item('CORR_C_ROT').Value = (df.loc[['CORR_C_ROT'], :].iloc[0, 0])
                    rs.Fields.Item('WAFER_NUM').Value = WfrNo
                    rs.Fields.Item('SHOT_NUM').Value = AlignNo

                    tmpstr = self.dataframe_to_string(dataframe=AlignPosition, shape0=eval(df.loc[['SHOT_NUM'], :].iloc[0, 0]),
                                                 shape1=2)
                    rs.Fields.Item('ALIGN_SHOT').Value = tmpstr  # '测试坐标位置，排列顺序为 1-x/y,2-x/y,3-x/y,,,,,,,,依次类推

                    rs.Fields.Item('SEARCH').Value = df.loc[['SENSOR(1)'], :].iloc[0, 0].split(',')[1].strip()[1:-1]
                    rs.Fields.Item('EGA').Value = df.loc[['SENSOR(2)'], :].iloc[0, 0].split(',')[1].strip()[1:-1]
                    rs.Fields.Item('UPPER_LIMIT').Value = eval(df.loc[['UPPER_LIMIT'], :].iloc[0, 0])
                    rs.Fields.Item('LOWER_LIMIT').Value = eval(df.loc[['LOWER_LIMIT'], :].iloc[0, 0])
                    rs.Fields.Item('RETICLE_ROTATION').Value = eval(df.loc[['CORR_C_ROT'], :].iloc[0, 0])

                    tmpstr = self.dataframe_to_string(dataframe=X, shape0=eval(df.loc[['SHOT_NUM'], :].iloc[0, 0]),
                                                 shape1=eval(df.loc[['WAFER_NUM'], :].iloc[0, 0]))
                    rs.Fields.Item('EGA_X').Value = tmpstr

                    tmpstr = self.dataframe_to_string(dataframe=tmpx, shape0=eval(df.loc[['SHOT_NUM'], :].iloc[0, 0]),
                                                 shape1=eval(df.loc[['WAFER_NUM'], :].iloc[0, 0]))
                    rs.Fields.Item('RESIDUAL_X').Value = tmpstr

                    tmpstr = self.dataframe_to_string(dataframe=Y, shape0=eval(df.loc[['SHOT_NUM'], :].iloc[0, 0]),
                                                 shape1=eval(df.loc[['WAFER_NUM'], :].iloc[0, 0]))
                    rs.Fields.Item('EGA_Y').Value = tmpstr

                    tmpstr = self.dataframe_to_string(dataframe=tmpy, shape0=eval(df.loc[['SHOT_NUM'], :].iloc[0, 0]),
                                                 shape1=eval(df.loc[['WAFER_NUM'], :].iloc[0, 0]))
                    rs.Fields.Item('RESIDUAL_Y').Value = tmpstr

                    tmpstr = self.dataframe_to_string(dataframe=magrotortx, shape1=2,
                                                 shape0=eval(df.loc[['WAFER_NUM'], :].iloc[0, 0]))
                    rs.Fields.Item('MODEL_X').Value = tmpstr
                    tmpstr = self.dataframe_to_string(dataframe=magrotorty, shape1=2,
                                                 shape0=eval(df.loc[['WAFER_NUM'], :].iloc[0, 0]))
                    rs.Fields.Item('MODEL_Y').Value = tmpstr
                    rs.Update()
                except:
                    pass
            newpath = "Y:\\NikonEgaLog\\" + path[10:]
            f.close()
            shutil.move(path, newpath)

        # rs.close
        conn.close
class NikonRecipeMaintain:
    def __init__(self):
        pass
    def generate_ftp(self,nikonpath, wipPart, list1):

        no_delete = ['CH', 'CH001', 'CH002', 'CH003', 'DH', 'DH001', 'DH002', 'DH003', 'GSL', 'GSL001', 'GSL002',
                     'GSL003', 'PYH', 'PYH001', 'PYH002', 'PYH003', 'QWH', 'QWH001', 'QWH002', 'QWH003', 'ZJZ',
                     'ZJZ001', 'ZJZ002', 'ZJZ003', 'ZJ', 'ZJ001', 'ZJ002', 'ZJ003', 'ZP', 'ZP001', 'ZP002', 'ZP003',
                     'YW', "YW001", 'YW002', 'YW003', 'QQ', 'QQ001', 'QQ002', 'QQ003', 'HFB', 'HFB001', 'HFB002',
                     'HFB003', 'LPG', 'LPG001', 'LPG002', 'LPG003', 'LYH', 'LYH001', 'LYH002', 'LYH003', 'SK', 'SK001',
                     'SK002', 'SK003', 'WYC', 'WYC001', 'WYC002', 'WYC003', 'WTX', 'WTX001', 'WTX002', 'WTX003', 'YXC',
                     'YXC001', 'YXC002', 'YXC003', 'ZFS', 'ZFS001', 'ZFS002', 'ZFS003', 'ZLF', 'ZLF001', 'ZLF002',
                     'ZLF003', 'ZQX', 'ZQX001', 'ZQX002', 'ZQX003', 'FH', 'FH001', 'FH002', 'FH003', 'SCR', 'SCR001',
                     'SCR002', 'SCR003', '2LXXXBST03', '2LXXXBST04', '2LXXXLSA01', '2LXXXFIA02', '2LXXXFIA03',
                     '2LXXXFIA04', '2LXXXFIA05', '2LXXXFIA06', '2LXXXFIA07', '2LXXXSCD01', '2LXXXSCD02', '2LXXXSCD03',
                     '2LXXXSCD04', '2LXXXSCD05', 'XK2047AZ1ST']

        # ppid in nikon tool
        tmp = list(pd.read_csv(nikonpath).reset_index()['level_0'])
        tmp = [i.split(".")[0] for i in tmp if '.PRV;' in i]
        nikonPpid = tmp.copy()

        # ppid to be processed
        to_be_copy1 = set(wipPart) - set(nikonPpid)
        to_be_delete1 = set(nikonPpid) - set(wipPart)

        to_be_copy = []

        for tmp in to_be_copy1:
            if tmp in list1:
                to_be_copy.append(tmp)
        to_be_copy.sort()

        to_be_delete = []
        for tmp in to_be_delete1:
            if tmp not in no_delete:
                to_be_delete.append(tmp)
        to_be_delete.sort()

        filestr1 = 'Z:\\_DailyCheck\\NikonRecipeMaintain\\' + nikonpath[-10:-4] + '_put.ftp'
        filestr2 = 'Z:\\_DailyCheck\\NikonRecipeMaintain\\' + nikonpath[-10:-4] + '_delete.ftp'
        filestr3 = 'Z:\\_DailyCheck\\NikonRecipeMaintain\\' + nikonpath[-10:-4] + '_bak.ftp'

        filestr1 = 'P:\\SEQLOG\\FTP\\' + nikonpath[-10:-4] + '_put.ftp'
        filestr2 = 'P:\\SEQLOG\\FTP\\' + nikonpath[-10:-4] + '_delete.ftp'
        filestr3 = 'P:\\SEQLOG\\FTP\\' + nikonpath[-10:-4] + '_bak.ftp'

        try:
            os.remove(filestr1)
        except:
            pass

        try:
            os.remove(filestr2)
        except:
            pass

        try:
            os.remove(filestr3)
        except:
            pass

        ftp = open(filestr1, 'a')
        if nikonpath[16:18] == '22' or nikonpath[16:18]== '23':
            ftp.write('lcd  dka300:[mcsvuser.user]\n')
            ftp.write('set def dka100:[mcsvuser.user]\nbin\n')
        else:
            ftp.write('lcd  dka300:[mcsvuser.user]\n')
            ftp.write('set def dka300:[mcsvuser.user]\nbin\n')

        if nikonpath[16:18]== '22' or nikonpath[16:18] == '23':
            for name in to_be_copy:
                ftp.write('put ' + name + '.prv\n')
        else:
            for name in to_be_copy:
                ftp.write('put ' + name + '.prv\n')
        ftp.write('bye\n')
        ftp.close()

        ftp = open(filestr3, 'a')
        if nikonpath[16:18]== '22' or nikonpath[16:18] == '23':
            ftp.write('lcd  dka300:[mcsvuser.user]\n')
            ftp.write('set def dka100:[bak]\nbin\n')
        else:
            ftp.write('lcd  dka300:[mcsvuser.user]\n')
            ftp.write('set def dka300:[bak]\nbin\n')

        if nikonpath[16:18] == '22' or nikonpath[16:18] == '23':
            for name in to_be_copy:
                ftp.write('put ' + name + '.prv\n')
        else:
            for name in to_be_copy:
                ftp.write('put ' + name + '.prv\n')
        ftp.write('bye\n')
        ftp.close()

        ftp = open(filestr2, 'a')
        if nikonpath[16:18] == '22' or nikonpath[16:18] == '23':
            ftp.write('set def dka100:[mcsvuser.user]\nbin\n')
        else:
            ftp.write('set def dka300:[mcsvuser.user]\nbin\n')

        if nikonpath[16:18] == '22' or nikonpath[16:18] == '23':
            for name in to_be_delete:
                ftp.write('delete ' + name + '.prv;*\n')
        else:
            for name in to_be_delete:
                ftp.write('delete ' + name + '.prv;*\n')
        ftp.write('bye\n')
        ftp.close()
    def generate_sequence_ftp(self,nikonpath):

        path = nikonpath.split('\\')[2][0:6]
        path = 'P:\\SEQLOG\\seqList\\' + path + '.seq'
        tmp = [i.strip() for i in  open(path).readlines() if i.strip()!="" and "LOG_SEQUENCE.LOG" in i][1:]



        seqftp = 'P:\\SEQLOG\\FTP\\seq_' + nikonpath[-6:-4] + '.ftp'



        try:
            os.remove(seqftp)
        except:
            pass




        ftp = open(seqftp, 'a')
        if nikonpath[-6:-4] in ['22','23']:
            ftp.write('lcd  dka400:[' + nikonpath[-6:-4] + ']\n')
            ftp.write('set def dka100:[mcsvlog]\nasc\n')
        else:
            ftp.write('lcd  dka400:[' + nikonpath[-6:-4] + ']\n')
            ftp.write('set def dka300:[mcsvlog]\nasc\n')

        for name in tmp:
            ftp.write('\nget ' + name)
        # for name in tmp:
        #     ftp.write('\ndelete ' + name )

        ftp.write('\nbye\n')
        ftp.close()
    def rename_sequence_log(self):
        path = "\\\\10.4.50.16\\PHOTO$\\PPCS\\SEQLOG\\"
        toollist= ['ALII19', 'ALII18', 'ALII17', 'ALII16', 'ALII15', 'ALII14',
 'ALII13', 'ALII12', 'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII06',
 'ALII07', 'ALII08', 'ALII09', 'ALII10', 'ALII11', 'BLII22', 'BLII20', 'BLII21',
 'BLII23']
        for tool in toollist[:]:
            filelist = [path + tool + '\\'+ i for i in os.listdir(path+tool) if ('TXT' in i or 'log_sequence.log' in i)]
            if len(filelist)>0:
                tmp1,tmp2='None','None'
                for file in filelist:
                    f= [ i.strip() for i in open(file).readlines()]
                    for i in f[0:100]:
                        if "Logging start at" in i:
                            tmp1= i.split("Logging start at")[1].strip()[0:-3]
                            print(tmp1)
                            break
                    for i in f[-100:]:
                        if "Logging end at" in i:
                            tmp2= i.split("Logging end at")[1].strip()[0:-3]
                            print(tmp2)
                            break
                    shutil.move(file, ((file[0:39]+"_"+tmp1+"#"+tmp2).replace(' ',"_")).replace(":",""))




    def MainFunction(self):

        tmp = str(datetime.datetime.now())
        wipPath = r'P:\_Script\ExcelFile\R2R_New_Part.xlsm'

        mfgPath = '\\\\10.4.50.16\\fab2文件库\\F2_PUBLIC\\Daily meeting\\'
        mfgPath = mfgPath + tmp[0:4] + '年生产会议\\'
        mfgPath = mfgPath + tmp[0:4] + "." + tmp[5:7] + '\\backlog'
        mfgPath = mfgPath + tmp[5:7] + tmp[8:10] + '.xls'

        # bank,wip,backlog==>part
        tmp1 = pd.read_excel(wipPath, sheet_name="WIP", usecols=[26])
        tmp1 = set([i.split('.')[0] for i in list(tmp1['Part'])])
        try:
            tmp2 = pd.read_excel(mfgPath, sheet_name="Backlog", usecols=[2], skiprows=[0])
            tmp2 = set(list(tmp2['PART']))
        except:
            tmp2 = tmp1.copy()

        part = tmp1 | tmp2
        wipPart = [i for i in part]

        ## list all PartID and Nikon PPID
        tmp = pd.read_excel(wipPath, sheet_name="WIP", usecols=[29, 30]).dropna().drop_duplicates()
        list1 = list(tmp['Y'])
        dic = {k: v for k, v in zip(list(tmp['X']), list(tmp['Y']))}

        # conver wip part name to nikon ppid(process)
        for n, tmp in enumerate(wipPart):
            try:
                if tmp != dic[tmp]:
                    wipPart[n] = dic[tmp]
                    print(tmp, wipPart[n])
            except:
                pass

        toollist = ['ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII06', 'ALII07', 'ALII08', 'ALII09', 'ALII10',
                    'ALII11', 'ALII12', 'ALII13', 'ALII14', 'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALII20',
                    'ALII21', 'ALII22', 'ALII23']

        for nikonpath in toollist:


            nikonpath = 'P:\\Nikondir\\' + nikonpath + '.TXT'
            try:
                self.generate_sequence_ftp(nikonpath)
            except:
                pass

            try:
                self.generate_ftp(nikonpath, wipPart, list1)

            except:
                pass
        self.rename_sequence_log()
class AsmlBatchReport_OBSELETE:#原始文件需不定期移到Y盘
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
        path_list = [r'Z:\ASMLbatch\8B', r'Z:\ASMLbatch\8C', r'Z:\ASMLbatch\8A', r'Z:\ASMLbatch\89', r'Z:\ASMLbatch\7D',
                     r'Z:\ASMLbatch\08', r'Z:\ASMLbatch\82', r'Z:\ASMLbatch\83', r'Z:\ASMLbatch\85', r'Z:\ASMLbatch\86',
                     r'Z:\ASMLbatch\87']

        # 字典，序列号对应设备名称
        tool = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                '3527': 'BLSD08'}

        for dir_batch_report in path_list:
            #    update_single_tool(dir_batch_report)

            # def update_single_tool(dir_batch_report):
            newset = None
            filename = None
            oldset = None
            newset = None
            df = None
            activeset = None
            data = None
            align = None
            mark_resiudal = None
            gc.collect()

            # 列出已完成的batch report文件名

            df = pd.read_csv('Y:/ASML_BATCH_REPORT/' + dir_batch_report[13:] + '_batchreport.csv', low_memory=False)
            oldset = set(df['Path'])

            # 指定batch report路径，列出其中文件名到 filename
            filename = self.get_batch_report_path(dir_batch_report)
            newset = set(filename)

            # 待完成的文件名
            activeset = newset - oldset
            print(dir_batch_report, "    ", str(len(activeset)), " files-->to be updated")

            if len(activeset) > 0:

                for filepath in (activeset):

                    print(filepath)
                    if os.path.getsize(filepath) < 8888 | ('rar' in filepath) | ('xls' in filepath):
                        os.system('del "' + filepath + '"')
                    else:
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
                        df.to_csv('Y:/ASML_BATCH_REPORT/' + dir_batch_report[13:] + '_batchreport.csv', columns=None,
                                  header=False, index=False, mode='a',
                                  encoding='utf-8')  #            df.to_csv('p:/huangwei/89_batchreport.csv',columns=None, header=False, index=False, mode='a',encoding='utf-8')
                        try:
                            newpath='Y:\\ASML_BATCH_REPORT\\' + filepath.split('\\')[2]+'\\'+filepath.split('\\')[3]+'\\'+filepath.split('\\')[4]
                            if os.path.exists(newpath):
                                pass
                            else:
                                os.makedirs(newpath)
                            shutil.move(filepath, 'Y:\\ASML_BATCH_REPORT' + filepath[12:])
                        except:
                            pass
                    try:
                        newpath = 'Y:\\ASML_BATCH_REPORT\\' + filepath.split('\\')[2] + '\\' + filepath.split('\\')[
                            3] + '\\' + filepath.split('\\')[4]
                        if os.path.exists(newpath):
                            pass
                        else:
                            os.makedirs(newpath)
                        shutil.move(filepath, 'Y:\\ASML_BATCH_REPORT' + filepath[12:])
                    except:
                        pass
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
        root = '//10.4.72.74/asml/_AsmlDownload/BatchReport/'
        alltools = ['7D','08','8A','8B','8C','82','83','85','86','87','89']
        tool = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                '3527': 'BLSD08'}
        for id in alltools[0:]:
            filelist = [ root + id + '/' + i for i in os.listdir(root + id) if i[0:4]!='Done' and i[-7:]=='.tar.gz']
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
                            df.to_csv('Y:/ASML_BATCH_REPORT/' + id + '_batchreport.csv', index=False, header=False, mode='a',  encoding='utf-8')
                    tar.close()
                    print(zipfile + '--> Extraction & Check Done')
                    print( "Renaming...." + zipfile)

                    os.rename(zipfile,zipfile[0:47]+"Done_"+zipfile[47:])
class AsmlPreAlignment:
    def __init__(self):
        pass
    def MainFunction(self):  # plot pre-align data of ASML
            filelist = ['Y:\\ASML_BATCH_REPORT\\08_batchreport.csv', 'Y:\\ASML_BATCH_REPORT\\7D_batchreport.csv',
                        'Y:\\ASML_BATCH_REPORT\\82_batchreport.csv', 'Y:\\ASML_BATCH_REPORT\\83_batchreport.csv',
                        'Y:\\ASML_BATCH_REPORT\\85_batchreport.csv', 'Y:\\ASML_BATCH_REPORT\\86_batchreport.csv',
                        'Y:\\ASML_BATCH_REPORT\\87_batchreport.csv', 'Y:\\ASML_BATCH_REPORT\\89_batchreport.csv',
                        'Y:\\ASML_BATCH_REPORT\\8A_batchreport.csv', 'Y:\\ASML_BATCH_REPORT\\8B_batchreport.csv',
                        'Y:\\ASML_BATCH_REPORT\\8C_batchreport.csv']
            for file in filelist:
                print(file)
                df = pd.read_csv(file, usecols=['Date', 'Time', 'JobName', 'Layer', 'Tran_X_Ave', 'Tran_Y_Ave'])
                df['Tool'] = file[21:23]
                df['Index'] = df['Date'] + ' ' + df['Time']
                df = df.iloc[-1000:]
                df['Index'] = pd.to_datetime(df['Index'])
                df = df.sort_values(by='Index')
                df = df.reset_index().set_index(['Index'])
                df = df[['Tool', 'JobName', 'Layer', 'Tran_X_Ave', 'Tran_Y_Ave']]
                flag = False
                df = df.dropna()
                df = df.iloc[-1000:]

                tmp = df[df['JobName'].str.contains('-L')]
                print(tmp.shape[0])

                if tmp.shape[0] > 0:
                    tmp['No'] = [i[7:11] for i in list(tmp['JobName'])]
                    tmp = tmp[tmp['No'] != '3559']
                    tmp = tmp[tmp['No'] != '1956']
                    print(tmp.shape[0])

                if tmp.shape[0] > 0:
                    flag = True
                    fig = plt.figure(figsize=(12, 9))

                    plt.subplot(221)
                    tmp['Tran_X_Ave'].plot()
                    plt.title(file[21:23] + '  Large Field Tran_X_Ave')
                    plt.ylim(-35, 35)

                    plt.subplot(222)
                    tmp['Tran_Y_Ave'].plot()
                    plt.title(file[21:23] + '   Large Field Tran_Y_Ave')
                    plt.ylim(-35, 35)

                tmp = df.drop(tmp.index)  # newdata = df-tmp -->without -L

                if tmp.shape[0] > 0:

                    if flag == True:
                        # fig = plt.figure(figsize=(8,6))

                        plt.subplot(223)
                        tmp['Tran_X_Ave'].plot()
                        plt.title(file[21:23] + '   Standard Field Tran_X_Ave')
                        plt.ylim(-35, 35)

                        plt.subplot(224)
                        tmp['Tran_Y_Ave'].plot()
                        plt.title(file[21:23] + '   Standard Field Tran_Y_Ave')
                        plt.ylim(-35, 35)
                        fig.subplots_adjust(hspace=0.4)
                    else:
                        fig = plt.figure(figsize=(12, 5))
                        plt.subplot(121)
                        tmp['Tran_X_Ave'].plot()
                        plt.title(file[21:23] + '   Standard Field Tran_X_Ave')
                        plt.ylim(-35, 35)

                        plt.subplot(122)
                        tmp['Tran_Y_Ave'].plot()
                        plt.title(file[21:23] + '   Standard Field Tran_Y_Ave')
                        plt.ylim(-35, 35)

                plt.savefig('Z:\\_DailyCheck\\PreAlign\\' + file[21:23], dpi=100, bbox_inches='tight')
                plt.close()
class PpcsAdo:
    def __init__(self):
        pass
    def get_rar_filename(self,dirxls):
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'

        filenamelist = []
        for root, dirs, files in os.walk(dirxls,
                                         False):  # root:所有目录名-->字符串 #dirs: root下的子目录名-->列表 #files：root下的文件名-->列表 # name.endswith(ext)-->文件名筛选
            for names in files:
                if names.endswith('.RAR') or names.endswith('.rar'):
                    filenamelist.append(root + '\\' + names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def unrar_latest_ChartDataXLS(self):
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'
        dirxls = r'Z:\_DailyCheck\outlook\Charting'
        filenamelist = self.get_rar_filename(dirxls)
        tmp = []
        for filename in filenamelist:
            print(time.ctime(os.stat(filename).st_mtime))
            print(time.localtime((int(os.stat(filename).st_mtime))))
            tmp.append([filename, time.localtime((int(os.stat(filename).st_mtime)))])
        tmp = pd.DataFrame(tmp)
        tmp = tmp.sort_values(by=1, ascending=True)
        tmp = tmp.iloc[-1, 0]
        tmp = 'Z:\\_DailyCheck\\outlook\\haozip\\haozipc e -y ' + tmp + ' -oZ:\\_DailyCheck\\outlook\\'
        os.system(tmp)
    def get_db_latest_date(self):
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'
        conn = win32com.client.Dispatch(r"ADODB.Connection")

        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)

        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs_name = ['CD_data', 'OL_ASML', 'OL_Nikon']
        # startdate = "'2018-04-27 06:00:00', 'YYYY-MM-DD HH:MM:SS'"
        # sql = " SELECT Dcoll_Time FROM " + rs_name[0] + "  where Dcoll_Time > format (" + startdate + " ) Order By Dcoll_Time desc "

        print('CD date is being extracted...........')
        sql = " SELECT Dcoll_Time FROM " + rs_name[0] + "  Order By Dcoll_Time asc "
        rs.Open(sql, conn, 1, 3)
        rs.MoveLast()
        CDdate = datetime.datetime.strptime(str(rs.Fields[0].Value)[:-6], '%Y-%m-%d %H:%M:%S')
        rs = None
        rs = win32com.client.Dispatch(r'ADODB.Recordset')

        print('ASML date is being extracted...........')
        sql = " SELECT Dcoll_Time FROM " + rs_name[1] + "  Order By Dcoll_Time asc "
        rs.Open(sql, conn, 1, 3)
        rs.MoveLast()
        ASMLdate = datetime.datetime.strptime(str(rs.Fields[0].Value)[:-6], '%Y-%m-%d %H:%M:%S')
        rs = None
        rs = win32com.client.Dispatch(r'ADODB.Recordset')

        print('Nikon date is being extracted...........')
        sql = " SELECT Dcoll_Time FROM " + rs_name[2] + "  Order By Dcoll_Time asc "
        rs.Open(sql, conn, 1, 3)
        rs.MoveLast()
        # NIKONdate = datetime.datetime.strptime(  str( rs.Fields[0].Value ).rstrip("+00:00"), '%Y-%m-%d %H:%M:%S')
        NIKONdate = datetime.datetime.strptime(str(rs.Fields[0].Value)[:-6], '%Y-%m-%d %H:%M:%S')

        rs = None

        # rs.Open('[' + rs_name[0] + ']', conn, 1, 3)

        # rs.MoveFirst()  #光标移到首条记录
        # rs.MoveNext()
        # rs.MoveLast()

        conn.close

        print('Latest Date in database: \n\n    CD:    ' + str(CDdate) + '\n\n    ASML:  ' + str(
            ASMLdate) + '\n\n    Nikon: ' + str(NIKONdate) + '\n')

        return CDdate, ASMLdate, NIKONdate
    def get_latest_chart_data(self,CDdate, ASMLdate, NIKONdate):  # from compiled CSV file
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'
        filefullpath = 'Z:\\_DailyCheck\\outlook\\ChartData.xls'

        print('CD data from CSV  is being extracted...........')
        cd_new = pd.read_excel(filefullpath, sheet_name='CD')
        cd_new['Dcoll Time'] = pd.to_datetime(cd_new['Dcoll Time'])
        cd_new = cd_new.sort_values(by="Dcoll Time", ascending=True)
        cd_new = cd_new[cd_new['Dcoll Time'] > CDdate]

        print('ASML data from CSV  is being extracted...........')
        asml_new = pd.read_excel(filefullpath, sheet_name='OL_ASML', skiprows=[1])
        asml_new['Dcoll Time'] = pd.to_datetime(asml_new['Dcoll Time'])
        asml_new = asml_new.sort_values(by="Dcoll Time", ascending=True)
        asml_new = asml_new[asml_new['Dcoll Time'] > ASMLdate]

        print('NIKON data from CSV  is being extracted...........')
        nikon_new = pd.read_excel(filefullpath, sheet_name='OL_NIKON', skiprows=[1])
        nikon_new['Dcoll Time'] = pd.to_datetime(nikon_new['Dcoll Time'])
        nikon_new = nikon_new.sort_values(by="Dcoll Time", ascending=True)
        nikon_new = nikon_new[nikon_new['Dcoll Time'] > NIKONdate]

        return cd_new, asml_new, nikon_new
    def update_db(self,cd_new, asml_new, nikon_new):
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'
        #################################################
        # https://zhidao.baidu.com/question/1832218251617721020.html
        import pytz
        tz = pytz.timezone("GMT")  # settime zone
        # datetime.datetime(2009, 2, 21, 15, 12, 33, tzinfo=tz)
        # datetime format of access:  pywintypes.datetime, with time zone information
        # datetime format of pandas:  pandas.tslib.Timestamp -->swith to datetime with time zone

        cd_new = cd_new.fillna(0)
        asml_new = asml_new.fillna(0)
        nikon_new = nikon_new.fillna(0)

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)

        rs_name = ['CD_data', 'OL_ASML', 'OL_Nikon']

        print('CD database is being updated................')
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(rs_name[0], conn, 1, 3)
        # rs.MoveLast()
        for i in range(cd_new.shape[0]):

            rs.AddNew()  # 添加一条新记录
            for j in range(cd_new.shape[1]):
                # print (i,j, cd_new.iloc[i,j])

                if j == 7:

                    tmp = cd_new.iloc[i, j]
                    tmp = datetime.datetime(tmp.year, tmp.month, tmp.day, tmp.hour, tmp.minute, tmp.second, tzinfo=tz)
                    rs.Fields.Item(
                        j).Value = tmp  # rs.Fields.Item(j).Value = cd_new.iloc[i,j].tz_localize("Asia/Shanghai")  #GMT
                else:
                    rs.Fields.Item(j).Value = cd_new.iloc[i, j]
            rs.Update()  # 更新
        print('CD database update is completed.    ' + str(cd_new.shape[0]) + ' rows have been inserted')
        rs = None

        print('ASML database is being updated................')
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(rs_name[1], conn, 1, 3)
        # rs.MoveLast()
        for i in range(asml_new.shape[0]):

            rs.AddNew()  # 添加一条新记录
            for j in range(asml_new.shape[1]):
                # print (i,j, asml_new.iloc[i,j])

                if j == 7:

                    tmp = asml_new.iloc[i, j]
                    tmp = datetime.datetime(tmp.year, tmp.month, tmp.day, tmp.hour, tmp.minute, tmp.second, tzinfo=tz)
                    rs.Fields.Item(
                        j).Value = tmp  # rs.Fields.Item(j).Value = cd_new.iloc[i,j].tz_localize("Asia/Shanghai")  #GMT
                else:
                    rs.Fields.Item(j).Value = asml_new.iloc[i, j]
            rs.Update()  # 更新
        print('asml database update is completed.    ' + str(asml_new.shape[0]) + ' rows have been inserted')
        rs = None

        print('NIKON database is being updated................')
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(rs_name[2], conn, 1, 3)
        # rs.MoveLast()
        for i in range(nikon_new.shape[0]):

            rs.AddNew()  # 添加一条新记录
            for j in range(nikon_new.shape[1]):
                # print (i,j, nikon_new.iloc[i,j])
                if j == 7:

                    tmp = nikon_new.iloc[i, j]
                    tmp = datetime.datetime(tmp.year, tmp.month, tmp.day, tmp.hour, tmp.minute, tmp.second, tzinfo=tz)
                    rs.Fields.Item(
                        j).Value = tmp  # rs.Fields.Item(j).Value = cd_new.iloc[i,j].tz_localize("Asia/Shanghai")  #GMT
                else:
                    rs.Fields.Item(j).Value = nikon_new.iloc[i, j]
            rs.Update()  # 更新
        print('Nikon database update is completed.    ' + str(nikon_new.shape[0]) + ' rows have been inserted')
        rs = None

        conn.close
    def MainFunction(self):
        CDdate, ASMLdate, NIKONdate = self.get_db_latest_date()
        self.unrar_latest_ChartDataXLS()
        cd_new, asml_new, nikon_new = self.get_latest_chart_data(CDdate, ASMLdate, NIKONdate)  # from compiled CSV file
        self.update_db(cd_new, asml_new, nikon_new)
class OvlOpt_FIA_LSA:
    def __init__(self):
        pass
    def extract_data(self):
        n = 25  # define time period for data extraction
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'

        enddate = datetime.datetime.now().date()
        startdate = enddate - datetime.timedelta(days=n)

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')

        sql = "SELECT * FROM OL_ASML WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"
        sql = "SELECT  \
              Proc_EqID, Dcoll_Time, \
              TranX_Optimum, TranY_Optimum,  ScalX_Optimum, ScalY_Optimum, \
              ORT_Optimum,  Wrot_Optimum, Mag_Optimum, Rot_Optimum,  ARMag_Optimum, ARRot_Optimum, \
              Tech,Layer \
              FROM OL_ASML WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"

        rs.Open(sql, conn, 1, 3)

        asml = []
        rs.MoveFirst()

        while True:
            if rs.EOF:
                break
            else:
                asml.append([rs.Fields.Item(i).Value for i in range(rs.Fields.Count)])
                rs.MoveNext()

        rs.close
        asml = pd.DataFrame(asml)

        sql = "SELECT * FROM OL_NIKON WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"
        sql = "SELECT  \
              Proc_EqID, Dcoll_Time, \
              OffsetX_Optimum, OffsetY_Optimum, ScalX_Optimum , ScalY_Optimum, \
              ORT_Optimum, Wrot_Optimum , Mag_Optimum , Rot_Optimum, Mag_Optimum , Rot_Optimum, \
              Tech, Layer \
              FROM OL_NIKON WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"
        rs.Open(sql, conn, 1, 3)

        nikon = []
        rs.MoveFirst()
        while True:
            if rs.EOF:
                break
            else:
                nikon.append([rs.Fields.Item(i).Value for i in range(rs.Fields.Count)])
                rs.MoveNext()
        rs.close
        nikon = pd.DataFrame(nikon)

        conn.close

        return asml, nikon
    def optvalue(self,asml, nikon):
        df = pd.concat([asml, nikon], axis=0).fillna(0)
        df[1] = [datetime.datetime.strptime(str(i)[0:-6], '%Y-%m-%d %H:%M:%S') for i in df[1]]

        df = df.reset_index().set_index(1)

        toollist = (
        'ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07', 'ALDI09', 'ALDI10', 'ALDI11', 'ALDI12', 'BLDI08', 'BLDI13',
        'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14', 'ALII15',
        'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'ALSIBJ', 'BLSIBK', 'BLSIBL', 'BLSIE1',
        'BLSIE2')

        # wb = xw.Book(r'p:\RoutineWork\ScriptSaving.xlsx')
        # sht = wb.sheets["OvlOpt"]

        for i, tool in enumerate(toollist):
            # print(i,"  ",toollist)

            # df = df.sort(['Dcoll Time'] , ascending=True)

            print(tool + ' is being updated......')

            tmp = df[df[0] == tool]
            if tmp.shape[0] > 0:

                if i < 11:

                    fig = plt.figure(figsize=(20, 16))

                    ax1 = plt.subplot(521)
                    # tmp[2].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[2].plot(marker='o', linestyle=':', color='r',
                                legend=False)  # linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    plt.ylim(-0.1, 0.1)
                    plt.title(tool + '  ' + "Optimum Tran-X")
                    ax1.yaxis.grid(True)
                    ax1.xaxis.grid(True)
                    plt.ylim(-0.04, 0.04)

                    ax2 = plt.subplot(522)
                    # tmp[3].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[3].plot(marker='o', linestyle=':', color='r', legend=False)
                    ax2.yaxis.grid(True)
                    ax2.xaxis.grid(True)
                    plt.title(tool + '  ' + "Optimum Tran-Y")
                    plt.ylim(-0.04, 0.04)

                    ax3 = plt.subplot(523)
                    # tmp[4].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[4].plot(marker='o', linestyle=':', color='r', legend=False)
                    plt.title(tool + '  ' + 'W.Expan.X_Optimum')
                    ax3.yaxis.grid(True)
                    ax3.xaxis.grid(True)
                    plt.ylim(-0.5, 0.5)

                    ax4 = plt.subplot(524)
                    # tmp[5].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[5].plot(marker='o', linestyle=':', color='r', legend=False)

                    plt.title(tool + '  ' + 'W.Expan.Y_Optimum')
                    ax4.yaxis.grid(True)
                    ax4.xaxis.grid(True)
                    plt.ylim(-0.5, 0.5)

                    ax5 = plt.subplot(525)
                    # tmp[6].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[6].plot(marker='o', linestyle=':', color='r', legend=False)
                    plt.title(tool + '  ' + 'NonOrtho_Optimum')
                    ax5.yaxis.grid(True)
                    ax5.xaxis.grid(True)
                    plt.ylim(-0.5, 0.5)

                    ax6 = plt.subplot(526)
                    # tmp[7].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[7].plot(marker='o', linestyle=':', color='r', legend=False)
                    ax6.yaxis.grid(True)
                    ax6.xaxis.grid(True)
                    plt.title(tool + '  ' + 'W.Rotation_Optimum')
                    plt.ylim(-0.5, 0.5)

                    ax7 = plt.subplot(527)
                    # tmp[8].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[8].plot(marker='o', linestyle=':', color='r', legend=False)
                    ax7.yaxis.grid(True)
                    ax7.xaxis.grid(True)
                    plt.title(tool + '  ' + 'Shot Magnification_Optimum')
                    plt.ylim(-3, 3)

                    ax8 = plt.subplot(528)

                    # tmp[9].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[9].plot(marker='o', linestyle=':', color='r', legend=False)
                    ax8.yaxis.grid(True)
                    ax8.xaxis.grid(True)
                    plt.title(tool + '  ' + 'Shot Rotation_Optimum')
                    plt.ylim(-3, 3)

                    ax9 = plt.subplot(529)
                    # tmp[10].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[10].plot(marker='o', linestyle=':', color='r', legend=False)
                    ax9.yaxis.grid(True)
                    ax9.xaxis.grid(True)
                    plt.title(tool + '  ' + 'Shot AsymMag_Optimum')
                    plt.ylim(-3, 3)

                    ax10 = plt.subplot(5, 2, 10)

                    # tmp[11].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                    tmp[11].plot(marker='o', linestyle=':', color='r', legend=False)
                    ax10.yaxis.grid(True)
                    ax10.xaxis.grid(True)
                    plt.title(tool + '  ' + 'Shot AsymRot_Optimum')
                    plt.ylim(-3, 3)

                    fig.subplots_adjust(hspace=0.5)

                    plt.savefig('z:\\_DailyCheck\\OptOvl_Others\\OvlPicture\\' + tool, dpi=100, bbox_inches='tight')

                    # sht.pictures.add(fig, name=(tool + '_Optimum Value'), update=True,
                    #                 left=sht.range('A2').left, top=sht.range('A2').top)

                    plt.clf()  # 清图。
                    plt.cla()  # 清坐标轴。
                    plt.close()  # 关窗口
                    gc.collect()

                else:
                    bak = tmp.copy()

                    t1 = tmp[tmp[12].str.contains("1") | tmp[12].str.contains("25") | tmp[12].str.contains("52")]
                    t1 = t1[t1[13].str.contains("PT") | t1[13].str.contains("TB") | t1[13].str.contains("HV") | t1[
                        13].str.contains("NX") | t1[13].str.contains("PX") | t1[13].str.contains("DN") | t1[
                                13].str.contains("DP")]

                    t2 = tmp[tmp[13].str.contains("A1") | tmp[13].str.contains("A2") | tmp[13].str.contains("AT") | tmp[
                        13].str.contains("TT") | tmp[13].str.contains("W1") | tmp[13].str.contains("W2") | tmp[
                                 13].str.contains("WT") | tmp[13].str.contains("CT")]

                    tmp = pd.concat([t1, t2], axis=0).sort_index()

                    if tmp.shape[0] > 0:
                        fig = plt.figure(figsize=(20, 16))

                        ax1 = plt.subplot(421)
                        # tmp[2].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[2].plot(marker='o', linestyle=':', color='r',
                                    legend=False)  # linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        plt.ylim(-0.1, 0.1)
                        plt.title(tool + '  ' + "Optimum Tran-X")
                        ax1.yaxis.grid(True)
                        ax1.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.04, 0.04)
                        else:
                            plt.ylim(-0.08, 0.08)

                        ax2 = plt.subplot(422)
                        # tmp[3].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[3].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax2.yaxis.grid(True)
                        ax2.xaxis.grid(True)
                        plt.title(tool + '  ' + "Optimum Tran-Y")
                        if i < 11:
                            plt.ylim(-0.04, 0.04)
                        else:
                            plt.ylim(-0.08, 0.08)

                        ax3 = plt.subplot(423)
                        # tmp[4].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[4].plot(marker='o', linestyle=':', color='r', legend=False)
                        plt.title(tool + '  ' + 'W.Expan.X_Optimum')
                        ax3.yaxis.grid(True)
                        ax3.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax4 = plt.subplot(424)
                        # tmp[5].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[5].plot(marker='o', linestyle=':', color='r', legend=False)

                        plt.title(tool + '  ' + 'W.Expan.Y_Optimum')
                        ax4.yaxis.grid(True)
                        ax4.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax5 = plt.subplot(425)
                        # tmp[6].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[6].plot(marker='o', linestyle=':', color='r', legend=False)
                        plt.title(tool + '  ' + 'NonOrtho_Optimum')
                        ax5.yaxis.grid(True)
                        ax5.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax6 = plt.subplot(426)
                        # tmp[7].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[7].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax6.yaxis.grid(True)
                        ax6.xaxis.grid(True)
                        plt.title(tool + '  ' + 'W.Rotation_Optimum')
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax7 = plt.subplot(427)
                        # tmp[8].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[8].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax7.yaxis.grid(True)
                        ax7.xaxis.grid(True)
                        plt.title(tool + '  ' + 'Shot Magnification_Optimum')
                        if i < 11:
                            plt.ylim(-3, 3)
                        else:
                            plt.ylim(-6, 6)

                        ax8 = plt.subplot(428)

                        # tmp[9].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[9].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax8.yaxis.grid(True)
                        ax8.xaxis.grid(True)
                        plt.title(tool + '  ' + 'Shot Rotation_Optimum')
                        if i < 11:
                            plt.ylim(-3, 3)
                        else:
                            plt.ylim(-6, 6)
                        fig.subplots_adjust(hspace=0.5)

                        plt.savefig('z:\\_DailyCheck\\OptOvl_Others\\OvlPicture\\FIA_' + tool, dpi=100,
                                    bbox_inches='tight')

                        # sht.pictures.add(fig, name=(tool + '_Optimum Value'), update=True,
                        #                 left=sht.range('A2').left, top=sht.range('A2').top)

                        plt.clf()  # 清图。
                        plt.cla()  # 清坐标轴。
                        plt.close()  # 关窗口
                        gc.collect()

                    print(i, ',   ', tool, ',   ', len(set(bak.index) - set(tmp.index)))

                    if len(set(bak.index) - set(tmp.index)) > 0:
                        tmp = bak.loc[list(set(bak.index) - set(tmp.index))]
                        tmp = tmp.sort_index()
                        # if tmp.shape[0] > 0:
                        fig = plt.figure(figsize=(20, 16))

                        ax1 = plt.subplot(421)
                        # tmp[2].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[2].plot(marker='o', linestyle=':', color='r',
                                    legend=False)  # linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        plt.ylim(-0.1, 0.1)
                        plt.title(tool + '  ' + "Optimum Tran-X")
                        ax1.yaxis.grid(True)
                        ax1.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.04, 0.04)
                        else:
                            plt.ylim(-0.08, 0.08)

                        ax2 = plt.subplot(422)
                        # tmp[3].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[3].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax2.yaxis.grid(True)
                        ax2.xaxis.grid(True)
                        plt.title(tool + '  ' + "Optimum Tran-Y")
                        if i < 11:
                            plt.ylim(-0.04, 0.04)
                        else:
                            plt.ylim(-0.08, 0.08)

                        ax3 = plt.subplot(423)
                        # tmp[4].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[4].plot(marker='o', linestyle=':', color='r', legend=False)
                        plt.title(tool + '  ' + 'W.Expan.X_Optimum')
                        ax3.yaxis.grid(True)
                        ax3.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax4 = plt.subplot(424)
                        # tmp[5].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[5].plot(marker='o', linestyle=':', color='r', legend=False)

                        plt.title(tool + '  ' + 'W.Expan.Y_Optimum')
                        ax4.yaxis.grid(True)
                        ax4.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax5 = plt.subplot(425)
                        # tmp[6].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[6].plot(marker='o', linestyle=':', color='r', legend=False)
                        plt.title(tool + '  ' + 'NonOrtho_Optimum')
                        ax5.yaxis.grid(True)
                        ax5.xaxis.grid(True)
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax6 = plt.subplot(426)
                        # tmp[7].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[7].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax6.yaxis.grid(True)
                        ax6.xaxis.grid(True)
                        plt.title(tool + '  ' + 'W.Rotation_Optimum')
                        if i < 11:
                            plt.ylim(-0.6, 0.6)
                        else:
                            plt.ylim(-1, 1)

                        ax7 = plt.subplot(427)
                        # tmp[8].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[8].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax7.yaxis.grid(True)
                        ax7.xaxis.grid(True)
                        plt.title(tool + '  ' + 'Shot Magnification_Optimum')
                        if i < 11:
                            plt.ylim(-3, 3)
                        else:
                            plt.ylim(-6, 6)

                        ax8 = plt.subplot(428)

                        # tmp[9].plot(linewidth = '0.1',kind='line',marker='o',linestyle=':',color='r',legend=False)
                        tmp[9].plot(marker='o', linestyle=':', color='r', legend=False)
                        ax8.yaxis.grid(True)
                        ax8.xaxis.grid(True)
                        plt.title(tool + '  ' + 'Shot Rotation_Optimum')
                        if i < 11:
                            plt.ylim(-3, 3)
                        else:
                            plt.ylim(-6, 6)
                        fig.subplots_adjust(hspace=0.5)

                        plt.savefig('z:\\_DailyCheck\\OptOvl_Others\\OvlPicture\\LSA_' + tool, dpi=100,
                                    bbox_inches='tight')

                        # sht.pictures.add(fig, name=(tool + '_Optimum Value'), update=True,
                        #                 left=sht.range('A2').left, top=sht.range('A2').top)

                        plt.clf()  # 清图。
                        plt.cla()  # 清坐标轴。
                        plt.close()  # 关窗口
                        gc.collect()
    def MainFunction(self):
        asml,nikon = self.extract_data()
        self.optvalue(asml,nikon)
class CDU_QC_IMAGE_CD:
    def __init__(self):
        pass
    def YE_folderlist(self,begin, end):  # n天内的图片目录
        print('=== folder lists of YE images server being extracted.........===')
        folderlist = []
        full = []
        for n in range(begin, end):
            datestr = (datetime.datetime.today() - datetime.timedelta(days=n)).strftime("%Y%m%d")
            print(datestr)

            foldername = 'D:\\ftpimages\\' + datestr
            foldername = '\\\\10.4.72.182\\d\\ftpimages\\' + datestr

            for root, dirs, files in os.walk(foldername, False):
                for name in dirs:
                    full.append(root + '\\' + name + '\\')
                    if '2LXXX' in name:
                        folderlist.append(root + '\\' + name + '\\')
        pd.DataFrame(folderlist).to_csv('p:\\temp\\nikonshot\\folderlist.csv', mode='a', header=False, index=False)
        # pd.DataFrame(full).to_csv('p:\\temp\\nikonshot\\fulllist.csv', mode='a', header=False, index=False)

        tmp = pd.read_csv('p:\\temp\\nikonshot\\folderlist.csv', header=None)
        pd.DataFrame(tmp[0].unique()).to_csv('p:\\temp\\nikonshot\\folderlist.csv', index=False)

        # tmp = pd.read_csv('p:\\temp\\nikonshot\\fulllist.csv', header=None)
        # pd.DataFrame(tmp[0].unique()).to_csv('p:\\temp\\nikonshot\\fulllist.csv', index=False)

        return folderlist
    def complete_collection(self):  # 已完成合并的图片名
        finish_merge = []
        for root, dirs, files in os.walk('p:\\temp\\nikonshot\\transfer', False):
            for name in files:
                finish_merge.append(name)
        return finish_merge
    def cd_measure_done(self):  # list image folders completed measurment
        df = pd.read_csv('p:\\temp\\nikonshot\\cd.csv')
        tmp = [i[0:-19] for i in list(df['Path'])]
        imgdone = set(tmp)
        return imgdone
    def folderFinish(self,folder, finish_merge):  # 判断YE_folderlist()中已完成贴图的目录
        flag = True
        tmp = folder.split('\\')[4]
        for i in finish_merge:
            if tmp in i:
                flag = False
        return flag
    def nikon_merge_image(self,folder):  # 图片合并程序
        # toImage = Image.new('RGBA',(3072,4096))
        toImage = Image.new('RGB', (3172, 2372), (255, 255, 255))

        line1 = Image.new('RGB', (3172, 100), (255, 255, 0))
        line2 = Image.new('RGB', (100, 2372), (255, 255, 0))

        toImage.paste(line1, (0, 1136))

        toImage.paste(line2, (1536, 0))

        # insert 4,5

        img1 = Image.open(folder + '_M0023-01MS.JPEG')
        toImage.paste(img1, (0, 0))
        img1 = Image.open(folder + '_M0022-01MS.JPEG')
        toImage.paste(img1, (1024, 0))
        img1 = Image.open(folder + '_M0019-01MS.JPEG')
        toImage.paste(img1, (512, 312))
        img1 = Image.open(folder + '_M0020-01MS.JPEG')
        toImage.paste(img1, (0, 624))
        img1 = Image.open(folder + '_M0021-01MS.JPEG')
        toImage.paste(img1, (1024, 624))

        # insert 4,3 vertical
        img1 = Image.open(folder + '_M0027-01MS.JPEG')
        toImage.paste(img1, (1636, 0))
        img1 = Image.open(folder + '_M0026-01MS.JPEG')
        toImage.paste(img1, (2660, 0))
        # toImage.paste(img1,(2298,512))
        img1 = Image.open(folder + '_M0024-01MS.JPEG')
        toImage.paste(img1, (1636, 624))
        img1 = Image.open(folder + '_M0025-01MS.JPEG')
        toImage.paste(img1, (2660, 624))

        # insert 4,4 vertical
        img1 = Image.open(folder + '_M0013-01MS.JPEG')
        toImage.paste(img1, (0, 1236))
        img1 = Image.open(folder + '_M0012-01MS.JPEG')
        toImage.paste(img1, (1024, 1236))
        img1 = Image.open(folder + '_M0005-01MS.JPEG')
        toImage.paste(img1, (512, 1548))
        img1 = Image.open(folder + '_M0010-01MS.JPEG')
        toImage.paste(img1, (0, 1860))
        img1 = Image.open(folder + '_M0011-01MS.JPEG')
        toImage.paste(img1, (1024, 1860))

        # insert 4,4 horzintal
        img1 = Image.open(folder + '_M0018-01MS.JPEG')
        toImage.paste(img1.transpose(Image.ROTATE_90), (1636, 1236))
        img1 = Image.open(folder + '_M0017-01MS.JPEG')
        toImage.paste(img1.transpose(Image.ROTATE_90), (2660, 1236))
        img1 = Image.open(folder + '_M0014-01MS.JPEG')
        toImage.paste(img1.transpose(Image.ROTATE_90), (2148, 1548))
        img1 = Image.open(folder + '_M0015-01MS.JPEG')
        toImage.paste(img1.transpose(Image.ROTATE_90), (1636, 1860))
        img1 = Image.open(folder + '_M0016-01MS.JPEG')
        toImage.paste(img1.transpose(Image.ROTATE_90), (2660, 1860))

        setfont = ImageFont.truetype('simsun.ttc', 48)
        fillcolor = (0, 0, 0)
        draw = ImageDraw.Draw(toImage)

        draw.text((650, 50), 'Shot(4,5)', fill=fillcolor, font=setfont)
        draw.text((512, 100), 'Norminal Focus +0.2um', fill=fillcolor, font=setfont)
        draw.text((588, 150), 'B1,B2,B8,BH,E1', fill=fillcolor, font=setfont)

        draw.text((2286, 50), 'Shot(4,3)', fill=fillcolor, font=setfont)
        draw.text((2148, 100), 'Norminal Focus -0.2um', fill=fillcolor, font=setfont)
        draw.text((2224, 150), 'B1,B2,B8,BH,E1', fill=fillcolor, font=setfont)

        draw.text((660, 1286), 'Ctr Shot', fill=fillcolor, font=setfont)
        draw.text((586, 1386), 'Norminal Focus', fill=fillcolor, font=setfont)
        draw.text((650, 1336), 'Shot(4,4)', fill=fillcolor, font=setfont)

        draw.text((2296, 1286), 'Ctr Shot', fill=fillcolor, font=setfont)
        draw.text((2222, 1386), 'Norminal Focus', fill=fillcolor, font=setfont)
        draw.text((2286, 1336), 'Shot(4,4)', fill=fillcolor, font=setfont)

        tmp = folder.split('\\')
        toImage.save('P:\\temp\\NikonShot\\transfer\\' + tmp[6] + '_' + tmp[7] + '.jpg')

        # r,g,b,a = toImage.split()  # toImage = Image.merge("RGB",(r,g,b))

        # toImage.save('P:\\temp\\NikonShot\\transfer\\' + tmp[6] + '_' + tmp[7] + '.png')
    def calculate_cd(self,img, threshold=0.25, differential=7):
        img_avg = np.mean(img, axis=0)  # 按列计算
        # newarr_avg = np.mean(newarr,axis=0)#按列计算

        # 求导
        # dxdy = np.delete(img_avg,0) - np.delete(img_avg,-1)

        tmp1 = img_avg
        tmp2 = img_avg
        for i in range(differential - 1):
            tmp1 = np.delete(tmp1, 0)
            tmp2 = np.delete(tmp2, -1)
        dxdy = (tmp1 - tmp2) / (differential - 1)

        # 显示信号曲线和微分曲线
        # plt.plot(img_avg)
        # plt.show()
        # plt.plot(dxdy)
        # plt.show()
        # plt.plot(dxdyn)
        # plt.show()

        df = pd.DataFrame(dxdy)
        # df_left = df.iloc[0:int (df.shape[0] / 2)].sort([0],ascending=True)
        df_left = df.iloc[0:int(df.shape[0] / 2)].sort_values(by=[0], ascending=True)

        # df_right = df.iloc[int (df.shape[0] / 2): ].sort([0],ascending=True)
        df_right = df.iloc[int(df.shape[0] / 2):].sort_values(by=[0], ascending=True)

        a = df_left.index[-1]
        b = df_left.index[0]
        c = df_right.index[-1]
        d = df_right.index[0]

        cd1 = df[0][a]
        cd2 = df[0][b]
        cd3 = df[0][c]
        cd4 = df[0][d]

        tmp = df[30:a + 1]
        # print(tmp)
        tmp = list(tmp.sort_index(ascending=False)[0])
        for i, value in enumerate(tmp):
            if value < threshold * cd1:
                x1 = a - i
                y1 = tmp[i]
                x2 = a - (i - 1)
                y2 = tmp[i - 1]
                # print(x1,y1,x2,y2,threshold*cd1)
                k1 = (y2 - y1) / (x2 - x1)
                k2 = y2 - k1 * x2
                ta = (threshold * cd1 - k2) / k1
                # print('ta = ',ta)
                break

        tmp = df[b:int((c - b) / 2) + b]
        # print(tmp)
        tmp = list(tmp[0])
        for i, value in enumerate(tmp):

            if value > threshold * cd2:
                x1 = b + i
                y1 = tmp[i]
                x2 = b + (i - 1)
                y2 = tmp[i - 1]
                # print(x1,y1,x2,y2,threshold*cd2)
                k1 = (y2 - y1) / (x2 - x1)
                k2 = y2 - k1 * x2
                tb = (threshold * cd2 - k2) / k1
                # print('tb = ',tb)
                break

        tmp = df[int((c - b) / 2) + b:c + 1]
        # print(tmp)
        tmp = list(tmp.sort_index(ascending=False)[0])
        for i, value in enumerate(tmp):
            if value < threshold * cd3:
                x1 = c - i
                y1 = tmp[i]
                x2 = c - (i - 1)
                y2 = tmp[i - 1]
                # print(x1,y1,x2,y2,threshold*cd3)
                k1 = (y2 - y1) / (x2 - x1)
                k2 = y2 - k1 * x2
                tc = (threshold * cd3 - k2) / k1
                # print('tc = ',tc)
                break

        tmp = df[d:350]
        # print(tmp)
        tmp = list(tmp[0])
        for i, value in enumerate(tmp):

            if value > threshold * cd4:
                x1 = d + i
                y1 = tmp[i]
                x2 = d + (i - 1)
                y2 = tmp[i - 1]
                # print(x1,y1,x2,y2,threshold*cd4)
                k1 = (y2 - y1) / (x2 - x1)
                k2 = y2 - k1 * x2
                td = (threshold * cd4 - k2) / k1
                # print('td = ',td)
                break

        line_cd = d - a
        space_cd = c - b
        left_boundary = b - a
        right_boundary = d - c

        try:
            line_cd1 = td - ta
        except:
            line_cd1 = 888

        try:
            space_cd1 = tc - tb
        except:
            space_cd1 = 888

        try:
            left_boundary1 = tb - ta
        except:
            left_boundary1 = 888

        try:
            right_boundary1 = td - tc
        except:
            right_boundary1 = 888

        # print(cd1,cd2,cd3,cd4)

        # print(a,b,c,d)
        # print(line_cd,space_cd,left_boundary,right_boundary)
        # print(ta,tb,tc,td)
        # print(line_cd1,space_cd1,left_boundary1,right_boundary1)

        cddata = [line_cd, space_cd, left_boundary, right_boundary, line_cd1, space_cd1, left_boundary1,
                  right_boundary1]

        return cddata
    def moving_average(self,newarr, n):
        # 图片数据平坦化
        # n=13

        newarr = newarr[0]  # 调用的newarr是个list,内容分别是去除测试虚线的图像矩阵和原始数据
        a = newarr.shape[0]
        b = newarr.shape[1] - n + 1

        # 零值矩阵
        img = np.zeros([a, b], dtype='int32')  # 导出的图像矩阵数据格式为int8，若零矩阵格式仍为int8，后续移动平均计算会溢出

        # 移动平均，只累加，不平均
        tmp1 = [n - i - 2 for i in range(n - 1)]
        k = newarr.shape[1]
        for i in range(n):
            tmp2 = tmp1.copy()
            if i == 0:
                pass
            else:
                for j in range(i):
                    tmp2[j] = k - j - 1

            img = img + np.delete(newarr, tmp2, axis=1)

        # 检查图片，正常步骤省
        img = img / n * 1.0
        return img
    def read_img(self,picture_path):
        tmp_img = Image.open(picture_path)
        # tmp_img.show() #显示读入的图片

        region = tmp_img.crop((60, 136, 452, 352))  # 图片尺寸512*512，截取部分选择一块区域
        # region = tmp_img.crop((143,136,460,352)) #选择一块区域
        # region.show()

        arr = np.array(region.convert('L'))

        # 图片上有点状线，自Y=0开始，间隔8个像素，需去除
        tmp1 = [i + 8 for i in range(8)]
        tmp2 = [i for i in range(13)]
        tmp3 = []
        for i in tmp2:
            for j in tmp1:
                tmp3.append(i * 16 + j)
        newarr = np.delete(arr, tmp3, axis=0)  # axia=0 删除X轴方向，axis=1 删除Y轴方向
        return newarr, arr
    def nikon_image_cd(self,folderlist, finish_merge, imgdone):

        count = 0
        filename = {'_M0005-01MS.JPEG', '_M0010-01MS.JPEG', '_M0011-01MS.JPEG', '_M0012-01MS.JPEG', '_M0013-01MS.JPEG',
                    '_M0014-01MS.JPEG', '_M0015-01MS.JPEG', '_M0016-01MS.JPEG', '_M0017-01MS.JPEG', '_M0018-01MS.JPEG',
                    '_M0019-01MS.JPEG', '_M0020-01MS.JPEG', '_M0021-01MS.JPEG', '_M0022-01MS.JPEG', '_M0023-01MS.JPEG',
                    '_M0024-01MS.JPEG', '_M0025-01MS.JPEG', '_M0026-01MS.JPEG', '_M0027-01MS.JPEG'}

        cdfile = ['_M0005-01MS.JPEG', '_M0010-01MS.JPEG', '_M0011-01MS.JPEG', '_M0012-01MS.JPEG', '_M0013-01MS.JPEG',
                  '_M0014-01MS.JPEG', '_M0015-01MS.JPEG', '_M0016-01MS.JPEG', '_M0017-01MS.JPEG', '_M0018-01MS.JPEG']

        direction = {'_M0005-01MS.JPEG': 'Vertical', '_M0010-01MS.JPEG': 'Vertical', '_M0011-01MS.JPEG': 'Vertical',
                     '_M0012-01MS.JPEG': 'Vertical', '_M0013-01MS.JPEG': 'Vertical', '_M0014-01MS.JPEG': 'Horizontal',
                     '_M0015-01MS.JPEG': 'Horizontal', '_M0016-01MS.JPEG': 'Horizontal',
                     '_M0017-01MS.JPEG': 'Horizontal', '_M0018-01MS.JPEG': 'Horizontal'}

        csv_output = []

        # Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon#Nikon
        # 判断目录中文件是否全==========================================
        nikonfolderlist = [tmp for tmp in folderlist if '2LXXXSCD' in tmp]
        for folder in nikonfolderlist:
            print(folder)
            imgnameset = []
            for root, dirs, files in os.walk(folder, False):
                for name in files:
                    imgnameset.append(name[3:].upper())  # 文件名出去除片号,全部大写

            imgnameset = set(imgnameset)  # 列表转集合
            imgflag = filename.issubset(imgnameset)  # 判断是否是子集，即文件是否全
            wfrno = name[0:3]

            # print(imgnameset)

            # Merge picture===========================================
            folderflag = self.folderFinish(folder, finish_merge)  # 判断YE目录中图片是否已处理
            if folderflag == True and imgflag == True:
                pass
                folder1 = folder + wfrno  # folder名改为目录+圆片号，也将用于cd
                self.nikon_merge_image(folder1)

            # check CD==============================================================================

            if folder not in imgdone and imgflag == True:
                for file in cdfile:

                    count += 1
                    folder1 = folder + wfrno  # 重新调用，因调试中Merge Picture不做，不被定义
                    picture_path = folder1 + file
                    print(picture_path)
                    newarr = self.read_img(picture_path)  # 返回的newarr是个list,内容分别是去除测试虚线的图像矩阵和原始数据
                    # newarr[0]供后续移动平均，CD计算
                    # newarr[1]供计算在线实际CD
                    # calculate real cd
                    realcd = pd.DataFrame(newarr[1])
                    # realcd = realcd.describe().T.sort(['std'],ascending = False)
                    realcd = realcd.describe().T.sort_values(by=['std'], ascending=False)
                    realcd = abs(realcd.index[0] - realcd.index[1])

                    n = 7
                    img = self.moving_average(newarr, n)  # 函数中调用newarr[0]处理数据
                    cd_data = self.calculate_cd(img, threshold=0.25, differential=7)

                    cd_data.append(realcd)
                    cd_data.append(folder1 + file)
                    # cd_data.append ( folder.split("\\")[4].split('_')[0] + ' '  + folder.split("\\")[4].split('_')[1] )
                    cd_data.append(folder.split("\\")[7].split('_')[0] + ' ' + folder.split("\\")[7].split('_')[1])
                    cd_data.append(folder.split('_')[3] + '.1')
                    cd_data.append(direction[file])
                    print(cd_data)
                    csv_output.append(cd_data)
                    if count > 5:
                        # break
                        pass  # sys.exit(0)
            if count > 5:
                pass  # break

        tmp = pd.DataFrame(csv_output)
        tmp.to_csv('p:\\temp\\nikonshot\\cd.csv', mode='a', header=False, index=False)
    def asml_merge_image(self,folder):  # 图片合并程序
        print(folder)
        # toImage = Image.new('RGBA',(3072,4096))
        toImage = Image.new('RGB', (4096, 1044), (255, 255, 255))

        # insert within wafer 2,3,4,5,6,7,8,9

        img1 = Image.open(folder + '_M0002-01MS.JPEG')
        toImage.paste(img1, (0, 0))
        img1 = Image.open(folder + '_M0003-01MS.JPEG')
        toImage.paste(img1, (512, 0))
        img1 = Image.open(folder + '_M0004-01MS.JPEG')
        toImage.paste(img1, (1024, 0))
        img1 = Image.open(folder + '_M0005-01MS.JPEG')
        toImage.paste(img1, (1536, 0))
        img1 = Image.open(folder + '_M0006-01MS.JPEG')
        toImage.paste(img1, (2048, 0))
        img1 = Image.open(folder + '_M0007-01MS.JPEG')
        toImage.paste(img1, (2560, 0))
        img1 = Image.open(folder + '_M0008-01MS.JPEG')
        toImage.paste(img1, (3072, 0))
        img1 = Image.open(folder + '_M0009-01MS.JPEG')
        toImage.paste(img1, (3584, 0))

        # insert within shot 1,10,11,12,13
        img1 = Image.open(folder + '_M0001-01MS.JPEG')
        toImage.paste(img1, (1536, 532))
        img1 = Image.open(folder + '_M0010-01MS.JPEG')
        toImage.paste(img1, (2048, 532))
        img1 = Image.open(folder + '_M0011-01MS.JPEG')
        toImage.paste(img1, (2560, 532))
        img1 = Image.open(folder + '_M0012-01MS.JPEG')
        toImage.paste(img1, (3072, 532))
        img1 = Image.open(folder + '_M0013-01MS.JPEG')
        toImage.paste(img1, (3584, 532))

        setfont = ImageFont.truetype('simsun.ttc', 72)
        fillcolor = (0, 0, 0)
        draw = ImageDraw.Draw(toImage)

        draw.text((10, 532), 'Row-1: center L-Bar within wafer', fill=fillcolor, font=setfont)
        draw.text((10, 632), 'Row-2: L-Bar within shot-->Ctr,LL,LR,UL,UR', fill=fillcolor, font=setfont)

        tmp = folder.split('\\')
        # toImage.save('p:\\temp\\NikonShot\\transfer\\' + tmp[3] + '_' + tmp[4] + '.jpg')
        toImage.save('p:\\temp\\NikonShot\\transfer\\' + tmp[6] + '_' + tmp[7] + '.jpg')

    def sepr602_read_img(self,picture_path):
        tmp_img = Image.open(picture_path)
        # tmp_img.show() #显示读入的图片

        region = tmp_img.crop((90, 0, 422, 55))  # 图片尺寸512*512，截取部分选择一块区域

        # region.show()

        arr = np.array(region.convert('L'))
        newarr = arr

        # 图片上十字线，自Y=0开始，间隔8个像素，需去除

        return newarr, arr
    def asml_image_cd(self,folderlist, finish_merge, imgdone):

        count = 0
        filename = {'_M0001-01MS.JPEG', '_M0002-01MS.JPEG', '_M0003-01MS.JPEG', '_M0004-01MS.JPEG', '_M0005-01MS.JPEG',
                    '_M0006-01MS.JPEG', '_M0007-01MS.JPEG', '_M0008-01MS.JPEG', '_M0009-01MS.JPEG', '_M0010-01MS.JPEG',
                    '_M0011-01MS.JPEG', '_M0012-01MS.JPEG', '_M0013-01MS.JPEG'}

        cdfile = ['_M0001-01MS.JPEG', '_M0010-01MS.JPEG', '_M0011-01MS.JPEG', '_M0012-01MS.JPEG', '_M0013-01MS.JPEG']

        direction = {'_M0001-01MS.JPEG': 'Vertical', '_M0010-01MS.JPEG': 'Vertical', '_M0011-01MS.JPEG': 'Vertical',
                     '_M0012-01MS.JPEG': 'Vertical', '_M0013-01MS.JPEG': 'Vertical'}

        csv_output = []

        # ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML#ASML
        # 判断目录中文件是否全==========================================
        asmlfolderlist = [tmp for tmp in folderlist if (('2LXXXUCD' in tmp) or ('2LXXXVCD' in tmp))]
        for folder in asmlfolderlist:
            print(folder)
            imgnameset = []
            for root, dirs, files in os.walk(folder, False):
                for name in files:
                    imgnameset.append(name[3:].upper())  # 文件名出去除片号,全部大写

            imgnameset = set(imgnameset)  # 列表转集合
            imgflag = filename.issubset(imgnameset)  # 判断是否是子集，即文件是否全
            wfrno = name[0:3]

            # print(imgnameset)

            # Merge picture===========================================
            folderflag = self.folderFinish(folder, finish_merge)  # 判断YE目录中图片是否已处理
            if folderflag == True and imgflag == True:
                pass
                folder1 = folder + wfrno  # folder名改为目录+圆片号，也将用于cd
                self.asml_merge_image(folder=folder1)

            # check CD==============================================================================

            if folder not in imgdone and imgflag == True:
                if 'UCD' in folder:  # UV135
                    for file in cdfile:

                        count += 1
                        folder1 = folder + wfrno  # 重新调用，因调试中Merge Picture不做，不被定义
                        picture_path = folder1 + file
                        print(picture_path)
                        newarr = self.read_img(picture_path)  # 返回的newarr是个list,内容分别是去除测试虚线的图像矩阵和原始数据
                        # newarr[0]供后续移动平均，CD计算
                        # newarr[1]供计算在线实际CD
                        # calculate real cd
                        realcd = pd.DataFrame(newarr[1])
                        realcd = realcd.describe().T.sort_values(by=['std'], ascending=False)
                        realcd = abs(realcd.index[0] - realcd.index[1])

                        n = 7
                        img = self.moving_average(newarr, n)  # 函数中调用newarr[0]处理数据
                        cd_data = self.calculate_cd(img, threshold=0.25, differential=7)

                        cd_data.append(realcd)
                        cd_data.append(folder1 + file)
                        cd_data.append(folder.split("\\")[7].split('_')[0] + ' ' + folder.split("\\")[7].split('_')[1])
                        cd_data.append(folder.split('_')[3] + '.1')
                        cd_data.append(direction[file])
                        print(cd_data)
                        csv_output.append(cd_data)
                        if count > 10:
                            # break
                            pass  # sys.exit(0)


                else:
                    for file in cdfile:  # SEPR602

                        count += 1
                        folder1 = folder + wfrno  # 重新调用，因调试中Merge Picture不做，不被定义
                        picture_path = folder1 + file
                        print(picture_path)
                        # multipoint的十字线无法准确去除
                        newarr = self.sepr602_read_img(picture_path)  # 返回的newarr是个list,newarr[0]和nearr[1]内容相同，取图片最上部分

                        # assign real cd
                        realcd = 888

                        n = 7
                        img = self.moving_average(newarr, n)  # 函数中调用newarr[0]处理数据
                        cd_data = self.calculate_cd(img, threshold=0.25, differential=7)

                        cd_data.append(realcd)
                        cd_data.append(folder1 + file)
                        cd_data.append(folder.split("\\")[7].split('_')[0] + ' ' + folder.split("\\")[7].split('_')[1])
                        cd_data.append(folder.split('_')[3] + '.1')
                        cd_data.append(direction[file])
                        print(cd_data)
                        csv_output.append(cd_data)
                        if count > 10:
                            # break
                            pass  # sys.exit(0)

            if count > 20:
                pass  # break

        tmp = pd.DataFrame(csv_output)
        tmp.to_csv('p:\\temp\\nikonshot\\cd.csv', mode='a', header=False, index=False)
    def MainFunction(self):
        pass
        folderlist = self.YE_folderlist(begin=0, end=3)  # 指定天数内YE目录
        finish_merge = self.complete_collection()  # 已完成图片贴图的YE目录-->后续改为集合，简化nikon_image_cd调用时的判断
        imgdone = self.cd_measure_done()  # 已完成CD测量的YE目录
        self.nikon_image_cd(folderlist, finish_merge, imgdone)
        self.asml_image_cd(folderlist, finish_merge, imgdone)
class CDU_QC_IMAGE_PLOT:
    def __init__(self):
        pass
    def Excel_Macro(self):
        # Run Excel Macro
        xlApp = win32com.client.DispatchEx('Excel.Application')
        xlsPath = os.path.expanduser('P:\\_Script\\ExcelFile\\MoveCdu.xls')
        wb = xlApp.Workbooks.Open(Filename=xlsPath)
        xlApp.Run('TotalMove')
        wb.Save()
        xlApp.Quit()
    def get_imagename(self):
        foldername = r'Z:\_DailyCheck\NikonShot\transfer'
        foldername = r'P:\temp\NikonShot\transfer'

        # foldername = r'Z:\0_CDU_QC_TopDownImages'
        filename = []
        for root, dirs, files in os.walk(foldername, False):
            for file in files:
                if ('jpg' in file or 'png' in file) and (not 'Done' in file) and len(
                        file) > 50:  # 部分文件名不规范，无lotid等，用>50剔除部分，后续重命名，用try/except
                    filename.append(file)
        return filename
    def rename_imgname(self):

        # 待确认作业设备的图片文件
        filename = self.get_imagename()

        # 数据按TRACKOUTTIME降序排
        # Read Data from Excel
        move = pd.read_excel('Z:\\_DailyCheck\\NikonShot\\transfer\\move.xls', sheetname=0)[
            ['LOTID', 'EQPID', 'TRACKOUTTIME']].sort_values(by=['TRACKOUTTIME'], ascending=False)

        move = pd.read_excel('P:\\_Script\\ExcelFile\\MoveCdu.xls', sheetname=0)[
            ['LOTID', 'EQPID', 'TRACKOUTTIME']].sort_values(by=['TRACKOUTTIME'], ascending=False)

        for name in filename:
            tmp = name.split('_')
            lotid = tmp[4] + '.1'
            date = tmp[1] + ' ' + tmp[2]  # 时间以字符串表示，格式YYYYMMDD HHMMSS，与move中格式不同，需转两次
            date = time.strptime(date, "%Y%m%d %H%M%S")  # 字符串转时间   YYMMDD HHMMSS
            date = time.strftime("%Y-%m-%d %H:%M:%S", date)  # 时间转字符串   YYYY-MM-DD HH：MM：SS
            date = datetime.datetime.strptime(date, "%Y-%m-%d %H:%M:%S")  # 字符串转时间   YYYY-MM-DD HH:MM:SS
            tmp = move[move['LOTID'].str.contains(lotid)]  # 列出包含特定LotID的数据

            try:  # 部分文件名不规范，无lotid
                tmp = tmp[tmp[
                              'TRACKOUTTIME'] < date]  # 小于date，CD SEM测试时间的第一笔stepper作业时间
                toolname = list(tmp.iloc[0])[1]  # 曝光设备名

                oldfilename = 'Z:\\_DailyCheck\\NikonShot\\transfer\\' + name
                oldfilename = 'P:\\temp\\NikonShot\\transfer\\' + name
                newfilename = 'Z:\\_DailyCheck\\NikonShot\\transfer\\' + toolname + '_' + 'Done_' + name
                newfilename = 'P:\\temp\\NikonShot\\transfer\\' + toolname + '_' + 'Done_' + name

                # oldfilename = 'Z:\\0_CDU_QC_TopDownImages\\' + name
                # newfilename = 'Z:\\0_CDU_QC_TopDownImages\\' + toolname + '_' + 'Done_' + name

                os.rename(oldfilename, newfilename)
            except:
                pass
    def cd(self):
        # df = pd.read_csv('Z:\\_DailyCheck\\nikonshot\\cd.csv')
        df = pd.read_csv('P:\\temp\\nikonshot\\cd.csv')
        move = pd.read_excel('P:\\_Script\\ExcelFile\\MoveCdu.xls', sheet_name=0)[
            ['LOTID', 'EQPID', 'TRACKOUTTIME']].sort_values(by=['TRACKOUTTIME'], ascending=False)

        l_toolid = list(df['ToolID'])
        l_lotid = list(df['LotID'])
        l_riqi = list(df['RiQi'])
        tool = []

        for i in range(df.shape[0]):

            try:
                isnan(l_toolid[i]) == True  # 未标注toolid
                date = l_riqi[i]
                date = time.strptime(date, "%Y%m%d %H%M%S")  # 字符串转时间   YYMMDD HHMMSS
                date = time.strftime("%Y-%m-%d %H:%M:%S", date)  # 时间转字符串   YYYY-MM-DD HH：MM：SS
                date = datetime.datetime.strptime(date, "%Y-%m-%d %H:%M:%S")  # 字符串转时间   YYYY-MM-DD HH:MM:SS

                tmp = move[move['LOTID'].str.contains(l_lotid[i])]  # 列出包含特定LotID的数据
                tmp = tmp[tmp['TRACKOUTTIME'] < date]  # 小于date，CD SEM测试时间的第一笔stepper作业时间
                toolname = list(tmp.iloc[0])[1]  # 曝光设备名
                tool.append(toolname)

            except:
                tool.append(l_toolid[i])

        df['ToolID'] = tool
        df.to_csv('Z:\\_DailyCheck\\nikonshot\\cd.csv', index=False, header=True)
        df.to_csv('P:\\temp\\nikonshot\\cd.csv', index=False, header=True)
        df.to_csv('Z:\\_DailyCheck\\nikonshot\\cd_bak.csv', index=False, header=True)


    def nikon(self,toollist, df, days):
        # days = 100
        toollist = [i for i in toollist if 'D' not in i]
        for tool in toollist:

            tmp = df[df['ToolID'].str.contains(tool)][['RiQi', 'R2', 'R3', 'R4', 'Direction']]
            # split data by 'RiQi' for boxplot
            l1 = tmp['RiQi'].unique()
            data_v1 = []  # reserve for slope R3
            data_h1 = []
            data_v = []  # reserve for top/bottome R2
            data_h = []

            for i in l1:
                v = []
                h = []
                v1 = []
                h1 = []

                v.append(i)
                h.append(i)
                v1.append(i)
                h1.append(i)

                tmp1 = tmp[tmp['RiQi'] == i]

                tmp_v = tmp1[tmp1['Direction'].str.contains('Vertical')]['R2']
                tmp_h = tmp1[tmp1['Direction'].str.contains('Horizontal')]['R2']

                v.extend(tmp_v)
                h.extend(tmp_h)
                data_v.append(v)
                data_h.append(h)

                tmp_v1 = tmp1[tmp1['Direction'].str.contains('Vertical')]['R4']
                tmp_h1 = tmp1[tmp1['Direction'].str.contains('Horizontal')]['R4']

                v1.extend(tmp_v1)
                h1.extend(tmp_h1)
                data_v1.append(v1)
                data_h1.append(h1)

                # convert data to DataFrame format, index by RiQi
            data_v = pd.DataFrame(data_v).tail(days)
            data_v = data_v.set_index([0])

            data_h = pd.DataFrame(data_h).tail(days)
            data_h = data_h.set_index([0])

            data_v1 = pd.DataFrame(data_v1).tail(days)
            data_v1 = data_v1.set_index([0])

            data_h1 = pd.DataFrame(data_h1).tail(days)
            data_h1 = data_h1.set_index([0])

            # plot image
            fig = plt.figure(figsize=(24, 16))

            ax1 = fig.add_subplot(2, 2, 1)
            data_v.T.boxplot(showmeans=True, patch_artist=True)
            ax1.set_xticklabels(data_v.index, rotation=90)
            ax1.set_title(tool + ' Vertical L-Bar Within Shot:Top CD/Bottom CD  100% Threshold Linear 1_4_1')
            plt.ylim(0.4, 0.9)
            # plt.show

            ax2 = fig.add_subplot(2, 2, 2)
            data_h.T.boxplot(showmeans=True, patch_artist=True)
            ax2.set_xticklabels(data_h.index, rotation=90)
            ax2.set_title(tool + ' Horizontal L-Bar Within Shot:Top CD/Bottom CD  100% Threshold Linear 1_4_1')
            plt.ylim(0.4, 0.9)
            # plt.show

            ax3 = fig.add_subplot(2, 2, 3)
            data_v1.T.boxplot(showmeans=True, patch_artist=True)
            ax3.set_xticklabels(data_v1.index, rotation=90)
            ax3.set_title(tool + ' Vertical L-Bar Within Shot Slope: (Right-Left)/(Right+Left)')
            plt.ylim(-0.5, 0.5)
            # plt.show

            ax4 = fig.add_subplot(2, 2, 4)
            data_h1.T.boxplot(showmeans=True, patch_artist=True)
            ax4.set_xticklabels(data_h1.index, rotation=90)
            ax4.set_title(tool + ' Horizontal L-Bar Within Shot Slope: (Upper - Lower)/(Upper + Lower)')
            plt.ylim(-0.5, 0.5)
            # plt.show

            fig.subplots_adjust(hspace=0.4)
            plt.savefig('Z:\\_DailyCheck\\Nikonshot\\' + tool + '.jpg', dpi=200, bbox_inches='tight')
            print('Z:\\_DailyCheck\\Nikonshot\\' + tool + '.jpg saved')

            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
    def asml(self,toollist, df, days):
        # days = 100
        toollist = [i for i in toollist if 'D' in i]
        # toollist = ['ALDI05']
        for tool in toollist:

            tmp = df[df['ToolID'].str.contains(tool)][
                ['RiQi', 'R2', 'R3', 'R4', 'CD1_50', 'CD2_50', 'CD3_50', 'CD4_50']]
            # split data by 'RiQi' for boxplot
            l1 = tmp['RiQi'].unique()
            data1 = []  # reserve   R2
            data2 = []  # reserve   bottom CD
            data3 = []  # reserve   R4
            data4 = []
            data5 = []

            for i in l1:
                d1 = []
                d1.append(i)
                tmp1 = tmp[tmp['RiQi'] == i]['R2']
                d1.extend(tmp1)
                data1.append(d1)

                d2 = []
                d2.append(i)

                if tool in ['ALDI02', 'ALDI03', 'ALDI06', 'ALDI07', 'ALDI09', 'ALDI10']:
                    tmp1 = tmp[tmp['RiQi'] == i]['CD2_50']
                    d2.extend(tmp1)
                    data2.append(d2)
                else:
                    tmp1 = tmp[tmp['RiQi'] == i]['CD1_50']
                    d2.extend(tmp1)
                    data2.append(d2)

                d3 = []
                d3.append(i)
                tmp1 = tmp[tmp['RiQi'] == i]['R4']
                d3.extend(tmp1)
                data3.append(d3)

                d4 = []
                d4.append(i)
                tmp1 = tmp[tmp['RiQi'] == i]['CD3_50']
                d4.extend(tmp1)
                data4.append(d4)

                d5 = []
                d5.append(i)
                tmp1 = tmp[tmp['RiQi'] == i]['CD4_50']
                d5.extend(tmp1)
                data5.append(d5)

                # convert data to DataFrame format, index by RiQi
            data1 = pd.DataFrame(data1).tail(days)
            data1 = data1.set_index([0])

            data2 = pd.DataFrame(data2).tail(days)
            data2 = data2.set_index([0])

            data3 = pd.DataFrame(data3).tail(days)
            data3 = data3.set_index([0])

            data4 = pd.DataFrame(data4).tail(days)
            data4 = data4.set_index([0])

            data5 = pd.DataFrame(data5).tail(days)
            data5 = data5.set_index([0])

            # plot image
            fig = plt.figure(figsize=(24, 16))

            ax1 = fig.add_subplot(2, 2, 1)
            data1.T.boxplot(showmeans=True, patch_artist=True)
            ax1.set_xticklabels(data1.index, rotation=90)
            ax1.set_title(tool + ' Vertical L-Bar Within Shot:Top CD/Bottom CD  100% Threshold Linear 1_4_1')
            plt.ylim(0.35, 0.75)
            # plt.show

            ax2 = fig.add_subplot(2, 2, 2)
            data2.T.boxplot(showmeans=True, patch_artist=True)
            ax2.set_xticklabels(data2.index, rotation=90)
            ax2.set_title(tool + ' Vertical L-Bar Within Shot CDU_Bottom CD (unit:pixel)')
            if tool in ['ALDI02', 'ALDI03', 'ALDI06', 'ALDI07', 'ALDI09', 'ALDI10']:
                plt.ylim(80, 140)
            else:
                plt.ylim(100, 170)

            # plt.show

            ax3 = fig.add_subplot(2, 2, 3)
            data3.T.boxplot(showmeans=True, patch_artist=True)
            ax3.set_xticklabels(data3.index, rotation=90)
            ax3.set_title(tool + ' Vertical L-Bar Within Shot Slope: (Right-Left)/(Right+Left)')
            plt.ylim(-0.4, 0.4)
            # plt.show

            ax4 = fig.add_subplot(2, 2, 4)

            f = data4.T.boxplot(patch_artist=True)
            data5.T.boxplot()  # notch = True)
            ax4.set_xticklabels(data4.index, rotation=90)
            ax4.set_title(tool + ' Vertical L-Bar Within Shot Slope: Green_Left Slope     Blue_Right Slope ')
            plt.ylim(20, 60)
            # plt.show

            fig.subplots_adjust(hspace=0.4)
            plt.savefig('Z:\\_DailyCheck\\Nikonshot\\' + tool + '.jpg', dpi=200, bbox_inches='tight')
            print('Z:\\_DailyCheck\\Nikonshot\\' + tool + '.jpg saved')

            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()

            # df.boxplot(sym='r*',vert=False,patch_artist=True,meanline=False,showmeans=True)  # for box in f['boxes']:  # 箱体边框颜色  #    box.set( color='#7570b3', linewidth=2)  # 箱体内部填充颜色  #    box.set( facecolor = '#1b9e77' )  # for whisker in f['whiskers']:  #    whisker.set(color='r', linewidth=2)  # for cap in f['caps']:  #    cap.set(color='g', linewidth=3)  # for median in f['medians']:  #    median.set(color='DarkBlue', linewidth=3)  # for flier in f['fliers']:  #    flier.set(marker='o', color='y', alpha=0.5)
    def move_file_to_Z_drive(self):

        toollist = ['ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13',
                    'ALII14', 'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9',
                    'ALSIBJ', 'BLSIBK', 'BLSIBL', 'BLSIE1', 'ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07',
                    'ALDI09', 'ALDI10', 'ALDI11', 'ALDI12', 'BLDI08', 'BLDI13']

        filefolder = 'Z:\\_DailyCheck\\NikonShot\\transfer\\'
        filefolder = 'P:\\temp\\NikonShot\\transfer\\'
        newfolder = 'Z:\\0_CDU_QC_TopDownImages\\'
        newfolder = 'Y:\\QC_CDU_IMAGES\\'

        for root, dirs, files in os.walk(filefolder, False):
            for file in files:

                if ('jpg' in file or 'png' in file) and ('Done' in file) and (file[0:6] in toollist) and (
                        len(file) > 50):  # 部分文件名不规范，无lotid等，用>50剔除部分，后续重命名，用try/except
                    old = filefolder + file
                    new = newfolder + file[0:6] + '\\' + file
                    shutil.move(old, new)
                    print(old + '  has been moved to Y drive')
    def plot_cd_image(self,days):

        toollist = ['ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
                    'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'ALSIBJ', 'BLSIBK',
                    'BLSIBL', 'BLSIE1', 'ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07', 'ALDI09', 'ALDI10', 'ALDI11',
                    'ALDI12', 'BLDI08', 'BLDI13']

        df = pd.read_csv('P:\\temp\\nikonshot\\cd.csv')
        df['R1'] = df['CD2'] / df['CD1']  # 100% threshold top/bottom
        df['R2'] = df['CD2_50'] / df['CD1_50']  # 50% threshold top/bottom
        df['R3'] = (df['CD4_50'] + df['CD3_50']) / df['CD1_50']  # 50% threshold slope/bottom
        df['R4'] = (df['CD4_50'] - df['CD3_50']) / (df['CD4_50'] + df['CD3_50'])

        df['RiQi'] = pd.to_datetime(df['RiQi'])  # convert string to datatime format -->not necessary
        df = pd.DataFrame(df).sort_values(by=['RiQi'], ascending=True)
        df = df[df['ToolID'] == df['ToolID']]  # remove NaN              # error triggered if not done

        self.nikon(toollist, df, days)
        self.asml(toollist, df, days)
    def MainFunction(self):
        pass
        # self.Excel_Macro()  # 读入  天move数据
        self.rename_imgname()
        self.cd()
        self.plot_cd_image(days=30)
        self.move_file_to_Z_drive()
class AWE_ANALYSIS:
    def __init__(self):
        pass
    def get_filepath(self,path='Z:\\AsmlAweFile\\RawData\\82\\'):
        n = 0
        filenamelist = []
        for root, dirs, files in os.walk(path,False):
            for names in files:
                # if names.endswith('ChartData.rar'):
                filenamelist.append(root + '\\' + names)
                n += 1
                print(n, "   ", names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def linearfit(self,XYin):

        linreg = LinearRegression()
        residual = pd.DataFrame(columns=[0])
        for i in XYin['WaferNr'].unique():
            input_y = XYin[XYin['WaferNr'] == i][XYin.columns[-1]]
            input_x = XYin[XYin['WaferNr'] == i][['NomPosX', 'NomPosY']]
            model = linreg.fit(input_x, input_y)
            tmp = pd.DataFrame(linreg.intercept_ + linreg.coef_[0] * input_x['NomPosX'] + linreg.coef_[1] * input_x[
                'NomPosY'] - input_y)
            residual = pd.concat([residual, tmp])
        residual = -1 * residual
        return residual.describe()
    def read_single_awe(self,name):

        f = [i.strip() for i in open(name) if i.strip('\n') != ""]
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
    def move_file(self,name):

        str1 = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")

        new = 'y:\\ASML_AWE\\' + name[23:26] + name[26:].split('\\')[0] + '\\' + name[26:].split('\\')[1]
        if os.path.exists(new):
            # print("TRUE",name,new)
            if os.path.exists(new + '\\' + name.split('\\')[6]):
                print("TRUE")
                new = new + '\\' + str1 + "_" + name.split('\\')[6]
                shutil.move(name, new)
            else:
                shutil.move(name, new)
        else:
            # print("FALSE",name,new)
            os.makedirs(new)
            shutil.move(name, new)
    def read_disk_z_awe(self,filelist):

        file = [i for i in filelist if i[-3:] == 'awe']

        listx = ['1XRedPos', '1XRedMCC', '1XRedWQ', '3XRedPos', '3XRedMCC', '3XRedWQ', '5XRedPos', '5XRedMCC',
                 '5XRedWQ', '7XRedPos', '7XRedMCC', '7XRedWQ', '88XRedPos', '88XRedMCC', '88XRedWQ', '1XGreenPos',
                 '1XGreenMCC', '1XGreenWQ', '3XGreenPos', '3XGreenMCC', '3XGreenWQ', '5XGreenPos', '5XGreenMCC',
                 '5XGreenWQ', '7XGreenPos', '7XGreenMCC', '7XGreenWQ', '88XGreenPos', '88XGreenMCC', '88XGreenWQ',
                 'XRedDelta', 'XGreenDelta']
        listy = ['1YRedPos', '1YRedMCC', '1YRedWQ', '3YRedPos', '3YRedMCC', '3YRedWQ', '5YRedPos', '5YRedMCC',
                 '5YRedWQ', '7YRedPos', '7YRedMCC', '7YRedWQ', '88YRedPos', '88YRedMCC', '88YRedWQ', '1YGreenPos',
                 '1YGreenMCC', '1YGreenWQ', '3YGreenPos', '3YGreenMCC', '3YGreenWQ', '5YGreenPos', '5YGreenMCC',
                 '5YGreenWQ', '7YGreenPos', '7YGreenMCC', '7YGreenWQ', '88YGreenPos', '88YGreenMCC', '88YGreenWQ',
                 'YRedDelta', 'YGreenDelta']

        summary = []
        for count, name in enumerate(file):

            try:
                if len(name.split('\\'))==7: # in case of file in root directory
                    print(count + 1, len(file), name)
                    basic, validNo, validX, validY = None, None, None, None
                    data = []

                    try:
                        basic, validNo, validX, validY = self.read_single_awe(name)
                        data.extend(basic)
                        data.extend(validNo)
                        for i in range(32):
                            data.extend(validX[listx[i]].describe())
                            data.extend(validY[listy[i]].describe())
                        # data-->basic,validNo,1XRedPos,1YRedPos,1XRedMCC,1YRedMCC......., "543" item

                        for i in range(10):
                            # print(listx[3*i])
                            XYin = validX[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', listx[3 * i]]].dropna()
                            data.extend(self.linearfit(XYin)[0])
                            XYin = validY[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', listy[3 * i]]].dropna()
                            data.extend(self.linearfit(XYin)[0])
                        summary.append(data)
                    except:
                        print('==file errro or small wfr qty===')
                    try:
                        self.move_file(name)  # summary.append(data)
                    except:
                        pass
            except:
                pass
        return summary
    def plot_MCC(self,file='y:/ASML_AWE/New_AWE.csv'):

        tool_dict = {6158: 'ALSD89', 4666: 'ALSD82', 5688: 'ALSD8A', 9726: 'ALSD8C', 4955: 'ALSD8B', 6450: 'ALSD85',
                     8111: 'BLSD7D', 8144: 'ALSD86', 4730: 'ALSD83', 4142: 'ALSD87',3527:'BLSD08'}

        tmp = ['RiQI', 'ShiJian', 'Part', 'Layer', 'Tool', 'Lot', 'MarkVariant', 'AlignStrategy', '5XRedMCC_count',
               '5XRedMCC_mean', '5XRedMCC_std', '5XRedMCC_min', '5XRedMCC_25%', '5XRedMCC_50%', '5XRedMCC_75%',
               '5XRedMCC_max', '5YRedMCC_count', '5YRedMCC_mean', '5YRedMCC_std', '5YRedMCC_min', '5YRedMCC_25%',
               '5YRedMCC_50%', '5YRedMCC_75%', '5YRedMCC_max', '5XGreenMCC_count', '5XGreenMCC_mean', '5XGreenMCC_std',
               '5XGreenMCC_min', '5XGreenMCC_25%', '5XGreenMCC_50%', '5XGreenMCC_75%', '5XGreenMCC_max',
               '5YGreenMCC_count', '5YGreenMCC_mean', '5YGreenMCC_std', '5YGreenMCC_min', '5YGreenMCC_25%',
               '5YGreenMCC_50%', '5YGreenMCC_75%', '5YGreenMCC_max']
        tmp = ['RiQI', 'ShiJian', 'Tool', '5XRedMCC_min', '5XRedMCC_max', '5YRedMCC_min', '5YRedMCC_max',
               '5XGreenMCC_min', '5XGreenMCC_max', '5YGreenMCC_min', '5YGreenMCC_max']
        df = pd.read_csv(file, usecols=tmp)
        df['Index'] = pd.to_datetime(df['RiQI'] + " " + df['ShiJian'])
        df = df.reset_index().set_index('Index')
        df = df.sort_index()

        for toolid in df['Tool'].unique():
            print(toolid,'---',df['Tool'].unique())
            tmp = df[df['Tool'] == toolid]
            # print(tmp.shape)
            # tmp=tmp.dropna()
            # print(tmp.shape)
            fig = plt.figure(figsize=(18, 10))
            # -----------------------------------
            ax1 = fig.add_subplot(4, 2, 1)

            # plt.plot(tmp.index,tmp['5XRedMCC_min'])
            tmp['5XRedMCC_min'][-1188:].plot()
            ax1.set_title(tool_dict[toolid] + "_5XRedMCC-Min")
            plt.ylim(0.9, 1)
            # -----------------------------------
            ax2 = fig.add_subplot(4, 2, 2)

            tmp['5XRedMCC_max'][-1188:].plot()
            ax2.set_title(tool_dict[toolid] + "_5XRedMCC-Max")
            plt.ylim(0.99, 1)

            # -----------------------------------
            ax3 = fig.add_subplot(4, 2, 3)

            tmp['5XGreenMCC_min'][-1188:].plot()
            ax3.set_title(tool_dict[toolid] + "_5XGreenMCC-Min")
            plt.ylim(0.9, 1)

            # -----------------------------------
            ax4 = fig.add_subplot(4, 2, 4)
            tmp['5XGreenMCC_max'][-1188:].plot()
            ax4.set_title(tool_dict[toolid] + "_5XGreenMCC-Max")
            plt.ylim(0.99, 1)

            # -----------------------------------
            ax5 = fig.add_subplot(4, 2, 5)

            # plt.plot(tmp.index,tmp['5XRedMCC_min'])
            tmp['5YRedMCC_min'][-1188:].plot()
            ax5.set_title(tool_dict[toolid] + "_5YRedMCC-Min")
            plt.ylim(0.9, 1)
            # -----------------------------------
            ax6 = fig.add_subplot(4, 2, 6)

            tmp['5YRedMCC_max'][-1188:].plot()
            ax6.set_title(tool_dict[toolid] + "_5YRedMCC-Max")
            plt.ylim(0.99, 1)

            # -----------------------------------
            ax7 = fig.add_subplot(4, 2, 7)

            tmp['5YGreenMCC_min'][-1188:].plot()
            ax7.set_title(tool_dict[toolid] + "_5YGreenMCC-Min")
            plt.ylim(0.9, 1)

            # -----------------------------------
            ax8 = fig.add_subplot(4, 2, 8)
            tmp['5YGreenMCC_max'][-1188:].plot()
            ax8.set_title(tool_dict[toolid] + "_5YGreenMCC-Max")
            plt.ylim(0.99, 1)

            fig.subplots_adjust(hspace=1)

            plt.savefig('z:/_DailyCheck/ASML_AWE/' + tool_dict[toolid] + '.jpg', dpi=100, bbox_inches='tight')
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
    def MainFunction(self):
        pass
        filelist = self.get_filepath(path='Z:\\AsmlAweFile\\RawData\\')
        pd.DataFrame(filelist).to_csv('z:/asmlawefile/list.csv')
        summary = self.read_disk_z_awe(filelist)
        pd.DataFrame(summary).to_csv('Y:\\ASML_AWE\\New_AWE.csv', index=None, mode='a', encoding='GBK', header=None)
        self.plot_MCC(file='y:/ASML_AWE/New_AWE.csv')
class NikonAdjust:
    def __init__(self):
        pass
    def ovl_qc(self):  # NIkon OVL QC Value
        n = 120  # define time period for data extraction
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'

        enddate = datetime.datetime.now().date()
        startdate = enddate - datetime.timedelta(days=n)

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')

        sql = "SELECT  PartID, \
              Proc_EqID, Dcoll_Time, \
              (OffsetX_jobin + OffsetX_Met_Value) as OffsetX, \
              (OffsetY_jobin + OffsetY_Met_Value) as OffsetY, \
              (Mag_jobin + Mag_Met_Value) as Mag ,\
              (Rot_jobin + Rot_Met_Value) as Rot,OffsetX_Met_Value,OffsetY_Met_Value,Mag_Met_Value , Rot_Met_Value \
              FROM OL_Nikon WHERE ((Dcoll_Time>#" + str(startdate) + "#)  \
              and (PartID like '2LXXX%')) ORDER BY Dcoll_Time"
        rs.Open(sql, conn, 1, 3)

        ovl = []
        rs.MoveFirst()

        while True:
            if rs.EOF:
                break
            else:
                ovl.append([rs.Fields.Item(i).Value for i in range(rs.Fields.Count)])
                rs.MoveNext()

        rs.close
        ovl = pd.DataFrame(ovl)

        ovl.columns = ['Part', 'Tool', 'Riqi', 'TranX', 'TranY', 'Mag', 'Rot', 'Mx', 'My', 'Mm', 'Mr']

        return ovl
    def Nikno_Product_OVL(self):
        n = 120  # define time period for data extraction
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'

        enddate = datetime.datetime.now().date()
        startdate = enddate - datetime.timedelta(days=n)

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')

        sql = "SELECT * FROM OL_NIKON WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"
        sql = "SELECT  \
              Proc_EqID, Dcoll_Time, \
              OffsetX_Optimum, OffsetY_Optimum, ScalX_Optimum , ScalY_Optimum, \
              ORT_Optimum, Wrot_Optimum , Mag_Optimum , Rot_Optimum,  \
              Tech, Layer \
              FROM OL_NIKON WHERE (Dcoll_Time>#" + str(
            startdate) + "#)  and  (PartID  not like '2LXXX%') ORDER BY Dcoll_Time"
        rs.Open(sql, conn, 1, 3)

        nikon = []
        rs.MoveFirst()
        while True:
            if rs.EOF:
                break
            else:
                nikon.append([rs.Fields.Item(i).Value for i in range(rs.Fields.Count)])
                rs.MoveNext()
        rs.close
        nikon = pd.DataFrame(nikon)

        conn.close
        nikon.columns = ['Tool', 'Riqi', 'TranX', 'TranY', 'ScalX', 'ScalY', 'Ort', 'Rot', 'SMag', 'SRot', 'Tech',
                         'Layer']
        nikon['Riqi'] = [datetime.datetime.strptime(str(i)[0:19], '%Y-%m-%d %H:%M:%S') for i in nikon['Riqi']]

        return nikon
    def LSA_FIA(self,tmp2):

        t1 = tmp2[tmp2['Tech'].str.contains("1") | tmp2['Tech'].str.contains("25") | tmp2['Tech'].str.contains("52")]
        t1 = t1[t1['Layer'].str.contains("PT") | t1['Layer'].str.contains("TB") | t1['Layer'].str.contains("HV") | t1[
            'Layer'].str.contains("NX") | t1['Layer'].str.contains("PX") | t1['Layer'].str.contains("DN") | t1[
                    'Layer'].str.contains("DP")]

        t2 = tmp2[
            tmp2['Layer'].str.contains("A1") | tmp2['Layer'].str.contains("A2") | tmp2['Layer'].str.contains("A3") |
            tmp2['Layer'].str.contains("A4") | tmp2['Layer'].str.contains("AT") | tmp2['Layer'].str.contains("TT") | \
            tmp2['Layer'].str.contains("W1") | tmp2['Layer'].str.contains("W2") | tmp2['Layer'].str.contains("WT") |
            tmp2['Layer'].str.contains("CT")]

        fia = pd.concat([t1, t2], axis=0).sort_index()

        lsa = tmp2.loc[list(set(tmp2.index) - set(fia.index))].sort_index()

        return lsa, fia
    def MainFunction(self):
        pass
        I11 = 'y:/NikonPara/Adjust11.csv'
        I14 = 'y:/NikonPara/Adjust14.csv'
        item11 = ['Tool', 'Date', 'basadj.fia.dx_FIA Base-line Offset X:[um]', 'basadj.fia.dy_Y:[um]',
                  'basadj.lsa.dx_LSA Base-line Offset X:[um]', 'basadj.lsa.dy_Y:[um]', 'magadj.Dnocalib_Offset:[um]',
                  'basadj.Drot_Reticle Rotation:[urad]']

        item14 = ['Tool', 'Date', ' basadj.fia.dx_FIA Base-line Offset X:[um]', ' basadj.fia.dy_Y:[um]',
                  ' basadj.lsa.dx_LSA Base-line Offset X:[um]', ' basadj.lsa.dy_Y:[um]', ' magadj.Dnocalib_Offset:[um]',
                  ' basadj.Drot_Reticle Rotation:[urad]']
        col = ['rot', 'lsa_x', 'lsa_y', 'fia_x', 'fia_y', 'mag', 'tool', 'date']
        df11 = pd.read_csv(I11, usecols=item11)
        df14 = pd.read_csv(I14, usecols=item14)
        df11.columns = col
        df14.columns = col
        df = pd.concat([df11, df14], axis=0)
        df11, df14 = None, None
        df['date'] = pd.to_datetime(df['date'])
        df = df.reset_index().set_index('date')
        ovl = self.ovl_qc()
        ovl['Riqi'] = [datetime.datetime.strptime(str(i)[:-6], '%Y-%m-%d %H:%M:%S') for i in ovl['Riqi']]
        ovl = ovl.reset_index().set_index('Riqi')
        ovltool = ['ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
                   'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'ALSIBJ', 'BLSIBK',
                   'BLSIBL', 'BLSIE1', 'BLSIE2']

        toolmap = {'alsib1': 'ALII01', 'alsib2': 'ALII02', 'alsib3': 'ALII03', 'alsib4': 'ALII04', 'alsib5': 'ALII05',
                   'alsib6': 'ALSIB6', 'alsib7': 'ALSIB7', 'alsib8': 'ALSIB8', 'alsib9': 'ALSIB9', 'alsiba': 'ALII10',
                   'alsibb': 'ALII11', 'alsibc': 'ALII12', 'alsibd': 'ALII13', 'alsibe': 'ALII14', 'alsibf': 'ALII15',
                   'alsibg': 'ALII15', 'alsibh': 'ALII17', 'alsibi': 'ALII18', 'alsibj': 'ALSIBJ', 'blsibk': 'BLSIBK',
                   'blsibl': 'BLSIBL', 'blsie1': 'BLSIE1', 'blsie2': 'BLSIE2'}

        nikon = self.Nikno_Product_OVL()

        for tool in df['tool'].unique()[0:]:
            tmp = df[df['tool'] == tool]
            tmp1 = ovl[ovl['Tool'] == toolmap[tool]]  # QC Value
            tmp2 = nikon[nikon['Tool'] == toolmap[tool]]  # Product value
            lsa, fia = self.LSA_FIA(tmp2)

            fig = plt.figure(figsize=(18, 10))
            # -----------------------------------
            ax1 = fig.add_subplot(3, 2, 1)
            x = list(tmp.index)
            y = list(tmp['lsa_x'])
            plt.plot(x, y, color='blue')
            # tmp['lsa_x'].plot()
            ax1.yaxis.grid(True)
            ax1.set_title(tool.upper() + ' LSA_X  Blue:Machine/Red:QcOpt/Green:QcMeasured/Black:ProductOpt')
            # plt.text(0, 0, 'Blue:Machine Red:QcOpt Green:QcMeasured Black:ProductOpt')

            ax11 = ax1.twinx()
            x = list(tmp1[tmp1['Part'].str.contains('LSA')].index)
            y = list(tmp1[tmp1['Part'].str.contains('LSA')]['TranX'])
            plt.plot(x, y, color='red')
            y = list(tmp1[tmp1['Part'].str.contains('LSA')]['Mx'])
            plt.plot(x, y, color='green')

            x = lsa['Riqi']
            y = lsa['TranX']
            plt.plot(x, y, color='black', marker='.', linestyle=':', alpha=0.1)

            # -----------------------------------

            # -----------------------------------
            ax2 = fig.add_subplot(3, 2, 2)
            x = list(tmp.index)
            y = list(tmp['lsa_y'])
            plt.plot(x, y, color='blue')
            # tmp['lsa_y'].plot()
            ax2.yaxis.grid(True)
            ax2.set_title(tool.upper() + ' LSA_Y')

            ax21 = ax2.twinx()
            x = list(tmp1[tmp1['Part'].str.contains('LSA')].index)
            y = list(tmp1[tmp1['Part'].str.contains('LSA')]['TranY'])
            plt.plot(x, y, color='red')
            y = list(tmp1[tmp1['Part'].str.contains('LSA')]['My'])
            plt.plot(x, y, color='green')
            x = lsa['Riqi']
            y = lsa['TranY']
            plt.plot(x, y, color='black', marker='.', linestyle=':', alpha=0.1)

            # -----------------------------------

            # -----------------------------------
            ax3 = fig.add_subplot(3, 2, 3)
            x = list(tmp.index)
            y = list(tmp['fia_x'])
            plt.plot(x, y, color='blue')
            # tmp['fia_x'].plot()
            ax3.yaxis.grid(True)
            ax3.set_title(tool.upper() + ' FIA_X')

            ax31 = ax3.twinx()
            x = list(tmp1[tmp1['Part'].str.contains('FIA')].index)
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['TranX'])
            plt.plot(x, y, color='red')
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['Mx'])
            plt.plot(x, y, color='green')
            x = fia['Riqi']
            y = fia['TranX']
            plt.plot(x, y, color='black', marker='.', linestyle=':', alpha=0.1)
            # -----------------------------------

            # -----------------------------------
            ax4 = fig.add_subplot(3, 2, 4)
            x = list(tmp.index)
            y = list(tmp['fia_y'])
            plt.plot(x, y, color='blue')
            ax4.yaxis.grid(True)
            ax4.set_title(tool.upper() + ' FIA_Y')

            ax41 = ax4.twinx()
            x = list(tmp1[tmp1['Part'].str.contains('FIA')].index)
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['TranY'])
            plt.plot(x, y, color='red')
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['My'])
            plt.plot(x, y, color='green')
            x = fia['Riqi']
            y = fia['TranY']
            plt.plot(x, y, color='black', marker='.', linestyle=':', alpha=0.1)

            # -----------------------------------

            # -----------------------------------
            ax5 = fig.add_subplot(3, 2, 5)
            x = list(tmp.index)
            y = list(tmp['rot'])
            plt.plot(x, y, color='blue')
            ax5.yaxis.grid(True)
            ax5.set_title(tool.upper() + ' ROT')

            ax51 = ax5.twinx()
            x = list(tmp1[tmp1['Part'].str.contains('FIA')].index)
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['Rot'])
            plt.plot(x, y, color='red')
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['Mr'])
            plt.plot(x, y, color='green')
            x = fia['Riqi']
            y = fia['SRot']
            plt.plot(x, y, color='black', marker='.', linestyle=':', alpha=0.1)
            # -----------------------------------

            # -----------------------------------
            ax6 = fig.add_subplot(3, 2, 6)
            x = list(tmp.index)
            y = list(tmp['mag'])
            plt.plot(x, y, color='blue')
            ax6.yaxis.grid(True)
            ax6.set_title(tool.upper() + ' MAG')

            ax61 = ax6.twinx()
            x = list(tmp1[tmp1['Part'].str.contains('FIA')].index)
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['Mag'])
            plt.plot(x, y, color='red')
            y = list(tmp1[tmp1['Part'].str.contains('FIA')]['Mm'])
            plt.plot(x, y, color='green')
            x = fia['Riqi']
            y = fia['SMag']
            plt.plot(x, y, color='black', marker='.', linestyle=':', alpha=0.1)

            # -----------------------------------

            fig.subplots_adjust(hspace=0.4)

            plt.savefig('z:\\_DailyCheck\\NikonAdjust\\' + tool, dpi=100, bbox_inches='tight')
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
class ESF:
    def __init__(self):
        pass
    def MainFunction(self):
        pass

        esfColumns = ['ACTION', 'PARTTITLE', 'PART', 'STAGE', 'RECIPE', 'PPID', 'EQPID', 'FLAG', 'TYPE', 'ACTIVEFLAG',
                      'FAILREASON', 'EXPIRETIME', 'CREATEUSER', 'USERDEPT', 'CREATETIME', 'REQUESTUSER']
        # os.system('P:\\_Script\\ExcelFile\\ESF.xlsm')
        new = pd.read_excel('P:\\_Script\\ExcelFile\\ESF.xlsm', sheet_name='ESF', header=0).fillna('')
        new.to_csv('z:\\_dailycheck\\ESF\\ESF_' + str(datetime.date.today()) + ".csv", index=None, encoding='GBK')
        # new = pd.read_csv('z:\\_dailycheck\\ESF\\ESF_' + str(datetime.date.today() )+".csv",encoding='GBK').fillna('')

        old = str(datetime.date.today() + datetime.timedelta(days=-1))
        if os.path.exists('z:\\_dailycheck\\ESF\\ESF_' + old + ".csv"):
            old = pd.read_csv('z:\\_dailycheck\\ESF\\ESF_' + old + ".csv", encoding='GBK').fillna('')

        new = set([('#' + str(new.loc[i][0]) + '#' + str(new.loc[i][1]) + '#' + str(new.loc[i][2]) + '#' + str(
            new.loc[i][3]) + '#' + str(new.loc[i][4]) + '#' + str(new.loc[i][5]) + '#' + str(new.loc[i][6]) + '#' + str(
            new.loc[i][7]) + '#' + str(new.loc[i][8]) + '#' + str(new.loc[i][9]) + '#' + str(
            new.loc[i][10]) + '#' + str(new.loc[i][11]) + '#' + str(new.loc[i][12]) + '#' + str(
            new.loc[i][13]) + '#' + str(new.loc[i][14])) for i in range(new.shape[0])])

        old = set([('#' + str(old.loc[i][0]) + '#' + str(old.loc[i][1]) + '#' + str(old.loc[i][2]) + '#' + str(
            old.loc[i][3]) + '#' + str(old.loc[i][4]) + '#' + str(old.loc[i][5]) + '#' + str(old.loc[i][6]) + '#' + str(
            old.loc[i][7]) + '#' + str(old.loc[i][8]) + '#' + str(old.loc[i][9]) + '#' + str(
            old.loc[i][10]) + '#' + str(old.loc[i][11]) + '#' + str(old.loc[i][12]) + '#' + str(
            old.loc[i][13]) + '#' + str(old.loc[i][14])) for i in range(old.shape[0])])

        added = pd.DataFrame([[k for k in i.split('#')] for i in (new - old)], columns=esfColumns)
        deleted = pd.DataFrame([[k for k in i.split('#')] for i in (old - new)], columns=esfColumns)

        added['ACTION'] = 'added'
        deleted['ACTION'] = 'deleted'
        result = pd.concat([added, deleted])
        result.to_csv('z:\\_dailycheck\\ESF\\result_' + str(datetime.date.today()) + ".csv", index=None, encoding="GBK")
class SPC99:
    def __init__(self):
        pass
        # os.system('P:/_Script/ExcelFile/SPC99.xlsm')
    def plot_chart(self,tmp, name, title, lsl, usl, lwl, uwl, fz, fzcl):

        tools = tmp['ToolID'].unique()
        if len(tools) % 2 != 0:
            n = len(tools) // 2 + 1
        else:
            n = len(tools) // 2

        fig = plt.figure(figsize=(16, n * 4))
        try:

            for i, tool in enumerate(tools):
                df = tmp[tmp['ToolID'] == tool]
                ax = plt.subplot(n, 2, i + 1)
                x = df['Date']
                y = df['Data']
                y1 = df['Sigma']

                if name == 'K99TXRWS02' and 'AMXR01 WSI X' in title:  # specila chart ,sigma is the actual value
                    y = df['Sigma']

                if name == 'C99CXR0101' and 'AMXR01 P' in title:  # specila chart ,sigma is the actual value
                    y = df['Sigma']

                    # ax.bar(x,y,color='g',alpha = 1,width = 0.1)
                ax.plot(x, y, color='r', linestyle='', marker="*")

                if fz == 'Y':
                    ax1 = ax.twinx()
                    ax1.plot(x, y1, color='b', linestyle=':', marker='+', linewidth=1)
                    ax1.set_ylim(0, fzcl)  # ax1.set_yticks([0,2,4,6,8,10])

                plt.title(tool + "_" + title)
                ax.set_ylim(lwl, uwl)

                # if uwl == '':
                #    ax.set_ylim(lwl ,uwl )
                # else:
                #    if lwl=='':
                #        ax.set_ylim(0,uwl)
                #    else:
                #        ax.set_ylim(lwl,uwl)

                ax.grid(True, linestyle='-')

                ax.set_xticklabels(x, rotation=45)

                # line = mlines.Line2D(x,[2 for i in range(len(X))],lw=3.,ls='-',alpha=1,color='red')
                x = list(x)
                if lwl != 0:
                    y = [lsl for i in range(len(x))]
                    line = mlines.Line2D(x, y, lw=2., ls='--', alpha=1, color='g')
                    ax.add_line(line)

                y = [usl for i in range(len(x))]
                line = mlines.Line2D(x, y, lw=2., ls='--', alpha=1, color='g')
                ax.add_line(line)

                # ax.set_xticks(pd.date_range(x[0],x[-1],freq='W'))

                plt.gca().xaxis.set_major_formatter(mdates.DateFormatter(
                    '%m/%d/%Y'))  # plt.gca().xaxis.set_major_locator(mdates.DayLocator())  # plt.gcf().autofmt_xdate()  # 自动旋转日期标记
                # plt.show()
        except:

            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
        try:
            fig.subplots_adjust(hspace=0.5)
            title = title.replace(' ', '_')
            plt.savefig('z:\\_DailyCheck\\SPC99\\' + name + "_" + title, dpi=200, bbox_inches='tight')
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
        except:
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()


        # plt.show()
    def move_file(self):
        filelist =[i for i in  os.listdir('z:/_dailycheck/spc99/') if 'L99T' in i or 'L99P' in i or 'L99D' in i]
        for i in filelist:
            shutil.move('z:/_dailycheck/spc99/'+i,'z:/_dailycheck/spc99/thickness/'+i)
    def MainFunction(self):
        path = 'P:/_Script/ExcelFile/SPC99.xlsm'
        raw = pd.read_excel(path, sheet_name='Summary', header=0)
        raw = raw.sort_values(by='Date')
        config = pd.read_excel(path, sheet_name='Config', header=0)
        config = config[['Chart', 'Title', 'LSL', 'Target', 'USL', 'LWL', 'UWL', 'Ax2', 'Ax2CL']]
        config = config.fillna('')
        k = 0
        for i in range(config.shape[0]):
            name = config.iloc[i][0]
            title = config.iloc[i][1]
            lsl = config.iloc[i][2]
            usl = config.iloc[i][4]
            lwl = config.iloc[i][5]
            uwl = config.iloc[i][6]
            fz = config.iloc[i][7]
            fzcl = config.iloc[i][8]

            tmp = raw[raw['ChartID'] == name]

            if tmp.shape[0] > 0:
                print(name, title, lsl, usl, lwl, uwl, fz, fzcl)
                try:
                    self.plot_chart(tmp, name, title, lsl, usl, lwl, uwl, fz, fzcl)
                except:
                    print('ERROR_' + name)
        self.move_file()
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
                    'Z:\\_DailyCheck\\MetalImages\\' + path.split('\\')[7] + '_' + path.split('\\')[6] + '.jpg')
            except:
                pass
    def MainFunction(self):
        try:
            shutil.rmtree(r'Z:\_DailyCheck\\MetalImages')
            os.mkdir(r'Z:\_DailyCheck\\MetalImages')
        except:
            pass
        try:
            os.mkdir(r'Z:\_DailyCheck\\MetalImages')
        except:
            pass
        folderlist = self.YE_folderlist()
        self.nikon_merge_image(folderlist)
class CD_SEM_111:
    def __init__(self):
        pass
    def All_PPID_FLAG(self):  # 近30万笔记录太慢
        # update student set age=tm.age from temporary tm where student.name=tm.name
        # 改成类似上述方式
        tmp = pd.read_excel('P:\\_Script\\ExcelFile\\_PPID.xlsm', sheet_name='PPID')
        tmp = tmp[['PART', 'STAGE', 'PPID']]
        tmp = list(tmp.dropna().drop_duplicates()['PPID'])
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select ID,IDW ,IDP,PPID FROM AMP WHERE PPID = False"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        count = 0
        for i in range(rs.recordcount):
            print(count, i,rs.recordcount, 'ALL_PPID_FLAG')
            if (rs.fields(1).value + "-" + rs.fields(2).value) in tmp or (
                    rs.fields(1).value[:-2] + '-' + rs.fields(2).value) in tmp:
                sql = "UPDATE AMP SET PPID=TRUE WHERE ID = " + str(rs.fields(0).value)
                conn.Execute(sql)
                count += 1

            else:
                pass
            rs.movenext
        conn.close
    def All_TEMPLATE_FLAG(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select ID,IDW ,IDP,TOOL,PPID FROM AMP"
        sql = sql + ' WHERE PPID=1'  # and ID between 0 and 1000'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        count = 0
        rsIDP = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "update idp set TEMPLATE='' where TEMPLATE is NULL or TEMPLATE='NA'"
        rsIDP.Open(sql, conn, 1, 3)
        for i in range(rs.recordcount):
            print(count,i, rs.recordcount,'ALL_TMPLATE_FLAG')
            rsIDP = win32com.client.Dispatch(r'ADODB.Recordset')
            if rs.fields(3).value is None:
                pass
            else:
                sql = "select * from idp where " \
                      "IDW='" + rs.fields(1).value + "' and IDP='" + rs.fields(2).value + "' and TOOL='" + rs.fields(
                    3).value + "' and TEMPLATE <>''"
                rsIDP.Open(sql, conn, 1, 3)
                if (rsIDP.recordcount) > 0:
                    count += 1

                    sql = "UPDATE AMP SET IsTemplate=1 WHERE ID = " + str(rs.fields(0).value)
                    conn.Execute(sql)
            rs.movenext
        conn.close
    def All_TECH_FLAG(self):
        #todo -->how to refresh tech table
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select ID,IDW,Tech FROM AMP where Tech='' or Tech is null"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        count = 0

        for i in range(rs.recordcount):
            tmp = rs.fields(1).value
            if tmp[-2:] == '-A' or tmp[-2:] == '-N':
                tmp = tmp[:-2]
            sql = "select * from TECH where PartID='" + tmp + "'"
            rsTECH = win32com.client.Dispatch(r'ADODB.Recordset')
            rsTECH.Open(sql, conn, 1, 3)
            if (rsTECH.recordcount) > 0:
                sql = "UPDATE AMP SET Tech='" + (rsTECH.fields(2).value)[0:3] + "' WHERE ID = " + str(
                    rs.fields(0).value)
                conn.Execute(sql)
            print(i, rs.recordcount,'ALL_TECH_FLAG')
            rs.movenext
        conn.close
    def All_CD_TYPE_FLAG(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select ID,mid(IDP,1,2) as Layer,Tech ,Type FROM AMP WHERE Tech IS NOT NULL and (Type ='' or Type is  null) and PPID=True"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        count = 0

        for i in range(rs.recordcount):

            sql = "select Type from R2R_CD_TEMPLATE where IsHasTemplate=True " \
                  "and Layer='" + rs.fields(1).value + "' and TechName='" + rs.fields(2).value + "'"
            rsTemplate = win32com.client.Dispatch(r'ADODB.Recordset')
            rsTemplate.Open(sql, conn, 1, 3)
            if (rsTemplate.recordcount) > 0:
                sql = "UPDATE AMP SET Type='" + (rsTemplate.fields(0).value) + "' WHERE ID = " + str(rs.fields(0).value)
                conn.Execute(sql)
            print(i, rs.recordcount,'ALL_CD_TYPE_FLAG')
            rs.movenext
        conn.close
    def All_AutoCheck_Flag(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select * from reference "
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        refCol, ref = [], []
        for i in range(rs.fields.count):
            refCol.append(rs.fields(i).name)
        for i in range(rs.recordcount):
            ref.append([str(rs.fields(j).value).strip() for j in range(rs.fields.count)])
            rs.movenext
        ref = pd.DataFrame(ref, columns=refCol).fillna('')
        refCol = refCol[:-1]

        sql = "select * FROM AMP WHERE PPID=1 AND IsTemplate=0 and Type IS NOT NULL"
        # sql = "select * FROM AMP WHERE PPID=1  and Type IS NOT NULL"




        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        dataCol = []
        for i in range(rs.fields.count):
            dataCol.append(rs.fields(i).name)
        count = 0
        for i in range(rs.recordcount):
            tmp = pd.DataFrame([str(rs.fields(i).value).strip() for i in range(len(dataCol))]).T.fillna('')
            tmp.columns = dataCol

            try: #in case of data error ,no TECH data actually
                if tmp['Tech'][0][1] == '1':
                    if tmp['IDP'][0][:2] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT','TT']:
                        x = ref[ref['Golden'].str.contains('018METAL')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    elif tmp['IDP'][0][:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:
                        x = ref[ref['Golden'].str.contains('018HOLE')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    elif tmp['IDP'][0][:2] in ['GT', 'PC']:
                        x = ref[ref['Golden'].str.contains('018GT')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    elif tmp['IDP'][0][:2] in ['TO']:
                        x = ref[ref['Golden'].str.contains('018TO')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    elif tmp['IDP'][0][:2] in ['P0']:
                        x = ref[ref['Golden'].str.contains('018TO')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    else:
                        if tmp['Type'][0] == 'Line':
                            x = ref[ref['Golden'].str.contains('018LINE')][refCol].reset_index().drop('index', axis=1).T
                            x = x[x[0] != 'None'].T
                        else:
                            x = ref[ref['Golden'].str.contains('018SPACE')][refCol].reset_index().drop('index', axis=1).T
                            x = x[x[0] != 'None'].T
                else:
                    if tmp['IDP'][0][:2] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT', 'TT']:
                        x = ref[ref['Golden'].str.contains('035METAL')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    elif tmp['IDP'][0][:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:
                        x = ref[ref['Golden'].str.contains('035HOLE')][refCol].reset_index().drop('index', axis=1).T
                        x = x[x[0] != 'None'].T
                    else:
                        if tmp['Type'][0] == 'Line':
                            x = ref[ref['Golden'].str.contains('035LINE')][refCol].reset_index().drop('index', axis=1).T
                            x = x[x[0] != 'None'].T
                        else:
                            x = ref[ref['Golden'].str.contains('035SPACE')][refCol].reset_index().drop('index', axis=1).T
                            x = x[x[0] != 'None'].T
                y = tmp[x.columns]
                x1 = [str(i) for i in (x.loc[0])]
                y1 = [str(i) for i in (y.loc[0])]
                if x1 != y1:
                    # if False in list((x == y).loc[0]):
                    sql = "UPDATE AMP SET AutoCheck='Wrong' Where  ID = " + str(rs.fields(0).value)
                else:
                    sql = "UPDATE AMP SET AutoCheck='Correct' Where  ID = " + str(rs.fields(0).value)
                conn.Execute(sql)
                print(i, rs.recordcount,"ALL_AutoCheck_FLAG")
            except:
                pass
            rs.movenext
        conn.close
    def LOGIN(self,tool):
        # timeout = 30
        # port = 21
        IPaddress = {'ALCD01': '10.4.152.56', 'ALCD02': '10.4.152.53', 'ALCD03': '10.4.152.54', 'ALCD04': '10.4.151.79',
                     'ALCD05': '10.4.151.81', 'ALCD06': '10.4.151.82', 'ALCD07': '10.4.151.50', 'ALCD08': '10.4.153.26',
                     'ALCD09': '10.4.152.55', 'ALCD10': '10.4.153.32', 'BLCD11': '10.4.131.48', 'BLCD12': '10.4.131.47',
                     'SERVER': '10.4.72.240'}
        HOST = IPaddress[tool]
        user = 'user02'
        password = 'qw!1234'
        try:
            ftp = ftplib.FTP(HOST)
        except ftplib.error_perm:
            print('无法连接到"%s"' % HOST)
            return
        print('连接到"%s"' % HOST)

        try:
            # user是FTP用户名，pwd就是密码了
            ftp.login(user, password)
        except ftplib.error_perm:
            print('登录失败')
            ftp.quit()
            return
        print('登陆成功')
        return ftp
    def READ_IDP(self,idppath, tool):
        try:
            tmp = ['NA' for i in range(6)]
            f = [i.strip() for i in open(idppath)]
            if len(f) > 25:
                for index, i in enumerate(f):
                    if 'idw_name' in i:
                        tmp[0] = i.split(':')[1].strip()
                        if 'FEM' in tmp[0] or 'MAP' in tmp[0]:
                            break
                    if 'idp_name ' in i:
                        tmp[1] = i.split(':')[1].strip()
                        if tmp[1][-2:] != 'LN' or 'FEM' in tmp[1] or 'MAP' in tmp[1]:
                            break
                    if 'template   : MS : 1' in i:
                        tmp[2] = i.split(',')[1].strip()
                    if 'msr_point  :      1 :' in i:
                        tmp[3] = int(int(i.split(',')[2]) / 1000)
                        tmp[4] = int(int(i.split(',')[3]) / 1000)
                        tmp[5] = tool
                        break
        except:
            pass  # tmp = pd.DataFrame(tmp).T
        # tmp.columns=['IDW',	'IDP',	'TEMPLATE',	'X',	'Y',	'TOOL']
        return tmp
    def READ_AMP(self,amppath, tool):
        try:
            f = [i.strip() for i in open(amppath) if '_dif' not in i]

            if len(f) == 54 or len(f) == 55:
                for index, i in enumerate(f):
                    if 'comment   ' in i:
                        f = [i.split(":")[1].strip() for i in f[index + 1:]]
                        f.append(tool)
                        f.append(amppath.split('/')[6][:amppath.split('/')[6].find('_', 1)])
                        f.append(amppath.split('/')[6][amppath.split('/')[6].find('_', 1) + 1:])
        except:
            pass
        return f
    def Download_Modified_List(self):
        os.chdir('Y:/ModifiedCdSemIDP/')
        toollist = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        for tool in toollist:
            try:
                ftp = self.LOGIN(tool)
                ftp.cwd('/dailycheck')
                for tmp in ftp.nlst():
                    file = open(tmp, 'wb')
                    ftp.retrbinary('RETR ' + tmp, file.write)
                ftp.quit()
            except:
                pass
    def Modified_IDP_AMP_DOWNLOAD(self):
        root = 'y:/ModifiedCdSemIDP/'
        toollist = ['DATAS1', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        str1 = str(datetime.datetime.now()).replace('-', '')[0:8]
        filelist = [root + i for i in os.listdir(root) if str1 in i and '-IDP-' in i]
        for tool in toollist[:]:
            try:
                file = [i for i in filelist if tool in i][0]
                idplist = [i.strip() for i in open(file)]
                if tool == 'DATAS1':
                    tool = 'SERVER'
                ftp = self.LOGIN(tool)
                os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/IDP/NEW/' + tool)
                for idpfile in idplist:
                    try:
                        print('downloading IDP_' + idpfile + '.........')
                        ftp.cwd(idpfile.replace('/', '%', 6).split('/')[0].replace('%', '/'))
                        tmp = open(idpfile.replace('/', '%', 6).split('%')[-1][:-3].replace('/', '_') + 'IDP', 'wb')
                        ftp.retrbinary('RETR ' + idpfile.replace('/', '%', 6).split('/')[1], tmp.write)
                    except:
                        pass

                # download AMP ==================================================================
                os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/AMP/NEW/' + tool)
                for idpfile in idplist:
                    try:
                        print('downloading AMP_' + idpfile + '.........')
                        ftp.cwd(idpfile[:-4])
                        tmp = open(idpfile.replace('/', '%', 6).split('%')[-1][:-4].replace('/', '_'), 'wb')
                        ftp.retrbinary('RETR PRMS0001', tmp.write)
                    except:
                        pass
                ftp.quit()


            except:
                tmp = open(root + 'log.txt', 'a')
                tmp.write('\n' + str1 + "_list of " + tool + ' is not available\n')
                tmp.close()
    def Modified_IDP_AMP_UPLOAD(self):
        #OPEN DB
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        #GOLDEN AMP
        sql = "select * from reference "
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        refCol, ref = [], []
        for i in range(rs.fields.count):
            refCol.append(rs.fields(i).name)
        for i in range(rs.recordcount):
            ref.append([str(rs.fields(j).value).strip() for j in range(rs.fields.count)])
            rs.movenext
        ref = pd.DataFrame(ref, columns=refCol).fillna('')
        refCol = refCol[:-1]
        #DB COLUMNS
        col2 = ['object', 'meas_kind', 'measurement', 'output_data', 'method', 'l_threshold', 'l_direction',
                'l_edge_no', 'l_base_line', 'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line',
                'r_base_area', 'centering', 'inverse', 'assist', 'rot_correct', 'smoothing', 'differential',
                'meas_point', 'diameters', 'sum_lines_point']
        col1 = ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
                'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing',
                'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line', 'l_base_area',
                'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1', 'dummy2', 'dummy3',
                'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering', 'grain_area_x', 'grain_area_y',
                'grain_min', 'high_pass_filter', 'y_design_rule', 'gap_value_mark', 'gap_shape1', 'gap_shape2',
                'corner_edge', 'width_design', 'area_design', 'number_design', 'grain_design', 'inverse', 'method2',
                'score_design', 'score_number_design', 'TOOL', 'IDW', 'IDP']
        #STANDARD PPID FROM MFG DB
        ppidDB = pd.read_excel('P:\\_Script\\ExcelFile\\_PPID.xlsm', sheet_name='PPID')
        ppidDB = ppidDB[['PART', 'STAGE', 'PPID']]
        ppidDB = list(ppidDB.dropna().drop_duplicates()['PPID'])
        #get modified list
        root = 'y:/ModifiedCdSemIDP/'
        toollist = ['DATAS1', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        str1 = str(datetime.datetime.now()).replace('-', '')[0:8]
        # str1 = '20190402'
        filelist = [root + i for i in os.listdir(root) if str1 in i and '-IDP-' in i]

        sql = "update AMP set conclusion='' where conclusion is null"
        conn.execute(sql)

        for tool in toollist[:]:
            try:
                file = [i for i in filelist if tool in i][0]
                idplist = [i.strip() for i in open(file)]
                if tool == 'DATAS1':
                    tool = 'SERVER'

                for idpfile in idplist:
                    print(tool,idpfile)
                    if  idpfile.replace('/', '%', 6).split('%')[-1][:-3].replace('/', '_')[-4:]=='-LN.':
                        # Read IDP and Compare  ===========================================================================
                        idppath = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/NEW/' + tool + '/' + \
                                  idpfile.replace('/', '%', 6).split('%')[-1][:-3].replace('/', '_') + 'IDP'
                        newidp = self.READ_IDP(idppath, tool)
                        if newidp[0] !='NA' and newidp[1][-2:]=='LN':
                            rs = win32com.client.Dispatch(r'ADODB.Recordset')
                            sql = "select * from IDP WHERE IDW='" + newidp[0]
                            sql = sql + "' and IDP='" + newidp[1]
                            sql = sql + "' and Conclusion=True"
                            rs.Open(sql, conn, 1, 3)
                            if rs.RecordCount==0 and newidp[3]!='NA' and newidp[3]!='NA':
                                rs.AddNew()  # 添加一条新记录
                                rs.Fields(0).Value = newidp[0]
                                rs.Fields(1).Value = newidp[1]
                                if newidp[2] == 'NA':
                                    newidp[2] = ''
                                rs.Fields(2).Value = newidp[2]
                                rs.Fields(3).Value = tool

                                rs.Fields(4).Value = newidp[3]
                                rs.Fields(5).Value = newidp[4]
                                rs.Fields(6).Value = str1
                                rs.Fields(7).Value = 'Added'
                                rs.Fields(12).Value = 1
                                rs.Update()  # 更新
                            else:
                                if newidp[3]!='NA' and newidp[4]!='NA':
                                    if abs(eval(rs.Fields(4).value)-newidp[3])>30 or \
                                            abs(eval(rs.Fields(5).value)-newidp[4])>30:
                                        dx = eval(rs.Fields(4).value)-newidp[3]
                                        dy = eval(rs.Fields(5).value)-newidp[4]
                                        rs.AddNew()  # 添加一条新记录
                                        rs.Fields(0).Value = newidp[0]
                                        rs.Fields(1).Value = newidp[1]
                                        if newidp[2] == 'NA':
                                            newidp[2] = ''
                                        rs.Fields(2).Value = newidp[2]
                                        rs.Fields(3).Value = tool
                                        rs.Fields(4).Value = newidp[3]
                                        rs.Fields(5).Value = newidp[4]
                                        rs.Fields(6).Value = str1
                                        rs.Fields(7).Value = 'Revised'
                                        rs.Fields(8).Value = dx
                                        rs.Fields(9).Value = dy
                                        rs.Update()
                        # Read AMP and Compare  ===========================================================================
                        amppath = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/NEW/' + tool + '/' + \
                                  idpfile.replace('/', '%', 6).split('%')[-1][:-4].replace('/', '_')
                        newamp = self.READ_AMP(amppath, tool)
                        tmp = idpfile.replace('/', '%', 6).split('%')[-1][:-4].replace('/', '_').split('_')[0]
                        tmp1= idpfile.replace('/', '%', 6).split('%')[-1][:-4].replace('/', '_').split('_')[1]

                        if tmp[-2:] in ['-A','-N']: # in case of large field with -A/-N for IDW
                            tmp=tmp[:-2]
                        if (tmp + '-' + tmp1) in ppidDB:
                            PPID=1   #PPID FLAG
                        else:
                            PPID=0
                        if newidp[2]=='' or newidp[2]=='NA':
                            IsTemplate=0 #TEMPLATE FLAG
                        else:
                            IsTemplate=1
                        sql = "select * from TECH where PartID='" + tmp + "'"
                        rsTECH = win32com.client.Dispatch(r'ADODB.Recordset')
                        rsTECH.Open(sql, conn, 1, 3)
                        if (rsTECH.recordcount) > 0:
                            Tech =  (rsTECH.fields(2).value)[0:3] #TECH FLAG

                            sql = "select Type from R2R_CD_TEMPLATE where IsHasTemplate=True " \
                                  "and Layer='" + tmp1[0:2] + "' and TechName='" +Tech + "'"
                            rsTemplate = win32com.client.Dispatch(r'ADODB.Recordset')
                            rsTemplate.Open(sql, conn, 1, 3)
                            if (rsTemplate.recordcount) > 0:
                                Type=(rsTemplate.fields(0).value)  #Type FLAG
                        else:
                            Tech=''
                            Type=''


    #--------------------------------------------------
                        tmp = pd.DataFrame(newamp).T
                        tmp.columns=col1 #newamp
                        if len(newamp) == 54:
                            rs = win32com.client.Dispatch(r'ADODB.Recordset')
                            sql = "select * from AMP WHERE IDW='" + newamp[-2]
                            sql = sql + "' and IDP='" + newamp[-1] + "'"
                            sql = sql + " and conclusion<>'错误'"
                            # sql = sql + "' and TOOL='" + tool + "'"
                            # sql = sql + "  and  (FLAG1 IS NULL OR FlAG1 = 'Added' or FLAG1='')"
                            rs.Open(sql, conn, 1, 3)
                            if rs.RecordCount == 0:
                                Flag1='Added'
                                if Tech!='' and Type!='':
                                    if Tech[1] == '1':
                                        if tmp['IDP'][0][:2] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT','TT']:
                                            x = ref[ref['Golden'].str.contains('018METAL')][refCol].reset_index().drop('index',
                                                                                                                       axis=1).T
                                            x = x[x[0] != 'None'].T
                                        elif tmp['IDP'][0][:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:
                                            x = ref[ref['Golden'].str.contains('018HOLE')][refCol].reset_index().drop('index', axis=1).T
                                            x = x[x[0] != 'None'].T
                                        elif tmp['IDP'][0][:2] in ['GT', 'PC']:
                                            x = ref[ref['Golden'].str.contains('018GT')][refCol].reset_index().drop('index', axis=1).T
                                            x = x[x[0] != 'None'].T
                                        elif tmp['IDP'][0][:2] in ['TO']:
                                            x = ref[ref['Golden'].str.contains('018TO')][refCol].reset_index().drop('index', axis=1).T
                                            x = x[x[0] != 'None'].T
                                        elif tmp['IDP'][0][:2] in ['P0']:
                                            x = ref[ref['Golden'].str.contains('018TO')][refCol].reset_index().drop('index', axis=1).T
                                            x = x[x[0] != 'None'].T
                                        else:
                                            if Type == 'Line':
                                                x = ref[ref['Golden'].str.contains('018LINE')][refCol].reset_index().drop('index',
                                                                                                                          axis=1).T
                                                x = x[x[0] != 'None'].T
                                            elif Type == 'Hole/Space':
                                                x = ref[ref['Golden'].str.contains('018SPACE')][refCol].reset_index().drop('index',
                                                                                                                           axis=1).T
                                                x = x[x[0] != 'None'].T
                                            else:
                                                x = pd.DataFrame()
                                    else:
                                        if tmp['IDP'][0][:2] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT', 'TT']:
                                            x = ref[ref['Golden'].str.contains('035METAL')][refCol].reset_index().drop('index',
                                                                                                                       axis=1).T
                                            x = x[x[0] != 'None'].T
                                        elif tmp['IDP'][0][:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:
                                            x = ref[ref['Golden'].str.contains('035HOLE')][refCol].reset_index().drop('index', axis=1).T
                                            x = x[x[0] != 'None'].T
                                        else:
                                            if Type == 'Line':
                                                x = ref[ref['Golden'].str.contains('035LINE')][refCol].reset_index().drop('index',
                                                                                                                          axis=1).T
                                                x = x[x[0] != 'None'].T
                                            elif Type=='Hole/Space':
                                                x = ref[ref['Golden'].str.contains('035SPACE')][refCol].reset_index().drop('index',
                                                                                                                           axis=1).T
                                                x = x[x[0] != 'None'].T
                                            else:
                                                x = pd.DataFrame()
                                    if x.shape[1]>1:
                                        y = tmp[x.columns]
                                        x1 = [str(i) for i in (x.loc[0])]
                                        y1 = [str(i) for i in (y.loc[0])]
                                        if x1!=y1:
                                        # if False in list((x == y).loc[0]):
                                            AutoCheck='Wrong'
                                        else:
                                            AutoCheck='Correct'
                                    else:
                                        AutoCheck='Pending'
                                else:
                                    AutoCheck='Pending'
                            else:
                                Flag1='Revised'
                                tmp=[rs.fields(i).value for i in range (1,55,1)] #existing record,存在多条，只和第一条比
                                tmp = pd.DataFrame([tmp,newamp],columns=col1)[col2]
                                # if False in list(tmp.loc[0]==tmp.loc[1]):
                                if [int(eval(str(x).strip())) for x in list(tmp.loc[0])] == [int(eval(str(y).strip())) for y
                                                                                             in list(tmp.loc[1])]:
                                    AutoCheck=''
                                else:
                                    AutoCheck='MisMatch'

                            if AutoCheck!='':
                                rs.AddNew()  # 添加一条新记录
                                for i in range(1, 57):
                                    if i < 55:
                                        rs.Fields(i).Value = newamp[i - 1]
                                    elif i==55:
                                        rs.Fields(i).Value = str1
                                    else:
                                        pass
                                rs.Fields(56).Value = Flag1
                                rs.Fields(57).Value = PPID
                                rs.Fields(62).Value = IsTemplate
                                rs.Fields(63).Value = Tech
                                rs.Fields(64).Value = Type
                                rs.Fields(65).Value = AutoCheck
                                rs.Update()  # 更新
            except:
                pass
        conn.close
    def MainFunction(self):
        pass

        # ##########
        # BATCH RUN#
        ############
        self.All_PPID_FLAG()
        self.All_TEMPLATE_FLAG()
        self.All_TECH_FLAG()
        self.All_CD_TYPE_FLAG()
        self.All_AutoCheck_Flag()


        # ##########
        # Daily Run#
        ############

        # self.Download_Modified_List()#list of files revised yesterday
        # self.Modified_IDP_AMP_DOWNLOAD()#download idp/amp file
        self.Modified_IDP_AMP_UPLOAD()
        print('=================DONE========================')
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
        # filelist = set(self.get_path(FileDir=r'Y:\OVERLAY\RawDataBackup\KLA'))
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)

        # sql = "select distinct Path from ZB WHERE Obselete=False"
        # rs = win32com.client.Dispatch(r'ADODB.Recordset')
        # rs.Open(sql, conn, 1, 3)
        # old=[]
        # for i in range(rs.recordcount):
        #     old.append(rs.fields('Path').value)
        #     rs.movenext
        # filelist = filelist - set(old)  #to remove [0:-2]

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

                        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
                        sql = "select * from ZB where Ppid='" + Ppid + "'"
                        sql = sql + " and EqpType='Archer' "
                        tmprs.Open(sql, conn, 1, 3)

                        if tmprs.recordcount==0:
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
                            rs.Fields('UploadRiqi').value = str(datetime.datetime.now())[0:20]
                            rs.Fields('EqpType').value = 'Archer'
                            rs.Update()  # 更新
                        else:
                            flag = abs(eval(tmprs.Fields('A1').Value) - tmp[0])<0.00001 and \
                               abs(eval(tmprs.Fields('A2').Value) - tmp[1])<0.001 and \
                               abs(eval(tmprs.Fields('A3').Value) - tmp[2])<0.001 and \
                               abs(eval(tmprs.Fields('A4').Value) - tmp[3])<0.001 and \
                               abs(eval(tmprs.Fields('A5').Value) - tmp[4])<0.001 and \
                               abs(eval(tmprs.Fields('A6').Value) - tmp[5])<0.001 and \
                               abs(eval(tmprs.Fields('A7').Value) - tmp[6])<0.001 and \
                               abs(eval(tmprs.Fields('A8').Value) - tmp[7])<0.001
                            if tmp[8]!='':
                                flag = flag and abs(eval(tmprs.Fields('A9').Value) - tmp[8])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A10').Value) - tmp[9])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A11').Value) - tmp[10])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A12').Value) - tmp[11])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A13').Value) - tmp[12])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A14').Value) - tmp[13])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A15').Value) - tmp[14])<0.001
                                flag = flag and abs(eval(tmprs.Fields('A16').Value) - tmp[15])<0.001
                            if flag==False:
                                sql = "update ZB set A1='" + str(round(tmp[0], 5)) + "'"
                                sql = sql + ",  A2='" + str(round(tmp[1], 5)) + "'"
                                sql = sql + ",  A3='" + str(round(tmp[2], 5)) + "'"
                                sql = sql + ",  A4='" + str(round(tmp[3], 5)) + "'"
                                sql = sql + ",  A5='" + str(round(tmp[4], 5)) + "'"
                                sql = sql + ",  A6='" + str(round(tmp[5] , 5)) + "'"
                                sql = sql + ",  A7='" + str(round(tmp[6], 5)) + "'"
                                sql = sql + ",  A8='" + str(round(tmp[7] , 5)) + "'"
                                sql = sql + ",  Obselete = False"
                                sql = sql + ",UploadRiqi='" + str(datetime.datetime.now())[0:20] + "'"
                                sql = sql + " where Ppid='" + Ppid + "' and EqpType='Archer' and Obselete=False"
                                conn.execute(sql)
                                try:
                                    sql = "update ZB set A9='" + str(round(tmp[8], 5)) + "'"
                                    sql = sql + ",  A10='" + str(round(tmp[9], 5)) + "'"
                                    sql = sql + ",  A11='" + str(round(tmp[10], 5)) + "'"
                                    sql = sql + ",  A12='" + str(round(tmp[11], 5)) + "'"
                                    sql = sql + ",  A13='" + str(round(tmp[12], 5)) + "'"
                                    sql = sql + ",  A14='" + str(round(tmp[13], 5)) + "'"
                                    sql = sql + ",  A15='" + str(round(tmp[14], 5)) + "'"
                                    sql = sql + ",  A16='" + str(round(tmp[15], 5)) + "'"
                                    sql = sql + ",UploadRiqi='" + str(datetime.datetime.now())[0:20] + "'"
                                    sql = sql + " where Ppid='" + Ppid + "' and EqpType='Archer' and Obselete=False"
                                except:
                                    pass
                else:
                    fail.append(file)
            except:
                fail.append(file)
            shutil.move(file, 'Y:/OVERLAY/RawDataBackup/KLA/'+file.split('\\')[3])
        # conn.close

        for i in fail:
            os.remove(i)

        # get Q200 coordinate
        FileDir = 'Z:\\_DailyCheck\\Q200_LD\\'
        # FileDir = "Y:\\OVERLAY\\RawDataBackup\\Q200"
        filenamelist=set(self.get_path(FileDir))
        filenamelist= [i for i in list(filenamelist) if i[-2:]=='ld']
        #filenamelist = list( set(filenamelist) - set(old) )
        #此步有问题：如果文件是后续修改过的，不能用集合减的方法
        filenamelist.sort(reverse=False)
        Q200 = []

        for file in filenamelist[:]:
            data = []
            try:
                f = [i.strip('\n') for i in open(file).readlines()]
                for n, tmp in enumerate(f):
                    if tmp == '\t\t\tlocations=4,':
                        Path=file
                        Ppid=file.split('\\')[-1][:-3]
                        data.extend([eval(f[n + 2][10:].split(',')[0]), eval(f[n + 2][10:].split(',')[1][0:-1])])
                        data.extend([eval(f[n + 4][10:].split(',')[0]), eval(f[n + 4][10:].split(',')[1][0:-1])])
                        data.extend([eval(f[n + 6][10:].split(',')[0]), eval(f[n + 6][10:].split(',')[1][0:-1])])
                        data.extend([eval(f[n + 8][10:].split(',')[0]), eval(f[n + 8][10:].split(',')[1][0:-1])])
                if len(data) == 8:
                    tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
                    sql = "select * from ZB where Ppid='" + Ppid + "'"
                    sql = sql + " and EqpType='Q200'"
                    tmprs.Open(sql, conn, 1, 3)
                    print(tmprs.recordcount)
                    if tmprs.recordcount==0:

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
                        rs.Fields('UploadRiqi').value = str(datetime.datetime.now())[0:20]
                        rs.Fields('EqpType').value = 'Q200'
                        rs.Update()  # 更新
                    else:
                        if abs(eval(tmprs.Fields('A1').Value) - data[0]/1000)<0.00001 and  \
                                abs(eval(tmprs.Fields('A2').Value) - data[1]/1000)<0.001 and  \
                                abs(eval(tmprs.Fields('A3').Value) - data[2]/1000)<0.001 and \
                                abs(eval(tmprs.Fields('A4').Value) - data[3]/1000)<0.001 and   \
                                abs(eval(tmprs.Fields('A5').Value) - data[4]/1000)<0.001 and    \
                                abs(eval(tmprs.Fields('A6').Value) - data[5]/1000)<0.001 and  \
                                abs(eval(tmprs.Fields('A7').Value) - data[6]/1000)<0.001 and \
                                abs(eval(tmprs.Fields('A8').Value) - data[7]/1000)<0.001 :
                            pass
                        else:
                            sql = "update ZB set A1='" + str(round( data[0] / 1000,5))+ "'"
                            sql = sql + ",  A2='" + str(round(data[1] / 1000,5))+ "'"
                            sql = sql + ",  A3='" + str(round(data[2] / 1000,5))+ "'"
                            sql = sql + ",  A4='" + str(round(data[3] / 1000,5))+ "'"
                            sql = sql + ",  A5='" + str(round(data[4] / 1000,5))+ "'"
                            sql = sql + ",  A6='" + str(round(data[5] / 1000,5))+ "'"
                            sql = sql + ",  A7='" + str(round(data[6] / 1000,5))+ "'"
                            sql = sql + ",  A8='" + str(round(data[7] / 1000,5))+ "'"
                            sql = sql + ",UploadRiqi='" + str(datetime.datetime.now())[0:20] + "'"
                            sql = sql + " where Ppid='" + Ppid + "' and EqpType='Q200' and Obselete=False"
                            conn.execute(sql)
            except:
                pass
            shutil.move(file,'Y:/OVERLAY/RawDataBackup/Q200/'+file.split('\\')[-1])
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
        filelist = self.get_path(FileDir=path)
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


        mfg = pd.read_excel('P:\\_Script\\ExcelFile\\_PPID.xlsm', sheet_name='PPID')
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

        new=[]
        for i in range(tmp.shape[0]):
            new.append( tmp.iloc[i,0]+ "_" + tmp.iloc[i,1])

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)

        sql = "Select PART,STAGE from PPID"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        old=[]
        for i in range(rs.recordcount):
            old.append(rs.fields(0).value + "_" + rs.fields(1).value)
            rs.movenext

        new=list(set(new)-set(old))


        sql = "select * from PPID"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)

        tmp=tmp.set_index(['PART','STAGE'])

        for i in new:
            try:
                part=i.split("_")[0]
                stage=i.split('_')[1]
                print(part,stage,tmp.loc[part].loc[stage][0],tmp.loc[part].loc[stage][1],tmp.loc[part].loc[stage][2])
                rs.AddNew()  # 添加一条新记录
                rs.Fields('PART').Value = part
                rs.Fields('STAGE').Value = stage
                rs.Fields('CD').Value = tmp.loc[part].loc[stage][0]
                rs.Fields('ToolType').Value = tmp.loc[part].loc[stage][1]
                rs.Fields('OVL').Value = tmp.loc[part].loc[stage][2]
                rs.Update()  # 更新
            except:
                pass
        conn.close




    def ppid_flag(self):
        #f1 是否有标准坐标
        #f2 bias table是否有测量对准顺序
        #f3 是否有asml stepping size
        #f4 是否有程序坐标
        #AutoCheck OVL自检结果
        #f5 是否已自检
        # f6-->cd run flag
        # f7-->ovl run flag

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

        print("Flag1 DONE  "+ str(datetime.datetime.now()))

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

        print('Flag2 DONE  '+ str(datetime.datetime.now()))
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

        sql = " update PPID set f4=False"
        conn.Execute(sql)  # 错误文件的必需标记为无坐标

        print('Reset F4  ' + str(datetime.datetime.now()))


        for n,ovl in enumerate(tmp):
            print(n,ovl,len(tmp))
            sql = "select Ovl_Ppid from BT where Ovl_Ppid='" + ovl + "'"
            tmprs=win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount>0:
                sql = "update PPID set f2=True where OVL='" + ovl + "'"
                conn.Execute(sql)
            else:
                pass
            #f4-->rcipe coordinate downloaded


            sql = "select Ppid from ZB where Ppid='" + ovl + "' and Obselete=False"
            sql = "select Ppid from ZB where Ppid='" + ovl + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)
            if tmprs.recordcount > 0:
                sql = "update PPID set f4=True where OVL='" + ovl + "'"
                conn.Execute(sql)
            else:
                pass
        conn.close
    def Archer_Check(self,part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i):
        AutoCheck=""
        sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='" + part + "' and Ppid='" + ovlto + "'"
        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
        tmprs.Open(sql, conn, 1, 3)
        if tmprs.recordcount > 0:
            refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value), eval(tmprs.fields('RDx').value),
                     eval(tmprs.fields('RDY').value), eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
                     eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)]  # 类似MIM层次，CE命名不规范，部分ovlto下有多组坐标

        ToolType = tmp[i][1]
        try:
            if stepx != '' and stepy != '' and ovlto != '' and recipeZb != '' and refZb != '':
                if part[-2:].upper() == "-L" and ToolType.upper() == 'LII':
                    c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
                    c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020

                    c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
                    c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020

                    c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
                    c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020

                    c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
                    c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020

                    if c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
                        AutoCheck = 'Correct'
                    else:
                        AutoCheck = 'Wrong'

                    sql = "update PPID set ArcherCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                    conn.Execute(sql)
                elif part[-2:].upper() == "-L" and ToolType.upper() == 'LDI':
                    if len(recipeZb) == 16:
                        c11 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
                        c12 = abs((-refZb[2] / 1000 + stepy / 4) - recipeZb[1]) < 0.020

                        c21 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
                        c22 = abs((-refZb[4] / 1000 + stepy / 4) - recipeZb[3]) < 0.020

                        c31 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
                        c32 = abs((-refZb[6] / 1000 + stepy * 3 / 4) - recipeZb[5]) < 0.020

                        c41 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
                        c42 = abs((-refZb[0] / 1000 + stepy * 3 / 4) - recipeZb[7]) < 0.020

                        c51 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[8]) < 0.020
                        c52 = abs((-refZb[2] / 1000 + stepy * 3 / 4) - recipeZb[9]) < 0.020

                        c61 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[10]) < 0.020
                        c62 = abs((-refZb[4] / 1000 + stepy * 3 / 4) - recipeZb[11]) < 0.020

                        c71 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[12]) < 0.020
                        c72 = abs((-refZb[6] / 1000 + stepy / 4) - recipeZb[13]) < 0.020

                        c81 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[14]) < 0.020
                        c82 = abs((-refZb[0] / 1000 + stepy / 4) - recipeZb[15]) < 0.020
                        if False in [c11, c12, c21, c22, c31, c32, c41, c42, c51, c52, c61, c62, c71, c72, c81, c82]:
                            AutoCheck = 'Wrong'
                        else:
                            AutoCheck = 'Correct'
                        sql = "update PPID set ArcherCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                        conn.Execute(sql)
                    else:
                        sql = "update PPID set ArcherCheck='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
                        conn.Execute(sql)
                        AutoCheck = '!=16points'
                else:
                    c11 = abs((refZb[0] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
                    c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020

                    c21 = abs((refZb[2] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
                    c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020

                    c31 = abs((refZb[4] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
                    c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020

                    c41 = abs((refZb[6] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
                    c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
                    if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
                        AutoCheck = 'Wrong'
                    else:
                        AutoCheck = 'Correct'
                    sql = "update PPID set ArcherCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                    conn.Execute(sql)
            else:
                sql = "update PPID set ArcherCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
                conn.Execute(sql)
                AutoCheck='data not ready'
        except:
            sql = "update PPID set ArcherCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
            conn.Execute(sql)
            AutoCheck = 'data not ready'
        return AutoCheck
    def Q200_Check(self,part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i):
        AutoCheck=''
        sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='" + part + "' and Ppid='" + ovlto + "'"
        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
        tmprs.Open(sql, conn, 1, 3)
        if tmprs.recordcount > 0:
            refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value), eval(tmprs.fields('RDx').value),
                     eval(tmprs.fields('RDY').value), eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
                     eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)]  # 类似MIM层次，CE命名不规范，部分ovlto下有多组坐标

        ToolType = tmp[i][1]
        try:
            if stepx != '' and stepy != '' and ovlto != '' and recipeZb != '' and refZb != '':
                if part[-2:].upper() == "-L" and ToolType.upper() == 'LII':
                    c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
                    c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020

                    c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
                    c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020

                    c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
                    c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020

                    c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
                    c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020

                    if c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
                        AutoCheck = 'Correct'
                    else:
                        AutoCheck = 'Wrong'

                    sql = "update PPID set Q200Check='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                    conn.Execute(sql)
                elif part[-2:].upper() == "-L" and ToolType.upper() == 'LDI':
                    if len(recipeZb) == 16:
                        c11 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
                        c12 = abs((-refZb[2] / 1000 + stepy / 4) - recipeZb[1]) < 0.020

                        c21 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
                        c22 = abs((-refZb[4] / 1000 + stepy / 4) - recipeZb[3]) < 0.020

                        c31 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
                        c32 = abs((-refZb[6] / 1000 + stepy * 3 / 4) - recipeZb[5]) < 0.020

                        c41 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
                        c42 = abs((-refZb[0] / 1000 + stepy * 3 / 4) - recipeZb[7]) < 0.020

                        c51 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[8]) < 0.020
                        c52 = abs((-refZb[2] / 1000 + stepy * 3 / 4) - recipeZb[9]) < 0.020

                        c61 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[10]) < 0.020
                        c62 = abs((-refZb[4] / 1000 + stepy * 3 / 4) - recipeZb[11]) < 0.020

                        c71 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[12]) < 0.020
                        c72 = abs((-refZb[6] / 1000 + stepy / 4) - recipeZb[13]) < 0.020

                        c81 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[14]) < 0.020
                        c82 = abs((-refZb[0] / 1000 + stepy / 4) - recipeZb[15]) < 0.020
                        if False in [c11, c12, c21, c22, c31, c32, c41, c42, c51, c52, c61, c62, c71, c72, c81, c82]:
                            AutoCheck = 'Wrong'
                        else:
                            AutoCheck = 'Correct'
                        sql = "update PPID set Q200Check='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                        conn.Execute(sql)
                    else:
                        sql = "update PPID set Q200Check='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
                        conn.Execute(sql)
                        AutoCheck='!=16points'
                else:
                    c11 = abs((refZb[0] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
                    c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020

                    c21 = abs((refZb[2] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
                    c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020

                    c31 = abs((refZb[4] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
                    c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020

                    c41 = abs((refZb[6] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
                    c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
                    if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
                        AutoCheck = 'Wrong'
                    else:
                        AutoCheck = 'Correct'
                    sql = "update PPID set Q200Check='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
                    conn.Execute(sql)
            else:
                sql = "update PPID set Q200Check='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
                conn.Execute(sql)
                AutoCheck='data not ready'
        except:
            sql = "update PPID set Q200Check='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
            conn.Execute(sql)
            AutoCheck = 'data not ready'
        return AutoCheck
    def auto_check(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)

        sql = "update PPID set AutoCheck=''"
        conn.execute(sql)

        sql = "select PART,ToolType,OVL from PPID where f4=True and f3=True and f2=True and f1=True"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp=[]

        for i in range(rs.recordcount):
            tmp.append([rs.fields('PART').value,rs.fields('ToolType').value,rs.fields('OVL').value])
            rs.movenext

        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        count = 0
        ddd=[]
        for i in range(len(tmp)):
            # print(i,len(tmp),tmp[i])
            ArcherFlag,Q200Flag='',''

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

            sql = "Select A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,Count,EqpType from ZB where Ppid='" + ppid + "'"
            tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            tmprs.Open(sql, conn, 1, 3)


            recipeZb=[eval(tmprs.fields(k).value) for k in range(int(eval(tmprs.fields('Count').value)))]
            for no in range(tmprs.recordcount):
                eqp= tmprs.fields('EqpType').value
                tmprs.movenext
                if eqp=='Archer':
                    ArcherFlag = self.Archer_Check(part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i)
                else:
                    Q200Flag = self.Q200_Check(part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i)
            print(i, len(tmp), tmp[i],'ArcherFlag='+ArcherFlag,'Q200Flag='+Q200Flag)

            AutoCheck=ArcherFlag + ", " + Q200Flag
            #TODO to ovlcheck for Q200 and Archer10
            #Parameter: part,ovlto,conn,refZB,tmp,stepx,stepy,recipeZB



            # sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='"+part + "' and Ppid='"+ ovlto + "'"
            # tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
            # tmprs.Open(sql, conn, 1, 3)
            # if tmprs.recordcount>0:
            #     refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value),
            #              eval(tmprs.fields('RDx').value), eval(tmprs.fields('RDY').value),
            #              eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
            #              eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)] #类似MIM层次，CE命名不规范，部分ovlto下有多组坐标
            #
            # ToolType=tmp[i][1]
            #
            # if stepx!='' and stepy!='' and ovlto!='' and recipeZb!='' and refZb!='':
            #     if part[-2:].upper()=="-L" and ToolType.upper()=='LII':
            #         c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
            #         c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020
            #
            #         c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
            #         c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020
            #
            #         c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
            #         c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020
            #
            #         c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
            #         c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020
            #
            #         if  c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
            #             AutoCheck='Correct'
            #         else:
            #             AutoCheck='Wrong'
            #
            #         sql = "update PPID set AutoCheck='" + AutoCheck +   "' where PART='"+part + "' and OVL='"+ppid + "'"
            #         conn.Execute(sql)
            #     elif part[-2:].upper()=="-L" and ToolType.upper()=='LDI':
            #         if len(recipeZb)==16:
            #             c11 = abs((refZb[3] / 1000  + stepx / 2) - recipeZb[0]) < 0.020
            #             c12 = abs((-refZb[2] / 1000  + stepy / 4) - recipeZb[1]) < 0.020
            #
            #             c21 = abs((refZb[5] / 1000  + stepx / 2) - recipeZb[2]) < 0.020
            #             c22 = abs((-refZb[4] / 1000  + stepy / 4) - recipeZb[3]) < 0.020
            #
            #             c31 = abs((refZb[7] / 1000  + stepx / 2) - recipeZb[4]) < 0.020
            #             c32 = abs((-refZb[6] / 1000  + stepy * 3 / 4) - recipeZb[5]) < 0.020
            #
            #             c41 = abs((refZb[1] / 1000  + stepx / 2) - recipeZb[6]) < 0.020
            #             c42 = abs((-refZb[0] / 1000  + stepy * 3 / 4) - recipeZb[7]) < 0.020
            #
            #             c51 = abs((refZb[3] / 1000  + stepx / 2) - recipeZb[8]) < 0.020
            #             c52 = abs((-refZb[2] / 1000  + stepy * 3 / 4) - recipeZb[9]) < 0.020
            #
            #             c61 = abs((refZb[5] / 1000  + stepx / 2) - recipeZb[10]) < 0.020
            #             c62 = abs((-refZb[4] / 1000  + stepy * 3 / 4) - recipeZb[11]) < 0.020
            #
            #             c71 = abs((refZb[7] / 1000  + stepx / 2) - recipeZb[12]) < 0.020
            #             c72 = abs((-refZb[6] / 1000  + stepy / 4) - recipeZb[13]) < 0.020
            #
            #             c81 = abs((refZb[1] / 1000  + stepx / 2) - recipeZb[14]) < 0.020
            #             c82 = abs((-refZb[0] / 1000  + stepy / 4) - recipeZb[15]) < 0.020
            #             if False in [c11,c12,c21,c22,c31,c32,c41,c42,c51,c52,c61,c62,c71,c72,c81,c82]:
            #                 AutoCheck='Wrong'
            #             else:
            #                 AutoCheck = 'Correct'
            #             sql = "update PPID set AutoCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
            #             conn.Execute(sql)
            #         else:
            #             sql = "update PPID set AutoCheck='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
            #             conn.Execute(sql)
            #     else:
            #         c11 = abs((refZb[0] / 1000 + stepx  / 2) - recipeZb[0]) < 0.020
            #         c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020
            #
            #         c21 = abs((refZb[2] / 1000 + stepx  / 2) - recipeZb[2]) < 0.020
            #         c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020
            #
            #         c31 = abs((refZb[4] / 1000 + stepx  / 2) - recipeZb[4]) < 0.020
            #         c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020
            #
            #         c41 = abs((refZb[6] / 1000 + stepx  / 2) - recipeZb[6]) < 0.020
            #         c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
            #         if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
            #             AutoCheck = 'Wrong'
            #         else:
            #             AutoCheck = 'Correct'
            #         sql = "update PPID set AutoCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
            #         conn.Execute(sql)
            # else:
            #     sql = "update PPID set AutoCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
            #     conn.Execute(sql)

            sql = "update PPID set f5=True,RiQi='"+str(datetime.datetime.now())[:10].replace('-','')+"',AutoCheck='"+AutoCheck+"' where PART='"+ part + "' and OVL='" + ppid + "'"
            # sql = "update PPID set f5=True where PART='" + part + "' and OVL='" + ppid + "'"
            conn.Execute(sql)
        conn.close
    def MainFunction(self):
        pass
        self.read_raw_data()
        print('OVL_001')
        self.read_standard_coordinate()
        print('OVL_002')
        self.read_asml_step_size()
        print('OVL_003')
        self.read_bias_table()
        print('OVL_004')
        # self.refresh_ppid_from_xls()  #too long
        print('OVL_005')
        self.ppid_flag()
        print('OVL_006')
        self.auto_check()
        print('OVL_007')
        # to create new function for data of single part/layer only
class QcCduOvl:
    def __init__(self):
        pass

    def cdu_plot(self,tool, x, y):

        fig = plt.figure(figsize=(16, 12))
        ax = fig.add_subplot(1, 1, 1)

        ax.boxplot(y, vert=True)

        ax.set_xticklabels(x, rotation=90)

        ax.yaxis.grid(True)
        ax.set_xlabel(tool + '_CDU')
        # ax.set_ylabel('CD Target=0.50')

        if tool in (
        'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14', 'ALII15',
        'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'ALSIBJ', 'BLSIBK', 'BLSIBL', 'BLSIE1'):
            plt.ylim(0.47, 0.53)
            plt.hlines(0.50, 1, len(x), linewidth=2, color='red')
            plt.hlines(0.48, 1, len(x), linewidth=2, color='red')
            plt.hlines(0.52, 1, len(x), linewidth=2, color='red')
        elif tool in ('ALDI05', 'ALDI11', 'ALDI12', 'BLDI08', 'BLDI13'):
            plt.ylim(0.235, 0.265)
            plt.hlines(0.25, 1, len(x), linewidth=2, color='red')
            plt.hlines(0.24, 1, len(x), linewidth=2, color='red')
            plt.hlines(0.26, 1, len(x), linewidth=2, color='red')
        else:
            plt.ylim(0.145, 0.175)
            plt.hlines(0.16, 1, len(x), linewidth=2, color='red')
            plt.hlines(0.15, 1, len(x), linewidth=2, color='red')
            plt.hlines(0.17, 1, len(x), linewidth=2, color='red')
        plt.savefig('z:\\_DailyCheck\\OptOvl_Others\\QcCdu\\' + tool, dpi=100, bbox_inches='tight')  # plt.show()
    def align_xaxis(self,ax2, ax1, x1, x2):
        "maps xlim of ax2 to x1（lower lim） and x2（upper lim） in ax1"
        (x1, _), (x2, _) = ax2.transData.inverted().transform(ax1.transData.transform([[x1, 0], [x2, 0]]))
        xs, xe = ax2.get_xlim()
        k, b = np.polyfit([x1, x2], [xs, xe], 1)
        ax2.set_xlim(xs * k + b, xe * k + b)
    def ovl_plot(self,tool, sensor, output):

        print(tool, '   ', sensor, '   ', output.shape)

        x1 = [str(datetime.datetime.date(abc)) for abc in output['Dcoll_Time']]
        x = [i + 1 for i in range(len(x1))]

        fig = plt.figure(figsize=(18, 10))
        # -----------------------------------
        ax1 = fig.add_subplot(2, 2, 1)
        plt.xticks(x, x1, rotation=90)
        plt.violinplot(np.array(output[['TranX_OX_min', 'TranX_OX_max', 'TranX_Met_Value']].T), showmeans=False,
                       showmedians=True)
        ax1.set_ylabel('TranX_Max-Min-Met')
        ax1.yaxis.grid(True)
        ax1.set_title(tool + ' ' + sensor + ' TranX')
        # -----------------------------------
        ax2 = fig.add_subplot(2, 2, 2)
        plt.xticks(x, x1, rotation=90)
        plt.violinplot(np.array(output[['TranY_OY_min', 'TranY_OY_max', 'TranY_Met_Value']].T), showmeans=False,
                       showmedians=True)
        ax2.set_ylabel('TranY_Max-Min-Met')
        ax2.yaxis.grid(True)
        # plt.legend(loc='lower center',ncol=4,bbox_to_anchor=(0., 1.02, 1., .102),mode="expand", borderaxespad=0.)
        ax2.set_title(tool + ' ' + sensor + ' TranY')

        # -----------------------------------

        ax3 = fig.add_subplot(2, 2, 3)

        plt.xticks(x, x1, rotation=90)
        plt.plot(x, output['ScalX_Met_Value'], linewidth='1', label='W.Sca.X', color='deepskyblue',
                 marker='v')  # , linestyle=':')#, marker='v')
        plt.plot(x, output['ScalY_Met_Value'], linewidth='1', label='W.Sca.Y', color='blue',
                 marker='v')  # linestyle=':', marker='^')
        plt.plot(x, output['ORT_Met_Value'], linewidth='1', label='Ort', color='red',
                 marker='v')  # linestyle='--', marker='o')
        plt.plot(x, output['Wrot_Met_Value'], linewidth='1', label='W.Rot', color='green',
                 marker='v')  # linestyle='--', marker='o')
        self.align_xaxis(ax3, ax1, 1, len(x1))

        plt.legend(loc='lower center', ncol=4, bbox_to_anchor=(0., 1.02, 1., .102), mode="expand", borderaxespad=0.)
        # plt.xticks(x, x1.values, rotation=90)
        ax3.set_ylabel('InterField')
        # ax3.set_xlabel(tool + ' '+ sensor)
        ax3.yaxis.grid(True)

        # -----------------------------------
        ax4 = fig.add_subplot(2, 2, 4)
        plt.xticks(x, x1, rotation=90)
        plt.plot(x, output['Mag_Met_Value'], linewidth='1', label='Mag.', color='deepskyblue',
                 marker='v')  # linestyle=':', marker='v')
        plt.plot(x, output['Rot_Met_Value'], linewidth='1', label='Rot.', color='blue',
                 marker='v')  # linestyle=':', marker='^')
        plt.plot(x, output['ARMAG_Met_Value'], linewidth='1', label='AsyM', color='red',
                 marker='v')  # linestyle='--', marker='o')
        plt.plot(x, output['ARRot_Met_Value'], linewidth='1', label='AsyR', color='green',
                 marker='v')  # linestyle='--', marker='o')
        self.align_xaxis(ax4, ax2, 1, len(x1))

        plt.legend(loc='lower center', ncol=4, bbox_to_anchor=(0., 1.02, 1., .102), mode="expand", borderaxespad=0.)
        # plt.xticks(x, x1.values, rotation=90)
        ax4.set_ylabel('IntraField')
        # ax4.set_xlabel(tool + ' '+ sensor)
        ax4.yaxis.grid(True)
        fig.subplots_adjust(hspace=0.4)
        plt.savefig('z:\\_DailyCheck\\OptOvl_Others\\QcOvl\\' + tool + '_' + sensor, dpi=100, bbox_inches='tight')
    def cdu_ovl_qc(self):#所有设备CDU——QC数据
        n = 60 #define time period for data extraction
        databasepath = r'Z:\_DailyCheck\Database\data.mdb'  
        enddate = datetime.datetime.now().date()
        startdate = enddate - datetime.timedelta(days=n)
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = '  + databasepath
        conn.Open(DSN)  
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "SELECT * FROM CD_data WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"

        sql = "SELECT  \
              Proc_EqID, Dcoll_Time, \
              TranX_Optimum, TranY_Optimum,  ScalX_Optimum, ScalY_Optimum, \
              ORT_Optimum,  Wrot_Optimum, Mag_Optimum, Rot_Optimum,  ARMag_Optimum, ARRot_Optimum \
              FROM CD_date WHERE (Dcoll_Time>#" + str(startdate) + "#) ORDER BY Dcoll_Time"
        
        sql = "SELECT PartID,  \
              Proc_EqID, Dcoll_Time, \
              [1],[2],[3],[4],[5],[6],[7],[8],[9] \
              FROM CD_data WHERE (Dcoll_Time>#" + str(startdate) + "#)  \
              AND (PartID in ('2LXXXSCD01', '2LXXXSCD02', '2LXXXSCD03', '2LXXXSCD04','2LXXXSCD05','2LXXXUCD01','2LXXXUCD02','2LXXXVCD01','2LXXXvCD02')) \
              ORDER BY Dcoll_Time"

        rs.Open(sql, conn, 1, 3)

        cd = []
        rs.MoveFirst()
        
        while True:
            if rs.EOF:
                break
            else:                
                cd.append ([rs.Fields.Item(i).Value for i in range( rs.Fields.Count)] )
                rs.MoveNext()
        
        rs.close
        cd = pd.DataFrame(cd)
        cd.columns = ['PartID','Proc EqID','Dcoll Time','1','2','3','4','5','6','7','8','9']
        toollist = ('ALII01', 'ALII02', 'ALII03',
           'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13',
           'ALII14', 'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6',
           'ALSIB7', 'ALSIB8', 'ALSIB9', 'ALSIBJ', 'BLSIBK', 'BLSIBL', 
           'BLSIE1','ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07', 
           'ALDI09','ALDI10', 'ALDI11', 'ALDI12','BLDI08', 'BLDI13')
        
        for tool in toollist:
            
            y = cd[cd['Proc EqID'] == tool][['1', '2', '3', '4', '5', '6', '7', '8', '9']]
            x = cd[cd['Proc EqID'] == tool] ['Dcoll Time']
            if len(y)>0:
                x = [str(i)[0:-6] for i in x]
                y = np.array(y).T
            
                self.cdu_plot(tool,x,y)
                print(tool + " CDU")
                plt.clf() # 清图。
                plt.cla() # 清坐标轴。
                plt.close() # 关窗口
                gc.collect()

        sql = "SELECT PartID, \
              Proc_EqID, Dcoll_Time, \
              TranX_OX_min,TranX_OX_max,TranX_Met_Value, \
              TranY_OY_min,TranY_OY_max,TranY_Met_Value, \
              ScalX_Met_Value, ScalY_Met_Value,ORT_Met_Value,Wrot_Met_Value, \
              Mag_Met_Value ,Rot_Met_Value,ARMAG_Met_Value,ARRot_Met_Value \
              FROM OL_ASML WHERE ((Dcoll_Time>#" + str(startdate) + "#)  \
              AND (PartID ='2LXXXSPM01')) ORDER BY Dcoll_Time"
        rs.Open(sql, conn, 1, 3)
    
        ovl = []
        rs.MoveFirst()
        
        while True:
            if rs.EOF:
                break
            else:                
                ovl.append ([rs.Fields.Item(i).Value for i in range( rs.Fields.Count)] )
                rs.MoveNext()
        
        rs.close
        ovl = pd.DataFrame(ovl)
        ovl.columns =[rs.Fields[i].Name for i in range( rs.Fields.Count)]
        tmp = ovl.columns

        for tool in toollist:
            if 'DI' in tool:
                sensor = 'SPM'
                output =  ovl[  ovl['Proc_EqID'].str.contains (tool) ]
                if output.shape[0]>0:
                    self.ovl_plot(tool,sensor,output)
                    plt.clf() # 清图。
                    plt.cla() # 清坐标轴。
                    plt.close() # 关窗口
                    gc.collect()
                    
                
    
    
        sql = "SELECT  PartID, \
              Proc_EqID, Dcoll_Time, \
              OffsetX_OX_min,OffsetX_OX_max,OffsetX_Met_Value, \
              OffsetY_OY_min,OffsetY_OY_max,OffsetY_Met_Value, \
              ScalX_Met_Value, ScalY_Met_Value,ORT_Met_Value,Wrot_Met_Value, \
              Mag_Met_Value ,Rot_Met_Value \
              FROM OL_Nikon WHERE ((Dcoll_Time>#" + str(startdate) + "#)  \
              and (PartID like '2LXXX%')) ORDER BY Dcoll_Time"
        rs.Open(sql, conn, 1, 3)
    
        ovl = []
        rs.MoveFirst()
        
        while True:
            if rs.EOF:
                break
            else:                
                ovl.append ([rs.Fields.Item(i).Value for i in range( rs.Fields.Count)] )
                rs.MoveNext()
        
        rs.close
        ovl = pd.DataFrame(ovl)
        
        ovl['s'] = 0
        ovl ['ss'] = 0
        ovl.columns = tmp
    
    
        for tool in toollist:
            if not ('DI' in tool ):
    
                sensor = 'LSA'        
                output =  ovl[  (ovl['Proc_EqID'].str.contains (tool)) & (ovl['PartID'].str.contains('LSA')) ]
                if output.shape[0] > 0:
                    self.ovl_plot(tool,sensor,output)
                    plt.clf() # 清图。
                    plt.cla() # 清坐标轴。
                    plt.close() # 关窗口
                    gc.collect()
            
                sensor = 'FIA'
                output =  ovl[  (ovl['Proc_EqID'].str.contains (tool)) & (ovl['PartID'].str.contains('FIA')) ]
                if output.shape[0] > 0:
                    self.ovl_plot(tool,sensor,output)
            
       
                    plt.clf() # 清图。
                    plt.cla() # 清坐标轴。
                    plt.close() # 关窗口
                    gc.collect()
    def MainFunction(self):
        self.cdu_ovl_qc()
class MovePpidExcel:
    def __init__(self):
        pass
    def Excel_Macro(self):
        # Run Excel Macro
        xlApp = win32com.client.DispatchEx('Excel.Application')
        xlsPath = os.path.expanduser('P:\\_Script\\ExcelFile\\move.xlsm')
        wb = xlApp.Workbooks.Open(Filename=xlsPath)
        xlApp.Run('TotalPpid')
        wb.Save()
        xlApp.Quit()
    def RefreshPpidTable(self):
        pass
        #f6-->cd run flag
        #f7-->ovl run flag
        databasepath = r'Z:\_database\LithoTool.mdb'
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "select * from MOVE_CD_PPID"
        rs.Open(sql, conn, 1, 3)
        cdppid=[]
        for i in range(rs.recordcount):
            cdppid.append(rs.fields('cdPpid').value)
            rs.movenext

        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "select * from PPID where f6=False"
        rs.Open(sql,conn,1,3)
        allcdppid=[]
        for i in range(rs.recordcount):
            allcdppid.append(rs.fields('CD').value)
            rs.movenext


        tmp = list(set(cdppid) & set(allcdppid))
        for no,i in enumerate(tmp):
            print(no,len(tmp),i)
            sql = "UPDATE PPID SET f6=TRUE WHERE CD = '" + i + "'"
            conn.Execute(sql)

        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "select * from MOVE_OVL_PPID"
        rs.Open(sql, conn, 1, 3)
        ovlppid = []
        for i in range(rs.recordcount):
            ovlppid.append(rs.fields('ovlPpid').value)
            rs.movenext

        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "select * from PPID where f7=False"
        rs.Open(sql, conn, 1, 3)
        allovlppid = []
        for i in range(rs.recordcount):
            allovlppid.append(rs.fields('OVL').value)
            rs.movenext


        tmp = list(set(ovlppid) & set(allovlppid))
        for no, i in enumerate(tmp):
            print(no, len(tmp), i)
            sql = "UPDATE PPID SET f7=TRUE WHERE OVL = '" + i + "'"
            conn.Execute(sql)
        conn.close
    def MainFunction(self):
        # self.Excel_Macro()
        # os.system('P:\\_Script\\ExcelFile\\move.xlsm')
        df=pd.read_excel('P:\\_Script\\ExcelFile\\move.xlsm')
        cd=df[['cdPpid']].dropna()
        ovl=df[['ovlPpid']].dropna()

        databasepath = r'Z:\_database\LithoTool.mdb'
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = " SELECT cdPpid  FROM MOVE_CD_PPID "
        rs.Open(sql, conn, 1, 3)
        old=[]
        print(rs.recordcount)
        if rs.recordcount>0:
            for i in range(rs.recordcount):
                old.append(rs.fields(0).value)
                rs.movenext

        cdlist = list(set(list(cd['cdPpid'])) - set(old))
        if len(cdlist)>0:
            for n,ppid in enumerate(cdlist):
                print(n,len(cdlist))
                rs.addnew
                rs.fields('cdPpid').value=ppid
                rs.update

        old=[]
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = " SELECT ovlPpid  FROM MOVE_OVL_PPID "
        rs.Open(sql, conn, 1, 3)
        old = []
        print(rs.recordcount)
        if rs.recordcount > 0:
            for i in range(rs.recordcount):
                old.append(rs.fields(0).value)
                rs.movenext

        ovllist = list(set(list(ovl['ovlPpid'])) - set(old))
        if len(ovllist) > 0:
            for n, ppid in enumerate(ovllist):
                print(n, len(ovllist))
                rs.addnew
                rs.fields('ovlPpid').value = ppid
                rs.update
        conn.close

        self.RefreshPpidTable()

        #16233
        #15656
class CD_SEM_111_GOLDEN_AMP:#obselete
    # run  at 10.4.3.111
    def __init__(self):
        pass
    def LOGIN(self,tool):
        # timeout = 30
        # port = 21
        IPaddress = {'ALCD01': '10.4.152.56', 'ALCD02': '10.4.152.53', 'ALCD03': '10.4.152.54', 'ALCD04': '10.4.151.79',
                     'ALCD05': '10.4.151.81', 'ALCD06': '10.4.151.82', 'ALCD07': '10.4.151.50', 'ALCD08': '10.4.153.26',
                     'ALCD09': '10.4.152.55', 'ALCD10': '10.4.153.32', 'BLCD11': '10.4.131.48', 'BLCD12': '10.4.131.47',
                     'SERVER': '10.4.72.240'}
        HOST = IPaddress[tool]
        user = 'user02'
        password = 'qw!1234'
        try:
            ftp = ftplib.FTP(HOST)
        except ftplib.error_perm:
            print('无法连接到"%s"' % HOST)
            return
        print('连接到"%s"' % HOST)

        try:
            # user是FTP用户名，pwd就是密码了
            ftp.login(user, password)
        except ftplib.error_perm:
            print('登录失败')
            ftp.quit()
            return
        print('登陆成功')
        return ftp
    def DIGITAL_FOLDER(self,ftp):
        root_dir = r'/HITACHI/DEVICE/HD/'
        try:
            # 得工作目录
            ftp.cwd(root_dir)
        except ftplib.error_perm:
            print('列出当前目录失败')
            # ftp.quit()
            return
        tmp = ftp.nlst()
        digitalFolder = [x for x in tmp if (len(x) == 2 and x.isdigit())]
        digitalFolder.append('XX')
        # ftp.retrlines('LIST')
        return digitalFolder
    def IDW_FOLDER_IDW_DOWNLOAD(self,ftp, folder1, tool):
        root_dir = r'/HITACHI/DEVICE/HD/' + folder1 + '/data'
        localDir = 'Z:/_DailyCheck/CD_SEM_Recipe/IDW/' + tool
        idwFile = []
        idwFolder = []

        # idw folder and idw file
        try:
            ftp.cwd(root_dir)
            tmp = ftp.nlst()

            idwFile = [x for x in tmp if '.idw' in x and "lock" not in x]
            os.chdir(localDir)
            ftp.cwd(root_dir)
            idwFile.sort()
            for n, idw in enumerate(idwFile):
                print('IDW_DOWNLOAD', n, len(idwFile), idw, folder1, tool)
                try:
                    file = open(idw, 'wb')
                    ftp.retrbinary('RETR ' + idw, file.write)
                except:
                    pass

            idwFolder = [x for x in tmp if '.idw' not in x and "lock" not in x]
            return idwFolder
        except:
            pass
            return idwFolder
    def IDP_FOLDER_IDP_DOWNLOAD(self,ftp, folder1, folder2, tool):
        root_dir = r'/HITACHI/DEVICE/HD/' + folder1 + '/data/' + folder2
        localDir = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/' + tool
        idpFile = []
        idpFolder = []

        # idp folder and idp file
        try:
            ftp.cwd(root_dir)
            tmp = ftp.nlst()
            idpFile = [x for x in tmp if '.idp' in x and "lock" not in x]
            os.chdir(localDir)
            ftp.cwd(root_dir)
            idpFile.sort()
            for n, idp in enumerate(idpFile):
                print('IDP_DOWNLOAD', n, len(idpFile), idp, folder2, tool)
                try:
                    file = open(folder2 + "_" + idp, 'wb')
                    ftp.retrbinary('RETR ' + idp, file.write)
                except:
                    pass

            idpFolder = [x for x in tmp if '.idp' not in x and "lock" not in x]
            return idpFolder
        except:
            pass

        # download idp file
    def DOWNLOAD_ALL_IDW_IDP_AMP(self): #download file
        Toollist = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        for tool in Toollist[:]:
            print(tool)
            try:
                ftp = self.LOGIN(tool)
                digitalFolder = self.DIGITAL_FOLDER(ftp)
                digitalFolder.sort()
                for folder1 in digitalFolder:
                    idwFolder = self.IDW_FOLDER_IDW_DOWNLOAD(ftp, folder1, tool)
                    idwFolder.sort()
                    for folder2 in idwFolder:
                        # print(folder2)
                        idpFolder = self.IDP_FOLDER_IDP_DOWNLOAD(ftp, folder1, folder2, tool)
                        os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + tool)
                        idpFolder.sort()
                        for amp in idpFolder:

                            ftp.cwd('/HITACHI/DEVICE/HD/' + folder1 + '/data/' + folder2 + '/' + amp)
                            print('AMP_DOWNLOAD', folder1, folder2, amp)
                            try:
                                file = open(folder2 + "_" + amp, 'wb')
                                ftp.retrbinary('RETR PRMS0001', file.write)
                            except:
                                pass
                ftp.quit()

            except:
                pass
    #functions above is to download All IDW/IDP/AMP
    # two functions below are for AMP extraction
    def ALL_IDP_AMP_EXTRACTION(self):
        root1 = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/'
        root2 = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'
        toollist = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        for tool in toollist[:]:
            idpfilelist = [root1 + tool + '/' + i for i in os.listdir(root1 + tool) if i[-6:-4]=='LN']
            ampfilelist = [root2 + tool + '/' + i for i in os.listdir(root2 + tool) if i[-2:]=='LN']
            idpfilelist.sort()
            ampfilelist.sort()

            # extract AMP data
            amp = []
            for k, file in enumerate(ampfilelist):
                print(tool,k, 'AMP', len(ampfilelist), file)
                try:
                    f = [i.strip() for i in open(file) if '_dif' not in i]

                    if len(f) == 54 or len(f) == 55:
                        for index, i in enumerate(f):
                            if 'comment   ' in i:
                                f = [i.split(":")[1].strip() for i in f[index + 1:]]
                                f.append(tool)
                                f.append(file.split('/')[5][:file.split('/')[5].find('_', 1)])
                                f.append(file.split('/')[5][file.split('/')[5].find('_', 1) + 1:])
                        amp.append(f)

                except:
                    pass
            df = pd.DataFrame(amp)
            df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_ampMay.csv', index=None, header=None, mode='a')

            # extract IDP data
            # idp = []
            # for k, file in enumerate(idpfilelist):
            #     print(tool,k, 'IDP', len(idpfilelist), file)
            #     try:
            #         tmp = ['NA' for i in range(6)]
            #         f = [i.strip() for i in open(file)]
            #         if len(f) > 25:
            #             for index, i in enumerate(f):
            #                 if 'idw_name' in i:
            #                     tmp[0] = i.split(':')[1].strip()
            #                     if 'FEM' in tmp[0] or 'MAP' in tmp[0]:
            #                         break
            #                 if 'idp_name ' in i:
            #                     tmp[1] = i.split(':')[1].strip()
            #                     if tmp[1][-2:] != 'LN' or 'FEM' in tmp[1] or 'MAP' in tmp[1]:
            #                         break
            #                 if 'template   : MS : 1' in i:
            #                     tmp[2] = i.split(',')[1].strip()
            #                 if 'msr_point  :      1 :' in i:
            #                     tmp[3] = int(int(i.split(',')[2]) / 1000)
            #                     tmp[4] = int(int(i.split(',')[3]) / 1000)
            #                     tmp[5] = tool
            #                     idp.append(tmp)
            #                     break
            #     except:
            #         pass
            #
            # df = pd.DataFrame(idp)
            # df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_idpMay.csv', index=None, header=None, mode='a')
    def ALL_AMP_ONLY_EXTRACTION(self):
        root1 = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/'
        root2 = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'
        toollist = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        for tool in toollist[:]:
            idpfilelist = [root1 + tool + '/' + i for i in os.listdir(root1 + tool) if i[-6:-4]=='LN']
            ampfilelist = [root2 + tool + '/' + i for i in os.listdir(root2 + tool) if i[-2:]=='LN']
            idpfilelist.sort()
            ampfilelist.sort()

            # extract AMP data
            amp = []
            for k, file in enumerate(ampfilelist):
                print(tool,k, 'AMP', len(ampfilelist), file)
                try:
                    f = [i.strip() for i in open(file) if '_dif' not in i]

                    if len(f) == 54 or len(f) == 55:
                        for index, i in enumerate(f):
                            if 'comment   ' in i:
                                f = [i.split(":")[1].strip() for i in f[index + 1:]]
                                f.append(tool)
                                f.append(file.split('/')[5][:file.split('/')[5].find('_', 1)])
                                f.append(file.split('/')[5][file.split('/')[5].find('_', 1) + 1:])
                        amp.append(f)

                except:
                    pass
            df = pd.DataFrame(amp)
            df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_ampMay.csv', index=None, header=None, mode='a')

            # extract IDP data
            # idp = []
            # for k, file in enumerate(idpfilelist):
            #     print(tool,k, 'IDP', len(idpfilelist), file)
            #     try:
            #         tmp = ['NA' for i in range(6)]
            #         f = [i.strip() for i in open(file)]
            #         if len(f) > 25:
            #             for index, i in enumerate(f):
            #                 if 'idw_name' in i:
            #                     tmp[0] = i.split(':')[1].strip()
            #                     if 'FEM' in tmp[0] or 'MAP' in tmp[0]:
            #                         break
            #                 if 'idp_name ' in i:
            #                     tmp[1] = i.split(':')[1].strip()
            #                     if tmp[1][-2:] != 'LN' or 'FEM' in tmp[1] or 'MAP' in tmp[1]:
            #                         break
            #                 if 'template   : MS : 1' in i:
            #                     tmp[2] = i.split(',')[1].strip()
            #                 if 'msr_point  :      1 :' in i:
            #                     tmp[3] = int(int(i.split(',')[2]) / 1000)
            #                     tmp[4] = int(int(i.split(',')[3]) / 1000)
            #                     tmp[5] = tool
            #                     idp.append(tmp)
            #                     break
            #     except:
            #         pass
            #
            # df = pd.DataFrame(idp)
            # df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_idpMay.csv', index=None, header=None, mode='a')
    #function below is to get recipe ID with different setting
    #if setting unified, copy to golden recipe folder
    #manually run
    def MULTI_IDP_AMP_SETTING_reset_database(self):
        # get recipe name with different settings
        # copy golder recipe to P/Y drive
        ppid = pd.read_excel('P:\\_Script\\ExcelFile\\_PPID.xlsm', sheet_name='PPID')
        ppid = ppid[['PART', 'STAGE', 'PPID']]
        ppid = list(ppid.dropna().drop_duplicates()['PPID'])



        col= ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters',
       'output_data', 'rot_correct', 'scan_rate', 'method', 'design_rule',
       'search_area', 'sum_lines', 'sum_lines2', 'smoothing', 'differential',
       'detect_start', 'l_threshold', 'l_direction', 'l_edge_no',
       'l_base_line', 'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no',
       'r_base_line', 'r_base_area', 'dummy1', 'dummy2', 'dummy3', 'dummy4',
       'sum_lines_point', 'assist', 'xy_direction', 'centering',
       'grain_area_x', 'grain_area_y', 'grain_min', 'high_pass_filter',
       'y_design_rule', 'gap_value_mark', 'gap_shape1', 'gap_shape2',
       'corner_edge', 'width_design', 'area_design', 'number_design',
       'grain_design', 'inverse', 'method2', 'score_design',
       'score_number_design', 'TOOL', 'IDW', 'IDP']

        amp = pd.read_csv('z:/_dailycheck/cd_SEM_recipe/check/_ampmay.csv')
        amp.columns=col

        bak = amp.copy()
        amp = amp.reset_index()
        amp = amp.set_index(['TOOL','index'])
        amp = amp[['object','meas_kind','measurement', 'output_data', 'method','l_threshold', 'l_direction', 'l_edge_no',
        'l_base_line','l_base_area', 'r_threshold','r_direction', 'r_edge_no','r_base_line','r_base_area',
         'inverse','assist', 'rot_correct','smoothing','differential','meas_point','diameters','sum_lines_point', 'IDW', 'IDP']]
        amp=amp.drop_duplicates()
        tmp=amp.reset_index()['index']
        amp = bak.loc[tmp]
        amp = amp.reset_index().drop('index',axis=1)

        amp['key']=amp['IDW']+','+amp['IDP']
        tmp= pd.DataFrame(amp.groupby(['key'])['key'].count())
        tmp = tmp[tmp['key']>1] #list of PPID -->multi AMP setting
        print(tmp)

        # to move golden file
        tmp = list(tmp.index)# multi setting
        tmp = amp[ [ not  (i in tmp) for i in amp['key']] ][['TOOL','IDW','IDP','key']] #single setting
        tmp=tmp.reset_index().drop('index',axis=1)

        root_dir = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'

        for i in range(tmp.shape[0]):
            print(i,tmp.shape[0])
            tool =tmp.iloc[i,0]
            idw =tmp.iloc[i,1]
            idp =tmp.iloc[i,2]
            shutil.copyfile(root_dir + tool + '/' + idw + '_' + idp, 'P:/_GOLDEN_AMP/' + idw + '_' + idp)
            shutil.copyfile(root_dir + tool + '/' + idw + '_' + idp, 'y:/GoldenAmp/' + idw + '_' + idp)
    #summarize golder amp parameter and save in csv format
    def GOLDEN_AMP_EXTRACTION(self):

        ampfilelist = [ 'y:/goldenamp/'+ i for i in os.listdir('y:/goldenamp') if i[-2:]=='LN']

        tmp = pd.read_csv('y:/goldenamp/_goldenAmp.csv',usecols=[51,52]).fillna('')
        tmp =set( list('y:/goldenamp/' + tmp['IDW'] + '_' +tmp['IDP']))

        ampfilelist = list(set(ampfilelist)-tmp)
        ampfilelist.sort()


        amp = []
        for k, file in enumerate(ampfilelist):
            print(k,len(ampfilelist), file)
            try:
                f = [i.strip() for i in open(file) if '_dif' not in i]

                if len(f) == 54 or len(f) == 55:
                    for index, i in enumerate(f):
                        if 'comment   ' in i:
                            f = [i.split(":")[1].strip() for i in f[index + 1:]]
                            f.append(file.split('/')[2][:file.split('/')[2].find('_', 1)])
                            f.append(file.split('/')[2][file.split('/')[2].find('_', 1) + 1:])

                    amp.append(f)

            except:
                pass
        df = pd.DataFrame(amp)
        df.to_csv('y:/goldenamp/_goldenAmp.csv', index=None,mode='a')
        df.to_csv('P:/_GOLDEN_AMP/_goldenAmp.csv', index=None,mode='a')
    # get list of all modified IDP sent by IT daily script
    # if modified IDP is in list of golden recipe,over-write it
    def CD_DAILY_REWRITE_DOWNLOAD(self):

        root = 'y:/ModifiedCdSemIDP/'
        toollist = ['DATAS1', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']

        str1 = str(datetime.datetime.now()).replace('-', '')[0:8]
        # str1='20190122'
        filelist = [root + i for i in os.listdir(root) if str1 in i and '-IDP-' in i]
        filelist = [root + i for i in os.listdir(root) if  '-IDP-201906' in i or '-IDP-201907' in i] # no date filtration

        idplist=[]
        for file in filelist[:]:
            print(file)
            try:
                idplist.extend( [i.strip() for i in open(file)])
            except:
                pass
        idplist=list(set(idplist))
        idplist= [i for i in idplist if '/HITACHI/DEVICE/HD/' in i and i[-7:]=='-LN.idp']
        newamp = [ i.split('/')[6]+"_" + i.split('/')[7][0:-4] for i in idplist] #modified amp
        goldenList =[ i for i in  os.listdir('Y:/GoldenAmp') if i[-3:]=='-LN']

        reWrite = list(set(goldenList) & set(newamp))
        downloadList =list( set(newamp)-set(goldenList) )
        downloadList = [ i for i in downloadList if i[-2:]=='LN']

        pd.DataFrame(downloadList).to_csv('y:/goldenAmp/pending.csv',index=None,mode='a')
        pd.DataFrame(downloadList).to_csv('P:/_GOLDEN_AMP/pending.csv', index=None, mode='a')

        #OVER_WRITE AMP
        for tool in toollist[:]:
            print(tool)
            if tool=='DATAS1':
                tool = 'SERVER'
            try:
                idplist = downloadList
                ftp = self.LOGIN(tool)
                # download IDP  ===========================================================================
                # os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/IDP/NEW/' + tool)
                # for idpfile in idplist:
                #     try:
                #         print('downloading IDP_' + idpfile + '.........')
                #         ftp.cwd(idpfile.replace('/', '%', 6).split('/')[0].replace('%', '/'))
                #         tmp = open(idpfile.replace('/', '%', 6).split('%')[-1][:-3].replace('/', '_') + 'IDP', 'wb')
                #         ftp.retrbinary('RETR ' + idpfile.replace('/', '%', 6).split('/')[1], tmp.write)
                #
                #     except:
                #         pass
                # download AMP ==================================================================
                # os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/AMP/NEW/' + tool)
                # for idpfile in idplist:
                #     try:
                #         print('downloading AMP_' + idpfile + '.........')
                #         ftp.cwd(idpfile[:-4])
                #         tmp = open(idpfile.replace('/', '%', 6).split('%')[-1][:-4].replace('/', '_'), 'wb')
                #         ftp.retrbinary('RETR PRMS0001', tmp.write)
                #     except:
                #         pass
                # upload AMP
                os.chdir('Y:/GoldenAmp')
                for i, amp in enumerate(reWrite[:]):
                    print(i,amp,len(reWrite))
                    try:
                        remotepath = '/HITACHI/DEVICE/HD/' + amp[2:4] + '/data/' + amp.split('_')[0] + '/' + amp.split('_')[1] + '/PRMS0001'
                        localpath = 'y:/GoldenAmp/' + amp
                        fp = open(localpath, 'rb')
                        ftp.storbinary('STOR ' + remotepath, fp)
                    except:
                        pass
                ftp.close()
            except:
                pass

        #Read_NEW_AMP
        downloadList=set(pd.read_csv('y:/goldenAmp/pending.csv')['0'].unique())
        downloadList=list(downloadList-set(goldenList))

        amp=[]
        for tool in  ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
            for file in downloadList:
                try:
                    f=open('\\\\10.4.72.74\\asml\\_DailyCheck\\CD_SEM_RECIPE\\AMP\\NEW\\'+ tool + '\\' + file)
                    f = [i.strip() for i in f if '_dif' not in i]
                    break
                    if len(f) == 54 or len(f) == 55:
                        print('SDFSD')
                        for index, i in enumerate(f):
                            if 'comment   ' in i:
                                f = [i.split(":")[1].strip() for i in f[index + 1:]]
                                f.append(file.split('_')[0])
                                f.append(file.split('_')[1])
                                f.append(tool)
                                f.append(file)

                        amp.append(f)

                    pass
                except:
                    pass
        amp = pd.DataFrame(amp)
        amp.columns=['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct', 'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing', 'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line', 'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1', 'dummy2', 'dummy3', 'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering', 'grain_area_x', 'grain_area_y', 'grain_min', 'high_pass_filter', 'y_design_rule', 'gap_value_mark', 'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design', 'area_design', 'number_design', 'grain_design', 'inverse', 'method2', 'score_design', 'score_number_design','IDW','IDP','TOOL','PPID']

        amp = amp.reset_index().drop('index',axis=1).set_index('TOOL')

        issueRecipe=[]
        for ppid in amp['PPID'].unique():
            tmp = amp[amp['PPID']==ppid]
            tmp = tmp.drop_duplicates()
            if tmp.shape[0]==1:
                source = '\\\\10.4.72.74\\asml\\_DailyCheck\\CD_SEM_RECIPE\\AMP\\NEW\\'+ tmp.index[0] + '\\' + ppid
                dest1 = '\\\\10.4.72.74\\litho\\GoldenAmp\\' + ppid
                dest2 = '\\\\10.4.50.16\\PHOTO$\\PPCS\\_GOLDEN_AMP\\' + ppid
                shutil.copyfile(source,dest1)
                shutil.copyfile(source,dest2)
            else:
                issueRecipe.append(ppid)


        #read new golden recipe
        self.GOLDEN_AMP_EXTRACTION()




    def MainFunction(self):
        pass
class CD_SEM_111_NEW(object):#obselete
    #TODO '/HITACHI/DEVICE/HD/.template/MS/
    # cdsem/hid   mainte/propro
    def __init__(self):
        pass
    def LOGIN(self,tool):
        # timeout = 30
        # port = 21
        IPaddress = {'ALCD01': '10.4.152.56', 'ALCD02': '10.4.152.53', 'ALCD03': '10.4.152.54', 'ALCD04': '10.4.151.79',
                     'ALCD05': '10.4.151.81', 'ALCD06': '10.4.151.82', 'ALCD07': '10.4.151.50', 'ALCD08': '10.4.153.26',
                     'ALCD09': '10.4.152.55', 'ALCD10': '10.4.153.32', 'BLCD11': '10.4.131.48', 'BLCD12': '10.4.131.47',
                     'SERVER': '10.4.72.240'}
        HOST = IPaddress[tool]
        user = 'user02'
        password = 'qw!1234'
        try:
            ftp = ftplib.FTP(HOST)
        except ftplib.error_perm:
            print('无法连接到"%s"' % HOST)
            return
        print('连接到"%s"' % HOST)

        try:
            # user是FTP用户名，pwd就是密码了
            ftp.login(user, password)
        except ftplib.error_perm:
            print('登录失败')
            ftp.quit()
            return
        print('登陆成功')
        return ftp
    def CD_PPID(self,path='P:\\_Script\\ExcelFile\\_PPID.xlsm'):
        #GET CD SEM PPID FROM MFG DATABASE
        #REVISE IDW NAME OF -L PRODUCT
        ppid = pd.read_excel(path, sheet_name='PPID',usecols=[0,1,2,7,8,9])
        ppid.columns = ['PART', 'STAGE', 'PPID', 'PART.1', 'STAGE.1', 'ToolType'] # python3.4和python3.6读取的标题不同
        tmp = ppid[[ 'PART.1', 'STAGE.1', 'ToolType']].dropna()
        tmp.columns = ['PART', 'STAGE', 'Tool']
        tmp=tmp[tmp['PART'].str.endswith('-L')]
        ppid = ppid[['PART', 'STAGE', 'PPID']].dropna()
        ppid=pd.merge(ppid,tmp,how='left', on =['PART','STAGE']).fillna('').drop_duplicates().reset_index().drop('index',axis=1)
        ppid=ppid[['PPID','Tool']]

        ppid['Tool']=ppid['Tool'].apply(lambda x: '-A' if x=='LDI' else ( '-N' if x=='LII' or x== 'LSI' else ''))
        ppid['IDW']=ppid['PPID'].str[0:-6]+ppid['Tool']
        ppid['IDP']=ppid['PPID'].str[-5:]
        ppid=ppid[['IDW','IDP']]
        try:
            ppid = ppid.sort_values(by='IDW')
        except:
            ppid = ppid.sort(['IDW'])
        ppid = ppid[[not i for i in (ppid['IDW'].str.startswith('2') | ppid['IDW'].str.startswith('8')|ppid['IDW'].str.startswith('*'))]]
        ppid = ppid.drop_duplicates()
        ppid['PPID']=ppid['IDW'] + '$' + ppid['IDP']
        ppid.to_csv(r'\\10.4.72.74\asml\_DailyCheck\CD_SEM_Recipe\PROMIS_CD_PPID.csv',index=None)

        # UPDATE NEW PPID IN ACCESS DB，below error @YE CD SERVER,skipped
        if 1==2:
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'Z:/_DailyCheck/CD_SEM_Recipe/cdRecipe.mdb'
            conn.Open(DSN)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            sql = "select* from tabPpid "
            rs.Open(sql, conn, 1, 3)
            old = []
            for i in range(rs.recordcount):
                old.append(rs.fields(0).value)
                rs.movenext
            new = list ( set( list(ppid['PPID']) ) - set(old) )
            for i in new:
                print(i)
                rs.AddNew()  # 添加一条新记录
                rs.Fields('PPID').Value = i
                rs.Update()  # 更新
            conn.close
    def DOWNLOAD_IDP(self,tool,allFlag=True):
        ftp = CD_SEM_111_NEW().LOGIN(tool)
        df = pd.read_csv(r'\\10.4.72.74\asml\_DailyCheck\CD_SEM_Recipe\PROMIS_CD_PPID.csv')
        for i in range(df.shape[0]):
            if i % 100 == 0:
                print(tool, i)
            idw, idp = df.iloc[i, 0], df.iloc[i, 1]
            try:
                source = '/HITACHI/DEVICE/HD/' + idw[2:4] + '/data/' + idw + '/' + idp + '.idp'
                dest = '//10.4.72.74/asml/_DailyCheck/CD_SEM_Recipe/IDP/' + tool + '/' + idw + "$" + idp + '.idp'
                file = open(dest, 'wb')
                ftp.retrbinary('RETR ' + source, file.write)
            except:
                pass
        ftp.quit()
    def READ_IDP(self,tool,allFlag=True):
        if allFlag==True:
            folder = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/' + tool + '/'
        else:
            folder = 'Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/IDP/' + tool + '/'
        filelist = [folder + i for i in os.listdir(folder) if 'idp' in i]
        filelist.sort()

        singleToolidp = []

        for k, idppath in enumerate(filelist[0:]):

            idpname = idppath
            # idppath =  'Z:/_DailyCheck/CD_SEM_Recipe/IDP/' + tool +'/' + idppath + '.idp'

            print(tool, k, idppath, len(filelist))
            try:
                tmpTemplate = []
                tmpCoordinate = []
                tmpPrms = []
                result = []
                f = [i.strip() for i in open(idppath)]

                if len(f) > 25:
                    for index, i in enumerate(f):
                        if 'history    :' in i:
                            result.append(i[14:].strip())
                        if 'no_of_mpid' in i:
                            result.append(i.split(':')[1].strip())  # structure qty
                        if 'PRMS000' in i:
                            tmpPrms.append(i.split(',')[9].strip())
                        if 'template   : MS' in i:
                            tmpTemplate.append(i.split(':')[-1].strip())  # template enabled

                        if 'msr_point  :' in i:
                            tmpCoordinate.append(
                                str(int(int(i.split(',')[2]) / 1000)) + "," + str(int(int(i.split(',')[3]) / 1000)))
                # tmpCoordinate=[eval(i) for i in tmpCoordinate]
                result.extend([tmpTemplate, tmpPrms, list(pd.DataFrame(tmpCoordinate)[0].unique()),
                               idpname])  # set-->wrong sequence
            except:

                result = []
            if len(tmpCoordinate) > 0:
                result.append(tool)
                singleToolidp.append(result)
            try:
                shutil.move(idppath,'Z:/_DailyCheck/CD_SEM_Recipe/IDP/'+tool+ '/' + str(datetime.datetime.now())[0:10] +'_'+ idppath.split('/')[-1]      )
            except:
                pass

        df = pd.DataFrame(singleToolidp,
                         columns=['history', 'dataNo', 'tmplate', 'prmsNo', 'coordinate', 'ppid', 'tool'])
        df['riqi']=str(datetime.datetime.now())[0:10].replace('-','')

        if allFlag==True:
            try:
                df.to_csv(
                'Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidp' + tool + '.csv', index=None)
                df.to_csv('p:/cd/opas/singleToolidp' + tool + '.csv', index=None)
            except:
                pass
        else:
            try:
                df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidp' + tool + '.csv', index=None,mode='a',header=False)
                df.to_csv('p:/cd/opas/singleToolidp' + tool + '.csv', index=None, mode='a', header=False)
            except:
                pass
            #会产生重复项，即内容相同，但日期不同-->后期可以将日期转为index，删除重复项
    def DOWNLOAD_AMP(self,tool,allFlag=True):

        df = pd.read_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidp' + tool + '.csv')
        if allFlag==False:
            # df = df[ df['riqi']== (str(datetime.datetime.now())[0:10]).replace('-','/')]# format changed,errro
            df = df[ df['riqi']== df['riqi'].unique()[-1] ]
            df =df[ ['history', 'dataNo', 'tmplate', 'prmsNo', 'coordinate', 'ppid', 'tool']]

        df = df[['prmsNo', 'ppid']].reset_index().drop('index', axis=1)
        ampPath = []
        if df.shape[0] > 0:
            for i in range(df.shape[0]):
                if allFlag==True:
                    ppid, prms = df.iloc[i, 1].split('/')[5], df.iloc[i, 0]
                else:
                    try:
                        ppid, prms = df.iloc[i, 1].split('/')[6], df.iloc[i, 0]
                        idppath = '/HITACHI/DEVICE/HD/' + ppid[2:4] + '/data/' + ppid.split('$')[0] + '/' + \
                                  ppid.split('$')[1].split('.')[0] + '/'
                        ampPath.extend([idppath + x[1:-1] for x in prms.replace(' ', '')[1:-1].split(',')])
                    except:
                        print("手动下载的路径不匹配，且不必再下载，AMP中已更新")



            try:
                ftp = CD_SEM_111_NEW().LOGIN(tool)
                if allFlag==True:
                    folder = '//10.4.72.74/asml/_DailyCheck/CD_SEM_Recipe/AMP/' + tool + '/'
                else:
                    folder = '//10.4.72.74/asml/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/AMP/' + tool + '/'
                for length, amp in enumerate(ampPath):
                    print(tool, length, amp, len(ampPath))
                    try:
                        dest = folder + amp.replace('/', '$')
                        source = amp
                        file = open(dest, 'wb')
                        ftp.retrbinary('RETR ' + source, file.write)
                    except:
                        pass
                ftp.close()
            except:
                pass
    def READ_AMP(self,tool,allFlag=True):
        if allFlag==True:
            folder = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + tool + '/'
        else:
            folder = 'Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/AMP/' + tool + '/'

        amppath = [folder + i for i in os.listdir(folder)]
        amppath.sort()
        summary = []

        try:
            for k, amp in enumerate(amppath):
                print(tool, k, amp, len(amppath))
                try:
                    f = [i.strip() for i in open(amp) if '_dif' not in i]
                    if len(f) == 54 or len(f) == 55:
                        for index, i in enumerate(f):
                            if 'comment   ' in i:
                                f = [i.split(":")[1].strip() for i in f[index + 1:]]
                                f.append(amp.split('$')[6])
                                f.append(amp.split('$')[7])
                                f.append(amp.split('$')[8])
                        summary.append(f)
                except:
                    pass
                shutil.move(amp,'Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + tool + '/'+ str(datetime.datetime.now())[:10] + '_' + amp.split('/')[-1]  )
        except:
            pass
        df = pd.DataFrame(summary)
        df['riqi'] = (str(datetime.datetime.now())[0:10]).replace('-','')
        if allFlag==True:
            df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolAmp/' + tool + '_amp.csv', index=None)
            df.to_csv('p:/cd/opas/' + tool + '_amp.csv', index=None)
        else:
            df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolAmp/' + tool + '_amp.csv', index=None,header=None,mode='a')
            df.to_csv('p:/cd/opas/' + tool + '_amp.csv', index=None,header=None,mode='a')
    def GOLDEN_AMP(self,allFlag=True):
        tool =['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
        col = ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
               'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing',
               'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line', 'l_base_area',
               'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1', 'dummy2', 'dummy3',
               'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering', 'grain_area_x', 'grain_area_y',
               'grain_min', 'high_pass_filter', 'y_design_rule', 'gap_value_mark', 'gap_shape1', 'gap_shape2',
               'corner_edge', 'width_design', 'area_design', 'number_design', 'grain_design', 'inverse', 'method2',
               'score_design', 'score_number_design', 'idw', 'idp', 'prms','riqi','tool']

        df = pd.DataFrame(columns=col)
        for i in tool:
            tmp = pd.read_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolAmp/'+ i + '_amp.csv')
            tmp['tool']=i
            tmp.columns=col
            df = pd.concat([df,tmp],axis=0)
            df=df.fillna('')
            df['riqi']=[str(i) for i in df['riqi']]
        print('single amp combined')

        # TODO 有重复的，待澄清，包括IDP,怀疑是系统日期混乱导致，需清除 history comment一致的-->生成单个设备的AMP文件时，部分AMP是用append模式，且日期前缀加0-->不影响其它新品排序-->最后生成的double check结果要更新--》例如正常日期 20190708，后续更新 0-20190710,0-20190711，凡是有多行的，取带零的，且日期最新的
        if True:
            tmp = pd.DataFrame(df.groupby(['idw', 'idp', 'prms', 'tool'])['riqi'].count())
            tmp = tmp[tmp['riqi'] > 1].reset_index().drop('riqi', axis=1)
            tmp = pd.merge(tmp, df, how='left', on=['idw', 'idp', 'prms', 'tool'])[
                ['idw', 'idp', 'prms', 'tool', 'riqi']]  #
            tmp = tmp[tmp['riqi'].str.startswith('0_')]  # 没有日期：一次性下载的，或日期起始于20.。。：后续的新文件-->都不如起始于0的手动下载的

            tmp = pd.DataFrame(tmp.groupby(['idw', 'idp', 'tool', 'prms'])['riqi'].max()).reset_index()

            for i in range(tmp.shape[0]):
                df = df[~(df['idw'].str.contains(tmp.iloc[i, 0]) & df['idp'].str.contains(tmp.iloc[i, 1]) & df[
                    'tool'].str.contains(tmp.iloc[i, 2]) & df['prms'].str.contains(tmp.iloc[i, 3]) & (
                              ~df['riqi'].str.contains(tmp.iloc[i, 4])))]

        #for OPAS only,all amp
        allamp=df.copy()
        allamp =allamp[['object','method','l_threshold','idw','idp','prms','tool','riqi']]
        allamp['object'] = allamp['object'].apply(lambda x: 'space' if x==1 else ( 'line' if x==0 else 'hole'))
        allamp['method'] = allamp['method'].apply(lambda x: 'linear' if x == 0 else 'threshold')
        allamp['MaskNo'] = allamp['idw'].str[2:4]
        try:
            allamp.to_csv('y:/GoldenAmp/Summary/ampSummary.csv',index=None)
            allamp.to_csv('p:/cd/opas/ampSummary.csv',index=None)
        except:
            pass
        allamp=None

        print('opas file done')





        if allFlag==False:
            try:
                df = df.sort('riqi')
            except:
                df = df.sort_values(by='riqi')
            df = df[df['riqi']==(df['riqi'].unique()[-1])]



        # assume each recipe in all tools is identical
        # extact distinct amp content as golden recipe

        df = df.drop('riqi', axis=1)



        df=df.reset_index().drop('index',axis=1).set_index(['tool']).drop_duplicates()#所有参数匹配的重复项
        df = df [['object', 'meas_kind', 'measurement', 'output_data', 'method', 'l_threshold', 'l_direction', 'l_edge_no',
             'l_base_line', 'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area',
             'inverse', 'assist', 'rot_correct', 'smoothing', 'differential', 'meas_point', 'diameters',
             'sum_lines_point', 'idw', 'idp','prms']].drop_duplicates()#关键参数匹配的重复项



        df['key'] = '$HITACHI$DEVICE$HD$' + df['idw'].str[2:4] + '$data$' + df['idw'] + '$' + df['idp']+'$'+df['prms']
        mismatch = pd.DataFrame(df.groupby(['key'])['key'].count())
        mismatch = mismatch[mismatch['key'] > 1]  # list of PPID -->multi AMP setting
        mismatch['riqi']=str(datetime.datetime.now())[0:19]
        mismatch.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/misMatch/misMatch.csv',mode='a',header=None)

        print('mismatch file done')

        #复制golden file
        df=df[['key']].drop_duplicates().reset_index()
        for i in range(df.shape[0]):
            tool=df.iloc[i,0]
            file = df.iloc[i,1]
            if allFlag==True:
                src = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'+tool+'/'+file
            else:
                src = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + tool + '/' +str(datetime.datetime.now())[:10] + '_' + file
            des = 'P:/_GOLDEN_AMP/' + file
            des1 = 'y:/GoldenAmp/' + file

            if os.path.exists(des):
                pass
                print(i,df.shape[0],'skipped')
            else:
                shutil.copyfile(src,des)
                shutil.copyfile(src,des1)
                print(i,df.shape[0],'copied')


        #不匹配AMP，统一
        mismatch=pd.DataFrame(mismatch.index)
        mismatch=pd.merge(mismatch,df,how='left',on='key')
        mismatch = mismatch.reset_index().drop('index',axis=1)
        if mismatch.shape[0]>0:
            if allFlag==True:
                path = [ 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'+mismatch.iloc[i,1]+'/'+mismatch.iloc[i,0] for i in range(mismatch.shape[0])]
            else:
                path = [ 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'+mismatch.iloc[i,1]+'/'+str(datetime.datetime.now())[:10] + '_'+ mismatch.iloc[i,0] for i in range(mismatch.shape[0])]
            print('over-write multi-amp')
            if True:
                try:
                    for tool in ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
                    # for tool in ['SERVER']:
                        try:
                            ftp = self.LOGIN(tool)
                            for n, amp in enumerate(path):
                                try:

                                    remotepath = amp.split('_',-1)[-1].replace('$','/')
                                    localpath =amp
                                    fp = open(localpath, 'rb')
                                    ftp.storbinary('STOR ' + remotepath, fp)
                                    print(tool, n, amp, len(path),'unification done')
                                except:
                                    pass
                            ftp.close()
                        except:
                            pass
                except:
                    pass
            print('over-write done')

        print('compile golden amp setting')
        if True:
            folder = 'y:/GoldenAmp/'
            amppath = [folder + i for i in os.listdir(folder) if 'PRMS00' in i]
            summary = []
            if allFlag==True:
                pass
            else:
                tmp = pd.read_csv('y:/GoldenAmp/Summary/goldenAmpRaw.csv',usecols=['idw','idp','pNo'])
                tmp['path']= 'y:/GoldenAmp/$HITACHI$DEVICE$HD$'+ tmp['idw'].str[2:4]+'$data$' + tmp['idw'] + '$' + tmp['idp'] + '$' + tmp['pNo']
                amppath = list(set(amppath)-set(list(tmp['path'])))
            if len(amppath)>0:

                try:
                    for k, amp in enumerate(amppath[0:]):
                        print( k, amp, len(amppath))
                        f = [i.strip() for i in open(amp) if '_dif' not in i]

                        if len(f) == 54 or len(f) == 55:
                            for index, i in enumerate(f):
                                if 'comment   ' in i:
                                    f = [i.split(":")[1].strip() for i in f[index + 1:]]
                                    f.append(amp.split('$')[6])
                                    f.append(amp.split('$')[7])
                                    f.append(amp.split('$')[8])
                            summary.append(f)
                except:
                    pass

                if len(summary)>0:

                    #get columns
                    f = [i.strip() for i in open(amp) if '_dif' not in i]
                    for index, i in enumerate(f):
                        if 'comment   ' in i:
                            f = [i.split(":")[0].strip() for i in f[index + 1:]]
                    f.extend(['idw','idp','pNo'])

                    summary=pd.DataFrame(summary,columns=f)
                    if allFlag==True:
                        try:
                            pd.DataFrame(summary).to_csv('y:/GoldenAmp/Summary/goldenAmpRaw.csv', index=None)
                            pd.DataFrame(summary).to_csv('p:/cd/opas/goldenAmpRaw.csv', index=None)
                        except:
                            pass
                    else:
                        try: #实际文件中有重复行，可能系多次手动运行宏导致
                            pd.DataFrame(summary).to_csv('y:/GoldenAmp/Summary/goldenAmpRaw.csv', index=None,header=None,mode='a')
                            pd.DataFrame(summary).to_csv('p:/cd/opas/goldenAmpRaw.csv', index=None,header=None,mode='a')
                        except:
                            pass

                    summary['object'] = summary['object'].apply(lambda x: 'space' if str(x) == '1' else ('line' if str(x) == '0' else ('hole' if str(x)=='20' else '')))
                    summary['method'] = summary['method'].apply(lambda x: 'linear' if str(x) == '0' else ('threshold' if str(x)=='1' else ''))
                    summary['measurement']=summary['measurement'].apply(lambda x: 'width' if str(x)=='0' else('diameter' if str(x)=='3' else 'others'))
                    summary['meas_kind']= summary['meas_kind'].apply(lambda x: 'single' if str(x)=='0' else('multipoint' if str(x)=='1' else('radial' if str(x)=='2' else ' ')))
                    summary['output_data']=summary['output_data'].apply(lambda x:"mean'" if str(x)=='1' else ('diameter' if str(x)=='7' else ''))
                    summary[['measurement', 'object', 'method','meas_kind','output_data']]
                    if allFlag==True:
                        try:
                            pd.DataFrame(summary).to_csv('y:/GoldenAmp/Summary/goldenAmp.csv', index=None)
                            pd.DataFrame(summary).to_csv('P:/cd/opas/goldenAmp.csv', index=None)
                        except:
                            pass
                    else:
                        try:
                            pd.DataFrame(summary).to_csv('y:/GoldenAmp/Summary/goldenAmp.csv', index=None,header=None,mode='a')
                            pd.DataFrame(summary).to_csv('P:/cd/opas/goldenAmp.csv', index=None,header=None,mode='a')
                        except:
                            pass
    def GOLDEN_AMP_CHECK(self):
        #如果以前有文件遗漏，无检查结果 1）用PPID批处理命令下载IDP/AMP  2）feedback文件清除相关记录 3）运行 ALL_IDP，将结果汇总，以便以下调用

        print('THIS IS GOLDEN AMP RAW DATA ')
        df=pd.read_csv('y:/GoldenAmp/Summary/goldenAmpRaw.csv')
        print('below is to identify 1st P_No')
        if True:
            tmp = pd.read_csv('y:/GoldenAmp/Summary/idpSummary.csv').fillna('')[['tool','ppid','riqi','p1']]
            tmp = tmp.set_index(['tool','riqi']).drop_duplicates()
            tmp = tmp.reset_index()
            tmp1 = pd.DataFrame((tmp.groupby(['tool', 'riqi','ppid'])['ppid'].count()))
            if tmp1[tmp1['ppid']>1].shape[0]>0:
                print('IDP 1st PRMS NOT CONSISTENT')
                tmp1.to_csv('p:/cd/opas/First_PRMS_NOT_CONSISTENT_' + str(datetime.datetime.now())[:10] + '.csv')
            else:
                print('IDP 1st PRMS  CONSISTENT')
            tmp1=None

            tmp=tmp[['ppid','p1']].drop_duplicates()

            tmp['idw']=[i[:-10] for i in tmp['ppid']] # tmp['idw']=[i.split('$')[0] for i in tmp['ppid']] 部分不带$,应该是手动下载的部分，会报错

            tmp['idp']=[i[-9:-4] for i in tmp['ppid']] # tmp['idp']=[i.split('$')[1].split('.')[0] for i in tmp['ppid']] 部分不带$,应该是手动下载的部分，会报错
            tmp=tmp[['idw','idp','p1']]
            tmp.columns=['idw','idp','pNo']
            tmp['check']=True
            tmp=tmp.drop_duplicates()

            df=pd.merge(df,tmp,how='left',on=['idw','idp','pNo']).fillna('')
            tmp=None
        print('below is to add process code\nmannuall get process code from OPAS\npending revise it to update process code ')
        # skipped-->r2r cd config has tech
        # if True:
        #     tmp = pd.read_csv('Y:/GoldenAmp/Summary/ProcessCode.csv')
        #     tmp = tmp[['PARTNAME','TECH']]
        #     tmp.columns=['idw','tech']
        #     df = pd.merge(df,tmp,how='left',on=['idw']).fillna('')
        print('below is to get r2r line/space setting\nmanually get from OPAS')
        if True:
            # tmp = pd.read_csv('Y:/GoldenAmp/Summary/r2rCdConfig.csv')
            tmp = pd.read_csv('p:/cd/opas/r2rCdConfig.csv')
            tmp.columns=['﻿CD', 'FAMILY', 'FEEDBACK', 'FULLTECH', 'LAYER', 'LINE_SPACE', '记录数', 'PARTNAME', 'PART', 'TECH']
            tmp = tmp[['﻿CD', 'FAMILY', 'FEEDBACK', 'FULLTECH', 'LAYER', 'LINE_SPACE',  'PART', 'TECH']].fillna('').drop_duplicates()
            tmp.columns=['cd','family','feedback','fulltech','layer','line_space','part','tech']

            tmp['part']=tmp['part'].apply(lambda x: x[:-4] if x[-4:-2]=='-0' else x )#rom code
            tmp['layer']=tmp['layer'].apply(lambda x: x[0:2])                        #rom code

            df['layer']=[i[0:2] for i in df['idp']]
            df['part']=df['idw'].apply(lambda x: x[:-2] if x[-4:]=='-L-A' or x[:-4]=='-L-N' else x) #大视场idw在part名上额外有-A/-N
            df = pd.merge(df,tmp,how='left',on =['part','layer']).fillna('') #df中的idw和R2R中idw不完全相同

        print('==============================Check Recipe===============================================')

        print('check measurement parameter===width 0, diameter 3')
        # if True: # not necessry, "object" parameter is OK
        #     df['measurementFlag'] = df['layer'].apply(lambda x: 3 if x in ['W1','W2','W3','W4','W5','W6','W7','WT','OV','OE','MV'] else 0 )
        #     df['measurementFlag']=(df['measurementFlag']==df['measurement'])
        #     tmp=df[df['check']==True]
        #     tmp = tmp[tmp['feedback'] == 'Y']
        #     tmp=tmp[tmp['measurementFlag']==False]

        print('check r2r line_space flag: extra type "H" to be converted, "H" and "L" mean "LINE" and only "S" stands for "SPACE" ')
        if True:
            df['r2rLineSpaceFlag'] = df['line_space'].apply(lambda x: 'S' if x == 'S' else ('L' if x == 'L' or x == 'H' else ''))
            df['tmp'] = df['object'].apply(lambda x: 'L' if x==0 else ('S' if x==1 or x==20 else 'WrongSetting')) #only 0,1,20 allowed
            df['r2rLineSpaceFlag']= df['r2rLineSpaceFlag']==df['tmp'] #R2R setting = Recipe Setting?
            df=df.drop('tmp',axis=1)

        print('Check object parameter')
        if True:
            tmpObject=df['object']
            tmpLayer=df['layer']
            tmpLineSpace=df['line_space']
            tmp=[]
            for i in range(df.shape[0]):
                if tmpLayer[i] in ['W1','W2','W3','W4','W5','W6','W7','WT','OE','OV','MV']: #diameter layers
                    tmp.append(20==tmpObject[i])
                elif tmpLayer[i] in ['A1','A2','A3','A4','A5','A6','A7','TT','MA','T1','T2','AT']: #line for all metal layers
                    tmp.append(0==tmpObject[i])
                else:
                    if tmpLineSpace[i] in ['L','H']: # line layers except hole/metal layers
                        tmp.append(0==tmpObject[i])
                    elif tmpLineSpace[i]=="S":      # space layers except hole/metal layers
                        tmp.append(1==tmpObject[i])
                    elif tmpLineSpace[i]=='':  # no config in OPAS ,obselete device
                        tmp.append('obselete')
                    else:
                        tmp.append('error')  #in case of other setting in r2r config
            df['objectFlag']=tmp
            tmp,tmpObject,tmpLayer,tmpLineSpace=None,None,None,None
            print('===revise setting per special request of each idw/idp/tech etc===')

        print('Check Method Parameter')
        if True:
            tmp=[]
            tmpMethod=df['method']
            tmpLayer = df['layer']
            tmpTech = df['tech']
            for i in range(df.shape[0]):
                if tmpLayer[i] in ['W1','W2','W3','W4','W5','W6','W7','WT','OE','OV','MV','A1','A2','A3','A4','A5',
                                   'A6','A7','AT','TT','MA','T1','T2']:
                    tmp.append(1==tmpMethod[i])    #threshold algorithm for metal/hole
                elif tmpTech[i].strip()=='':
                    tmp.append('Pending')          #tech code not availabe for -RD short loop
                elif tmpTech[i].strip()[1]=='1' or tmpTech[i].strip()[1]=='A':
                    tmp.append(0==tmpMethod[i])    # _1% ,_A% tech, linear
                else:
                    tmp.append(1==tmpMethod[i])    # threshold except _1% tech
            df['methodFlag'] = tmp
            print('revise setting per special request of each idw/idp/tech etc')

        print('Check Algorithm')
        if True:
            tmp=[]
            tmpl_threshold=df['l_threshold']
            tmpr_threshold=df['l_threshold']
            tmpr_base_line=df['r_base_line']
            tmpl_base_line=df['l_base_line']
            tmpr_base_area=df['r_base_area']
            tmpl_base_area=df['l_base_area']
            for i in range(df.shape[0]):
                if tmpMethod[i]==0:        #  linear algorithm is identical for all process
                    tmp.append(tmpr_base_line[i]==2 and tmpl_base_line[i]==2 and tmpr_base_area[i]==8 and tmpl_base_area[i]==8 )
                elif tmpMethod[i]==1:
                    if tmpLayer[i] in ['A1','A2','A3','A4','A5','A6','A7','AT','TT','MA','T1','T2']: # metal threshold
                        if tmpTech[i].strip()=='':      # no tech code for short loop
                            tmp.append('pending')
                        elif tmpTech[i].strip()[1]=='1' or tmpTech[i].strip()[1]=='A': # _1% ,_A% process 15% threshold
                            tmp.append( tmpl_threshold[i]==15 and tmpr_threshold[i]==15)
                        else:                            # 20% except _1% code
                            tmp.append(tmpl_threshold[i] == 20 and tmpr_threshold[i] == 20)

                    else:   #50% for all threshold except _1% tech
                        tmp.append( tmpl_threshold[i]==50 and tmpr_threshold[i]==50)
                else:
                    tmp.append('pending')
            df['algorithmFlag']=tmp

        tmp = None
        tmpMethod = None
        tmpLayer = None
        tmpTech = None
        tmpl_threshold = None
        tmpr_threshold = None
        tmpr_base_line = None
        tmpl_base_line = None
        tmpr_base_area = None
        tmpl_base_area = None



        df['object']= df['object'].apply(lambda x: 'space' if str(x) == '1' else (
            'line' if str(x) == '0' else ('hole' if str(x) == '20' else 'wrong')))
        df['method'] = df['method'].apply(
            lambda x: 'linear' if str(x) == '0' else ('threshold' if str(x) == '1' else ''))

        tmp = []
        for i in range(df.shape[0]):
            try:
                tmp.append(df['objectFlag'][i] and df['methodFlag'][i] and df['algorithmFlag'][i])
            except:
                tmp.append('boolean error')
        df['conclusion'] = tmp







        try: #in case file is opened
            df.to_csv('y:/GoldenAmp/Summary/ampFullSummary.csv')
            df.to_csv('p:/cd/opas/ampFullSummary.csv')
        except:
            pass

        output = df[df['check'] == True]
        output = output[output['feedback'] == "Y"]
        output = output[['conclusion','idw', 'idp',  'line_space','tech', 'family',
                         'r2rLineSpaceFlag', 'objectFlag', 'methodFlag', 'algorithmFlag',
                         'object','method','l_threshold','r_threshold']]#, 'l_base_line', 'l_base_area','r_base_line', 'r_base_area']]
        try:
            output=output.sort('family',ascending=False)
        except:
            output = output.sort_values(by='family',ascending=False)



        try:
            output.to_csv('y:/GoldenAmp/Summary/simpleSummary.csv',index=None)


            output.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/simpleSummary.csv')

            output.to_csv('P:/cd/opas/simpleSummary.csv', index=None)
        except:
            pass

        tmp = pd.read_csv('y:/GoldenAmp/Summary/feedback.csv') #idw/idp 非唯一，可能重复更新，取最新的日期
        tmp = pd.DataFrame(tmp.groupby(['idw','idp'])['feedback'].max()).reset_index()

        output = pd.merge(output,tmp,how='outer',on=['idw','idp']).reset_index().drop('index',axis=1)

        output['ppid'] = output['idw'] + '.' + output['idp']
        # output=output.drop(['idw','idp'],axis=1)
        output['objectMethodLthresholdRthreshold']=output['object']+','+output['method']+','+[str(i) for i in output['l_threshold']]+','+[str(i) for i in output['r_threshold']]
        # output=output.drop(['object','method','l_threshold','r_threshold'],axis=1)



        

        try:
            output=output.sort('family',ascending=False)
        except:
            output=output.sort_values(by='family',ascending=False)
        try:
            try:
                output.to_csv('y:/GoldenAmp/Summary/forOpas.csv', index=None)
            except:
                pass
            try:
                output.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/forOpas.csv')
            except:
                pass
            try:
                output.to_csv('P:/cd/opas/forOpas.csv')
            except:
                pass
        except:
            pass
        try:
            output.to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\cd\\forOpas.csv')
            output.to_excel('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\cd\\forOpas.xlsx')
        except:
            pass
    def All_IDP(self,allFlag=True):#summarize idp of each tool to file-->y:/GoldenAmp/Summary/idpSummary.csv
        tool = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        df=pd.DataFrame(columns=['history', 'dataNo', 'tmplate', 'prmsNo', 'coordinate', 'ppid', 'tool','riqi'])
        for i in tool:
            print(i)
            tmp = pd.read_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidp'+ i + '.csv')
            df = pd.concat([df,tmp],axis=0)


        df['ppid']=[ i.split('/')[-1] for i in df['ppid']]
        df['p1']=[ i[1:-1].split(',')[0][1:-1] for i in list(df['prmsNo'])]



        try:
            df=df.sort_values(by=['ppid','tool'])
            print('PY36')
        except:
            df = df.sort(['ppid','tool'])
            print('PY34')
        try:
            df.to_csv('y:/GoldenAmp/Summary/idpSummary.csv',index=None)
            df.to_csv('p:/cd/opas/idpSummary.csv',index=None)
        except:
            pass
    def Modified_File(self):
        #download_list
        path = 'Y:/ModifiedCdSemIDP/'
        os.chdir(path)
        toollist = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
        for tool in toollist:
            try:
                ftp = self.LOGIN(tool)
                ftp.cwd('/dailycheck')
                for tmp in ftp.nlst():
                    file = open(tmp, 'wb')
                    ftp.retrbinary('RETR ' + tmp, file.write)
                ftp.quit()
            except:
                pass

        #get file name from list
        filelist = [path + i for i in os.listdir(path) if  '-IDP-' in i]
        idplist=[]
        for k,file in enumerate(filelist[:]):
            print(k,file,len(filelist))
            idplist.extend( [i.strip()[:-4] for i in open(file)] )
            # add one step to move file to another folder,otherwise too many files are repeated
        idplist = list(set(idplist))
        idplist.sort()
        idplist=[i.replace('/','$') for i in idplist]

        pd.DataFrame(idplist).to_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/list/idplist_'+ str(datetime.datetime.now())[:10]+'.csv')
        for file in os.listdir(path):
            src = path + file
            dest = 'Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/LIST/downloadFile/' + file
            try:
                if 'log' not in src:
                    shutil.move(src,dest)
            except:
                pass







        #generate list of file names to download or upload
        tmp = [ [i,i[:-9]] for i in os.listdir('y:/GoldenAmp') if 'PRMS' in i ]  #golden amp
        tmp = pd.DataFrame(tmp,columns=['path','key']) #golden amp
        tmp1= pd.DataFrame(idplist,columns=['key']) # modified amp
        tmp2=pd.merge(tmp1,tmp,how='left',on='key').fillna('')
        toBeUpload = tmp2[tmp2['path'].str.len()>1]
        toBeDownload = tmp2[tmp2['path'].str.len()==0].drop('path',axis=1) #with engineering idp
        toBeDownload['PPID'] = toBeDownload['key'].str[27:]
        tmp,tmp1,tmp2=None,None,None
        standardPpid= pd.read_csv(r'\\10.4.72.74\asml\_DailyCheck\CD_SEM_Recipe\PROMIS_CD_PPID.csv')[['PPID']]
        toBeDownload = pd.merge(standardPpid,toBeDownload,how='inner',on='PPID')

        toBeDownload = toBeDownload.reset_index().drop('index',axis=1)
        toBeUpload = toBeUpload.reset_index().drop('index',axis=1)
        return [toBeDownload,toBeUpload]
    def Daily_CHECK(self):
        print('get upload/download list, 10.4.3.111')
        if True:
            tmp = self.Modified_File()
            toBeDownload,toBeUpload=tmp[0],tmp[1]
            toBeUpload.to_csv(
                'Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/LIST/toBeUpload_' + str(datetime.datetime.now())[:10] + '.csv',
                index=None)
            toBeDownload.to_csv(
                'Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/LIST/toBeDownload_' + str(datetime.datetime.now())[:10] + '.csv',
                index=None)
            toBeUpload = list(toBeUpload['path'])

        toBeUpload=pd.read_csv( 'Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/LIST/toBeUpload_' + str(datetime.datetime.now())[:10] + '.csv')
        toBeUpload = list(toBeUpload['path'])

        print('over-write AMP of modified IDP')
        if True:
            try:
                for tool in ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
                # for tool in ['SERVER']:
                    try:
                        ftp = self.LOGIN(tool)
                        for n, amp in enumerate(toBeUpload):
                            try:

                                remotepath = amp.replace('$','/')
                                localpath =( 'y:/GoldenAmp/' + amp )
                                fp = open(localpath, 'rb')
                                ftp.storbinary('STOR ' + remotepath, fp)
                                print('pass',tool, n, amp, len(toBeUpload))
                            except:
                                print('fail',tool, n, amp, len(toBeUpload),)
                        ftp.close()
                    except:
                        pass
            except:
                pass
        print('over-write done')

        #=====NewIDP
        print(' idp downloading')
        if 1==1:
            df = toBeDownload
            for tool in ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
                try:
                    ftp = CD_SEM_111_NEW().LOGIN(tool)
                    for i in range(df.shape[0]):
                        print(tool, i,df.iloc[i,1])
                        try:
                            source = df.iloc[i,1].replace('$','/') + '.idp'
                            dest = '//10.4.72.74/asml/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/IDP/' + tool + '/' + df.iloc[i,0].split('$')[0] + "$" + df.iloc[i,0].split('$')[1] + '.idp'
                            file = open(dest, 'wb')
                            ftp.retrbinary('RETR ' + source, file.write)
                        except:
                            pass
                    ftp.close()
                except:
                    pass
        print('download idp done')

        print('reading idp')
        if True:
            for tool in ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
                self.READ_IDP( tool, allFlag=False)
        print('reading idp completed')

        print('downloading amp')
        if True:
            for tool in ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
                self. DOWNLOAD_AMP(tool,allFlag=False)
        print('downloading amp completed')

        print('reading amp')
        if True:
            for tool in ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']:
                self.READ_AMP(tool,allFlag=False)
        print('reading amp done')

        print('combine all idp')
        self.All_IDP(allFlag=False) #identical to fresh new,duplicated-->history date
        print('combine all idp done')

        print('generate golden amp')
        self.GOLDEN_AMP(allFlag=False) #identical to fresh new,duplicated-->history date
        print('generate golden amp done')
        
        print('analyze golden amp-->one more column to be added-->final conclusion post check by layer owner')
        
        self.GOLDEN_AMP_CHECK()
    def Recipe_Updated_By_Layerowner(self):#每天最先执行，保证手动下载的AMP优先被保存到golden amp目录中
        pass
        tmpFb=[]
        tmp = 'P:/cd'
        idpList = ['P:/cd/' + i for i in os.listdir(tmp) if '.idp' in  i ]
        ampList = [ 'P:/cd/' +i for i in os.listdir(tmp) if 'PRMS000' in i]

        print('read idp')
        if True:
            if len(idpList)>0:
                singleToolidp = []
                for k, idppath in enumerate(idpList[0:]):
                    idpname = idppath
                    print(k, idppath, len(idpList))
                    try:
                        tmpTemplate = []
                        tmpCoordinate = []
                        tmpPrms = []
                        result = []
                        f = [i.strip() for i in open(idppath)]
                        if len(f) > 25:
                            for index, i in enumerate(f):
                                if 'history    :' in i:
                                    result.append(i[14:].strip()+'_dosBatDownload')
                                if 'no_of_mpid' in i:
                                    result.append(i.split(':')[1].strip())  # structure qty
                                if 'PRMS000' in i:
                                    tmpPrms.append(i.split(',')[9].strip())
                                if 'template   : MS' in i:
                                    tmpTemplate.append(i.split(':')[-1].strip())  # template enabled
                                if 'msr_point  :' in i:
                                    tmpCoordinate.append(
                                        str(int(int(i.split(',')[2]) / 1000)) + "," + str(int(int(i.split(',')[3]) / 1000)))
                        result.extend([tmpTemplate, tmpPrms, list(pd.DataFrame(tmpCoordinate)[0].unique()),
                                       idpname])  # set-->wrong sequence
                    except:
                        result = []
                    if len(tmpCoordinate) > 0:
                        result.append('SERVER')
                        singleToolidp.append(result)
                    try:
                        shutil.move(idppath,
                                    'Z:/_DailyCheck/CD_SEM_Recipe/IDP/SERVER/dosBatDownload_' + str(datetime.datetime.now())[
                                                                                       0:10] + '_' + idppath.split('/')[-1])
                    except:
                        pass

                df = pd.DataFrame(singleToolidp,
                                  columns=['history', 'dataNo', 'tmplate', 'prmsNo', 'coordinate', 'ppid', 'tool'])
                df['riqi'] = str(datetime.datetime.now())[0:10].replace('-', '')


                try:
                    df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidpSERVER.csv',
                              index=None, mode='a', header=False)
                    df.to_csv('p:/cd/opas/singleToolidpSERVER.csv', index=None, mode='a', header=False)
                except:
                    pass  # 会产生重复项，即内容相同，但日期不同-->后期可以将日期转为index，删除重复项
        print('read amp')
        if True:
            if len(ampList)>0:
                amppath = ampList
                amppath.sort()
                summary = []
                try:
                    for k, amp in enumerate(amppath):
                        print( k, amp, len(amppath))
                        try:
                            f = [i.strip() for i in open(amp) if '_dif' not in i]
                            if len(f) == 54 or len(f) == 55:
                                for index, i in enumerate(f):
                                    if 'comment   ' in i:
                                        f = [i.split(":")[1].strip() for i in f[index + 1:]]
                                        f.append(amp[6:-15])
                                        f.append(amp[-14:-9])
                                        f.append(amp[-8:])
                                summary.append(f)
                                src = amp  # windows XP 下会生成全部四个PRMS文件，包括大小为0的
                                des = 'P:/_GOLDEN_AMP/$HITACHI$DEVICE$HD$' + amp[8:10] + "$data$" + amp[
                                                                                                    6:-15] + '$' + amp[
                                                                                                                   -14:-9] + '$' + amp[
                                                                                                                                   -8:]
                                des1 = 'y:/GoldenAmp/$HITACHI$DEVICE$HD$' + amp[8:10] + "$data$" + amp[
                                                                                                   6:-15] + '$' + amp[
                                                                                                                  -14:-9] + '$' + amp[
                                                                                                                                  -8:]
                                shutil.copyfile(src,
                                                des)  # 手动更改的AMP，优先级最高，作为golden amp，而汇总的数据中，日期前添0。汇总数据按当天日期排序，排序最后的，即最新日期的，而上述des/des1目录没有的amp会被加入

                                shutil.copyfile(src, des1)
                                print('to update feedback')
                                tmpFb.append([str(datetime.datetime.now())[:10].replace('-',""), amp[6:-15], amp[-14:-9]])
                        except:
                            pass
                        # src=amp #windows XP 下会生成全部四个PRMS文件，包括大小为0的
                        # des = 'P:/_GOLDEN_AMP/$HITACHI$DEVICE$HD$' + amp[8:10] + "$data$" + amp[6:-15] + '$' + amp[-14:-9] + '$' + amp[-8:]
                        # des1 = 'y:/GoldenAmp/$HITACHI$DEVICE$HD$' + amp[8:10] + "$data$" + amp[6:-15] + '$' + amp[-14:-9] + '$' + amp[-8:]
                        # shutil.copyfile(src, des)   #手动更改的AMP，优先级最高，作为golden amp，而汇总的数据中，日期前添0。汇总数据按当天日期排序，排序最后的，即最新日期的，而上述des/des1目录没有的amp会被加入

                        # shutil.copyfile(src, des1)
                        shutil.move(amp,
                                    'Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + 'SERVER' + '/dosBatDownload_' + str(datetime.datetime.now())[
                                                                                       :10] + '_' + amp.split('/')[-1])

                        # print('to update feedback')
                        # tmpFb.append([str(datetime.datetime.now())[:10].replace('-',""),amp[6:-15],amp[-14:-9]])
                except:
                    pass
                df = pd.DataFrame(summary)
                df['riqi'] ="0_"+(str(datetime.datetime.now())[0:10]).replace('-', '') #优先级最低，不会影响后续golden amp汇总


                try:#update feedback record
                    tmpFb=pd.DataFrame(tmpFb,columns=['feedback', 'idw', 'idp'])
                    tmpFb.to_csv('y:/goldenamp/summary/feedback.csv',mode='a',header=None,index=None)
                    tmpFb.to_csv('p:/cd/opas/feedback.csv',mode='a',header=None,index=None)

                except:
                    pass


                for tool in ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']: #原CSV文件中的旧算法未删除，后续分析golden recipe合并时需处置
                    try:
                        df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolAmp/' + tool + '_amp.csv', index=None,
                                  header=None, mode='a')
                        df.to_csv('p:/cd/opas/' + tool + '_amp.csv', index=None,
                                  header=None, mode='a')
                    except:
                        pass

        goldenAmp = pd.read_csv('y:/goldenamp/summary/goldenampraw.csv')
        
        try: #如果当日没有批处理下载的文件，会报错
            df=df.drop('riqi',axis=1)
            df.columns=goldenAmp.columns
            for i in range(df.shape[0]):#删除原有记录
                goldenAmp=goldenAmp  [~(goldenAmp['idw'].str.contains(df['idw'][i]) &  \
                            goldenAmp['idp'].str.contains(df['idp'][i]) & \
                            goldenAmp['pNo'].str.contains(df['pNo'][i]))]
            goldenAmp = pd.concat([goldenAmp,df])
            goldenAmp.to_csv('P:/CD/OPAS/goldenAmpRaw.csv',index=None)
            goldenAmp.to_csv('Y:/GoldenAmp/Summary/goldenAmpRaw.csv',index=None)
            #TODO objec,method等参数需解释为中文，并更新goldenAmp.csv
        except:
            pass

    def OVER_WRITE_CD09_IDP(self):
        dst = pd.read_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/LIST/toBeUpload_2019-07-08.csv')
        dst['idp'] = [i + '.idp' for i in dst['key']]
        dst=dst[['idp']].drop_duplicates().reset_index().drop('index',axis=1)
        dst.to_csv('C:/temp/dst.csv', index=None)

        src =[ 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/ALCD09/' + i for i in  os.listdir('Z:/_DailyCheck/CD_SEM_Recipe/IDP/ALCD09/') if '.idp' in i]
        src1=[]
        for i,x in enumerate(src):
            print(i)
            if '2019-' in x:
                src1.append(x.split("/")[5].split('_')[1])
            else:
                src1.append(x.split("/")[5])
        df = pd.DataFrame([src,src1]).T
        df.columns=['src','src1']
        df.to_csv('c:/temp/src.csv', index=None)

        try:
            df=df.sort('src')
        except:
            df=df.sort_values(by='src')
        df=df.set_index('src').drop_duplicates().reset_index()
        dst['src1'] = [ i[27:] for i in list(dst['idp']) ]
        dst.to_csv('c:/temp/dst.csv')


        upload=pd.merge(dst,df,how='left',on=['src1'])
        upload = upload[['idp', 'src']].dropna()
        upload.columns = ['dst', 'src']
        upload=upload.reset_index().drop('index',axis=1)
        upload.to_csv('c:/temp/upload.csv',index=None                      )
        ftp = self.LOGIN('ALCD09')
        for i in range(upload.shape[0]):
            src=upload.iloc[i,1]
            dst=upload.iloc[i,0].replace('$','/')
            print(i,src,dst)
            try:
                fp = open(src, 'rb')
                ftp.storbinary('STOR ' + dst, fp)
                print('pass',i, src, dst)
            except:
                pass
                print('fail', i, src, dst)
    def UPDATE_AMP_DESIGN_RULE(self,idw,idp,prms,designCd,file,xy):#'利用R2R CD Target替换 AMP中的Design Value'
        f = open(file)
        tmp = open(file).read()
        f.close()
        i = tmp.find('design_rule          : ')
        j = tmp.find('design_rule_dif      : ')-1
        tmp = tmp.replace(tmp[i+23:j],designCd)

        m = tmp.find('width_design         : ')
        n = tmp.find('width_design_dif     : ') - 1
        tmp = tmp.replace(tmp[m + 23:n], designCd)

        k = tmp.find('history    : ')
        l = tmp.find('comment    : ')-1
        tmp = tmp.replace(tmp[k + 15:l], file.split('/')[3] + "_" + str(datetime.datetime.now())[0:10]).replace('-','')

        o = tmp.find('xy_direction         :')
        p = tmp.find('xy_direction_dif     :' )-1
        tmp = tmp.replace(tmp[o+23:p],xy)



        ampStr=tmp

        newAmp = 'Y:/GoldenAmp/standarizedAmp/$HITACHI$DEVICE$HD$' + idw[2:4] + '$data$' + idw + '$' + idp + '$' + prms
        newAmp = open(newAmp, 'w')
        newAmp.write(ampStr)
        newAmp.close()
        #pd.DataFrame([[idw, idp, prms, file]], columns=['idw', 'idp', 'pNo', 'template']).to_csv('P:/cd/opas/revisedAmp.csv', mode='a', index=None, header=None) #改掉，汇总后再写入
    def STANDARDIZE_AMP_PARAMETER(self):
        df = pd.read_csv('y:/GoldenAmp/Summary/ampFullSummary.csv')
        df = df.drop('Unnamed: 0',axis=1)# delete index column
        df = df.drop_duplicates()
        tmp = pd.read_csv('y:/GoldenAmp/Summary/feedback.csv')
        tmp.columns=['final','idw','idp']
        df = pd.merge(df,tmp,how='left',on=['idw','idp']).fillna('')
        df = df[ df['feedback']=='Y' ] #CD反馈的
        df = df[ df['check']==True ]    #第一测试点
        df = df [ df['conclusion'].str.startswith('True') | df['final'].str.startswith('20')] #AMP正确的产品
        df = df[df['tech'].str.len()>1] #含工艺代码的
        df=df.drop_duplicates()#重复项哪里来的？原始文件就有的


        df = df.reset_index().drop('index',axis=1) #to be revised amp
        # pd.DataFrame(columns=['idw','idp','pNo','template']).to_csv('P:/cd/opas/revisedAmp.csv',index=None)
        revisedAmp=[]

        print('finished file excluded,   or  read "P:/cd/opas/revisedAmp.csv" to get list')
        tmp = pd.DataFrame(os.listdir('y:/GoldenAmp/standarizedAmp'),columns=['path'])
        tmp = tmp['path'].str.split('$',expand=True)[[6,7,8]]
        tmp.columns=['idw','idp','pNo']
        tmp['tmpFlag']='done'

        df = pd.merge(df,tmp,how='left',on=['idw','idp','pNo']).fillna('')
        df = df[df['tmpFlag']!='done']
        df = df.drop('tmpFlag',axis=1)
        df=df.reset_index().drop('index',axis=1)




        for i in range(df.shape[0]):
            idw, idp, prms, designCd,xy= df['idw'][i], df['idp'][i], df['pNo'][i], str(int(df['cd'][i] * 1000)),str(df['xy_direction'][i])
            print(i,df.shape[0],idw,idp,prms,designCd,xy)

            if df['idp'][i][0:2] in  ['A1','A2','A3','A4','A5','A6','A7','AT','TT','T1','T2','MA']: # to modify metal layers
                if df['object'][i]=='line' and df['method'][i]=='threshold':
                    if df['tech'][i][1]=='1':
                        file='P:/CD/templateAmp/018MetalLine'
                        self.UPDATE_AMP_DESIGN_RULE(idw,idp,prms,designCd,file,xy)
                        revisedAmp.append([idw, idp, prms, file])
                    else:
                        file='P:/CD/templateAmp/035MetalLine'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
            elif df['idp'][i][0:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT', 'OE', 'OV', 'MV']: # to modify hole layers
                if df['object'][i]=='hole' and df['method'][i]=='threshold':
                    if df['tech'][i][1]=='1':
                        file ='P:/CD/templateAmp/018Hole'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
                    else:
                        file = 'P:/CD/templateAmp/035Hole'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
            elif df['idp'][i][0:2] in ['GT','PC','GC']:
                if df['tech'][i][1] == '1':
                    if df['object'][i]=='line' and df['method'][i]=='linear':
                        file = 'P:/CD/templateAmp/018GtLine'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
                else:
                    if df['object'][i]=='line' and df['method'][i]=='threshold':
                        file = 'P:/CD/templateAmp/035GtLine'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
            elif df['idp'][i][0:2] in ['TO']:
                if df['tech'][i][1] == '1':
                    if df['object'][i]=='line' and df['method'][i]=='linear':
                        file = 'P:/CD/templateAmp/018ToLine'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
                else:
                    if df['object'][i]=='line' and df['method'][i]=='threshold':
                        file = 'P:/CD/templateAmp/035ToLine'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
            elif df['idp'][i][0:2] in ['P0']:
                if df['tech'][i][1] == '1':
                    if df['object'][i] == 'space' and df['method'][i] == 'linear':
                        file = 'P:/CD/templateAmp/018P0Space'
                        self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                        revisedAmp.append([idw, idp, prms, file])
            else:
                if df['object'][i]=='line' and df['method'][i]=='linear' and  df['tech'][i][1]=='1':
                    file = 'P:/CD/templateAmp/018NclLine'
                    self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                    revisedAmp.append([idw, idp, prms, file])
                elif df['object'][i]=='space' and df['method'][i]=='linear' and  df['tech'][i][1]=='1':
                    file = 'P:/CD/templateAmp/018NclSpace'
                    self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                    revisedAmp.append([idw, idp, prms, file])
                elif df['object'][i]=='line' and df['method'][i]=='threshold' and  df['tech'][i][1]!='1':
                    file = 'P:/CD/templateAmp/035NclLine'
                    self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                    revisedAmp.append([idw, idp, prms, file])
                elif df['object'][i]=='space' and df['method'][i]=='threshold' and  df['tech'][i][1]!='1':
                    file = 'P:/CD/templateAmp/018NclSpace'
                    self.UPDATE_AMP_DESIGN_RULE(idw, idp, prms, designCd, file,xy)
                    revisedAmp.append([idw, idp, prms, file])
                else:
                    print('special setting')
        pd.DataFrame(revisedAmp,columns=['idw','idp','pNo','template']).to_csv('P:/cd/opas/revisedAmp.csv',index=None,mode='a',header=None)
    def MainFunction(self):
        pass
class CD_SEM_NO_111:
    def __init__(self):
        if "manually download R2R CD config from OPAS" == 1:
            pass
        if "CD PPID" == "CD PPID":
            path = 'P:\\_Script\\ExcelFile\\_PPID.xlsm'
            ppid = pd.read_excel(path, sheet_name='PPID', usecols=[0, 1, 2, 7, 8, 9])
            ppid.columns = ['PART', 'STAGE', 'PPID', 'PART.1', 'STAGE.1', 'ToolType']  # python3.4和python3.6读取的标题不同
            tmp = ppid[['PART.1', 'STAGE.1', 'ToolType']].dropna()
            tmp.columns = ['PART', 'STAGE', 'Tool']
            tmp = tmp[tmp['PART'].str.endswith('-L')]
            ppid = ppid[['PART', 'STAGE', 'PPID']].dropna()
            ppid = pd.merge(ppid, tmp, how='left', on=['PART', 'STAGE']).fillna(
                '').drop_duplicates().reset_index().drop('index', axis=1)
            ppid = ppid[['PPID', 'Tool']]

            ppid['Tool'] = ppid['Tool'].apply(
                lambda x: '-A' if x == 'LDI' else ('-N' if x == 'LII' or x == 'LSI' else ''))
            ppid['IDW'] = ppid['PPID'].str[0:-6] + ppid['Tool']
            ppid['IDP'] = ppid['PPID'].str[-5:]
            ppid = ppid[['IDW', 'IDP']]
            try:
                ppid = ppid.sort_values(by='IDW')
            except:
                ppid = ppid.sort(['IDW'])
            ppid = ppid[[not i for i in (
                        ppid['IDW'].str.startswith('2') | ppid['IDW'].str.startswith('8') | ppid['IDW'].str.startswith(
                    '*'))]]
            ppid = ppid.drop_duplicates()
            ppid['PPID'] = ppid['IDW'] + '$' + ppid['IDP']
            try:
                ppid.to_csv(r'\\10.4.72.74\asml\_DailyCheck\CD_SEM_Recipe\PROMIS_CD_PPID.csv', index=None)
            except:
                pass
            try:
                ppid.to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\cd\PROMIS_CD_PPID.csv', index=None)
            except:
                pass
    def FTP_SERVER(self):
        if "FTP" == "FTP":
            tool='SERVER'
            IPaddress = {'ALCD01': '10.4.152.56', 'ALCD02': '10.4.152.53', 'ALCD03': '10.4.152.54',
                         'ALCD04': '10.4.151.79', 'ALCD05': '10.4.151.81', 'ALCD06': '10.4.151.82',
                         'ALCD07': '10.4.151.50', 'ALCD08': '10.4.153.26', 'ALCD09': '10.4.152.55',
                         'ALCD10': '10.4.153.32', 'BLCD11': '10.4.131.48', 'BLCD12': '10.4.131.47',
                         'SERVER': '10.4.72.240'}
            HOST = IPaddress[tool]
            user = 'user02'
            password = 'qw!1234'
            try:
                ftp = ftplib.FTP(HOST)
            except ftplib.error_perm:
                print('无法连接到"%s"' % HOST)
                return
            print('连接到"%s"' % HOST)

            try:
                # user是FTP用户名，pwd就是密码了
                ftp.login(user, password)
            except ftplib.error_perm:
                print('登录失败')
                ftp.quit()
                return
            print('登陆成功')
        return ftp
    def DOWNLOAD_FROM_SERVER(self):
        ftp = self.FTP_SERVER()
        if "DOWNLOAD"=="DOWNLOAD":
            path = '//10.4.72.74/litho/HITACHI/'
            toollist = ['SERVER', 'ALCD01', 'ALCD02', 'ALCD03', 'ALCD08', 'ALCD09', 'ALCD10', 'BLCD11']
            for tool in toollist:
                os.chdir(path + tool)
                if tool=="SERVER":
                    ftp.cwd('/log')
                else:
                    ftp.cwd('/HITACHI/DEVICE/TRANSFER/'+tool)
                for name in ftp.nlst():
                    if name[-3:]=='tar':
                        print("==downloading " + name + "==")
                        file = open(name,'wb')
                        ftp.retrbinary('RETR ' + name, file.write)
                        ftp.delete(name)

            if 'down TAR'=='down TAR':
                os.chdir(path + "BACKUP")
                ftp.cwd('/HITACHI/TAR')
                for name in ftp.nlst():
                    if name[-3:]=='tar' or name in ['prmslist','idplist']:
                        print(name)
                        file = open(name, 'wb')
                        ftp.retrbinary('RETR ' + name, file.write)
            if 'new' == 'new':
                os.chdir(path+"SERVER")
                ftp.cwd('/dailycheck')
                for name in ftp.nlst():
                    if name[0:3]=='new':
                        print("==downloading " + name + "==")
                        file = open(name,'wb')
                        ftp.retrbinary('RETR ' + name, file.write)
                        ftp.delete(name)
            if 'overwrite' == 'overwrite':
                os.chdir(path+"SERVER")
                ftp.cwd('/dailycheck')
                for name in ftp.nlst():
                    if name[0:9]=='overwrite':
                        print("==downloading " + name + "==")
                        file = open(name,'wb')
                        ftp.retrbinary('RETR ' + name, file.write)
                        ftp.delete(name)

            ftp.quit()
    def READ_AMP(self,amp):
        famp=[]

        try:
            f = [i.strip() for i in open(amp) if '_dif' not in i]
            if len(f) == 54 or len(f) == 55 or len(f)==53:
                for index, i in enumerate(f):
                    if 'comment   ' in i:
                        famp = [i.split(":")[1].strip() for i in f[index + 1:]]
        except:
            pass
        return famp
    def READ_IDP(self,idp):
        try:
            tmpTemplate = []
            tmpCoordinate = []
            tmpPrms = []
            result = []
            f = [i.strip() for i in open(idp)]

            if len(f) > 25:
                for index, i in enumerate(f):
                    if 'history    :' in i:
                        result.append(i[14:].strip())
                    if 'no_of_mpid' in i:
                        result.append(i.split(':')[1].strip())  # structure qty
                    if 'PRMS000' in i:
                        tmpPrms.append(i.split(',')[9].strip())
                    if 'template   : MS' in i:
                        tmpTemplate.append(i.split(':')[-1].strip())  # template enabled

                    if 'msr_point  :' in i:
                        tmpCoordinate.append(
                            str(int(int(i.split(',')[2]) / 1000)) + "," + str(int(int(i.split(',')[3]) / 1000)))
            result.extend([tmpTemplate, tmpPrms, list(pd.DataFrame(tmpCoordinate)[0].unique())])  # set-->wrong sequence
        except:

            result = []

        return result
    def DAILY_NEW_IDP_AMP_BAK(self):
        path = '//10.4.72.74/litho/HITACHI/SERVER/'
        str1 = datetime.datetime.now();
        str1 = str(str1).replace("-", "")[0:8]
        if "PRMS"=="PRMS":
            f1=False;f2=False

            if os.path.exists(path + 'prms' + str1 + '.tar'):
                f1=True
            if os.path.exists(path + 'newprms' + str1):
                f2=True

            if f1 and f2:
                f = tarfile.open(path + 'prms' + str1 + '.tar')
                l = [ i.strip() for i in open(path + 'newprms' + str1).readlines()]
                if os.path.exists('C:/temp/HITACHI'):
                    os.chdir('c:/temp')
                    shutil.rmtree('HITACHI')
                allamp=[]
                for file in l:
                    # print(file)
                    f.extract(file,'c:/temp/HITACHI')
                    amp = 'c:/temp/HITACHI' + file[1:]
                    amp = self.READ_AMP(amp)
                    amp.extend(file.split('/')[3:]) ; amp.append(str1)
                    os.remove('c:/temp/HITACHI/' + file[1:])
                    if len(amp)==55:
                        allamp.append(amp)
                f.close()
                os.chdir('c:/temp')
                shutil.rmtree('HITACHI')
                col = ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
                           'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing',
                           'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line',
                           'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1',
                           'dummy2', 'dummy3', 'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering',
                           'grain_area_x', 'grain_area_y', 'grain_min', 'high_pass_filter', 'y_design_rule',
                           'gap_value_mark', 'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design', 'area_design',
                           'number_design', 'grain_design', 'inverse', 'method2', 'score_design', 'score_number_design',
                           'idw', 'idp', 'prms', 'riqi']
                allamp = pd.DataFrame(allamp,columns=col)

        if "IDP"=="IDP":

            f1=False;f2=False

            if os.path.exists(path + 'idp' + str1 + '.tar'):
                f1=True
            if os.path.exists(path + 'newidp' + str1):
                f2=True


            if f1 and f2:
                f = tarfile.open(path + 'idp' + str1 + '.tar')
                l = [ i.strip() for i in open(path + 'newidp' + str1).readlines()]
                if os.path.exists('C:/temp/HITACHI'):
                    os.chdir('c:/temp')
                    shutil.rmtree('HITACHI')
                allidp=[]
                for file in l:
                    print(file)
                    f.extract(file,'c:/temp/HITACHI')
                    idp = 'c:/temp/HITACHI' + file[1:]
                    idp = self.READ_IDP(idp)
                    idp.extend(file.split('/')[3:]) ; idp.append(str1)
                    os.remove('c:/temp/HITACHI/' + file[1:])
                    if len(idp)>5:
                        allidp.append(idp)
                f.close()
                os.chdir('c:/temp')
                shutil.rmtree('HITACHI')
                col = ['history','dataNo','template','prmsNo','coordinate','idw','idp','riqi']
                allidp = pd.DataFrame(allidp,columns=col)
        return allidp,allamp
    def DAILY_NEW_IDP_AMP(self):
        path = '//10.4.72.74/litho/HITACHI/SERVER/'
        if "PRMS"=="PRMS":
            tarlist = [path + i for i in os.listdir(path) if i [:4]=='prms']
            allamp = []
            if len(tarlist)>0:
                for file in tarlist:
                    f = tarfile.open(file)
                    if os.path.exists('C:/temp/HITACHI'):
                        os.chdir('c:/temp')
                        shutil.rmtree('HITACHI')

                    for ampfile in f.getmembers():
                        print(ampfile)
                        f.extract(ampfile,'c:/temp/HITACHI')
                        amp = 'c:/temp/HITACHI' + ampfile.name[1:]
                        amp = self.READ_AMP(amp)
                        amp.extend(ampfile.name.split('/')[3:]) ; amp.append(file[-12:-4])
                        try:
                            os.remove('c:/temp/HITACHI' + ampfile.name[1:])
                        except:
                            pass
                        if len(amp)==55:
                            allamp.append(amp)
                    f.close()
                    os.chdir('c:/temp')
                    shutil.rmtree('HITACHI')
                for file in tarlist:
                    shutil.move(file,  file[:34]+'done_'+file[34:])



                col = ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
                                   'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing',
                                   'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line',
                                   'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1',
                                   'dummy2', 'dummy3', 'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering',
                                   'grain_area_x', 'grain_area_y', 'grain_min', 'high_pass_filter', 'y_design_rule',
                                   'gap_value_mark', 'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design', 'area_design',
                                   'number_design', 'grain_design', 'inverse', 'method2', 'score_design', 'score_number_design',
                                   'idw', 'idp', 'prms', 'riqi']
                allamp = pd.DataFrame(allamp,columns=col)
            else:
                allamp = pd.DataFrame()
        if "IDP"=="IDP":
            tarlist = [path + i for i in os.listdir(path) if i[:3] == 'idp']
            allidp = []
            if len(tarlist)>0:
                for file in tarlist:
                    f = tarfile.open(file)
                    if os.path.exists('C:/temp/HITACHI'):
                        os.chdir('c:/temp')
                        shutil.rmtree('HITACHI')

                    for idpfile in f.getmembers():
                        print(idpfile)
                        f.extract(idpfile,'c:/temp/HITACHI')
                        idp = 'c:/temp/HITACHI' + idpfile.name[1:]
                        idp = self.READ_IDP(idp)
                        idp.extend(idpfile.name.split('/')[3:]) ; idp.append(file[-12:-4])
                        try:
                            os.remove('c:/temp/HITACHI/' + idpfile.name[1:])
                        except:
                            pass
                        if len(idp)>5:
                            allidp.append(idp)
                    f.close()
                    os.chdir('c:/temp')
                    shutil.rmtree('HITACHI')
                for file in tarlist:
                    shutil.move(file, file[:34]+'done_'+file[34:])

                col = ['history','dataNo','template','prmsNo','coordinate','idw','idp','riqi']
                allidp = pd.DataFrame(allidp,columns=col)
            else:
                allidp=pd.DataFrame()
        return allidp,allamp #多天合并，同一IDP，第一点不同？
    def GOLDEN_RECIPE(self):
        path = '//10.4.72.74/litho/HITACHI/BACKUP/'
        if "PRMS"=="PRMS":
            tarlist = [path + i for i in os.listdir(path) if i [:4]=='prms' and i[-4:]=='.tar']
            allamp = []
            for file in tarlist[0:]:
                f = tarfile.open(file)
                if os.path.exists('C:/temp/HITACHI'):
                    os.chdir('c:/temp')
                    shutil.rmtree('HITACHI')

                for n, ampfile in enumerate(f.getmembers()):

                    print(file, n,len(f.getmembers()),ampfile)
                    f.extract(ampfile,'c:/temp/HITACHI')
                    amp = 'c:/temp/HITACHI' + ampfile.name[1:]
                    if amp[-8:-1]=='PRMS000':
                        amp = self.READ_AMP(amp)
                        mtime = self.GET_MTIME('c:/temp/HITACHI' + ampfile.name[1:])
                        amp.extend(ampfile.name.split('/')[3:]) ; amp.append(mtime)
                        try:
                            os.remove('c:/temp/HITACHI' + ampfile.name[1:])
                        except:
                            pass

                        if len(amp)==55:

                            allamp.append(amp)
                f.close()
                # os.chdir('c:/temp')
                # shutil.rmtree('HITACHI')
            col = ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
                               'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing',
                               'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line',
                               'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1',
                               'dummy2', 'dummy3', 'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering',
                               'grain_area_x', 'grain_area_y', 'grain_min', 'high_pass_filter', 'y_design_rule',
                               'gap_value_mark', 'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design', 'area_design',
                               'number_design', 'grain_design', 'inverse', 'method2', 'score_design', 'score_number_design',
                               'idw', 'idp', 'prms', 'riqi']
            allamp = pd.DataFrame(allamp,columns=col)
        if "IDP"=="IDP":
            tarlist = [path + i for i in os.listdir(path) if i[:3] == 'idp' and i[-4:]=='.tar']
            allidp = []
            for file in tarlist[0:]:
                f = tarfile.open(file)
                if os.path.exists('C:/temp/HITACHI'):
                    os.chdir('c:/temp')
                    shutil.rmtree('HITACHI')

                for n,idpfile in enumerate(f.getmembers()):
                    print(file,n,len(f.getmembers()),idpfile)
                    f.extract(idpfile,'c:/temp/HITACHI')
                    idp = 'c:/temp/HITACHI' + idpfile.name[1:]
                    if idp[-3:]=='idp':
                        idp = self.READ_IDP(idp)
                        mtime = self.GET_MTIME('c:/temp/HITACHI' + idpfile.name[1:])
                        idp.extend(idpfile.name.split('/')[3:]) ; idp.append(mtime)
                        try:
                            os.remove('c:/temp/HITACHI/' + idpfile.name[1:])
                        except:
                            pass
                        if len(idp)>5:
                            allidp.append(idp)
                f.close()
                # os.chdir('c:/temp')
                # shutil.rmtree('HITACHI')
            col = ['history','dataNo','template','prmsNo','coordinate','idw','idp','riqi']
            allidp = pd.DataFrame(allidp,columns=col)
        allamp.to_csv('//10.4.72.74/litho/HITACHI/CHECK/allamp.csv',index=False)
        allidp.to_csv('//10.4.72.74/litho/HITACHI/CHECK/allidp.csv', index=False)
        os.chdir('c:/temp')
        shutil.rmtree('HITACHI')
    def AMP_CHECK(self):
        if "GET_IDP_AMP"=="GET_IDP_AMP":
            idp,amp = self.DAILY_NEW_IDP_AMP()

            print('idp',idp.shape)
            print('amp',amp.shape)

            # amp['object'] = amp['object'].apply(lambda x: 'space' if x == 1 else ('line' if x == 0 else 'hole'))
            # amp['method'] = amp['method'].apply(lambda x: 'linear' if x == 0 else 'threshold')

        if idp.shape[0]>0 and amp.shape[0]>0:

            if "first No" =="first No":
                if idp.shape[0]>0:
                    idp['prmsNo'] = [ i[0] for i in idp['prmsNo']]
                    idp['idp'] = [ i[0:-4] for i in idp['idp']]
                    idp=idp[['idw','idp','prmsNo']]; idp.columns=['idw','idp','prms']
                    amp = pd.merge(idp, amp, how='left', on=['idw', 'idp', 'prms'])
            if 'below is to add process code' \
               '\nmannuall get process code from OPAS' \
               '\npending revise it to update process code '\
                    =='below is to add process code\nmannuall get process code from OPAS\npending revise it to update process code ':
                tmp = pd.read_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\cd\r2rCdConfig.csv')
                tmp.columns = ['﻿CD', 'FAMILY', 'FEEDBACK', 'FULLTECH', 'LAYER', 'LINE_SPACE', '记录数', 'PARTNAME', 'PART',
                               'TECH']
                tmp = tmp[['﻿CD', 'FAMILY', 'FEEDBACK', 'FULLTECH', 'LAYER', 'LINE_SPACE', 'PART', 'TECH']].fillna(
                    '').drop_duplicates()
                tmp.columns = ['cd', 'family', 'feedback', 'fulltech', 'layer', 'line_space', 'part', 'tech']

                tmp['part'] = tmp['part'].apply(lambda x: x[:-4] if x[-4:-2] == '-0' else x)  # rom code
                tmp['layer'] = tmp['layer'].apply(lambda x: x[0:2])  # rom code

                amp['layer'] = [i[0:2] for i in amp['idp']]
                amp['part'] = amp['idw'].apply(
                    lambda x: x[:-2] if x[-4:] == '-L-A' or x[:-4] == '-L-N' else x)  # 大视场idw在part名上额外有-A/-N
                amp = pd.merge(amp, tmp, how='left', on=['part', 'layer']).fillna('')  # df中的idw和R2R中idw不完全相同
            if "check r2r line_space flag: extra type 'H' to be converted, 'H' and 'L' mean 'LINE' and only 'S' stands for 'SPACE'"=="check r2r line_space flag: extra type 'H' to be converted, 'H' and 'L' mean 'LINE' and only 'S' stands for 'SPACE'":
                df = amp.copy();amp=None
                df['r2rLineSpaceFlag'] = df['line_space'].apply(
                    lambda x: 'S' if x == 'S' else ('L' if x == 'L' or x == 'H' else ''))
                df['tmp'] = df['object'].apply(
                    lambda x: 'L' if x == "0" else ('S' if x == "1" or x == "20" else 'WrongSetting'))  # only 0,1,20 allowed
                df['r2rLineSpaceFlag'] = df['r2rLineSpaceFlag'] == df['tmp']  # R2R setting = Recipe Setting?
                df = df.drop('tmp', axis=1)

            if 'Check object parameter'=='Check object parameter':
                tmpObject = df['object']
                tmpLayer = df['layer']
                tmpLineSpace = df['line_space']
                tmp = []
                for i in range(df.shape[0]):
                    if tmpLayer[i] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT', 'OE', 'OV', 'MV']:  # diameter layers
                        tmp.append("20" == tmpObject[i])
                    elif tmpLayer[i] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'TT', 'MA', 'T1', 'T2',
                                         'AT']:  # line for all metal layers
                        tmp.append("0" == tmpObject[i])
                    else:
                        if tmpLineSpace[i] in ['L', 'H']:  # line layers except hole/metal layers
                            tmp.append("0" == tmpObject[i])
                        elif tmpLineSpace[i] == "S":  # space layers except hole/metal layers
                            tmp.append("1" == tmpObject[i])
                        elif tmpLineSpace[i] == '':  # no config in OPAS ,obselete device
                            tmp.append('NoConfig')
                        else:
                            tmp.append('error')  # in case of other setting in r2r config
                df['objectFlag'] = tmp
                tmp, tmpObject, tmpLayer, tmpLineSpace = None, None, None, None
                print('===revise setting per special request of each idw/idp/tech etc===')

            if 'Check Method Parameter'=='Check Method Parameter':
                tmp = []
                tmpMethod = df['method']
                tmpLayer = df['layer']
                tmpTech = df['tech']
                for i in range(df.shape[0]):
                    if tmpLayer[i] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT', 'OE', 'OV', 'MV', 'A1', 'A2', 'A3',
                                       'A4', 'A5', 'A6', 'A7', 'AT', 'TT', 'MA', 'T1', 'T2']:
                        tmp.append("1" == tmpMethod[i])  # threshold algorithm for metal/hole
                    elif tmpTech[i].strip() == '':
                        tmp.append('Pending')  # tech code not availabe for -RD short loop
                    elif tmpTech[i].strip()[1] == '1' or tmpTech[i].strip()[1] == 'A':
                        tmp.append("0" == tmpMethod[i])  # _1%, _A%, CAF tech, linear
                    else:
                        tmp.append("1" == tmpMethod[i])  # threshold except _1% tech
                df['methodFlag'] = tmp
                print('revise setting per special request of each idw/idp/tech etc')

            if 'Check Algorithm'=='Check Algorithm':
                tmp = []
                tmpl_threshold = df['l_threshold']
                tmpr_threshold = df['l_threshold']
                tmpr_base_line = df['r_base_line']
                tmpl_base_line = df['l_base_line']
                tmpr_base_area = df['r_base_area']
                tmpl_base_area = df['l_base_area']
                for i in range(df.shape[0]):
                    if tmpMethod[i] == '0':  # linear algorithm is identical for all process
                        tmp.append(
                            tmpr_base_line[i] == '2' and tmpl_base_line[i] == '2' and tmpr_base_area[i] == '8' and tmpl_base_area[
                                i] == '8')
                    elif tmpMethod[i] == '1':
                        if tmpLayer[i] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT', 'TT', 'MA', 'T1',
                                           'T2']:  # metal threshold
                            if tmpTech[i].strip() == '':  # no tech code for short loop
                                tmp.append('pending')
                            elif tmpTech[i].strip()[1] == '1' or tmpTech[i].strip()[
                                1] == 'A':  # _1%,_A%, CAF process 15% threshold
                                tmp.append(tmpl_threshold[i] == '15' and tmpr_threshold[i] == '15')
                            else:  # 20% except _1% code
                                tmp.append(tmpl_threshold[i] == '20' and tmpr_threshold[i] == '20')

                        else:  # 50% for all threshold except _1% tech
                            tmp.append(tmpl_threshold[i] == '50' and tmpr_threshold[i] == '50')
                    else:
                        tmp.append('pending')
                df['algorithmFlag'] = tmp



            tmp = None
            tmpMethod = None
            tmpLayer = None
            tmpTech = None
            tmpl_threshold = None
            tmpr_threshold = None
            tmpr_base_line = None
            tmpl_base_line = None
            tmpr_base_area = None
            tmpl_base_area = None

            df['object'] = df['object'].apply(lambda x: 'space' if str(x) == '1' else (
                'line' if str(x) == '0' else ('hole' if str(x) == '20' else 'wrong')))
            df['method'] = df['method'].apply(
                lambda x: 'linear' if str(x) == '0' else ('threshold' if str(x) == '1' else ''))

            for i in range(df.shape[0]):
                if df.iloc[i,61]=="":
                    df.iloc[i,63]="";df.iloc[i,64]="";df.iloc[i,65]="";df.iloc[i,66]=""




            tmp = []
            for i in range(df.shape[0]):
                try:
                    tmp.append(df['objectFlag'][i] and df['methodFlag'][i] and df['algorithmFlag'][i])
                except:
                    tmp.append('boolean error')
            df['conclusion'] = tmp
            df.to_csv('c:/temp/00A.csv', index=False)

            df = df[['riqi','idw', 'idp', 'prms','cd', 'feedback', 'fulltech', 'line_space','r2rLineSpaceFlag', 'objectFlag', 'methodFlag', 'algorithmFlag',
                     'conclusion', 'object', 'method','measurement', 'meas_kind',
           'meas_point', 'diameters', 'output_data', 'rot_correct', 'scan_rate','design_rule', 'search_area', 'sum_lines', 'sum_lines2',
           'smoothing', 'differential', 'detect_start', 'l_threshold',
           'l_direction', 'l_edge_no', 'l_base_line', 'l_base_area', 'r_threshold',
           'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1',
           'dummy2', 'dummy3', 'dummy4', 'sum_lines_point', 'assist',
           'xy_direction', 'centering', 'grain_area_x', 'grain_area_y',
           'grain_min', 'high_pass_filter', 'y_design_rule', 'gap_value_mark',
           'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design',
           'area_design', 'number_design', 'grain_design', 'inverse', 'method2',
           'score_design', 'score_number_design']]
            df = df[df['riqi']!=""]
            df = df.drop_duplicates()
            df.to_csv('//10.4.72.74/litho/HITACHI/CHECK/dailycheck.csv',index=None,mode='a',header=False)


        if 1==2:

            try:  # in case file is opened
                df.to_csv('y:/GoldenAmp/Summary/ampFullSummary.csv')
                df.to_csv('p:/cd/opas/ampFullSummary.csv')
            except:
                pass
            try:  # in case file is opened
                df.to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\cd\\ampFullSummary.csv')
            except:
                pass

            output = df[df['check'] == True]
            output = output[output['feedback'] == "Y"]
            output = output[
                ['conclusion', 'idw', 'idp', 'line_space', 'tech', 'family', 'r2rLineSpaceFlag', 'objectFlag', 'methodFlag',
                 'algorithmFlag', 'object', 'method', 'l_threshold',
                 'r_threshold']]  # , 'l_base_line', 'l_base_area','r_base_line', 'r_base_area']]
            try:
                output = output.sort('family', ascending=False)
            except:
                output = output.sort_values(by='family', ascending=False)

            try:
                output.to_csv('y:/GoldenAmp/Summary/simpleSummary.csv', index=None)

                output.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/simpleSummary.csv')

                output.to_csv('P:/cd/opas/simpleSummary.csv', index=None)
            except:
                pass
            try:
                output.to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\cd\\simpleSummary.csv', index=None)
            except:
                pass

            tmp = pd.read_csv('y:/GoldenAmp/Summary/feedback.csv')  # idw/idp 非唯一，可能重复更新，取最新的日期
            tmp = pd.DataFrame(tmp.groupby(['idw', 'idp'])['feedback'].max()).reset_index()

            output = pd.merge(output, tmp, how='outer', on=['idw', 'idp']).reset_index().drop('index', axis=1)

            output['ppid'] = output['idw'] + '.' + output['idp']
            # output=output.drop(['idw','idp'],axis=1)
            output = output.fillna('')
            output['objectMethodLthresholdRthreshold'] = output['object'] + ',' + output['method'] + ',' + [str(i) for i in
                                                                                                            output[
                                                                                                                'l_threshold']] + ',' + [
                                                             str(i) for i in output['r_threshold']]
            # output=output.drop(['object','method','l_threshold','r_threshold'],axis=1)

            try:
                output = output.sort('family', ascending=False)
            except:
                output = output.sort_values(by='family', ascending=False)
            try:
                output.to_csv('y:/GoldenAmp/Summary/forOpas.csv', index=None)
                output.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/DailyIdpAmp/forOpas.csv')
                output.to_csv('P:/cd/opas/forOpas.csv')
            except:
                pass
            try:
                output.to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\cd\\forOpas.csv')
                output.to_excel('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\cd\\forOpas.xlsx')
            except:
                pass
    def GET_MTIME(self,file):
        filemt = time.localtime(os.stat(file).st_mtime)
        return time.strftime("%Y-%m-%d %H:%M:%S", filemt)
    def GOLDEN_RECIPE_BAK(self):
        try:
            os.chdir('c:/temp')
            shutil.rmtree('HITACHI')
        except:
            pass
        path = '//10.4.72.74/litho/HITACHI/BACKUP/'
        if 'PRMS'=='PRMS':
            filelist = [ i.strip() for i in open( path + 'prmslist')]
            l = len ( [ i.strip() for i in open(path + 'old_prmslist')] )
            filelist = filelist[:]; filelist.sort()
            ref = None
            allamp=[]
            if len(filelist)>0:
                for n,file in enumerate(filelist):
                    print(n,len(filelist),file)
                    if file[2:4].isdigit()==True:
                        if ref == None:
                            ref = file[2:4]
                            tar = tarfile.open(path + 'prms' + ref + '.tar')
                            tar.extract(file, 'c:/temp/HITACHI')
                            ampfile = 'c:/temp/HITACHI' + file[1:]
                            amp = self.READ_AMP(ampfile)
                            if len(amp)>50:
                                mtime = self.GET_MTIME(ampfile)
                                amp.extend(ampfile.split('/')[5:]); amp.append(mtime);allamp.append(amp)
                        elif file[2:4]==ref:
                            tar.extract(file, 'c:/temp/HITACHI')
                            ampfile = 'c:/temp/HITACHI' + file[1:]
                            amp = self.READ_AMP(ampfile)
                            if '00/data/AM0037CA/W2-LN/PRMS0002' in ampfile:
                                print('====='+ampfile)
                                print(amp)
                            if len(amp) > 50:
                                mtime = self.GET_MTIME(ampfile)
                                amp.extend(ampfile.split('/')[5:]);
                                amp.append(mtime);
                                allamp.append(amp)
                        else:
                            tar.close(); ref = file[2:4]
                            tar = tarfile.open(path + 'prms' + ref + '.tar')
                            tar.extract(file, 'c:/temp/HITACHI')
                            ampfile = 'c:/temp/HITACHI' + file[1:]
                            amp = self.READ_AMP(ampfile)
                            if len(amp) > 50:
                                mtime = self.GET_MTIME(ampfile)
                                amp.extend(ampfile.split('/')[5:]);
                                amp.append(mtime);
                                allamp.append(amp)
                col = ['measurement', 'object', 'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
                       'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines', 'sum_lines2', 'smoothing',
                       'differential', 'detect_start', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line',
                       'l_base_area', 'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'dummy1',
                       'dummy2', 'dummy3', 'dummy4', 'sum_lines_point', 'assist', 'xy_direction', 'centering',
                       'grain_area_x', 'grain_area_y', 'grain_min', 'high_pass_filter', 'y_design_rule', 'gap_value_mark',
                       'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design', 'area_design', 'number_design',
                       'grain_design', 'inverse', 'method2', 'score_design', 'score_number_design', 'idw', 'idp', 'prms',
                       'riqi']
                allamp = pd.DataFrame(allamp,columns=col)
                allamp.to_csv('//10.4.72.74/litho/HITACHI/CHECK/allamp.csv',index=False,header=False,mode='a')
        if 'IDP'=='IDP':
            filelist = [ i.strip() for i in open( path + 'idplist')]
            l = len ( [ i.strip() for i in open(path + 'old_idplist')] )
            filelist = filelist[:]; filelist.sort()
            ref = None
            allidp=[]
            if len(filelist)>0:
                for n,file in enumerate(filelist):
                    print(n,len(filelist),file)
                    if file[2:4].isdigit()==True:
                        if ref == None:
                            ref = file[2:4]
                            tar = tarfile.open(path + 'idp' + ref + '.tar')
                            tar.extract(file, 'c:/temp/HITACHI')
                            idpfile = 'c:/temp/HITACHI' + file[1:]
                            idp = self.READ_IDP(idpfile)
                            if len(idp)>4:
                                mtime = self.GET_MTIME(idpfile)
                                idp.extend(idpfile.split('/')[5:]); idp.append(mtime);allidp.append(idp)
                        elif file[2:4]==ref:
                            tar.extract(file, 'c:/temp/HITACHI')
                            idpfile = 'c:/temp/HITACHI' + file[1:]
                            idp = self.READ_IDP(idpfile)
                            if len(idp) > 4:
                                mtime = self.GET_MTIME(idpfile)
                                idp.extend(idpfile.split('/')[5:]);
                                idp.append(mtime);
                                allidp.append(idp)
                        else:
                            tar.close(); ref = file[2:4]
                            tar = tarfile.open(path + 'idp' + ref + '.tar')
                            tar.extract(file, 'c:/temp/HITACHI')
                            idpfile = 'c:/temp/HITACHI' + file[1:]
                            idp = self.READ_IDP(idpfile)
                            if len(idp) > 4:
                                mtime = self.GET_MTIME(idpfile)
                                idp.extend(idpfile.split('/')[5:]);
                                idp.append(mtime);
                                allidp.append(idp)
                col = ['history', 'dataNo', 'template', 'prmsNo', 'coordinate', 'idw', 'idp', 'riqi']
                allidp = pd.DataFrame(allidp, columns=col)
                allidp.to_csv('//10.4.72.74/litho/HITACHI/CHECK/allidp.csv',index=False,header=False,mode='a')
        shutil.copy(path + 'prmslist',path + 'old_prmslist')
        shutil.copy(path + 'idplist',path + 'old_idplist')
        try:
            os.chdir('c:/temp')
            shutil.rmtree('HITACHI')
            tar.close()
        except:
            pass
    def MAINFUNCTION(self):
        self.DOWNLOAD_FROM_SERVER()
        self.AMP_CHECK()
        self.GOLDEN_RECIPE()
class WAFER_MAP:

    def __init__(self):
        pass



    def Calculate_Die_Notch_Down(self):
        tmpcount = 0
        stepX = eval(input(" Please Input Step X (mm): "))
        stepY = eval(input(" Please Input Step Y (mm): "))
        dieX = eval(input(" Please Input Die X (mm): "))
        dieY = eval(input(" Please Input Die Y (mm): "))
        # offX = eval(input(" Please Input Map Offset X (mm): "))
        # offY = eval(input(" Please Input Map Offset Y (mm): "))
        #wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        # part = input('Please Input Part Name: ')
        wee=97
        col1,row1 = int(wee // stepX),int(wee // stepY)
        col2,row2 = int(stepX / dieX),int(stepY / dieY)
        shotDie = stepX / dieX * stepY / dieY
        summary=[]
        pricision=0.5


        #=====================================================
        # 'This script is trying to minimize NonebyLS area at left and right sides of wafer.
        # 'Assume: FEC is 3 mm, ZbyLS can reach 8 mm from wafer edge along X-axis
        # '        Pre scan is 13 mm, slit scan is 6 mm per side
        # '        Field is not exposable when scan cent is more than 99.5 mm from wafer center.
        # 'Warning: Step size X less than 16 mm is NOT considered

        # LSmapX
        b1,b2=stepX,stepY
        Ximg=0  #Image Shift on reticle X(size on wafer)
        shiftX1_min,shiftX1_max,shiftX2_min,shiftX2_max="","","",""

        if int(184 / b1 + 1) < 194 / b1:                        #'NonebyLS is unavoidable.
            if 194 - int(194 / b1) * b1 + 2.5 > b1 / 2:         #'(B) NonebyLS exposable in one side when the other side field edge touch FEC
                K2 = 97 - (2 * int(194 / 2 / b1) + 1) * b1 / 2           # 'Field edge touch FEC
                if Ximg>0:
                    XcellA1 = K2 + Ximg                         #'Field edge touch right FEC
                else:
                    XcellA1 = -K2 + Ximg                        #'Field edge touch left FEC
            else:
                K3 = 100 - 0.5 - int(100 / b1 + 1 / 2) * b1     #'Scan center touch wafer edge (99.5 mm from wafer center)
                if 100 - b1 * int(200 / b1) / 2 < 4.25:         #    '(D) Left and right NonebyLS => symmetric map
                    K3 = (int(100 / b1) - int(100 / b1 - 1 / 2)) * b1 / 2
                                                                    #'(C)'
                if Ximg>0:
                    XcellA1=-K3+Ximg                            #'Scan center touch left wafer edge
                else:
                    XcellA1=K3+Ximg                             #'Scan center touch right wafer edge
            shiftX1_min = XcellA1
        else:                                                    #'(A)
            D1 = 92 - b1 * (int(184 / b1 + 1) - 1) / 2           #'Reticle center touch NonebyLS
            D2 = b1 * (int(184 / b1 + 1) / 2) - 97               #''Field edge touch FEC
            if D1<D2:
                K1=D1
            else:
                K1=D2

            if int(184 / b1 + 1) - int(int(184 / b1 + 1) / 2) * 2 == 1:      #'Odd columns within ZbyLS
                if Ximg==0:
                    XcellA1=-K1
                    XcellA2=K1
                else:
                    XcellA1=Ximg
                    XcellA2=Ximg-Ximg/abs(Ximg)*K1
            else:
                if Ximg==0:                                                 # 'Even columns within ZbyLS
                    XcellA1=-b1/2
                    XcellA2=-b1/2+K1
                    XcellB1=b1/2
                    XcellB2=b1/2-K1

                    if  XcellB1 > XcellB2:
                        XBmin = XcellB2
                        XBmax = XcellB1
                    else:
                        XBmin = XcellB1
                        XBmax = XcellB2
                    shiftX2_min=XBmin
                    shiftX2_max=XBmax
                else:
                    XcellA1 = Ximg - Ximg / abs(Ximg) * b1 / 2
                    XcellA2 = Ximg - Ximg / abs(Ximg) * (b1 / 2 - K1)
            if XcellA1 > XcellA2:
                XAmin = XcellA2
                XAmax = XcellA1
            else:
                XAmin = XcellA1
                XAmax = XcellA2
            shiftX1_min = XAmin
            shiftX1_max = XAmax

        print(shiftX1_min)
        print(shiftX1_max)
        print(shiftX2_min)
        print(shiftX2_max)

        l=[]
        if shiftX1_max=='' and shiftX2_min=='' and shiftX2_max=='':
            l=[shiftX1_min]
        else:
            try:
                for i in range  ( int((shiftX1_max-shiftX1_min)/pricision)+1 ):
                    l.append(shiftX1_min + i * pricision)
            except:
                pass
            try:
                for i in range  ( int((shiftX2_max-shiftX2_min)/pricision)+1 ):
                    l.append(shiftX2_min + i * pricision)
            except:
                pass
        l=[round(i,3) for i in l]

        #calculate LSmapY:
        for Xcell in l:
            l1=[]
            shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max = "", "", "", ""
            E1 = (100 - Xcell + Ximg) - int((92 - Xcell + Ximg) / b1) * b1       #'Right side scan center
            E2 = (100 + Xcell - Ximg) - int((92 + Xcell - Ximg) / b1) * b1       # 'Left side scan center
            if E1>E2:
                E=E2
            else:
                E=E1

            H=pow(97 * 97 - pow((100 - E) , 2),0.5)
            dH = H - 13 - 6 - 5
            if dH <= 0:
                shiftY1_min,shiftY2_min = -b2/2,b2/2
                l1=[shiftY1_min,shiftY2_min]
            else:
                if dH<b2/2:
                    shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max= -b2/2,-b2/2+dH,b2/2-dH,b2/2
                    for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
                        l1.append(shiftY1_min + i *pricision)
                    for i in range(int((shiftY2_max - shiftY2_min) / pricision) + 1):
                        l1.append(shiftY2_min + i * 0.1)

                else:
                    shiftY1_min, shiftY1_max= -b2/2,b2/2
                    for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
                        l1.append(shiftY1_min + i * pricision)
            l1=[round(i,3) for i in l1]

            for Ycell in l1:
                offX,offY=Xcell,Ycell
                # summary.append([Xcell, Ycell])








                totalDie = 0
                fullShot=0
                partialShot=0
                for i in range(-col1 - 1, col1 + 2):
                    for j in range(-row1 - 1, row1 + 2):

                        llx = i * stepX - stepX / 2 + offX
                        lly = j * stepY - stepY / 2 + offY

                        f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                        f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                        f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        # laser mark
                        f5 = ((lly + stepY) > 92) and ((llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                        f5 = not f5
                        # notch
                        f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                        f6 = not f6

                        if f1 and f2 and f3 and f4 and f6:
                            totalDie = totalDie + shotDie
                            fullShot=fullShot+1
                        else:
                            if f1 or f2 or f3 or f4:
                                partialShotDie = 0
                                partialShot=partialShot+1
                                for k in range(0, col2):
                                    for l in range(0, row2):
                                        sx = llx + k * dieX
                                        sy = lly + l * dieY

                                        f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
                                        f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
                                        f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                        f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                        f5 = ((sy + dieY) > 92) and ((sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
                                        f5 = not f5

                                        f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                        f6 = not f6

                                        if f1 and f2 and f3 and f4 and f5 and f6:
                                            partialShotDie += 1

                                totalDie = totalDie + partialShotDie
                print(totalDie)
                summary.append([Xcell, Ycell,totalDie,fullShot,partialShot,fullShot+partialShot])
        summary = pd.DataFrame(summary,columns=['shiftX','shiftY','DieQty','FullShot','PartialShot','TotalShot'])
        summary = summary.sort_values(by='TotalShot')
        print(summary)
    def Calculate_Die_Notch_Left(self):
        tmpcount = 0
        stepX = eval(input(" Please Input Step X (mm): "))
        stepY = eval(input(" Please Input Step Y (mm): "))
        dieX = eval(input(" Please Input Die X (mm): "))
        dieY = eval(input(" Please Input Die Y (mm): "))
        # offX = eval(input(" Please Input Map Offset X (mm): "))
        # offY = eval(input(" Please Input Map Offset Y (mm): "))
        #wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        # part = input('Please Input Part Name: ')
        wee=97
        col1,row1 = int(wee // stepX),int(wee // stepY)
        col2,row2 = int(stepX / dieX),int(stepY / dieY)
        shotDie = stepX / dieX * stepY / dieY
        summary=[]
        pricision=0.5


        #=====================================================
        # 'This script is trying to minimize NonebyLS area at left and right sides of wafer.
        # 'Assume: FEC is 3 mm, ZbyLS can reach 8 mm from wafer edge along X-axis
        # '        Pre scan is 13 mm, slit scan is 6 mm per side
        # '        Field is not exposable when scan cent is more than 99.5 mm from wafer center.
        # 'Warning: Step size X less than 16 mm is NOT considered

        # LSmapX
        b1,b2=stepX,stepY
        Ximg=0  #Image Shift on reticle X(size on wafer)
        shiftX1_min,shiftX1_max,shiftX2_min,shiftX2_max="","","",""

        if int(184 / b1 + 1) < 194 / b1:                        #'NonebyLS is unavoidable.
            if 194 - int(194 / b1) * b1 + 2.5 > b1 / 2:         #'(B) NonebyLS exposable in one side when the other side field edge touch FEC
                K2 = 97 - (2 * int(194 / 2 / b1) + 1) * b1 / 2           # 'Field edge touch FEC
                if Ximg>0:
                    XcellA1 = K2 + Ximg                         #'Field edge touch right FEC
                else:
                    XcellA1 = -K2 + Ximg                        #'Field edge touch left FEC
            else:
                K3 = 100 - 0.5 - int(100 / b1 + 1 / 2) * b1     #'Scan center touch wafer edge (99.5 mm from wafer center)
                if 100 - b1 * int(200 / b1) / 2 < 4.25:         #    '(D) Left and right NonebyLS => symmetric map
                    K3 = (int(100 / b1) - int(100 / b1 - 1 / 2)) * b1 / 2
                                                                    #'(C)'
                if Ximg>0:
                    XcellA1=-K3+Ximg                            #'Scan center touch left wafer edge
                else:
                    XcellA1=K3+Ximg                             #'Scan center touch right wafer edge
            shiftX1_min = XcellA1
        else:                                                    #'(A)
            D1 = 92 - b1 * (int(184 / b1 + 1) - 1) / 2           #'Reticle center touch NonebyLS
            D2 = b1 * (int(184 / b1 + 1) / 2) - 97               #''Field edge touch FEC
            if D1<D2:
                K1=D1
            else:
                K1=D2

            if int(184 / b1 + 1) - int(int(184 / b1 + 1) / 2) * 2 == 1:      #'Odd columns within ZbyLS
                if Ximg==0:
                    XcellA1=-K1
                    XcellA2=K1
                else:
                    XcellA1=Ximg
                    XcellA2=Ximg-Ximg/abs(Ximg)*K1
            else:
                if Ximg==0:                                                 # 'Even columns within ZbyLS
                    XcellA1=-b1/2
                    XcellA2=-b1/2+K1
                    XcellB1=b1/2
                    XcellB2=b1/2-K1

                    if  XcellB1 > XcellB2:
                        XBmin = XcellB2
                        XBmax = XcellB1
                    else:
                        XBmin = XcellB1
                        XBmax = XcellB2
                    shiftX2_min=XBmin
                    shiftX2_max=XBmax
                else:
                    XcellA1 = Ximg - Ximg / abs(Ximg) * b1 / 2
                    XcellA2 = Ximg - Ximg / abs(Ximg) * (b1 / 2 - K1)
            if XcellA1 > XcellA2:
                XAmin = XcellA2
                XAmax = XcellA1
            else:
                XAmin = XcellA1
                XAmax = XcellA2
            shiftX1_min = XAmin
            shiftX1_max = XAmax

        print(shiftX1_min)
        print(shiftX1_max)
        print(shiftX2_min)
        print(shiftX2_max)

        l=[]
        if shiftX1_max=='' and shiftX2_min=='' and shiftX2_max=='':
            l=[shiftX1_min]
        else:
            try:
                for i in range  ( int((shiftX1_max-shiftX1_min)/pricision)+1 ):
                    l.append(shiftX1_min + i * pricision)
            except:
                pass
            try:
                for i in range  ( int((shiftX2_max-shiftX2_min)/pricision)+1 ):
                    l.append(shiftX2_min + i * pricision)
            except:
                pass
        l=[round(i,3) for i in l]

        #calculate LSmapY:
        for Xcell in l:
            l1=[]
            shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max = "", "", "", ""
            E1 = (100 - Xcell + Ximg) - int((92 - Xcell + Ximg) / b1) * b1       #'Right side scan center
            E2 = (100 + Xcell - Ximg) - int((92 + Xcell - Ximg) / b1) * b1       # 'Left side scan center
            if E1>E2:
                E=E2
            else:
                E=E1

            H=pow(97 * 97 - pow((100 - E) , 2),0.5)
            dH = H - 13 - 6 - 5
            if dH <= 0:
                shiftY1_min,shiftY2_min = -b2/2,b2/2
                l1=[shiftY1_min,shiftY2_min]
            else:
                if dH<b2/2:
                    shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max= -b2/2,-b2/2+dH,b2/2-dH,b2/2
                    for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
                        l1.append(shiftY1_min + i *pricision)
                    for i in range(int((shiftY2_max - shiftY2_min) / pricision) + 1):
                        l1.append(shiftY2_min + i * 0.1)

                else:
                    shiftY1_min, shiftY1_max= -b2/2,b2/2
                    for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
                        l1.append(shiftY1_min + i * pricision)
            l1=[round(i,3) for i in l1]

            for Ycell in l1:
                offX,offY=Xcell,Ycell
                # summary.append([Xcell, Ycell])








                totalDie = 0
                fullShot=0
                partialShot=0
                for i in range(-col1 - 1, col1 + 2):
                    for j in range(-row1 - 1, row1 + 2):

                        llx = i * stepX - stepX / 2 + offX
                        lly = j * stepY - stepY / 2 + offY

                        f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                        f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                        f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        # laser mark
                        f5 = ((llx + stepX) > 92) and ((lly + stepY < 13 and lly + stepY > -13) or (lly < 13 and lly > -13))
                        f5 = not f5
                        # notch
                        f6 = (llx < -94) and ((lly + stepY < 14 and lly + stepY > -14) or (lly < 14 and lly > -14))
                        f6 = not f6

                        if f1 and f2 and f3 and f4 and f6:
                            totalDie = totalDie + shotDie
                            fullShot=fullShot+1
                        else:
                            if f1 or f2 or f3 or f4:
                                partialShotDie = 0
                                partialShot=partialShot+1
                                for k in range(0, col2):
                                    for l in range(0, row2):
                                        sx = llx + k * dieX
                                        sy = lly + l * dieY

                                        f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
                                        f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
                                        f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                        f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                        f5 = ((sx + dieX) > 92) and ((sy + dieY < 13 and sy + dieY > -13) or (sy < 13 and sy > -13))
                                        f5 = not f5

                                        f6 = (sx < -94) and ((sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
                                        f6 = not f6

                                        if f1 and f2 and f3 and f4 and f5 and f6:
                                            partialShotDie += 1

                                totalDie = totalDie + partialShotDie
                print(totalDie)
                summary.append([Xcell, Ycell,totalDie,fullShot,partialShot,fullShot+partialShot])
        summary = pd.DataFrame(summary,columns=['shiftX','shiftY','DieQty','FullShot','PartialShot','TotalShot'])
        summary = summary.sort_values(by='TotalShot')
        print(summary)
    def gating_notch_down(self,shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee):
        tmpcount = 0
        tmpcount1 = 0

        for k in range(0, col2):
            for l in range(0, row2):
                sx = llx + k * dieX
                sy = lly + l * dieY

                f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
                f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
                f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                f5 = ((sy + dieY) > 92) and ((sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
                f5 = not f5

                f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                f6 = not f6

                if (f1 and f2 and f3 and f4 and f5 and f6):
                    tmpcount += 1

                tmpcount1 = tmpcount1 + len([i for i in [f1, f2, f3, f4] if i == True]) / 4

        # return (tmpcount1 / shotDie < 0.15) and (tmpcount / gdw < 0.005)
        return False
    def Plot_Map_Notch_Down(self):


        tmpcount = 0
        stepX = eval(input(" Please Input Step X (um): "))/1000
        stepY = eval(input(" Please Input Step Y (um): "))/1000
        dieX = eval(input(" Please Input Die X (um): "))/1000
        dieY = eval(input(" Please Input Die Y (um): "))/1000
        offX = eval(input(" Please Input Map Offset X (um): "))/1000
        offY = eval(input(" Please Input Map Offset Y (um): "))/1000
        wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        part = input('Please Input Part Name: ')
        gdw = 3.14*97*97/dieX/dieY




        col1 = int(wee // stepX)
        row1 = int(wee // stepY)

        col2 = int(stepX / dieX)
        row2 = int(stepY / dieY)

        shotDie = stepX / dieX * stepY / dieY

        totalDie = 0

        fig = plt.figure(figsize=(10, 10))
        ax = fig.add_subplot(111)

        # ell1 = Ellipse(xy = (0.0, 0.0), width = 4, height = 8, angle = 30.0, facecolor= 'yellow', alpha=0.3)
        # ax.add_patch(ell1)

        cir1 = Circle(xy=(0, 0), radius=100, alpha=1, fill=False, edgecolor='black', linewidth=1)
        ax.add_patch(cir1)
        cir1 = Circle(xy=(0, 0), radius=wee, alpha=1, fill=False, edgecolor='blue', linewidth=1)
        ax.add_patch(cir1)

        for i in range(-col1 - 1, col1 + 2):
            # for i in range(-100,100):
            for j in range(-row1 - 1, row1 + 2):

                llx = i * stepX - stepX / 2 + offX
                lly = j * stepY - stepY / 2 + offY

                f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                # laser mark
                f5 = ((lly + stepY) > 92) and ((llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                f5 = not f5
                # notch
                f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                f6 = not f6

                if f1 and f2 and f3 and f4 and f6 and f5:
                    square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False, edgecolor='red',
                                       linewidth=0.3)
                    ax.add_patch(square)
                    totalDie = totalDie + shotDie
                else:
                    if f1 or f2 or f3 or f4:

                        partialShotDie = 0

                        flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee)

                        for k in range(0, col2):
                            for l in range(0, row2):
                                sx = llx + k * dieX
                                sy = lly + l * dieY

                                f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
                                f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
                                f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                f5 = ((sy + dieY) > 92) and (
                                            (sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
                                f5 = not f5

                                f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                f6 = not f6

                                if f1 and f2 and f3 and f4 and f5 and f6:
                                    if flag == True:
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='pink',
                                                           alpha=1, fill=True, edgecolor='pink', linewidth=0.3)
                                        ax.add_patch(square)

                                    else:

                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
                                                           alpha=0.3, fill=True, edgecolor='red', linewidth=0.3)
                                        ax.add_patch(square)
                                        partialShotDie += 1

                                else:
                                    if f1 or f2 or f3 or f4:
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
                                                           alpha=0.5, fill=True, edgecolor='green', linewidth=0.3)
                                        ax.add_patch(square)
                            square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
                                               edgecolor='red', linewidth=0.3)
                            ax.add_patch(square)
                        totalDie = totalDie + partialShotDie

        square = Rectangle(xy=(-13, 92), width=26, height=8, facecolor='yellow', alpha=0.5, fill=True,
                           edgecolor='black', linewidth=0.3)
        ax.add_patch(square)

        square = Rectangle(xy=(-14, -100), width=28, height=6, facecolor='yellow', alpha=0.5, fill=True,
                           edgecolor='black', linewidth=0.3)
        ax.add_patch(square)

        triangle = plt.Polygon([[0, -95], [3, -100], [-3, -100]], color='purple', alpha=1)  # 顶点坐标颜色α
        ax.add_patch(triangle)

        x, y = 0, 0
        ax.plot(x, y, 'ro')

        plt.axis('scaled')
        # ax.set_xlim(-8, 8)
        # ax.set_ylim(-8,8)
        plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length

        plt.text(-50, 50, 'Total Die Qty: ' + str(int(totalDie)))
        plt.text(-50, 40, 'Step Size: ' + str(stepX) + ', ' + str(stepY))
        plt.text(-50, 30, 'Die Size ' + str(dieX) + ', ' + str(dieY))
        plt.text(-50, 20, 'Offset Size: ' + str(offX) + ', ' + str(offY))
        plt.text(-50, 10, 'Edge Exclusion: ' + str(100 - wee))
        plt.text(-50, 0, 'Unit: mm ')
        plt.text(-50, -10, 'Product: ' + part)
        plt.text(-50, -20, 'Orientation:Notch Down ' )

        plt.savefig('c:\\temp\\' + part + '1.jpg', dpi=600)
        plt.savefig('c:\\temp\\' + part + '2.jpg', dpi=100)
        plt.show()
    def Plot_Map_Notch_Left(self):


        tmpcount = 0
        stepX = eval(input(" Please Input Step X (um): "))/1000
        stepY = eval(input(" Please Input Step Y (um): "))/1000
        dieX = eval(input(" Please Input Die X (um): "))/1000
        dieY = eval(input(" Please Input Die Y (um): "))/1000
        offX = eval(input(" Please Input Map Offset X (um): "))/1000
        offY = eval(input(" Please Input Map Offset Y (um): "))/1000
        wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        part = input('Please Input Part Name: ')
        gdw = 3.14*97*97/dieX/dieY




        col1 = int(wee // stepX)
        row1 = int(wee // stepY)

        col2 = int(stepX / dieX)
        row2 = int(stepY / dieY)

        shotDie = stepX / dieX * stepY / dieY

        totalDie = 0

        fig = plt.figure(figsize=(10, 10))
        ax = fig.add_subplot(111)

        # ell1 = Ellipse(xy = (0.0, 0.0), width = 4, height = 8, angle = 30.0, facecolor= 'yellow', alpha=0.3)
        # ax.add_patch(ell1)

        cir1 = Circle(xy=(0, 0), radius=100, alpha=1, fill=False, edgecolor='black', linewidth=1)
        ax.add_patch(cir1)
        cir1 = Circle(xy=(0, 0), radius=wee, alpha=1, fill=False, edgecolor='purple', linewidth=1)
        ax.add_patch(cir1)

        for i in range(-col1 - 1, col1 + 2):
            # for i in range(-100,100):
            for j in range(-row1 - 1, row1 + 2):

                llx = i * stepX - stepX / 2 + offX
                lly = j * stepY - stepY / 2 + offY

                f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                # laser mark
                f5 = ((llx + stepX) > 92) and ((lly + stepY < 13 and lly + stepY > -13) or (lly < 13 and lly > -13))
                f5 = not f5
                # notch
                f6 = (llx < -94) and ((lly + stepY < 14 and lly + stepY > -14) or (lly < 14 and lly > -14))
                f6 = not f6

                if f1 and f2 and f3 and f4 and f6 and f5:
                    square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False, edgecolor='red',
                                       linewidth=0.5)
                    ax.add_patch(square)
                    totalDie = totalDie + shotDie
                else:
                    if f1 or f2 or f3 or f4:
                        # square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
                        #                    edgecolor='red', linewidth=0.5)
                        # ax.add_patch(square)
                        partialShotDie = 0

                        # flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee)
                        flag=False
                        for k in range(0, col2):
                            for l in range(0, row2):
                                sx = llx + k * dieX
                                sy = lly + l * dieY

                                f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
                                f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
                                f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
                                f5 = ((sx + dieX) > 92) and (
                                            (sy + dieY < 13 and sy + dieY > -13) or (sy < 13 and sy > -13))
                                f5 = not f5

                                f6 = (sx < -94) and ((sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
                                f6 = not f6

                                if f1 and f2 and f3 and f4 and f5 and f6:
                                    if flag == True:
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='pink',
                                                           alpha=1, fill=True, edgecolor='pink', linewidth=0.3)
                                        ax.add_patch(square)

                                    else:

                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
                                                           alpha=0.5, fill=True, edgecolor='blue', linewidth=0.3)
                                        ax.add_patch(square)
                                        partialShotDie += 1

                                else:
                                    if f1 or f2 or f3 or f4:
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
                                                           alpha=0.5, fill=True, edgecolor='green', linewidth=0.3)
                                        ax.add_patch(square)

                            square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
                                               edgecolor='red', linewidth=0.5)
                            ax.add_patch(square)
                        totalDie = totalDie + partialShotDie

        square = Rectangle(xy=(92, -13), width=8, height=26, facecolor='yellow', alpha=0.5, fill=True,
                           edgecolor='black', linewidth=0.3)
        ax.add_patch(square)

        square = Rectangle(xy=(-100, -14), width=6, height=28, facecolor='yellow', alpha=0.5, fill=True,
                           edgecolor='black', linewidth=0.3)
        ax.add_patch(square)

        triangle = plt.Polygon([[-95, 0], [-100, 3], [100, -3]], color = 'r', alpha = 1 )#顶点坐标颜色α
        ax.add_patch(triangle)




        x, y = 0, 0
        ax.plot(x, y, 'ro')

        plt.axis('scaled')
        # ax.set_xlim(-8, 8)
        # ax.set_ylim(-8,8)
        plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length

        plt.text(-50, 50, 'Total Die Qty: ' + str(int(totalDie)))
        plt.text(-50, 40, 'Step Size: ' + str(stepX) + ', ' + str(stepY))
        plt.text(-50, 30, 'Die Size ' + str(dieX) + ', ' + str(dieY))
        plt.text(-50, 20, 'Offset Size: ' + str(offX) + ', ' + str(offY))
        plt.text(-50, 10, 'Edge Exclusion: ' + str(100 - wee))
        plt.text(-50, 0, 'Unit: mm ')
        plt.text(-50, -10, 'Product: ' + part)
        plt.text(-50,-20,'Orientation: Notch Left')

        plt.savefig('c:\\temp\\' + part + '1.jpg', dpi=600)
        plt.savefig('c:\\temp\\' + part + '2.jpg', dpi=100)
        plt.show()
class AVAILABLE_TOOL:
    def __init__(self):
        path = 'Z:/_DailyCheck/outlook/NewFlow/'
        filenamelist = [path + i for i in os.listdir(path) if '.TXT' in i]
        old = pd.read_csv('p:/_script/ExcelFile/TECH.csv', encoding='GBK')
        filenamelist = list(set(filenamelist) - set(old['PATH']))

        result = []
        for file in filenamelist[:]:
            new = pd.read_html(file, encoding='GBK', header=0)
            new = pd.DataFrame(new[0])

            result.append([new.iloc[1, 0], new.iloc[1, 1], new.iloc[2, 1], str(datetime.datetime.now())[0:10], file])
            print('3\n',result)
        new = pd.DataFrame(result, columns=['PART', 'TECH', 'OWNER', 'RIQI', 'PATH'])
        new.to_csv('p:/_script/ExcelFile/TECH.csv', index=None, encoding='GBK', mode='a',header=None)
        print(new.shape)


    def ESF(self):
        import re
        # all = {'ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07',
        #         'ALDI09', 'ALDI10', 'ALDI11', 'ALDI12', 'BLDI08', 'BLDI13', 'ALII01', 'ALII02', 'ALII03', 'ALII04',
        #         'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14', 'ALII15', 'ALII16', 'ALII17', 'ALII18',
        #         'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL', 'BLSIE1', 'BLSIE2', 'ALII01', 'ALII02',
        #         'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14', 'ALII15', 'ALII16',
        #         'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL', 'BLSIE1', 'BLSIE2'}

        # asml ={'LDI': {'ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07', 'ALDI09', 'ALDI10', 'ALDI11', 'ALDI12', 'BLDI08',
        #         'BLDI13'}}

        # nikon ={'LII': {'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
        #          'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL',
        #          'BLSIE1', 'BLSIE2'},'LSI': {'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
        #          'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL',
        #          'BLSIE1', 'BLSIE2'}}

        all = {
            'LII': {'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
                    'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL',
                    'BLSIE1', 'BLSIE2'},
            'LSI': {'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
                    'ALII15', 'ALII16', 'ALII17', 'ALII18', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL',
                    'BLSIE1', 'BLSIE2'},
            'LDI': {'ALDI02', 'ALDI03', 'ALDI05', 'ALDI06', 'ALDI07', 'ALDI09', 'ALDI10', 'ALDI11', 'ALDI12', 'BLDI08',
                    'BLDI13'}
                }

        # nikonLII = ['ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14',
        #             'ALII15', 'ALII16', 'ALII17', 'ALII18']

        # nikonLSI = ['ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'BLSIBK', 'BLSIBL', 'BLSIE1', 'BLSIE2']

        print("Tech is not the latest data, it was extracted from OPAS,needs to be refreshed")
        Tech = pd.read_excel('P:/_Script/ExcelFile/flow.xlsm', sheet_name='Tech')
        Tech = Tech[['PART','TECH']]
        tmp = [ True if len(str(i).strip())>5 else False for i in Tech['TECH']]
        Tech = Tech[tmp]

        tmp = pd.read_csv('p:/_script/ExcelFile/TECH.csv', encoding='GBK')
        tmp = tmp[['PART','TECH']]
        tmp['PART'] = [i.strip().split('.')[0] for i in tmp['PART']]
        
        # if True:
        #     print('alternatively refresh data from LiYan')
        #     tmp = pd.read_excel('P:/recipe/曝光菜单备份表1.xls',sheet_name='曝光菜单备份表')
        #     tmp = tmp[['产品名','工艺名']]
        #     tmp.columns=['PART','TECH']
        #     tmp = tmp.drop_duplicates().dropna().reset_index().drop('index',axis=1)
        #     tmp['PART'] = [ x.upper().strip() for x in list(tmp['PART'])]
        #     tmp['TECH'] = [ x.upper().strip() for x in list(tmp['TECH'])]
        #     tmp['PART'] = tmp['PART'].apply(lambda x: x.split('-L')[0]+'-L' if ('NIKON' in x or 'ASML' in x) else x ) #仍有部分旧产品不规范，忽略
        #     tmp = tmp.drop_duplicates().dropna().reset_index().drop('index', axis=1)

        new = list(set(tmp['PART'])-set(Tech['PART']))
        tmp = tmp[[ x in new for x in list(tmp['PART'])]]

        Tech = pd.concat([Tech,tmp]) # merge OPAS old data with outlook data
        new=None
        Tech=Tech.dropna() #in case full tech code is not defined for some parts(-RD flows,etc)


        Flow = pd.read_excel('P:/_Script/ExcelFile/flow.xlsm', sheet_name='Flow') #All FLOW,  demo data only,switch to real data, only inline wip
        Flow = pd.merge(Flow, Tech, how='left', on='PART')
        Tech = None
        ActiveFlow = pd.read_excel('P:/_Script/ExcelFile/flow.xlsm', sheet_name='Real')
        Flow = pd.merge(ActiveFlow,Flow,how='left',on='PART')
        ActiveFlow=None

        tmp= [ True if len(str(i))>5 else False for i in Flow['TECH']]
        #TODO   Flow[[ not i for i in tmp ]]['PART'].unique()  part without FULL TECH
        Flow = Flow[tmp]
        #TODO LiYan table does dot define TECH for 'BA','CA',etc,  CD RECIPE DOUBLE CHECK CAF13F910015工艺 参照1%



        # ['PART', 'RECPID', 'EQPTYPE', 'STAGE', 'TRACKRECIPE', 'LAYER', 'TECH', 'DIENUM']

        ESF = pd.read_excel('P:/_Script/ExcelFile/flow.xlsm', sheet_name='ESF', usecols=[0, 1, 2, 3, 5, 7])
        ESF = ESF.fillna('')
        # remove constraint for tool shut down
        ESF =ESF[[ not (ESF.iloc[i,0]=='' and   ESF.iloc[i,1]=='' and   ESF.iloc[i,2]==''and ESF.iloc[i,3]=='') for i in range(ESF.shape[0])]]
        ESF = ESF.reset_index().drop('index',axis=1)


        Flow = Flow.fillna('')
        Flow = Flow[['RECPID', 'EQPTYPE', 'STAGE', 'PART', 'TRACKRECIPE', 'LAYER', 'TECH']]#, 'DIENUM']]
    
        # if True:#ESF LOOP
        #
        #     t1=datetime.datetime.now()
        #     for i in range(100):#range(ESF.shape[0]):
        #         print(i)
        #         f0 = [True if ESF.iloc[i,0] == "" else (True if re.match( ESF.iloc[i,0], x) != None else False) for x in Flow['TECH']]
        #         f1 = [True if ESF.iloc[i, 1] == "" else (True if re.match(ESF.iloc[i, 1], x) != None else False) for x in Flow['PART']]
        #         f2 = [True if ESF.iloc[i, 2] == "" else (True if re.match(ESF.iloc[i, 2], x) != None else False) for x in Flow['STAGE']]
        #         f3 = [True if ESF.iloc[i, 3] == "" else (True if re.match(ESF.iloc[i, 3], x) != None else False) for x in Flow['RECPID']]
        #         f=f0 and f1 and f2 and f3
        #     t2 = datetime.datetime.now()
        
        print('========================\nData Ready For Check\n========================\n')
        
        
        enabledTool = []
        toolQty=[]
        t1 = datetime.datetime.now()
        for i in range (Flow.shape[0]):
            print(i, Flow.shape[0])

            f1 = [ True  if x=="" else ( True if re.match(x, Flow.iloc[i, 6])!=None else False ) for x in ESF['TECH'] ]
            f2 = [ True  if x=="" else ( True if re.match(x, Flow.iloc[i, 3])!=None else False ) for x in ESF['PART'] ]
            f3 = [ True  if x=="" else ( True if re.match(x, Flow.iloc[i, 2])!=None else False ) for x in ESF['STAGE'] ]
            f4 = [ True  if x=="" else ( True if re.match(x, Flow.iloc[i, 0])!=None else False ) for x in ESF['RECIPE'] ]
            # f = f1 and f2 and f3 and f4
            f = [f1[n] and f2[n] and f3[n] and f4[n] for n in range(ESF.shape[0])]


            tmpCheck = ESF.copy()
            tmpCheck['Flag']=f
            tmpCheck = tmpCheck[tmpCheck['Flag']==True]
            tmpDisable = tmpCheck[tmpCheck['TYPE']==1]['EQPID'].unique()
            tmpEnable = tmpCheck[tmpCheck['TYPE']==0]['EQPID'].unique()

            if len(tmpEnable) == 0:
                calculation = all[Flow.iloc[i,1]] - set(tmpDisable)

            else:
                calculation = set(tmpEnable) & ( all[Flow.iloc[i,1]] - set(tmpDisable))
            enabledTool.append(calculation)
            toolQty.append(len(calculation))
        t2=datetime.datetime.now()




        try:
            Flow['Available'] = enabledTool
            Flow['ToolQty'] = toolQty
            Flow = Flow.sort_values(by=['ToolQty', 'LAYER', 'PART'], ascending=True)

            Flow.to_csv('Z:/_DailyCheck/ESF/ESF_TOOL_AVAILABLE_' +str(datetime.datetime.now())[:10]+'.csv',index=None)

            Flow = Flow[[ 'EQPTYPE', 'STAGE', 'PART', 'LAYER', 'TECH', 'Available', 'ToolQty']]


            Flow['EQPTYPE'] = ['ASML' if i=='LDI' else 'NIKON' for i in Flow['EQPTYPE']]
            Flow = Flow.drop_duplicates()

            Flow.to_csv('Z:/_DailyCheck/ESF/ESF_TOOL_AVAILABLE.csv')



        except:
            pass

        print(str(t2 - t1))



        # if True: #memory error len(X)=58075683
        #     x=y=[]
        #     for i in Flow['TECH']:
        #         x.extend([i] * (ESF.shape[0]))
        #     for i in ESF['TECH']:
        #         y.extend([i] * (Flow.shape[0]))
        #     t1=datetime.datetime.now()
        #     f1=[True if y == "" else (True if re.match(y, x) != None else False) for (x,y) in zip(x,y)]
        #     t2=datetime.datetime.now()
        #     print(str(t2-t1))
class ASML_MC_CONS_LOG_SUMMARY:
    def __init__(self):
        pass
    def ReadStrLines(self,strLines):
        history=[]
        for x in strLines:
            if len(x)>20 and x[-3]==':' and x[-6]==':':
                riqi = x[-20:]
            if x[-1]==':':
                parameter=x[:-1].strip().replace('-','')

            if  '(was' in x:
                try:
                    post = eval( x.split('(was')[0].strip() )
                except: # some parameters is Y or N
                    post = x.split('(was')[0].strip()
                try:
                    pre = eval( x.split('(was')[1].split(')')[0].strip() )
                except:
                    pre = x.split('(was')[1].split(')')[0].strip()
                history.append([riqi,parameter,post,pre])

    def MainFunction(self):
        mcpath= '//10.4.72.74/asml/_AsmlDownload/AsmlSysData/MC/'
        filelist = [ mcpath + i for  i in os.listdir(mcpath) if ('old' not in i and i[0:2]=='MC')]
        df = pd.DataFrame(columns=['riqi','parameter','post','pre','tool'])
        for file in filelist[:]:
            tool = file[-2:]
            print(tool)
            strLines = [ i.strip() for i in open(file) if i.strip()!=""]
            # history = self.ReadStrLines(self,strLines)
            if True:
                history = []
                for no,x in enumerate(strLines):
                    if len(x) > 20 and x[-3] == ':' and x[-6] == ':':
                        riqi = x[-20:]
                    if x[-1] == ':':
                        parameter = x[:-1].strip().replace('-', '')
                    if '(was' in x:
                        try:
                            post = (x.split('(was')[0].strip())
                        except:  # some parameters is Y or N
                            pass
                            # post = x.split('(was')[0].strip()
                        try:
                            pre = (x.split('(was')[1].split(')')[0].strip())
                        except:
                            pass
                            # pre = x.split('(was')[1].split(')')[0].strip()
                        if ('NaN' in post and 'NaN' in pre)!=True:# and parameter !='':
                            history.append([riqi, parameter, post, pre])

            history = pd.DataFrame(history,columns=['riqi','parameter','post','pre'])
            history['tool'] = 'SD'+tool
            df = pd.concat([df,history])

        old = pd.read_csv(mcpath + 'summary.csv').fillna('')

        df = pd.concat([old,df])

        df=df.drop_duplicates()
        print(df.shape)
        df = df.sort_values(by=['tool','parameter','riqi'])

        df.to_csv(mcpath + 'summary.csv', index=None)
        df.to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\ASML_MC_LOG.csv',index=None)
class MFG_FLOW_PPID_FOR_OPAS:
    def __init__(self):
        pass
    def MainFunction(self):
        if False:
            df = pd.read_excel('P:/_Script/ExcelFile/FLOW_FOR_OPAS.xlsm',sheet_name='Flow')
            df['LAYER']=[ '' if '.' not in i else i.split('.')[1][0:2] for i in df['PPID']]
            df['KEY'] = df['PART'] + '#' + df['STAGE']
            tmp1 = df[df['EQPTYPE']=='LSI']['KEY']
            tmp2 = df[df['EQPTYPE']=='LII']['KEY']
            tmp3 =[x for x in  list(set(tmp1) - set(tmp2)) if  not ('FU' in x or 'PI' in x or 'PAD' in x or 'PAF' in x)]
            #单独的LSI工艺均为PAD/PAF/PI/FU 及老产品PO1

        if True:
            df = pd.read_excel('P:/_Script/ExcelFile/FLOW.xlsm', sheet_name='Flow')
            tmp = [ not  i for i in  list(df['EQPTYPE'].str.contains('S'))]
            df=df[tmp]
            df.to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\flowPpid.csv',index=None)
class ASML_ERROR_LOG_FOR_OPAS:
    def __init__(self):
        pass
    def MianFunction(self,path='//10.4.72.74/asml/_AsmlDownload/AsmlErrLog/'):
        toollist = ['08','7D','82','83','85','86','87','89','8A','8B','8C']
        summary=pd.DataFrame(columns=['content','id'])
        for tool in toollist:
            try:
                tmp = open(path + tool + '/ER_event_log.old')
                data = [i.strip() for i in tmp if i.strip() != ""]
                data.extend([i.strip() for i in open(path + tool + '/ER_event_log') if i.strip() != ''])
                data = pd.DataFrame(data,columns=['content'])
                data['id']='ALSD'+tool
                summary = pd.concat([summary,data])
            except:
                pass
        summary=summary.reset_index().drop('index',axis=1)
        summary.to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlErrLog.csv',header=None)
class OvlCheckNew:
    def __init__(self):
        pass
    def get_path(self,FileDir=r'P:\OVLdata\GOL'):
        filenamelist = []
        for root, dirs, files in os.walk(FileDir,
                                         False):  # root:所有目录名-->字符串 #dirs: root下的子目录名-->列表 #files：root下的文件名-->列表 # name.endswith(ext)-->文件名筛选
            for names in files:
                filenamelist.append(root + '\\' + names)
        filenamelist.sort(reverse=False)
        return (filenamelist)
    def get_path_time(self,FileDir=r'P:\OVLdata\GOL'):
        filelist = []
        for root, dirs, files in os.walk(FileDir, False):
            # root:所有目录名-->字符串 #dirs: root下的子目录名-->列表 #files：root下的文件名-->列表
            # name.endswith(ext)-->文件名筛选
            for singlefile in files:
                tmp1= root + '\\' + singlefile
                tmp2 = (os.stat(tmp1).st_mtime)
                tmp2 = time.strftime('%Y-%m-%d %H:%M:%S', time.localtime(tmp2))

                filelist.append([tmp1,tmp2])
        return (filelist)
    def read_raw_data(self):
        col = ['Path', 'Folder', 'Part', 'Ppid', 'RiQi', 'Max_x', 'Max_y', 'Count',
                'A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8', 'A9', 'A10', 'A11',
                'A12', 'A13', 'A14', 'A15', 'A16', 'Obselete', 'UploadRiqi', 'EqpType']
        if 1==1: #get kla data
            data = []
            FileDir = r'P:\OVLdata\Old'
            # FileDir = r'Y:\OVERLAY\RawDataBackup\KLA'
            filelist = self.get_path(FileDir)
            for n, file in enumerate(filelist):
                print(n,file)
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

                            singleData=[Path,Folder,Part,Ppid,RiQi,Max_x,Max_y,Count]
                            singleData.extend(tmp)
                            singleData.extend(([False,str(datetime.datetime.now())[0:20],'Archer']))
                            data.append(singleData)
                            print(data)
                except:
                    pass
                    os.remove(file)
                shutil.move(file, 'Y:/OVERLAY/RawDataBackup/KLA/'+file.split('\\')[3])
            data = pd.DataFrame(data,columns=col)
            data.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\zuobiao.csv',index=None,header=None,mode='a')
        if 2==2: #get q200 data--》data duplicated
            FileDir = 'Z:\\_DailyCheck\\Q200_LD\\'
            # FileDir = "Y:\\OVERLAY\\RawDataBackup\\Q200"
            if 'getPath' == 'getPath':
                filelist = []
                for root, dirs, files in os.walk(FileDir, False):
                    # root:所有目录名-->字符串 #dirs: root下的子目录名-->列表 #files：root下的文件名-->列表
                    # name.endswith(ext)-->文件名筛选
                    for singlefile in files:
                        if singlefile[-2:]=='ld':
                            tmp1 = root + '\\' + singlefile
                            tmp2 = (os.stat(tmp1).st_mtime)
                            tmp2 = time.strftime('%Y-%m-%d %H:%M:%S', time.localtime(tmp2))

                            filelist.append([tmp1, tmp2])
            if len(filelist)>0:
                if 'getLastestDate'=='getLastestDate':
                    tmp=[]
                    for x in filelist:
                        try:
                            if x[0][-2:]=='ld':
                                tmp.append([x[0].split('\\')[-1]+x[1],x[0].split('\\')[-1],x[0]])
                        except:
                            pass
                    tmp.sort(reverse=True)

                    filelist=[tmp[0][-1]]
                    ref = tmp[0][1]
                    for x in tmp:
                        if x[1] != ref:
                            ref = x[1][:]
                            filelist.append(x[-1])
                if 'readQ200'=='readQ200':
                    Q200 = []
                    for n,file in enumerate(filelist[:]):
                        print(n,len(filelist),file)
                        data=[]
                        try:
                            f = [i.strip('\n') for i in open(file).readlines()]
                            for n, tmp in enumerate(f):
                                if tmp == '\t\t\tlocations=4,':
                                    Path=file
                                    Ppid=file.split('\\')[-1][:-3]
                                    data.extend([eval(f[n + 2][10:].split(',')[0]), eval(f[n + 2][10:].split(',')[1][0:-1])])
                                    data.extend([eval(f[n + 4][10:].split(',')[0]), eval(f[n + 4][10:].split(',')[1][0:-1])])
                                    data.extend([eval(f[n + 6][10:].split(',')[0]), eval(f[n + 6][10:].split(',')[1][0:-1])])
                                    data.extend([eval(f[n + 8][10:].split(',')[0]), eval(f[n + 8][10:].split(',')[1][0:-1])])
                                    data = [ i/1000 for i in data]
                            if len(data) == 8:
                                singleData = [Path, '', Ppid[:-3], Ppid, '', '', '', 8]
                                singleData.extend(data)
                                singleData.extend(['', '', '', '', '', '', '', ''])
                                singleData.extend(([False, str(datetime.datetime.now())[0:20], 'Q200']))
                                Q200.append(singleData)
                            shutil.move(file, 'Y:/OVERLAY/RawDataBackup/Q200/' + file.split('\\')[-1])
                        except:
                            try:
                                os.remove(file)
                            except:
                                pass
                    Q200 = pd.DataFrame(Q200, columns=col)
                    Q200.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\zuobiao.csv',index=None,header=None,mode='a')
    def read_standard_coordinate(self):
        old = list(pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\standardZuobiao.csv')['Read'].unique())
        tmp = 'P:\\Recipe\\Coordinate\\'
        filelist = [tmp  + i for i in os.listdir(tmp) if i[-3:]=='txt']
        filelist =list( set(filelist) - set(old))


        for n, file in enumerate(filelist):
            print(n,len(filelist),file)
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

                    tmp['Path']=file
                    tmp['Part']=file.split('\\')[3][:-4]
                    tmp=tmp[['Path','Part','PPID', 'LDx', 'LDy', 'RDx', 'RDy', 'RUx', 'RUy', 'LUx', 'LUy']]
                    tmp.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\standardZuobiao.csv',header=None,index=None,mode='a')
                    new.append(file)
            except:
                pass

        df = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\standardZuobiao.csv')
        filelist.extend(old)
        filelist.extend(['']*(df.shape[0] - len(filelist)))

        df['Read']=filelist
        df.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\standardZuobiao.csv',index=None)
    def read_asml_step_size(self):
        old = list(pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\stepSize.csv')['Path'])
        asmlfilepath = 'P:/Recipe/ASMLBACKUP/'  # only latest files
        asmlfilepathnew = 'P:/Recipe/recipe/'
        filelist = []
        for file in os.listdir(asmlfilepath):
            filelist.append(os.path.join(asmlfilepath, file))
        for file in os.listdir(asmlfilepathnew):
            filelist.append(os.path.join(asmlfilepathnew, file))
        filelist =list( set(filelist) - set(old))

        data = []
        for n, file in enumerate(filelist):
            try:
                tmp = [i.strip('\n') for i in open(file).readlines() if i.strip()[0:22] == 'Cell Size [mm]       X'][
                    0].strip().split(':')
                Path=file
                Part = file.split('/')[3]
                StepX=tmp[1].strip().split(' ')[0]
                StepY=tmp[2].strip()
                if StepX !='':
                    data.append([Path,Part,StepX,StepY])
                    if Part[-2:]=='-D':
                        data.append([Path, Part[0:-2], StepX, StepY]) # "-D" is added by LiYan, not original Part Name
            except:
                pass
        if len(data)>0:
            pd.DataFrame(data).to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\stepSize.csv',index=None,header=None,mode='a')
    def read_bias_table(self):
        old=[]
        try:
            old = list(pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\BT.csv',encoding='GBK')['Path'].unique())
            tmp=True
        except:
            tmp=False
        if tmp==False:
            try:
                old = list(pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\BT.csv')['Path'].unique())
            except:
                pass

        path = 'P:/recipe/biastable/'
        filelist = []
        for file in os.listdir(path):
            filelist.append(os.path.join(path, file))
        filelist =list( set(filelist) -set(old))

        for n, file in enumerate(filelist[:]):
            print(n, '===', len(filelist))
            try:
                BiasTable = pd.read_excel(file)
                BiasTable.columns = [i for i in range(len(BiasTable.columns))]
                BiasTable = BiasTable[[3,6, 19]].dropna() # 3：PPID，6：physical Layer label
                tmp ,tmp1= [],[]
                for n,i in enumerate(list(BiasTable[3])):  ##PPID
                    try:
                        tmp.append(i.strip()[0:2])
                    except:
                        tmp.append(str(n))
                for n, i in enumerate(list(BiasTable[6])): #physical name
                    try:
                        if len(i.strip())>=7 and i.strip()[4]=='-':  #部分放版名
                            tmp1.append(i.strip()[5:7])
                        else:
                            tmp1.append(i.strip()[0:2])
                    except:
                        tmp1.append(str(n))


                BiasTable['Path'] = file
                BiasTable['Part'] = file.split('/')[3].split('.')[0]
                d = dict(zip(tmp,tmp1))  #d = dict([(tmp[i],tmp1[i]) for i in range(len(tmp))])

                tmp2=[]
                for i in range(len(tmp1)):#不清楚ovl-to的层次是PPID还是物理名
                    try:
                        tmp2.append(tmp1[i] +  '-' + d[ list( BiasTable[19] )[i][0:2] ] )
                    except:
                        tmp2.append(tmp1[i] + '-' + list( BiasTable[19] )[i][0:2])
                BiasTable['Ppid'] = tmp2

                BiasTable['Ovl_Ppid'] = BiasTable['Part'] + '-' + tmp  # BiasTable[6] 这个有问题，AA/BA共享
                BiasTable = BiasTable.drop(columns=[3,6, 19])

                if BiasTable.shape[0]>0:
                    BiasTable.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\BT.csv',index=None,header=None,mode='a')
                    if (file.split('/')[3].split('.')[0])[-2:]=='-D':  # filename(part) ends with '-D' is not correct ,remove '-D'
                        BiasTable['Ovl_Ppid'] = [i[:-5] + i[-3:] for i in  BiasTable['Ovl_Ppid']]
                        BiasTable['Part'] =   [i[:-2] for i in  BiasTable['Part']]
                        BiasTable.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\BT.csv', index=None,
                                         header=None, mode='a')
            except:
                pass
    def refresh_ppid_from_xls(self):
        mfg = pd.read_excel('P:\\_Script\\ExcelFile\\_PPID.xlsm', sheet_name='PPID')
        tmp1 = mfg[['PART.1', 'STAGE.1', 'PPID.1']].dropna()
        tmp1.columns = ['Part', 'Stage', 'OVL-PPID']
        tmp2 = mfg[['TECH', 'PART.2', 'STAGE.2', 'ToolType']].dropna()
        tmp2.columns = ['TECH', 'Part', 'Stage', 'ToolType']
        flow = pd.merge(tmp1, tmp2, how='left', on=['Part', 'Stage'])
        flow['ToolType']= [ 'LII' if i=='LSI' else i for i in flow['ToolType']]
        flow = flow.drop_duplicates()
        flow = flow[['Part', 'Stage', 'OVL-PPID', 'ToolType']]
        #=======================================================
        tmp = mfg[['PART', 'STAGE', 'PPID']].dropna()
        tmp.columns = ['PART','STAGE','CD']
        tmp1 = flow[['Part','Stage','ToolType','OVL-PPID']]
        tmp1.columns=['PART','STAGE','ToolType','OVL']
        tmp = pd.merge(tmp,tmp1,how='outer',on=['PART','STAGE'])
        tmp = tmp.fillna('')
        tmp = tmp.drop_duplicates() #部分OVL测两步？？？
        tmp=tmp.reset_index().drop('index',axis=1)
        
        tmp['key']=tmp['PART']+tmp['STAGE']
        new=set(list(tmp['key']))
        old = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\PPID.csv')[['PART', 'STAGE']]
        old['key']=old['PART']+old['STAGE']
        old=set(list(old['key']))
        new=list(new-old)

        choice = [True if i in new else False for i in list(tmp['key'])]
        tmp=tmp[choice]
        tmp=tmp[ ['PART', 'STAGE', 'CD', 'ToolType', 'OVL']]

        tmp.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\PPID.csv',index=None,header=None,mode='a')
    def ppid_flag_check(self):
        # F1 严格意义的匹配
        # F2 坐标原点不准的匹配
        # F3 有测试坐标，无标准坐标：1）CE未提供数据--》缺少相关层次数据，且OVL位置未共享，例如MEMS工艺MV/MA；OVL位置共享，只提供了一个，例如NL，PL
        if 'merge'=='merge': #标准坐标的标题不对，实际是从左下角开始的逆时针
            if 1==1: #PPID with coordinate only
                PPID = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\PPID.csv')[['PART', 'STAGE', 'ToolType', 'OVL', 'ArcherCheck', 'Q200Check',
           'Check', 'RiQi', 'F1', 'F2', 'F3', 'F4', 'F5', 'F6', 'F7']].fillna('')
                PPID = PPID[PPID['OVL']!='']
                STANDARD_ZB = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\standardZuoBiao.csv')

                t1 =[i[:6] for i in  list(STANDARD_ZB['Part'].unique())]
                t2 = [i[:6] for i in  list(PPID['PART'])]
                t2 = [ True if i in t1 else False for i in t2]
                PPID = PPID[t2].reset_index().drop('index',axis=1)
                t1=t2=None
                if 'ROMCODE'=="ROMCODE":
                    PPID['PART'] = [ i[:-4] if (i[-4]=='-' and i[-3:-1]!='RD') else i for i in PPID['PART'] ]
            if 2==2: #merge measurement sequence
                try: #早期biastable格式不对，measurement sequence数据不对--》忽略，没有标准坐标
                    BT = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\BT.csv')
                except:
                    BT = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\BT.csv',encoding='GBK')


                BT.columns=['Path','PART','Ppid','LAYER']
                BT['LAYER']=[i[-2:] for i in BT['LAYER']]
                BT=BT[['PART','Ppid','LAYER']]
                PPID['LAYER']=[i[-2:] for i in PPID['OVL']]
                PPID = pd.merge(PPID,BT,how='left',on=['PART','LAYER']) #BA,CA
                BT=None   #部分ROM CODE Part和同一stage测两层的，有问题; 部分ROM-code Part名后只有两位
            if 3==3: #stepping size for large field coordination calculation
                STEPSIZE = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\stepSize.csv')[['Part', 'StepX', 'StepY']]
                STEPSIZE.columns=['PART','StepX','StepY']
                STEPSIZE['key'] = [i[:6] for i in STEPSIZE['PART']]
                STEPSIZE = STEPSIZE.drop('PART',axis=1)
                STEPSIZE=STEPSIZE.drop_duplicates()
                PPID['key'] = [i[:6] for i in PPID['PART']]

                PPID = pd.merge(PPID, STEPSIZE, how='left', on=['key'])  # BA,CA
                STEPSIZE=None
            if 4==4:#read coordinate

                STANDARD_ZB['key']=[i[:6] for i in STANDARD_ZB['Part']]
                PPID =  pd.merge(PPID, STANDARD_ZB, how='left', on=['key','Ppid'])
                STANDARD_ZB=None
                PPID = PPID.drop('Read',axis=1)
            if 5==5:
                ZB = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\zuoBiao.csv') #coordinates duplicated,keep latest
                ZB = ZB[['Ppid', 'Count', 'A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8', 'A9', 'A10', 'A11',
       'A12', 'A13', 'A14', 'A15', 'A16', 'Obselete', 'UploadRiqi', 'EqpType']].fillna('')
                if 1==1: #keep latest data only
                    ZB['key']=ZB['Ppid']+'#'+ZB['EqpType']
                    tmp = pd.DataFrame(ZB.groupby('key')['key'].count())
                    t1 = list(tmp[tmp['key']==1].index)
                    t1 = [ True if i in t1 else False for i in list(ZB['key'])]
                    t1 = ZB[t1]#PPID 唯一


                    t2 = list(tmp[tmp['key']!=1].index)
                    t2 = [True if i in t2 else False for i in list(ZB['key'])]
                    t2 = ZB[t2]
                    t2 = t2.sort_values(by=['key','UploadRiqi'],ascending=False)


                    tmp = list(t2['key'])
                    for no,x in enumerate(tmp):
                        if no==0:
                            ref = tmp[0]
                            t3=[True]
                        else:
                            if x==ref:
                                t3.append(False)
                            else:
                                ref = x[:]
                                t3.append(True)
                    t2 = t2[t3]

                    ZB = pd.concat([t1,t2],axis=0).reset_index().drop(['index','key'],axis=1)

                PPID.columns = ['PART', 'STAGE', 'ToolType', 'Ppid', 'ArcherCheck', 'Q200Check', 'Check',
       'RiQi', 'F1', 'F2', 'F3', 'F4', 'F5', 'F6', 'F7', 'LAYER', 'AlignTo',
       'key', 'StepX', 'StepY', 'Path', 'Part', 'LDx', 'LDy', 'LUx', 'LUy',
       'RDx', 'RDy', 'RUx', 'RUy']

                PPID = pd.merge(PPID,ZB,how='left',on=['Ppid'])
                PPID=PPID.fillna('')
                ZB=None
                PPID = PPID.reset_index()
                PPID=PPID.drop(['Path','Part','index'],axis=1)
            if 51==51:#F3标记为已下载坐标的层次
                PPID['F3'] = [ True if i in [8,16] else False for i in PPID['Count']]
            PPID.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\merge.csv',index=None)
            if 6==6:#Archer 坐标负值加StepSize
                # PPID = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\merge.csv')
                # PPID = PPID.reset_index().drop('index',axis=1)
                # PPID = PPID.fillna('')
                for i in range(PPID.shape[0]):
                    if PPID.iloc[i,47]=='Archer':#此处逻辑不明，Archer10有的负值要加stepsize，有的不需要；Q200似乎不要

                        if PPID.iloc[i,0][-2:]=='-L' and PPID.iloc[i,2]=='LII':
                            for j in [29,31,33,35,]:

                                if PPID.iloc[i,j]!="" and PPID.iloc[i,j]<0 and abs(PPID.iloc[i,j])>PPID.iloc[i,19]/4:
                                    PPID.iloc[i,j] = PPID.iloc[i,j] + PPID.iloc[i,18]


                            for j in [30,32,34,36]:
                                if PPID.iloc[i,j]!="" and PPID.iloc[i,j]<0 and abs(PPID.iloc[i,j])>PPID.iloc[i,18]/2:
                                    PPID.iloc[i,j] = PPID.iloc[i,j] + PPID.iloc[i,19]
                        else:
                            for j in [29,31,33,35,37,39,41,43]:

                                if PPID.iloc[i,j]!="" and PPID.iloc[i,j]<0 and abs(PPID.iloc[i,j])>PPID.iloc[i,18]/2:
                                    PPID.iloc[i,j] = PPID.iloc[i,j] + PPID.iloc[i,18]


                            for j in [30,32,34,36,38,40,42,44]:
                                if PPID.iloc[i,j]!="" and PPID.iloc[i,j]<0 and abs(PPID.iloc[i,j])>PPID.iloc[i,19]/2:
                                    PPID.iloc[i,j] = PPID.iloc[i,j] + PPID.iloc[i,19]

        if 'check'=='check':
            x = ['' for i in range(PPID.shape[0])]
            x = [x for i in range(32)]
            x = pd.DataFrame(x).T
            x.columns = ['B1', 'B2', 'B3', 'B4', 'B5', 'B6', 'B7', 'B8', 'B9', 'B10', 'B11', 'B12', 'B13', 'B14', 'B15',
                         'B16',
                         'C1', 'C2', 'C3', 'C4', 'C5', 'C6', 'C7', 'C8', 'C9', 'C10', 'C11', 'C12', 'C13', 'C14', 'C15',
                         'C16']
            PPID=pd.concat([PPID,x],axis=1)
            for i in range(PPID.shape[0]):
                if PPID.iloc[i,20]!='' and PPID.iloc[i,29]!="": #标准坐标和测试坐标都有
                    if PPID.iloc[i,0][-2:]=='-L' and PPID.iloc[i,2]=='LII':
                        try:
                            PPID.iloc[i,48] = ((PPID.iloc[i,20] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i,29])
                            PPID.iloc[i,49] = ((PPID.iloc[i,21] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i,30])

                            PPID.iloc[i,50] = ((PPID.iloc[i,22] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i,31])
                            PPID.iloc[i,51] = ((PPID.iloc[i,23] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i,32])

                            PPID.iloc[i,52] = ((PPID.iloc[i,24] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i,33])
                            PPID.iloc[i,53] = ((PPID.iloc[i,25] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i,34])

                            PPID.iloc[i,54] = ((PPID.iloc[i,26] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i,35])
                            PPID.iloc[i,55] = ((PPID.iloc[i,27] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i,36])

                            PPID.iloc[i, 64] = abs(
                                (PPID.iloc[i, 20] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 29]) < 0.03
                            PPID.iloc[i, 65] = abs(
                                (PPID.iloc[i, 21] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 30]) < 0.03

                            PPID.iloc[i, 66] = abs(
                                (PPID.iloc[i, 22] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 31]) < 0.03
                            PPID.iloc[i, 67] = abs(
                                (PPID.iloc[i, 23] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 32]) < 0.03

                            PPID.iloc[i, 68] = abs(
                                (PPID.iloc[i, 24] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 33]) < 0.03
                            PPID.iloc[i, 69] = abs(
                                (PPID.iloc[i, 25] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 34]) < 0.03

                            PPID.iloc[i, 70] = abs(
                                (PPID.iloc[i, 26] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 35]) < 0.03
                            PPID.iloc[i, 71] = abs(
                                (PPID.iloc[i, 27] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 36]) < 0.03


                            if False in  list(PPID.loc[i][64:72]):
                                PPID.iloc[i,8]=False
                            elif True in list(PPID.loc[i][64:72]):
                                PPID.iloc[i,8]=True

                            x = [PPID.iloc[i,48],PPID.iloc[i,50],PPID.iloc[i,52],PPID.iloc[i,54]]
                            y = [PPID.iloc[i,49],PPID.iloc[i,51],PPID.iloc[i,53],PPID.iloc[i,55]]
                            PPID.iloc[i,9]=(max(x)-min(x))<0.03 and (max(y)-min(y))<0.03


                        except:
                            print('-L,LII-->ERROR')
                    elif PPID.iloc[i,0][-2:]=='-L' and PPID.iloc[i,2]=='LDI':
                        try:
                            if PPID.iloc[i,28]==16:
                                PPID.iloc[i, 48] = (
                                    (PPID.iloc[i, 23] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 29])
                                PPID.iloc[i, 49] = (
                                    (-PPID.iloc[i, 22] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 30])

                                PPID.iloc[i, 50] = (
                                    (PPID.iloc[i, 25] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 31])
                                PPID.iloc[i, 51] = (
                                    (-PPID.iloc[i, 24] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 32])

                                PPID.iloc[i, 52] = (
                                    (PPID.iloc[i, 27] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 33])
                                PPID.iloc[i, 53] = (
                                    (-PPID.iloc[i, 26] / 1000 + PPID.iloc[i, 19] * 3 / 4) - PPID.iloc[i, 34])

                                PPID.iloc[i, 54] = (
                                    (PPID.iloc[i, 21] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 35])
                                PPID.iloc[i, 55] = (
                                    (-PPID.iloc[i, 20] / 1000 + PPID.iloc[i, 19] * 3 / 4) - PPID.iloc[i, 36])

                                PPID.iloc[i, 56] = (
                                    (PPID.iloc[i, 23] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 37])
                                PPID.iloc[i, 57] = (
                                    (-PPID.iloc[i, 22] / 1000 + PPID.iloc[i, 19] * 3 / 4) - PPID.iloc[i, 38])

                                PPID.iloc[i, 58] = (
                                    (PPID.iloc[i, 25] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 39])
                                PPID.iloc[i, 59] = (
                                    (-PPID.iloc[i, 24] / 1000 + PPID.iloc[i, 19] * 3 / 4) - PPID.iloc[i, 40])

                                PPID.iloc[i, 60] = (
                                    (PPID.iloc[i, 27] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 41])
                                PPID.iloc[i, 61] = (
                                    (-PPID.iloc[i, 26] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 42])

                                PPID.iloc[i, 62] = (
                                    (PPID.iloc[i, 21] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 43])
                                PPID.iloc[i, 63] = (
                                    (-PPID.iloc[i, 20] / 1000 + PPID.iloc[i, 19] / 4) - PPID.iloc[i, 44])


                                PPID.iloc[i,64] = abs((PPID.iloc[i,23] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 29]) < 0.03
                                PPID.iloc[i,65] = abs((-PPID.iloc[i,22] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i, 30]) < 0.03

                                PPID.iloc[i,66] = abs((PPID.iloc[i,25] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 31]) < 0.03
                                PPID.iloc[i,67] = abs((-PPID.iloc[i,24] / 1000 +PPID.iloc[i,19] / 4) - PPID.iloc[i, 32]) < 0.03

                                PPID.iloc[i,68] = abs((PPID.iloc[i,27] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 33]) < 0.03
                                PPID.iloc[i,69] = abs((-PPID.iloc[i,26] / 1000 + PPID.iloc[i,19] * 3 / 4) - PPID.iloc[i, 34]) < 0.03

                                PPID.iloc[i,70] = abs((PPID.iloc[i,21] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 35]) < 0.03
                                PPID.iloc[i,71] = abs((-PPID.iloc[i,20] / 1000 + PPID.iloc[i,19] * 3 / 4) - PPID.iloc[i, 36]) < 0.03

                                PPID.iloc[i,72] = abs((PPID.iloc[i,23] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 37]) < 0.03
                                PPID.iloc[i,73] = abs((-PPID.iloc[i,22] / 1000 + PPID.iloc[i,19] * 3 / 4) - PPID.iloc[i, 38]) < 0.03

                                PPID.iloc[i,74] = abs((PPID.iloc[i,25] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 39]) < 0.03
                                PPID.iloc[i,75] = abs((-PPID.iloc[i,24] / 1000 + PPID.iloc[i,19] * 3 / 4) - PPID.iloc[i, 40]) < 0.03

                                PPID.iloc[i,76]= abs((PPID.iloc[i,27] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 41]) < 0.03
                                PPID.iloc[i,77] = abs((-PPID.iloc[i,26] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i, 42]) < 0.03

                                PPID.iloc[i,78] = abs((PPID.iloc[i,21] / 1000 + PPID.iloc[i,18] / 2) - PPID.iloc[i, 43]) < 0.03
                                PPID.iloc[i,79] = abs((-PPID.iloc[i,20] / 1000 + PPID.iloc[i,19] / 4) - PPID.iloc[i, 44]) < 0.03
                                if False in list(PPID.loc[i][64:79]):
                                    PPID.iloc[i, 8] = False
                                elif True in list(PPID.loc[i][64:79]):
                                    PPID.iloc[i, 8] = True

                                x = [PPID.iloc[i, 48], PPID.iloc[i, 50], PPID.iloc[i, 52], PPID.iloc[i, 54],
                                     PPID.iloc[i, 56], PPID.iloc[i, 58], PPID.iloc[i, 60], PPID.iloc[i, 62]]
                                y = [PPID.iloc[i, 49], PPID.iloc[i, 51], PPID.iloc[i, 53], PPID.iloc[i, 55],
                                     PPID.iloc[i, 57], PPID.iloc[i, 59], PPID.iloc[i, 61], PPID.iloc[i, 63]]
                                PPID.iloc[i, 9] = (max(x) - min(x)) < 0.03 and (max(y) - min(y)) < 0.03


                            else:
                                PPID.loc[i,8]=='largeFieldAsmlPoints'
                        except:
                            print('-L,LDI-->ERROR')
                    else:
                        try:
                            PPID.iloc[i, 48] = ((PPID.iloc[i, 20] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 29])
                            PPID.iloc[i, 49] = ((PPID.iloc[i, 21] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 30])

                            PPID.iloc[i, 50] = ((PPID.iloc[i, 22] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 31])
                            PPID.iloc[i, 51] = ((PPID.iloc[i, 23] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 32])

                            PPID.iloc[i, 52] = ((PPID.iloc[i, 24] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 33])
                            PPID.iloc[i, 53] = ((PPID.iloc[i, 25] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 34])

                            PPID.iloc[i, 54] = ((PPID.iloc[i, 26] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 35])
                            PPID.iloc[i, 55] = ((PPID.iloc[i, 27] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 36])

                            PPID.iloc[i, 64] = abs(
                                (PPID.iloc[i, 20] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 29]) < 0.03
                            PPID.iloc[i, 65] = abs(
                                (PPID.iloc[i, 21] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 30]) < 0.03

                            PPID.iloc[i, 66] = abs(
                                (PPID.iloc[i, 22] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 31]) < 0.03
                            PPID.iloc[i, 67] = abs(
                                (PPID.iloc[i, 23] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 32]) < 0.03

                            PPID.iloc[i, 68] = abs(
                                (PPID.iloc[i, 24] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 33]) < 0.03
                            PPID.iloc[i, 69] = abs(
                                (PPID.iloc[i, 25] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 34]) < 0.03

                            PPID.iloc[i, 70] = abs(
                                (PPID.iloc[i, 26] / 1000 + PPID.iloc[i, 18] / 2) - PPID.iloc[i, 35]) < 0.03
                            PPID.iloc[i, 71] = abs(
                                (PPID.iloc[i, 27] / 1000 + PPID.iloc[i, 19] / 2) - PPID.iloc[i, 36]) < 0.03

                            if False in list(PPID.loc[i][64:72]):
                                PPID.iloc[i, 8] = False
                            elif True in list(PPID.loc[i][64:72]):
                                PPID.iloc[i, 8] = True
                            x = [PPID.iloc[i, 48], PPID.iloc[i, 50], PPID.iloc[i, 52], PPID.iloc[i, 54]]
                            y = [PPID.iloc[i, 49], PPID.iloc[i, 51], PPID.iloc[i, 53], PPID.iloc[i, 55]]
                            PPID.iloc[i, 9] = (max(x) - min(x)) < 0.03 and (max(y) - min(y)) < 0.03
                        except:
                            print('Normal-->ERROR')
                else:
                    print('No Data')





            if 'NL_HL'=='NL_HL':
                t = PPID[PPID['F1']!='']
                t1 = PPID[PPID['F3']==True]
                t1 = t1[t1['F1']==''] #有测试坐标，无比对结果
                for i in t1.index:
                    raw = t1.loc[i]
                    key = raw['PART'][:6]
                    t2 = t[t['PART'].str.contains(key)]
                    if t2.shape[0]>0:
                        for j in t2.index:
                            ref = t2.loc[j]
                            if raw['Count']==ref['Count']:
                                flag=True
                                if raw['Count']==8.0:
                                    for n in range(8):
                                        flag = flag and abs(raw[29:37][n]-ref[29:37][n])<0.02
                                    if flag==True:
                                        PPID.iloc[i,8] = t2.loc[j]['F1']
                                        PPID.iloc[i,9] = t2.loc[j]['F2']
                                        PPID.iloc[i,10] = t2.loc[j]['F3']
                                        PPID.iloc[i,11] = t2.loc[j]['Ppid']

                                else:
                                    for n in range(16):
                                        flag = flag and abs(raw[29:37][n] - ref[29:37][n]) < 0.02
                                    if flag == True:
                                        PPID.iloc[i,8] = t2.loc[j]['F1']
                                        PPID.iloc[i,9] = t2.loc[j]['F2']
                                        PPID.iloc[i,10] = t2.loc[j]['F3']
                                        PPID.iloc[i,11] = t2.loc[j]['Ppid']














            PPID.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\result.csv',index=None)
            PPID.to_excel(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\result.xlsx',index=None)

            # PPID = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\result.csv')
            PPID=PPID.fillna('')
            PPID=PPID.drop(['PART','UploadRiqi'],axis=1).drop_duplicates()
            PPID = PPID[['Ppid', 'F1', 'F2','F3','EqpType']]


            PPID['key']=PPID['Ppid'] + '#' + PPID['EqpType']
            PPID = PPID[PPID['F3']==True]
            # PPID=PPID[PPID['F1']!=""] #有坐标未比对的剔除了

            x=pd.DataFrame(PPID.groupby(['key'])['key'].count())
            repeat=list(x[x['key']!=1].index)
            single = set(list(PPID['key'].unique()))-set(repeat)
            single = pd.DataFrame(list(single),columns=['key'])


            single = pd.merge(single,PPID,how='left',on=['key'])
            multi=[]
            for i in repeat:
                x= PPID[PPID['key']==i]
                if True in list(x['F1']):
                    F1=True
                else:
                    F1=False
                if True in list(x['F2']):
                    F2=True
                else:
                    F2=False
                multi.append([i,i.split('#')[0],F1,F2,True,i.split('#')[1]])
            repeat = pd.DataFrame(multi,columns=single.columns)

            all = pd.concat([single,repeat],axis=0)[['Ppid', 'F1', 'F2', 'F3','EqpType']]

            all.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\summary.csv',index=None)
        if 'summary'=='summary':
            PPID = pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\merge.csv')
            PPID = PPID[['PART', 'STAGE', 'ToolType', 'Ppid']].drop_duplicates()
            PPID['MaskNo'] = [ i[2:6] for i in PPID['PART']]
            PPID=PPID.sort_values(by=['MaskNo','Ppid'],ascending=False)
            PPID=pd.merge(PPID,all,how='left',on=['Ppid']).fillna('')

            move = pd.read_excel(r'P:\_Script\ExcelFile\move.xlsm')
            move=move[['ovlPpid','ovlTool']]
            move['move']=True
            move.columns=['Ppid','ovlTool','move']
            PPID = pd.merge(PPID,move,how='left',on=['Ppid']).fillna('')

            download=[]
            for i in range(PPID.shape[0]):
                if PPID['F3'][i]=='' and PPID['move'][i]==True:
                    download.append(True)
                else:
                    download.append(False)
            PPID['download']=download
            PPID=PPID.sort_values(by=['Ppid'])



            PPID.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\SummrayForOpas.csv')
            PPID.to_excel(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\overlay\SummrayForOpas.xlsx',index=False)

    # def Archer_Check(self,part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i):
    #     AutoCheck=""
    #     sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='" + part + "' and Ppid='" + ovlto + "'"
    #     tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
    #     tmprs.Open(sql, conn, 1, 3)
    #     if tmprs.recordcount > 0:
    #         refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value), eval(tmprs.fields('RDx').value),
    #                  eval(tmprs.fields('RDY').value), eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
    #                  eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)]  # 类似MIM层次，CE命名不规范，部分ovlto下有多组坐标
    #
    #     ToolType = tmp[i][1]
    #     try:
    #         if stepx != '' and stepy != '' and ovlto != '' and recipeZb != '' and refZb != '':
    #             if part[-2:].upper() == "-L" and ToolType.upper() == 'LII':
    #                 c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
    #                 c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020
    #
    #                 c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
    #                 c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020
    #
    #                 c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
    #                 c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020
    #
    #                 c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
    #                 c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020
    #
    #                 if c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
    #                     AutoCheck = 'Correct'
    #                 else:
    #                     AutoCheck = 'Wrong'
    #
    #                 sql = "update PPID set ArcherCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #                 conn.Execute(sql)
    #             elif part[-2:].upper() == "-L" and ToolType.upper() == 'LDI':
    #                 if len(recipeZb) == 16:
    #                     c11 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
    #                     c12 = abs((-refZb[2] / 1000 + stepy / 4) - recipeZb[1]) < 0.020
    #
    #                     c21 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
    #                     c22 = abs((-refZb[4] / 1000 + stepy / 4) - recipeZb[3]) < 0.020
    #
    #                     c31 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
    #                     c32 = abs((-refZb[6] / 1000 + stepy * 3 / 4) - recipeZb[5]) < 0.020
    #
    #                     c41 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
    #                     c42 = abs((-refZb[0] / 1000 + stepy * 3 / 4) - recipeZb[7]) < 0.020
    #
    #                     c51 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[8]) < 0.020
    #                     c52 = abs((-refZb[2] / 1000 + stepy * 3 / 4) - recipeZb[9]) < 0.020
    #
    #                     c61 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[10]) < 0.020
    #                     c62 = abs((-refZb[4] / 1000 + stepy * 3 / 4) - recipeZb[11]) < 0.020
    #
    #                     c71 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[12]) < 0.020
    #                     c72 = abs((-refZb[6] / 1000 + stepy / 4) - recipeZb[13]) < 0.020
    #
    #                     c81 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[14]) < 0.020
    #                     c82 = abs((-refZb[0] / 1000 + stepy / 4) - recipeZb[15]) < 0.020
    #                     if False in [c11, c12, c21, c22, c31, c32, c41, c42, c51, c52, c61, c62, c71, c72, c81, c82]:
    #                         AutoCheck = 'Wrong'
    #                     else:
    #                         AutoCheck = 'Correct'
    #                     sql = "update PPID set ArcherCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #                     conn.Execute(sql)
    #                 else:
    #                     sql = "update PPID set ArcherCheck='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
    #                     conn.Execute(sql)
    #                     AutoCheck = '!=16points'
    #             else:
    #                 c11 = abs((refZb[0] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
    #                 c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020
    #
    #                 c21 = abs((refZb[2] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
    #                 c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020
    #
    #                 c31 = abs((refZb[4] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
    #                 c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020
    #
    #                 c41 = abs((refZb[6] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
    #                 c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
    #                 if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
    #                     AutoCheck = 'Wrong'
    #                 else:
    #                     AutoCheck = 'Correct'
    #                 sql = "update PPID set ArcherCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #                 conn.Execute(sql)
    #         else:
    #             sql = "update PPID set ArcherCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
    #             conn.Execute(sql)
    #             AutoCheck='data not ready'
    #     except:
    #         sql = "update PPID set ArcherCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
    #         conn.Execute(sql)
    #         AutoCheck = 'data not ready'
    #     return AutoCheck
    # def Q200_Check(self,part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i):
    #     AutoCheck=''
    #     sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='" + part + "' and Ppid='" + ovlto + "'"
    #     tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
    #     tmprs.Open(sql, conn, 1, 3)
    #     if tmprs.recordcount > 0:
    #         refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value), eval(tmprs.fields('RDx').value),
    #                  eval(tmprs.fields('RDY').value), eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
    #                  eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)]  # 类似MIM层次，CE命名不规范，部分ovlto下有多组坐标
    #
    #     ToolType = tmp[i][1]
    #     try:
    #         if stepx != '' and stepy != '' and ovlto != '' and recipeZb != '' and refZb != '':
    #             if part[-2:].upper() == "-L" and ToolType.upper() == 'LII':
    #                 c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
    #                 c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020
    #
    #                 c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
    #                 c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020
    #
    #                 c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
    #                 c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020
    #
    #                 c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
    #                 c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020
    #
    #                 if c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
    #                     AutoCheck = 'Correct'
    #                 else:
    #                     AutoCheck = 'Wrong'
    #
    #                 sql = "update PPID set Q200Check='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #                 conn.Execute(sql)
    #             elif part[-2:].upper() == "-L" and ToolType.upper() == 'LDI':
    #                 if len(recipeZb) == 16:
    #                     c11 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
    #                     c12 = abs((-refZb[2] / 1000 + stepy / 4) - recipeZb[1]) < 0.020
    #
    #                     c21 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
    #                     c22 = abs((-refZb[4] / 1000 + stepy / 4) - recipeZb[3]) < 0.020
    #
    #                     c31 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
    #                     c32 = abs((-refZb[6] / 1000 + stepy * 3 / 4) - recipeZb[5]) < 0.020
    #
    #                     c41 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
    #                     c42 = abs((-refZb[0] / 1000 + stepy * 3 / 4) - recipeZb[7]) < 0.020
    #
    #                     c51 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[8]) < 0.020
    #                     c52 = abs((-refZb[2] / 1000 + stepy * 3 / 4) - recipeZb[9]) < 0.020
    #
    #                     c61 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[10]) < 0.020
    #                     c62 = abs((-refZb[4] / 1000 + stepy * 3 / 4) - recipeZb[11]) < 0.020
    #
    #                     c71 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[12]) < 0.020
    #                     c72 = abs((-refZb[6] / 1000 + stepy / 4) - recipeZb[13]) < 0.020
    #
    #                     c81 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[14]) < 0.020
    #                     c82 = abs((-refZb[0] / 1000 + stepy / 4) - recipeZb[15]) < 0.020
    #                     if False in [c11, c12, c21, c22, c31, c32, c41, c42, c51, c52, c61, c62, c71, c72, c81, c82]:
    #                         AutoCheck = 'Wrong'
    #                     else:
    #                         AutoCheck = 'Correct'
    #                     sql = "update PPID set Q200Check='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #                     conn.Execute(sql)
    #                 else:
    #                     sql = "update PPID set Q200Check='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
    #                     conn.Execute(sql)
    #                     AutoCheck='!=16points'
    #             else:
    #                 c11 = abs((refZb[0] / 1000 + stepx / 2) - recipeZb[0]) < 0.020
    #                 c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020
    #
    #                 c21 = abs((refZb[2] / 1000 + stepx / 2) - recipeZb[2]) < 0.020
    #                 c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020
    #
    #                 c31 = abs((refZb[4] / 1000 + stepx / 2) - recipeZb[4]) < 0.020
    #                 c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020
    #
    #                 c41 = abs((refZb[6] / 1000 + stepx / 2) - recipeZb[6]) < 0.020
    #                 c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
    #                 if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
    #                     AutoCheck = 'Wrong'
    #                 else:
    #                     AutoCheck = 'Correct'
    #                 sql = "update PPID set Q200Check='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #                 conn.Execute(sql)
    #         else:
    #             sql = "update PPID set Q200Check='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
    #             conn.Execute(sql)
    #             AutoCheck='data not ready'
    #     except:
    #         sql = "update PPID set Q200Check='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
    #         conn.Execute(sql)
    #         AutoCheck = 'data not ready'
    #     return AutoCheck
    # def auto_check(self):
    #     conn = win32com.client.Dispatch(r"ADODB.Connection")
    #     DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
    #     conn.Open(DSN)
    #
    #     sql = "update PPID set AutoCheck=''"
    #     conn.execute(sql)
    #
    #     sql = "select PART,ToolType,OVL from PPID where f4=True and f3=True and f2=True and f1=True"
    #     rs = win32com.client.Dispatch(r'ADODB.Recordset')
    #     rs.Open(sql, conn, 1, 3)
    #     tmp=[]
    #
    #     for i in range(rs.recordcount):
    #         tmp.append([rs.fields('PART').value,rs.fields('ToolType').value,rs.fields('OVL').value])
    #         rs.movenext
    #
    #     rs = win32com.client.Dispatch(r'ADODB.Recordset')
    #     count = 0
    #     ddd=[]
    #     for i in range(len(tmp)):
    #         # print(i,len(tmp),tmp[i])
    #         ArcherFlag,Q200Flag='',''
    #
    #         stepx,stepy,ovlto,recipeZb,refZb='','','','',''
    #         part = tmp[i][0]
    #         sql = "select StepX,StepY from STEP_SIZE where Part='"+part+"'"
    #         tmprs= win32com.client.Dispatch(r'ADODB.Recordset')
    #         tmprs.Open(sql, conn, 1, 3)
    #         if tmprs.recordcount>0:
    #             stepx,stepy=eval(tmprs.fields('StepX').value),eval(tmprs.fields('StepY').value)
    #
    #         ppid=tmp[i][2]
    #         sql = "select Ppid from BT Where Ovl_Ppid='" + ppid + "'"
    #         tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
    #         tmprs.Open(sql, conn, 1, 3)
    #         if tmprs.recordcount>0:
    #             ovlto=tmprs.fields('Ppid').value.replace(' ','')
    #
    #         sql = "Select A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,Count,EqpType from ZB where Ppid='" + ppid + "'"
    #         tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
    #         tmprs.Open(sql, conn, 1, 3)
    #
    #
    #         recipeZb=[eval(tmprs.fields(k).value) for k in range(int(eval(tmprs.fields('Count').value)))]
    #         for no in range(tmprs.recordcount):
    #             eqp= tmprs.fields('EqpType').value
    #             tmprs.movenext
    #             if eqp=='Archer':
    #                 ArcherFlag = self.Archer_Check(part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i)
    #             else:
    #                 Q200Flag = self.Q200_Check(part,ovlto,conn,tmp,stepx,stepy,recipeZb,ppid,i)
    #         print(i, len(tmp), tmp[i],'ArcherFlag='+ArcherFlag,'Q200Flag='+Q200Flag)
    #
    #         AutoCheck=ArcherFlag + ", " + Q200Flag
    #         #TODO to ovlcheck for Q200 and Archer10
    #         #Parameter: part,ovlto,conn,refZB,tmp,stepx,stepy,recipeZB
    #
    #
    #
    #         # sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='"+part + "' and Ppid='"+ ovlto + "'"
    #         # tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
    #         # tmprs.Open(sql, conn, 1, 3)
    #         # if tmprs.recordcount>0:
    #         #     refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value),
    #         #              eval(tmprs.fields('RDx').value), eval(tmprs.fields('RDY').value),
    #         #              eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
    #         #              eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)] #类似MIM层次，CE命名不规范，部分ovlto下有多组坐标
    #         #
    #         # ToolType=tmp[i][1]
    #         #
    #         # if stepx!='' and stepy!='' and ovlto!='' and recipeZb!='' and refZb!='':
    #         #     if part[-2:].upper()=="-L" and ToolType.upper()=='LII':
    #         #         c11 = abs((refZb[0] / 1000 + stepy / 4) - recipeZb[0]) < 0.020
    #         #         c12 = abs((refZb[1] / 1000 + stepx / 2) - recipeZb[1]) < 0.020
    #         #
    #         #         c21 = abs((refZb[2] / 1000 + stepy / 4) - recipeZb[2]) < 0.020
    #         #         c22 = abs((refZb[3] / 1000 + stepx / 2) - recipeZb[3]) < 0.020
    #         #
    #         #         c31 = abs((refZb[4] / 1000 + stepy / 4) - recipeZb[4]) < 0.020
    #         #         c32 = abs((refZb[5] / 1000 + stepx / 2) - recipeZb[5]) < 0.020
    #         #
    #         #         c41 = abs((refZb[6] / 1000 + stepy / 4) - recipeZb[6]) < 0.020
    #         #         c42 = abs((refZb[7] / 1000 + stepx / 2) - recipeZb[7]) < 0.020
    #         #
    #         #         if  c11 and c12 and c21 and c22 and c31 and c32 and c41 and c42:
    #         #             AutoCheck='Correct'
    #         #         else:
    #         #             AutoCheck='Wrong'
    #         #
    #         #         sql = "update PPID set AutoCheck='" + AutoCheck +   "' where PART='"+part + "' and OVL='"+ppid + "'"
    #         #         conn.Execute(sql)
    #         #     elif part[-2:].upper()=="-L" and ToolType.upper()=='LDI':
    #         #         if len(recipeZb)==16:
    #         #             c11 = abs((refZb[3] / 1000  + stepx / 2) - recipeZb[0]) < 0.020
    #         #             c12 = abs((-refZb[2] / 1000  + stepy / 4) - recipeZb[1]) < 0.020
    #         #
    #         #             c21 = abs((refZb[5] / 1000  + stepx / 2) - recipeZb[2]) < 0.020
    #         #             c22 = abs((-refZb[4] / 1000  + stepy / 4) - recipeZb[3]) < 0.020
    #         #
    #         #             c31 = abs((refZb[7] / 1000  + stepx / 2) - recipeZb[4]) < 0.020
    #         #             c32 = abs((-refZb[6] / 1000  + stepy * 3 / 4) - recipeZb[5]) < 0.020
    #         #
    #         #             c41 = abs((refZb[1] / 1000  + stepx / 2) - recipeZb[6]) < 0.020
    #         #             c42 = abs((-refZb[0] / 1000  + stepy * 3 / 4) - recipeZb[7]) < 0.020
    #         #
    #         #             c51 = abs((refZb[3] / 1000  + stepx / 2) - recipeZb[8]) < 0.020
    #         #             c52 = abs((-refZb[2] / 1000  + stepy * 3 / 4) - recipeZb[9]) < 0.020
    #         #
    #         #             c61 = abs((refZb[5] / 1000  + stepx / 2) - recipeZb[10]) < 0.020
    #         #             c62 = abs((-refZb[4] / 1000  + stepy * 3 / 4) - recipeZb[11]) < 0.020
    #         #
    #         #             c71 = abs((refZb[7] / 1000  + stepx / 2) - recipeZb[12]) < 0.020
    #         #             c72 = abs((-refZb[6] / 1000  + stepy / 4) - recipeZb[13]) < 0.020
    #         #
    #         #             c81 = abs((refZb[1] / 1000  + stepx / 2) - recipeZb[14]) < 0.020
    #         #             c82 = abs((-refZb[0] / 1000  + stepy / 4) - recipeZb[15]) < 0.020
    #         #             if False in [c11,c12,c21,c22,c31,c32,c41,c42,c51,c52,c61,c62,c71,c72,c81,c82]:
    #         #                 AutoCheck='Wrong'
    #         #             else:
    #         #                 AutoCheck = 'Correct'
    #         #             sql = "update PPID set AutoCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #         #             conn.Execute(sql)
    #         #         else:
    #         #             sql = "update PPID set AutoCheck='!=16points' where PART='" + part + "' and OVL='" + ppid + "'"
    #         #             conn.Execute(sql)
    #         #     else:
    #         #         c11 = abs((refZb[0] / 1000 + stepx  / 2) - recipeZb[0]) < 0.020
    #         #         c12 = abs((refZb[1] / 1000 + stepy / 2) - recipeZb[1]) < 0.020
    #         #
    #         #         c21 = abs((refZb[2] / 1000 + stepx  / 2) - recipeZb[2]) < 0.020
    #         #         c22 = abs((refZb[3] / 1000 + stepy / 2) - recipeZb[3]) < 0.020
    #         #
    #         #         c31 = abs((refZb[4] / 1000 + stepx  / 2) - recipeZb[4]) < 0.020
    #         #         c32 = abs((refZb[5] / 1000 + stepy / 2) - recipeZb[5]) < 0.020
    #         #
    #         #         c41 = abs((refZb[6] / 1000 + stepx  / 2) - recipeZb[6]) < 0.020
    #         #         c42 = abs((refZb[7] / 1000 + stepy / 2) - recipeZb[7]) < 0.020
    #         #         if False in [c11, c12, c21, c22, c31, c32, c41, c42]:
    #         #             AutoCheck = 'Wrong'
    #         #         else:
    #         #             AutoCheck = 'Correct'
    #         #         sql = "update PPID set AutoCheck='" + AutoCheck + "' where PART='" + part + "' and OVL='" + ppid + "'"
    #         #         conn.Execute(sql)
    #         # else:
    #         #     sql = "update PPID set AutoCheck='data not ready' where PART='" + part + "' and OVL='" + ppid + "'"
    #         #     conn.Execute(sql)
    #
    #         sql = "update PPID set f5=True,RiQi='"+str(datetime.datetime.now())[:10].replace('-','')+"',AutoCheck='"+AutoCheck+"' where PART='"+ part + "' and OVL='" + ppid + "'"
    #         # sql = "update PPID set f5=True where PART='" + part + "' and OVL='" + ppid + "'"
    #         conn.Execute(sql)
    #     conn.close
    def MainFunction(self):
        pass
        self.read_raw_data()
        print('OVL_001')
        self.read_standard_coordinate()
        print('OVL_002')
        self.read_asml_step_size()
        print('OVL_003')
        self.read_bias_table()
        print('OVL_004')
        self.refresh_ppid_from_xls()  #too long
        print('OVL_005')
        self.ppid_flag_check()
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
        if "MoveFile"=="MoveFile":
            FileDir = 'P:\\EgaLog\\'
            filenamelist = []
            for root, dirs, files in os.walk(FileDir, False):
                for names in files:
                    if '.egam' in names:
                        filenamelist.append(root + '\\' + names)
            filenamelist.sort(reverse=False)
            if len(filenamelist)>0:
                for k, path in enumerate(filenamelist[0:]):
                    newpath = "Y:\\NikonEgaLog\\" + path[10:]
                    shutil.move(path, newpath)
        if 'AllTool'=='AllTool':
            toollist = ['ALII01','ALII02','ALII03','ALII04','ALII05','ALII06','ALII07','ALII08',
                        'ALII09','ALII10','ALII11','ALII12','ALII13','ALII14','ALII15','ALII16',
                        'ALII17','ALII18','ALII19','ALII20','ALII21','ALII22','ALII23']
            for tool in toollist[:]:
                print(tool)

                d1={};d2={};d0={}
                if 'generateDummy'=='generateDummy':
                    index = pd.DataFrame(columns=['File', 'MEAS_SENS', 'Tool', 'Ppid', 'StepX', 'StepY', 'OffsetX',
           'OffsetY', 'LsaReqShot', 'FiaReqShot', 'FM_ROT', 'TranX', 'TranY',
           'ScalX', 'ScalY', 'Ort', 'Rot', 'MagX', 'MagY', 'SRot', 'WfrNo',
           'AlignNo', 'Search', 'EGA', 'ULimit', 'Llimit', 'No'])
                    #index = pd.concat([index,df],axis=0)
                    # parameter = pd.DataFrame(columns = ['wfrNo', 'No', 'scalingx', 'scalingy', 'rot', 'ort'])
                    # vector = pd.DataFrame(columns = ['x', 'y', 'No', 'wfr', 'shot', 'type', 'index1', 'index2'])
                if 'GetFileList' == 'GetFileList':
                    pathDir = r'\\10.4.72.74\litho\NikonEgaLog' + '\\' + tool + '\\'
                    filenamelist = []
                    for root, dirs, files in os.walk(pathDir,False):
                        for names in files:
                            if '.egam' in names:
                                filenamelist.append(root  + names)
                    # filenamelist= [ i for i in filenamelist if int(i.split('\\')[-1][4:10])>=201901 ]
                    try:
                        old = list(pd.read_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.csv')['File'])
                        filenamelist = list(set(filenamelist)-set(old))
                    except:
                        pass

                    filenamelist.sort(reverse=False)
                    filenamelist = [ i for i in filenamelist if i[-4:]=='egam']

                    try:
                        No = list(pd.read_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.csv')['No'])[-1]
                        No = int(No[2:])
                    except:
                        No = 0
                if 'readData'=='readData' and len(filenamelist)>0:
                    for k, path in enumerate(filenamelist[:]):
                        if 'makedir'=='makedir':
                            if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                              path.split('ega_')[1][:6] + '\\'):
                                pass
                            else:
                                os.makedirs('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                            path.split('ega_')[1][:6] + '\\')
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
                                    tmp['No']='No' + str(No)
                                    if 'index' in d0.keys():
                                        d0['index'] =  pd.concat([d0['index'],tmp],axis=0)
                                    else:
                                        d0['index'] = tmp.copy()
                                if 'generate_merge'=='generate_merge':
                                    merge = pd.DataFrame(columns=['x', 'y', 'No', 'wfr', 'shot','type'])
                                    for i in tmpx.columns:
                                        tx = (tmpx[i] * 1000000 + AlignPosition[1])
                                        ty = (-tmpy[i] * 1000000 + AlignPosition[2])
                                        xy = pd.DataFrame([tx,ty]).T
                                        xy['No']=No; xy['wfr']=i; xy['shot'] = [ 'A' + str(i) for i in range(xy.shape[0])]
                                        xy['type'] = 'residual'
                                        xy.columns=['x','y','No','wfr','shot','type']
                                        xy['y']=[200-i for i in xy['y']]

                                        ref = AlignPosition.copy()
                                        ref['No'] = No; ref['wfr'] = i; ref['shot'] = ['A' + str(i) for i in range(ref.shape[0])]
                                        ref['type'] = 'coordinate'
                                        ref.columns = ['x', 'y', 'No', 'wfr', 'shot','type']
                                        ref['y']=[200-i for i in ref['y']]

                                        merge = pd.concat([merge,ref])
                                        merge = pd.concat([merge,xy])

                                    merge['index1'] = [ str(x)+"_"+str(y)+"_"+z  for x,y,z in zip(merge['No'],merge['wfr'],merge['shot'])]
                                    merge['index2'] = [ str(x)+"_"+ y  for x,y in zip(merge['wfr'],merge['shot'])]
                                    merge['No']=['No' + str(i) for i in merge['No']]


                                    if path.split('ega_')[1][:6] in d1.keys():
                                        d1[path.split('ega_')[1][:6]] = pd.concat([d1[path.split('ega_')[1][:6]],merge],axis=0)
                                    else:
                                        d1[path.split('ega_')[1][:6]] = merge.copy()
                                if 'generate para'=='generate para':
                                    para = pd.concat([data[5],data[6]],axis=1)
                                    para.columns = ['scalingx','ort','scalingy','rot']
                                    para['ort'] = para['ort']-para['rot']
                                    para['scalingy']= -1 * para['scalingy']
                                    para['No']='No' + str(No)
                                    para['wfrNo'] = [str(i + 1) for i in para.index]
                                    if path.split('ega_')[1][:6] in d2.keys():
                                        d2[path.split('ega_')[1][:6]] = pd.concat([d2[path.split('ega_')[1][:6]],para],axis=0)
                                    else:
                                        d2[path.split('ega_')[1][:6]] = para.copy()
                            else:
                                try:
                                    os.rename(path, path + '_bad')
                                except:
                                    os.remove(path) #重复下载时会出错

                        except:
                            os.rename(path,path+'_bad')
                if 'saveall'=='saveall':
                    if 'saveindex' == 'saveindex' and len(d0)>0:
                        if os.path.exists(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.csv'):
                            d0['index'].to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.csv', index=None,
                                       header=None, mode='a')
                            tmp = pd.read_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.csv')
                            tmp.to_excel(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.xlsx',index=None)
                        else:
                            d0['index'].to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.csv', index=None)
                            d0['index'].to_excel(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\Index.xlsx', index=None)

                    if 'savemerge' == 'savemerge' and len(d1)>0:
                        for key in d1.keys():
                            t1 = str(datetime.datetime.now())[0:7].replace('-', '')
                            t2 = str(datetime.datetime.now() - datetime.timedelta(days=31))[0:7].replace('-', '')
                            if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                              key + '\\vector.csv'):
                                d1[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                             key + '\\vector.csv', index=None, header=None, mode='a')
                                if key in [t1,t2]:
                                    tmp = pd.read_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                             key + '\\vector.csv')
                                    tmp.to_excel('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                             key + '\\vector.xlsx',index=None)


                            else:
                                d1[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                             key + '\\vector.csv', index=None)
                                d1[key].to_excel(
                                    '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' + key + '\\vector.xlsx',
                                    index=None)

                    # if 'savepara' == 'savepara' and parameter.shape[0]>0:
                    #
                    #     if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                    #                       path.split('ega_')[1][:6] + '\\parameter.csv'):
                    #         parameter.to_csv(
                    #             '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                    #             path.split('ega_')[1][:6] + '\\parameter.csv', header=None, mode='a', index=None)
                    #     else:
                    #         parameter.to_csv(
                    #             '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                    #             path.split('ega_')[1][:6] + '\\parameter.csv', index=None)
                    if 'savepara' == 'savepara' and len(d2)>0:
                        for key in d2.keys():

                            if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                              key + '\\parameter.csv'):
                                d2[key].to_csv(
                                    '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                    key + '\\parameter.csv', header=None, mode='a', index=None)
                                if key in [t1,t2]:
                                    tmp = pd.read_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                             key + '\\parameter.csv')
                                    tmp.to_excel('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                             key + '\\parameter.xlsx',index=None)

                            else:
                                d2[key].to_csv(
                                    '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' +
                                    key + '\\parameter.csv', index=None)
                                d2[key].to_excel(
                                    '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\' + tool + '\\' + key + '\\parameter.xlsx',
                                    index=None)
class AWE_ANALYSIS_NEW: #OBSELETE
    def __init__(self):
        pass
    def linearfit(self,XYin):

        linreg = LinearRegression()
        lot=pd.DataFrame()
        para = []
        for i in XYin['WaferNr'].unique():
            wfr = XYin[XYin['WaferNr'] == i].dropna()
            if XYin.columns[-1][1]=='X':
                input_y = wfr[XYin.columns[-1]] - wfr['NomPosX']
            else:
                input_y = wfr[XYin.columns[-1]] - wfr['NomPosY']

            input_x = wfr[['NomPosX', 'NomPosY']]
            model = linreg.fit(input_x, input_y)
            tmp = pd.DataFrame(linreg.intercept_ + linreg.coef_[0] * input_x['NomPosX'] + linreg.coef_[1] * input_x[
                'NomPosY'] - input_y)
            tmp.columns=[XYin.columns[-1][:-3]+'Residual']
            tmp = -1 * tmp
            wfr = pd.concat([wfr,tmp],axis=1)
            lot = pd.concat([lot,wfr],axis=0)
            para.append([linreg.intercept_,linreg.coef_[0],linreg.coef_[1]])
        lot[['NomPosX', 'NomPosY']]=lot[['NomPosX', 'NomPosY']].apply(lambda x: round(x*1000,4)) #标准坐标转换为mm，四位有效数字
        lot[list(lot.columns[-2:])] = lot[list(lot.columns[-2:])].astype('float64')
        lot[list(lot.columns[-2:])] =  lot[list(lot.columns[-2:])].apply(lambda x: round(x*1000,6))
        para=pd.DataFrame(para).apply(lambda x: round(x*1000000,3))
        if 'VectorZoom'=='VectorZoom':
            position= lot.columns[-2];residual=lot.columns[-1]
            if 'X' in position:
                lot[position] = [round(x - (x - y) * 1000000, 4) for x, y in zip(lot['NomPosX'], lot[position])]
                lot[residual] = [round(x + y * 1000000, 4) for x, y in zip(lot['NomPosX'], lot[residual])]
            else:
                lot[position] = [ round(x - (x-y)*1000000,4) for x,y in zip(lot['NomPosY'],lot[position])]
                lot[residual] = [round(x + y * 1000000, 4) for x, y in zip(lot['NomPosY'], lot[residual])]





        return lot,para
    def read_single_awe(self,name):
        try:
            tmp = open(name)
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
        if 'dict'=='dict':
            toolid = {'4666':'ALSD82','4730':'ALSD83','6450':'ALSD85','8144':'ALSD86','4142':'ALSD87',
                      '6158':'ALSD89','5688':'ALSD8A','4955':'ALSD8B','9726':'ALSD8C','8111':'BLSD7D','3527':'BLSD08'}
        if "GetFileList" == "GetFileList": #new file 'Z:\\AsmlAweFile\\RawData\\'
                filelist=[]
                toollist = ['7D','08','82','83','85','86','87','89','8A','8B','8C']
                for tool in toollist[:]:
                    path = 'Z:\\AsmlAweFile\\RawData\\' + tool + '\\'
                    for root, dirs, files in os.walk(path, False):
                        for names in files:
                            filelist.append(root + '\\' + names)
        if "getIndex"=="getIndex":
            try:
                No = list(pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv')['No'])
                if len(No)==0:
                    No=0
                else:
                    No=int(No[-1][2:])
            except:
                No=0
        if 'ReadFile'=='ReadFile':
            allVector={};allResidual={};allParameter={};allX_WQ_MCC_DELTA={};allY_WQ_MCC_DELTA={};index={}
            for i,file in enumerate(filelist[:]):
                print(i,file,len(filelist))
                basic, validNo, validX, validY = None, None, None, None
                try:
                    if 'getdata'=='getdata':
                        basic, validNo, validX, validY = self.read_single_awe(file)
                        # validNo = [validNo[2],validNo[4],validNo[7],validNo[9],validNo[12],validNo[14],validNo[17],validNo[19]] #['1XRedValid',  '5XRedValid',  '1XGreenValid', '5XGreenValid', '1YRedValid','5YRedValid', '1YGreenValid',  '5YGreenValid'] 有效点数
                        if 'generate smooth color dynamic data'=='generate smooth color dynamic data': # NaN data for WQ/Position??
                            try:
                                validX['5XSmPos']=''
                                for i in range(validX.shape[0]):
                                    if validX.iloc[i,28]<0.00001 or validX.iloc[i,13] / validX.iloc[i,28] >=10: #red/green>10
                                        validX.iloc[i,37]= validX.iloc[i,11]
                                    elif validX.iloc[i,13]<0.00001 or validX.iloc[i,13] / validX.iloc[i,28] <=0.1: #red/green<0.1
                                        validX.iloc[i,37] = validX.iloc[i,26]
                                    else:
                                        validX.iloc[i,37] = validX.iloc[i,11] * validX.iloc[i,13]/( validX.iloc[i,13] + validX.iloc[i,28]) + validX.iloc[i,26]* validX.iloc[i,28]/( validX.iloc[i,13] + validX.iloc[i,28])
                                validY['5YSmPos'] = ''
                                for i in range(validY.shape[0]):
                                    if validY.iloc[i,28]<0.00001 or validY.iloc[i, 13] / validY.iloc[i, 28] >= 10:  # red/green>10
                                        validY.iloc[i, 37] = validY.iloc[i, 11]
                                    elif validY.iloc[i,13]<0.00001 or validY.iloc[i, 13] / validY.iloc[i, 28] <= 0.1:  # red/green<0.1
                                        validY.iloc[i, 37] = validY.iloc[i, 26]
                                    else:
                                        validY.iloc[i, 37] = validY.iloc[i, 11] * validY.iloc[i, 13] / (
                                                    validY.iloc[i, 13] + validY.iloc[i, 28]) + validY.iloc[i, 26] * validY.iloc[
                                                            i, 28] / (validY.iloc[i, 13] + validY.iloc[i, 28])
                                validX=validX[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', 'BasicMarkType',
                                               '1XRedPos', '1XRedMCC', '1XRedWQ', '5XRedPos',  '5XRedMCC', '5XRedWQ',
                                               '1XGreenPos', '1XGreenMCC', '1XGreenWQ','5XGreenPos', '5XGreenMCC', '5XGreenWQ',
                                               'XRedDelta', 'XGreenDelta','5XSmPos']]
                                validY=validY[['WaferNr', 'MarkNr', 'NomPosX', 'NomPosY', 'BasicMarkType',
                                               '1YRedPos','1YRedMCC', '1YRedWQ', '5YRedPos','5YRedMCC', '5YRedWQ',
                                               '1YGreenPos', '1YGreenMCC', '1YGreenWQ', '5YGreenPos', '5YGreenMCC','5YGreenWQ',
                                               'YRedDelta', 'YGreenDelta','5YSmPos']]
                            except:
                                print('calcualte smooth-color-dynamic data error, script stopped')
                                print(file)
                    if 'basic'=='basic':
                        if 'compileData'=='compileData':
                            basic[0] = basic[0].replace('/', '_')
                            basic[1] = basic[1].replace(':', '_')
                            basic[4] = toolid[basic[4]]
                            b = pd.DataFrame(basic[:-1]).T
                            b.columns=['date','time','part','layer','tool','lot','mark','recipe']
                            No += 1
                            b['No']='No' + str(No)
                            src = basic[-1]
                            dst = '\\\\10.4.72.74\\litho\\ASML_AWE\\RawData\\' + basic[4] +'\\'+ basic[0][0:7] + '\\' +basic[0]+basic[1]+'#'+basic[2]+"#"+basic[3]+'#'+basic[5]+"#" + basic[6] + "#" + basic[7]
                            if os.path.exists('\\\\10.4.72.74\\litho\\ASML_AWE\\RawData\\' + basic[4] + '\\'+ basic[0][0:7] + '\\'):
                                pass
                            else:
                                os.makedirs('\\\\10.4.72.74\\litho\\ASML_AWE\\RawData\\' + basic[4] + '\\'+ basic[0][0:7] + '\\')
                            shutil.move(src,dst)

                            if len(index)==0:
                                index['index'] = b.copy()
                            else:
                                index['index'] = pd.concat([index['index'],b.copy()],axis=0)

                            # if os.path.exists(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv'):
                            #     b.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv',index=None,header=None,mode='a')
                            # else:
                            #     b.to_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv', index=None)
                        if 'makedir'=='makedir':
                            if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '\\' + basic[0][:7] + '\\'):
                                pass
                            else:
                                os.makedirs('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '\\' + basic[0][:7]+ '\\')
                    if 'WQ_MCC_delta'=='WQ_MCC_delta':
                        tmp= validX[['WaferNr', 'MarkNr',  '1XRedMCC', '1XRedWQ',   '5XRedMCC', '5XRedWQ',
                                       '1XGreenMCC', '1XGreenWQ', '5XGreenMCC', '5XGreenWQ',
                                       'XRedDelta', 'XGreenDelta']].copy()
                        tmp['No']='No' +str(No)
                        tmp=tmp.fillna('')
                        if (basic[4]+basic[0][:7]) in allX_WQ_MCC_DELTA.keys():
                            allX_WQ_MCC_DELTA[basic[4]+basic[0][:7]] = pd.concat([allX_WQ_MCC_DELTA[basic[4]+basic[0][:7]],tmp.copy()],axis=0)
                        else:
                            allX_WQ_MCC_DELTA[basic[4]+basic[0][:7]] = tmp.copy()
                        tmp = validY[['WaferNr', 'MarkNr', '1YRedMCC', '1YRedWQ', '5YRedMCC', '5YRedWQ', '1YGreenMCC',
                                      '1YGreenWQ', '5YGreenMCC', '5YGreenWQ', 'YRedDelta', 'YGreenDelta']].copy()
                        tmp['No'] = 'No' + str(No)
                        if (basic[4]+basic[0][:7]) in allY_WQ_MCC_DELTA.keys():
                            allY_WQ_MCC_DELTA[basic[4]+basic[0][:7]] = pd.concat([allY_WQ_MCC_DELTA[basic[4]+basic[0][:7]],tmp.copy()],axis=0)
                        else:
                            allY_WQ_MCC_DELTA[basic[4]+basic[0][:7]] = tmp.copy()

                        # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'X_WQ_MCC_DELTA.csv'):
                        #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'X_WQ_MCC_DELTA.csv',index=None,header=None,mode='a')
                        # else:
                        #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + 'X_WQ_MCC_DELTA.csv', index=None)
                        # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'Y_WQ_MCC_DELTA.csv'):
                        #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'Y_WQ_MCC_DELTA.csv',index=None,header=None,mode='a')
                        # else:
                        #     tmp.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] +'Y_WQ_MCC_DELTA.csv',index=None)
                    if 'fit'=='fit':
                        if 'normalizeStandardCoordinate'=='normalizeStandardCoordinate':
                            X = validX[['MarkNr','NomPosX', 'NomPosY']].drop_duplicates().copy()
                            Y = validY[['MarkNr','NomPosX', 'NomPosY']].drop_duplicates().copy()

                            count=0
                            for i in X['NomPosX']:
                                for j in Y['NomPosX']:
                                    if count==0:
                                        delta=i-j
                                        count += 1
                                    else:
                                        if abs(i-j)<abs(delta):
                                            delta=i-j
                                            count +=1
                            validY['NomPosX'] = [ i + delta for i in validY['NomPosX']]

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
                        if 'runFit'=='runFit':
                            if 'fit'=='fit':
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


                                lot = pd.merge(red5x[0], green5x[0], how='outer', on=['WaferNr', 'NomPosX', 'NomPosY'])
                                lot = pd.merge(lot, red5y[0], how='outer', on=['WaferNr',  'NomPosX', 'NomPosY'])
                                lot = pd.merge(lot,green5y[0], how='outer', on=['WaferNr',  'NomPosX', 'NomPosY'])
                                lot = pd.merge(lot,sm5x[0], how='outer', on=['WaferNr',  'NomPosX', 'NomPosY'])
                                lot = pd.merge(lot,sm5y[0], how='outer', on=['WaferNr',  'NomPosX', 'NomPosY'])
                            if 'ShotLocationMisMatch'=='ShotLocationMisMatch':
                                for i in lot.index:
                                    if lot.iloc[i,4]!=lot.iloc[i,4]: #NaN
                                        lot.iloc[i,4]=lot.iloc[i,5]=lot.iloc[i,7]=lot.iloc[i,8]=lot.iloc[i,16]=lot.iloc[i,17]=lot.iloc[i,2]
                                        if lot.iloc[i,1]!=lot.iloc[i,1]:
                                            lot.iloc[i,1]=lot.iloc[i,9]
                                    else:
                                        if lot.iloc[i,10]!=lot.iloc[i,10]:
                                            lot.iloc[i, 10] = lot.iloc[i, 11] = lot.iloc[i, 13] = lot.iloc[i, 14] = lot.iloc[i, 19]= lot.iloc[i, 20]= lot.iloc[i, 3]
                            if 'residual'=='residual':
                                residual=lot[['WaferNr','MarkNr_x']].fillna('')
                                residual.columns = ['wfrNo','markNo','1','2']
                                for i in range(residual.shape[0]):
                                    if residual.iloc[i,1]=="":
                                        residual.iloc[i, 1]=residual.iloc[i,2]
                                residual = residual.drop(['1','2'],axis=1)
                                residual['5XRedResidual'] = lot['5XRedResidual']-lot['NomPosX']
                                residual['5YRedResidual'] = lot['5YRedResidual']-lot['NomPosY']
                                residual['5XGreenResidual'] = lot['5XGreenResidual'] - lot['NomPosX']
                                residual['5YGreenResidual'] = lot['5YGreenResidual'] - lot['NomPosY']
                                residual['5XSmResidual'] = lot['5XSmResidual'] - lot['NomPosX']
                                residual['5YSmResidual'] = lot['5YSmResidual'] - lot['NomPosY']
                                residual['No']='No' +str(No)
                                if (basic[4] + basic[0][:7]) in allResidual.keys():
                                    allResidual[basic[4] + basic[0][:7]] = pd.concat(
                                        [allResidual[basic[4] + basic[0][:7]],residual.copy()], axis=0)
                                else:
                                    allResidual[basic[4] + basic[0][:7]] = residual.copy()

                                # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + '_Residual.csv'):
                                #     residual.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + '_Residual.csv',
                                #                 header=None, mode='a',index=None)
                                # else:
                                #     residual.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + '_Residual.csv',index=None)
                            if 'parameter'=='parameter':
                                red5x[1].columns=['r5x_tr','r5x_exp','r5x_rotort']
                                red5y[1].columns=['r5y_tr','r5y_rotort','r5y_exp']
                                green5x[1].columns = ['g5x_tr', 'g5x_exp', 'g5x_rotort']
                                green5y[1].columns = ['g5y_tr', 'g5y_rotort', 'g5y_exp']
                                sm5x[1].columns = ['sm5x_tr', 'sm5x_exp', 'sm5x_rotort']
                                sm5y[1].columns = ['sm5y_tr', 'sm5y_rotort', 'sm5y_exp']

                                para = pd.concat([red5x[1],red5y[1]],axis=1)
                                para = pd.concat([para,green5x[1]],axis=1)
                                para = pd.concat([para,green5y[1]],axis=1)
                                para = pd.concat([para,sm5x[1]],axis=1)
                                para = pd.concat([para,sm5y[1]],axis=1)

                                para['No']='No' +str(No)
                                para = para.fillna('')
                                para = para.reset_index()
                                para.columns = ['wfrid', 'r5x_tr', 'r5x_exp', 'r5x_rotort', 'r5y_tr', 'r5y_rotort',
                                                'r5y_exp', 'g5x_tr', 'g5x_exp', 'g5x_rotort', 'g5y_tr', 'g5y_rotort',
                                                'g5y_exp', 'sm5x_tr', 'sm5x_exp', 'sm5x_rotort', 'sm5y_tr', 'sm5y_rotort',
                                                'sm5y_exp', 'No']

                                if (basic[4] + basic[0][:7]) in allParameter.keys():
                                    allParameter[basic[4] + basic[0][:7]] = pd.concat(
                                        [allParameter[basic[4] + basic[0][:7]],para.copy()], axis=0)
                                else:
                                    allParameter[basic[4] + basic[0][:7]] = para.copy()

                                # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '_parameter.csv'):
                                #     para.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '_parameter.csv',index=None,header=None,mode='a')
                                # else:
                                #     para.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + '_parameter.csv',index=None)
                            if 'opas'=='opas':
                                lot = lot.fillna('')
                                tmp = lot[['WaferNr', 'MarkNr_x', 'NomPosX', 'NomPosY']]
                                tmp.columns = ['WaferNr', 'MarkNrX', 'MarkNrY','toDelete','X', 'Y']
                                tmp = tmp.drop('toDelete',axis=1)
                                tmp1 = lot[['WaferNr', 'MarkNr_x','5XRedPos', '5YRedPos']]
                                tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY', 'toDelete','X', 'Y']
                                tmp1 = tmp1.drop('toDelete',axis=1)
                                tmp1 = pd.concat([tmp,tmp1],axis=0)
                                tmp1['type']='R5_Measured'
                                tmp1['key'] = [str(x)+','+str(y)+','+z for x,y, z in zip(tmp1['WaferNr'],tmp1['MarkNrX'],tmp1['type'])]
                                output=tmp1.copy()

                                tmp1 = lot[['WaferNr', 'MarkNr_x', '5XGreenPos', '5YGreenPos']]
                                tmp1.columns = ['WaferNr', 'MarkNrX', 'MarkNrY','toDelete','X', 'Y']
                                tmp1 = tmp1.drop('toDelete', axis=1)
                                tmp1 = pd.concat([tmp, tmp1], axis=0)
                                tmp1['type'] = 'G5_Measured'
                                tmp1['key'] = [str(x) + ',' + str(y) + ',' + z for x, y, z in
                                               zip(tmp1['WaferNr'], tmp1['MarkNrX'], tmp1['type'])]
                                output = pd.concat([output,tmp1],axis=0)

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

                                output['No']='No' +str(No)
                                output=output.fillna('')
                                if (basic[4] + basic[0][:7]) in allVector.keys():
                                    allVector[basic[4] + basic[0][:7]] = pd.concat(
                                        [allVector[basic[4] + basic[0][:7]],output.copy()], axis=0)
                                else:
                                    allVector[basic[4] + basic[0][:7]] = output.copy()


                                # if os.path.exists('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + "_" + basic[0][:7]+ '_5th.csv'):
                                #     output.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4] + "_" + basic[0][:7]+ '_5th.csv',index=None,header=None,mode='a')
                                # else:
                                #     output.to_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\' + basic[4]  + "_" + basic[0][:7]+ '_5th.csv',index=None)
                except:
                    print('==file errro or small wfr qty===')
                    try: # file has been moved in previous steps
                        src=file
                        tmp = file.split('\\')[-1]
                        dst = 'Y:\\ASML_AWE\\RawData_XPA_RAR\\' + file.split('\\')[-1]
                        shutil.move(src,dst)
                    except:
                        pass
            if 'saveFile'=='saveFile':
                if 'index'=='index' and len(index)>0:
                    if os.path.exists(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv'):
                        index['index'].to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv', index=None,
                                           header=None, mode='a')
                    else:
                        index['index'].to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv', index=None)
                if 'vector'=='vector' and len(allVector)>0:
                    for key in allVector.keys():
                        if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\vector.csv'):
                            allVector[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\vector.csv',index=None,header=None,mode='a')
                        else:
                            allVector[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\vector.csv',index=None)
                if 'allResidual'=='allResidual' and len(allResidual)>0:
                    for key in allResidual.keys():
                        if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\residual.csv'):
                            allResidual[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\residual.csv',index=None,header=None,mode='a')
                        else:
                            allResidual[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\residual.csv',index=None)
                if 'allX_WQ_MCC_DELTA'=='allX_WQ_MCC_DELTA' and len(allX_WQ_MCC_DELTA)>0:
                    for key in allX_WQ_MCC_DELTA.keys():
                        if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\X_WQ_MCC_DELTA.csv'):
                            allX_WQ_MCC_DELTA[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\X_WQ_MCC_DELTA.csv',index=None,header=None,mode='a')
                        else:
                            allX_WQ_MCC_DELTA[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\X_WQ_MCC_DELTA.csv',index=None)
                if 'allY_WQ_MCC_DELTA'=='allY_WQ_MCC_DELTA' and len(allY_WQ_MCC_DELTA)>0:
                    for key in allY_WQ_MCC_DELTA.keys():
                        if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\Y_WQ_MCC_DELTA.csv'):
                            allY_WQ_MCC_DELTA[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\Y_WQ_MCC_DELTA.csv',index=None,header=None,mode='a')
                        else:
                            allY_WQ_MCC_DELTA[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\Y_WQ_MCC_DELTA.csv',index=None)
                if 'allParameter'=='allParameter' and len(allParameter)>0:
                    for key in allParameter.keys():
                        if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\parameter.csv'):
                            allParameter[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\parameter.csv',index=None,header=None,mode='a')
                        else:
                            allParameter[key].to_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[:6] + '\\' + key[6:] + '\\parameter.csv',index=None)

    def temp(self):
        if 'vector'=='vector':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [ path + i for i in os.listdir(path) if '_5th' in i]
            for file in files:
                newpath = path + file.split('\\')[-1][0:6] + '\\' + file.split('\\')[-1][7:14] + '\\'
                if os.path.exists(newpath):
                    dest = newpath + 'vector.csv'
                    shutil.move(file,dest)
                else:
                    os.makedirs( newpath )
                    dest = newpath + 'vector.csv'
                    shutil.move(file, dest)
        if 'residual'=='residual':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'Residual' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df,index,how='left',on=['No'])
                df = df [['wfrNo', 'markNo', '5XRedResidual', '5YRedResidual', '5XGreenResidual',
       '5YGreenResidual', '5XSmResidual', '5YSmResidual', 'No', 'date']]
                df['date']=[i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool,key)
                    tmp = df[df['date']==key]
                    tmp.to_csv(path + tool + '\\' + key + '\\residual.csv',index=None)
                os.remove(file)
        if 'parameter'=='parameter':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'parameter' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files[:]:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df,index,how='left',on=['No'])
                df = df [['wfrid', 'r5x_tr', 'r5x_exp', 'r5x_rotort', 'r5y_tr', 'r5y_rotort',
       'r5y_exp', 'g5x_tr', 'g5x_exp', 'g5x_rotort', 'g5y_tr', 'g5y_rotort',
       'g5y_exp', 'sm5x_tr', 'sm5x_exp', 'sm5x_rotort', 'sm5y_tr',
       'sm5y_rotort', 'sm5y_exp', 'No', 'date',]]
                df['date']=[i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool,key)
                    tmp = df[df['date']==key]
                    tmp = tmp.drop('date',axis=1)
                    tmp.to_csv(path + tool + '\\' + key + '\\parameter.csv',index=None)
                os.remove(file)
        if 'X-WQ-MCC-Delta'=='X-WQ-MCC-Delta':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'X_WQ_MCC_DELTA' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files[:]:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df,index,how='left',on=['No'])
                df = df [['WaferNr', 'MarkNr', '1XRedMCC', '1XRedWQ', '5XRedMCC', '5XRedWQ',
       '1XGreenMCC', '1XGreenWQ', '5XGreenMCC', '5XGreenWQ', 'XRedDelta',
       'XGreenDelta', 'No', 'date']]
                df['date']=[i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool,key)
                    tmp = df[df['date']==key]
                    tmp = tmp.drop('date',axis=1)
                    tmp.to_csv(path + tool + '\\' + key + '\\X_WQ_MCC_DELTA.csv',index=None)
                os.remove(file)
        if 'Y-WQ-MCC-Delta'=='Y-WQ-MCC-Delta':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'Y_WQ_MCC_DELTA' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files[:]:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df,index,how='left',on=['No'])
                df = df [['WaferNr', 'MarkNr', '1YRedMCC', '1YRedWQ', '5YRedMCC', '5YRedWQ',
       '1YGreenMCC', '1YGreenWQ', '5YGreenMCC', '5YGreenWQ', 'YRedDelta',
       'YGreenDelta', 'No', 'date']]
                df['date']=[i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool,key)
                    tmp = df[df['date']==key]
                    tmp = tmp.drop('date',axis=1)
                    tmp.to_csv(path + tool + '\\' + key + '\\Y_WQ_MCC_DELTA.csv',index=None)
                os.remove(file)

        if 'TO_EXCEL'=='TO_EXCEL': #index 文件不对
            path = 'V:\\ExcelCsvFile\\NikonEga\\'
            filelist = []
            filelist = []
            for root, dirs, files in os.walk(path, False):
                for names in files:
                    filelist.append(root + '\\' + names)
            filelist = [i for i in filelist if i[-3:] == 'csv']
            for file in filelist[0:-1]:
                print(file)
                df = pd.read_csv(file)
                df.to_excel(file[:-3] + '.xlsx', index=None)

        # writer = pd.ExcelWriter('v:/excelcsvfile/asmlawe/alsd8a/2019_08/vector.xlsx')
        # for i in range(math.ceil(df.shape[0] / 1000000)):
        #     i = i + 1
        #     df.loc[(i - 1) * 1000000:i * 1000000].to_excel(writer,
        #                                                    'sheet' + str(i), index=False)
        # writer.save()
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
            tmp = open(name)
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
        if 'dict' == 'dict':
            toolid = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                      '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                      '3527': 'BLSD08'}
        if "GetFileList" == "GetFileList":  # new file 'Z:\\AsmlAweFile\\RawData\\'
            filelist = []
            toollist = ['7D', '08', '82', '83', '85', '86', '87', '89', '8A', '8B', '8C']
            for tool in toollist[:]:
                path = '\\\\10.4.72.74\\asml\\_AsmlDownload\\AWE\\' + tool
                for root, dirs, files in os.walk(path, False):
                    for names in files:
                        filelist.append(root + '\\' + names)
            filelist = [file for file in filelist if file.split('\\')[7][0:4] != 'Done']
        if "getIndex" == "getIndex":
            try:
                No = list(pd.read_csv(r'\\10.4.3.130\ftpdata\litho\excelcsvfile\asmlawe\index.csv')['No'])
                if len(No) == 0:
                    No = 0
                else:
                    No = int(No[-1][2:])
            except:
                No = 0
        if 'ReadFile' == 'ReadFile':
            allVector = {};
            allResidual = {};
            allParameter = {};
            allX_WQ_MCC_DELTA = {};
            allY_WQ_MCC_DELTA = {};
            index = {}
            for i, zipfile in enumerate(filelist[:]):
                allVector = {};
                allResidual = {};
                allParameter = {};
                allX_WQ_MCC_DELTA = {};
                allY_WQ_MCC_DELTA = {};
                index = {}
                print(No, i, zipfile, len(filelist))
                if os.path.isdir('c:/usr'):
                    shutil.rmtree('c:/usr')
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
                    os.remove(file)
                tar.close()

                try:
                    print('==========rename================')
                    print(zipfile)
                    print(zipfile[:39] + 'Done_' + zipfile[39:])
                    os.rename(zipfile, zipfile[:39] + 'Done_' + zipfile[39:])
                    print('===============succeeed')
                except:
                    pass

                for key in allVector.keys():
                    print(key)

                if 1 == 1:
                    if 'saveFile' == 'saveFile':
                        if 'index' == 'index' and len(index) > 0:
                            if os.path.exists(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv'):
                                index['index'].to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv',
                                                      index=None, header=None, mode='a')
                            else:
                                index['index'].to_csv(r'\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv',
                                                      index=None)
                        if 'vector' == 'vector' and len(allVector) > 0:
                            for key in allVector.keys():
                                if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                              :6] + '\\' + key[
                                                                                                                           6:] + '\\vector.csv'):
                                    allVector[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\vector.csv',
                                        index=None, header=None, mode='a')
                                else:
                                    allVector[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\vector.csv',
                                        index=None)
                        if 'allResidual' == 'allResidual' and len(allResidual) > 0:
                            for key in allResidual.keys():
                                if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                              :6] + '\\' + key[
                                                                                                                           6:] + '\\residual.csv'):
                                    allResidual[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\residual.csv',
                                        index=None, header=None, mode='a')
                                else:
                                    allResidual[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\residual.csv',
                                        index=None)
                        if 'allX_WQ_MCC_DELTA' == 'allX_WQ_MCC_DELTA' and len(allX_WQ_MCC_DELTA) > 0:
                            for key in allX_WQ_MCC_DELTA.keys():
                                if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                              :6] + '\\' + key[
                                                                                                                           6:] + '\\X_WQ_MCC_DELTA.csv'):
                                    allX_WQ_MCC_DELTA[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\X_WQ_MCC_DELTA.csv',
                                        index=None, header=None, mode='a')
                                else:
                                    allX_WQ_MCC_DELTA[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\X_WQ_MCC_DELTA.csv',
                                        index=None)
                        if 'allY_WQ_MCC_DELTA' == 'allY_WQ_MCC_DELTA' and len(allY_WQ_MCC_DELTA) > 0:
                            for key in allY_WQ_MCC_DELTA.keys():
                                if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                              :6] + '\\' + key[
                                                                                                                           6:] + '\\Y_WQ_MCC_DELTA.csv'):
                                    allY_WQ_MCC_DELTA[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\Y_WQ_MCC_DELTA.csv',
                                        index=None, header=None, mode='a')
                                else:
                                    allY_WQ_MCC_DELTA[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\Y_WQ_MCC_DELTA.csv',
                                        index=None)
                        if 'allParameter' == 'allParameter' and len(allParameter) > 0:
                            for key in allParameter.keys():
                                if os.path.exists('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                              :6] + '\\' + key[
                                                                                                                           6:] + '\\parameter.csv'):
                                    allParameter[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\parameter.csv',
                                        index=None, header=None, mode='a')
                                else:
                                    allParameter[key].to_csv(
                                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\' + key[
                                                                                                    :6] + '\\' + key[
                                                                                                                 6:] + '\\parameter.csv',
                                        index=None)
                if 1 == 2:
                    if 'saveFile' == 'saveFile':
                        index['index'].to_csv('c:/temp/index.csv', index=None)
                        for key in allVector.keys():
                            allVector[key].to_csv('c:\\temp\\' + key[:6] + '_' + key[6:] + '_vector.csv', index=None)

                        for key in allResidual.keys():
                            allResidual[key].to_csv('c:/temp/' + key[:6] + '_' + key[6:] + '_residual.csv', index=None)
                        for key in allX_WQ_MCC_DELTA.keys():
                            pass
                            allX_WQ_MCC_DELTA[key].to_csv('c:/temp/' + key[:6] + '_' + key[6:] + '_X_WQ_MCC_DELTA.csv',
                                                          index=None)

                        for key in allY_WQ_MCC_DELTA.keys():
                            allY_WQ_MCC_DELTA[key].to_csv('c:/temp/' + key[:6] + '_' + key[6:] + '_Y_WQ_MCC_DELTA.csv',
                                                          index=None)

                        for key in allParameter.keys():
                            allParameter[key].to_csv('c:/temp/' + key[:6] + '_' + key[6:] + '_parameter.csv',
                                                     index=None)

    def temp(self):
        if 'vector' == 'vector':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if '_5th' in i]
            for file in files:
                newpath = path + file.split('\\')[-1][0:6] + '\\' + file.split('\\')[-1][7:14] + '\\'
                if os.path.exists(newpath):
                    dest = newpath + 'vector.csv'
                    shutil.move(file, dest)
                else:
                    os.makedirs(newpath)
                    dest = newpath + 'vector.csv'
                    shutil.move(file, dest)
        if 'residual' == 'residual':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'Residual' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df, index, how='left', on=['No'])
                df = df[['wfrNo', 'markNo', '5XRedResidual', '5YRedResidual', '5XGreenResidual', '5YGreenResidual',
                         '5XSmResidual', '5YSmResidual', 'No', 'date']]
                df['date'] = [i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool, key)
                    tmp = df[df['date'] == key]
                    tmp.to_csv(path + tool + '\\' + key + '\\residual.csv', index=None)
                os.remove(file)
        if 'parameter' == 'parameter':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'parameter' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files[:]:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df, index, how='left', on=['No'])
                df = df[
                    ['wfrid', 'r5x_tr', 'r5x_exp', 'r5x_rotort', 'r5y_tr', 'r5y_rotort', 'r5y_exp', 'g5x_tr', 'g5x_exp',
                     'g5x_rotort', 'g5y_tr', 'g5y_rotort', 'g5y_exp', 'sm5x_tr', 'sm5x_exp', 'sm5x_rotort', 'sm5y_tr',
                     'sm5y_rotort', 'sm5y_exp', 'No', 'date', ]]
                df['date'] = [i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool, key)
                    tmp = df[df['date'] == key]
                    tmp = tmp.drop('date', axis=1)
                    tmp.to_csv(path + tool + '\\' + key + '\\parameter.csv', index=None)
                os.remove(file)
        if 'X-WQ-MCC-Delta' == 'X-WQ-MCC-Delta':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'X_WQ_MCC_DELTA' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files[:]:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df, index, how='left', on=['No'])
                df = df[['WaferNr', 'MarkNr', '1XRedMCC', '1XRedWQ', '5XRedMCC', '5XRedWQ', '1XGreenMCC', '1XGreenWQ',
                         '5XGreenMCC', '5XGreenWQ', 'XRedDelta', 'XGreenDelta', 'No', 'date']]
                df['date'] = [i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool, key)
                    tmp = df[df['date'] == key]
                    tmp = tmp.drop('date', axis=1)
                    tmp.to_csv(path + tool + '\\' + key + '\\X_WQ_MCC_DELTA.csv', index=None)
                os.remove(file)
        if 'Y-WQ-MCC-Delta' == 'Y-WQ-MCC-Delta':
            path = '\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\'
            files = [path + i for i in os.listdir(path) if 'Y_WQ_MCC_DELTA' in i]
            index = pd.read_csv('\\\\10.4.3.130\\ftpdata\\litho\\excelcsvfile\\asmlawe\\index.csv')
            for file in files[:]:
                tool = file.split('\\')[-1][:6]
                df = pd.read_csv(file)
                df = pd.merge(df, index, how='left', on=['No'])
                df = df[['WaferNr', 'MarkNr', '1YRedMCC', '1YRedWQ', '5YRedMCC', '5YRedWQ', '1YGreenMCC', '1YGreenWQ',
                         '5YGreenMCC', '5YGreenWQ', 'YRedDelta', 'YGreenDelta', 'No', 'date']]
                df['date'] = [i[:7] for i in df['date']]
                for key in df['date'].unique():
                    print(tool, key)
                    tmp = df[df['date'] == key]
                    tmp = tmp.drop('date', axis=1)
                    tmp.to_csv(path + tool + '\\' + key + '\\Y_WQ_MCC_DELTA.csv', index=None)
                os.remove(file)

        if 'TO_EXCEL' == 'TO_EXCEL':  # index 文件不对
            path = 'V:\\ExcelCsvFile\\NikonEga\\'
            filelist = []
            filelist = []
            for root, dirs, files in os.walk(path, False):
                for names in files:
                    filelist.append(root + '\\' + names)
            filelist = [i for i in filelist if i[-3:] == 'csv']
            for file in filelist[0:-1]:
                print(file)
                df = pd.read_csv(file)
                df.to_excel(file[:-3] + '.xlsx', index=None)

        # writer = pd.ExcelWriter('v:/excelcsvfile/asmlawe/alsd8a/2019_08/vector.xlsx')  # for i in range(math.ceil(df.shape[0] / 1000000)):  #     i = i + 1  #     df.loc[(i - 1) * 1000000:i * 1000000].to_excel(writer,  #                                                    'sheet' + str(i), index=False)  # writer.save()
class HOUSEKEEPING:
    def __init__(self):
        pass

    #https: // blog.csdn.net / weixin_42703149 / article / details / 84974991
    def MAKE_TAR_GZ(self,output_filename, source_dir):
        with tarfile.open(output_filename, "w:gz") as tar:
           tar.add(source_dir, arcname=os.path.basename(source_dir))

    def F1(self):
        root = '//10.4.72.74/litho/ASML_BATCHALIGN_REPORT/'
        for x in ['7D','08','82','83','85','86','87','89','8A','8B','8C'][1:]:
            n=0
            path = root + x + '/'
            list = os.listdir(path )
            for i in list:
                if os.path.isdir(path + i)==True:
                    print(n,x, i)
                    output_filename = path + i + '.tar.gz'
                    source_dir = path+i
                    self.MAKE_TAR_GZ(output_filename, source_dir)
                    try:
                        shutil.rmtree(path + i)
                    except:
                        pass
                    n+=1
                    # if n>200:
                    #     break
    def F2(self):
        root = '//10.4.72.74/litho/ASML_AWE/RawData/'
        for x in ['BLSD7D','BLSD08','ALSD82','ALSD83','ALSD85','ALSD86','ALSD87','ALSD89','ALSD8A','ALSD8B','ALSD8C'][0:]:
            n=0
            path = root + x + '/'
            list = os.listdir(path )
            for i in list:
                if os.path.isdir(path + i)==True:
                    print(n,x, i)
                    output_filename = path + i + '.tar.gz'
                    source_dir = path+i
                    self.MAKE_TAR_GZ(output_filename, source_dir)
                    try:
                        pass
                        shutil.rmtree(path + i)
                    except:
                        pass
                    n+=1
                    # if n>0:
                    #     break
    def F3(self): #ZIP FAB CD SEM FILE:idp/prms
        month_str = str(datetime.datetime.now())[0:7].replace("-",'')
        root = '//10.4.72.74/litho/HITACHI/'
        for x in ['ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11','SERVER'][:]:
            path = root + x + '/'
            list = os.listdir(path )
            list.sort()
            d = {}
            os.chdir(path)
            for i in list[0:]:
                if i[:3]=='new' or i[:9]=='overwrite':
                    if i[:-2] in d.keys():
                        tmp = d[i[:-2]];
                        tmp.append(i);
                        d[i[:-2]] = tmp
                    else:
                        d[i[:-2]] = [i]

                else:
                    if i[:-6] in d.keys():
                        tmp = d[i[:-6]];  tmp.append(i);    d[i[:-6]]= tmp
                    else:
                        d[i[:-6]]=[i]



            for i in d.keys():
                if i[-6:]<month_str:
                    tar = tarfile.open(path + 'ALL_' + i + '.tar','a')
                    for j in d[i]:
                        with tarfile.open(j+".gz", "w:gz") as tmp:
                            tmp.add(j)
                        tmp.close()
                        tar.add(j+".gz")
                        os.remove( path + j ) ;os.remove(path + j + '.gz')
                    tar.close()
class SQLITE3_DB:
    def __init__(self):
        pass
    def NikonParameter(self):
        if 1==1:
            filenamelist = []
            rootpath = '\\\\10.4.3.130\\ftpdata\\litho\\ExcelCsvFile\\NikonEga\\'
            for root, dirs, files in os.walk(rootpath, False):
                for names in files:
                    if 'parameter.csv' in names:
                        filenamelist.append(root + '\\' +names)
        df = pd.read_csv('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\NikonEga\\Index.csv')
        df['Date']=[i[-17:-5] for i in df['File']]
        df = df.drop(['File'],axis=1)

        conn=sqlite3.connect('//10.4.72.74/asml/_sqlite/NikonEgaPara.db')
        df.to_sql('tbl_index', conn, if_exists='append', index=None)

        for n,file in enumerate(filenamelist[:]):
            print(n, len(filenamelist),file)
            if 1==1:
            # if n%20==0:

                df = pd.read_csv(file)
                df =df .fillna('')
                df.to_sql('tbl_parameter', conn, if_exists='append', index=None)
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




    def ImportCsv(self):
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


        t1=datetime.datetime.now()
        conn = sqlite3.connect('\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\AsmlAwe\\sqlite3Awe.db')
        conn.execute('delete from tbl_index')
        conn.execute("COMMIT;")
        conn.execute("VACUUM")
        cursor = conn.cursor()
        # conn.execute("BEGIN TRANSACTION;")  # 关键点
        for i in range(1000):
            cursor.execute("insert into tbl_index values (?, ?,?,?,?,?,?,?,?)", ('1','2','3','4','5','6','7','8','9'))
        conn.execute("COMMIT;")  # 关键点
        t2=datetime.datetime.now()
        print(str(t2-t1))
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




class temp:#
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
                    print(line)
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
        root = '//10.4.72.74/asml/_AsmlDownload/BatchReport/'
        alltools = ['7D','08','8A','8B','8C','82','83','85','86','87','89']
        tool = {'4666': 'ALSD82', '4730': 'ALSD83', '6450': 'ALSD85', '8144': 'ALSD86', '4142': 'ALSD87',
                '6158': 'ALSD89', '5688': 'ALSD8A', '4955': 'ALSD8B', '9726': 'ALSD8C', '8111': 'BLSD7D',
                '3527': 'BLSD08'}
        for id in alltools[8:9]:
            filelist = [ root + id + '/' + i for i in os.listdir(root + id) if i[0:4]!='###Done' and i[-7:]=='.tar.gz']
            if len(filelist)>0:
                for zipfile in filelist[0:1]:
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
                            df.to_csv('c:/temp/' + id + '_batchreport.csv', index=False, header=False, mode='a',  encoding='utf-8')
                    tar.close()
                    print(zipfile + '--> Extraction & Check Done')
                    print( "Renaming...." + zipfile)

                    # os.rename(zipfile,zipfile[0:47]+"Done_"+zipfile[47:])

class AsmlBatchReport_TAR:#
    def __init__(self):
        pass
    def get_batch_report_path(self):




        filenamelist = []
        for root, dirs, files in os.walk("Y:\\ASML_BATCH_REPORT\\",False):
            for names in files:
                filenamelist.append(root + '\\' + names)
        filenamelist.sort(reverse=False)
        filenamelist = [x for x in filenamelist if x[-2:] == 'gz']
        df=pd.DataFrame(filenamelist)
        df['name'] = [x.split("archive\\")[1] for x in df[0]]
        df.columns = ["path", "name"]
        df["tmpdate"]=""
        df["readfile"]=""
        df["upper"]=""
        df["lower"]=""

        for i in range(0,df.shape[0]):
        # for i in range(40000, 40100):
            print(i,df.iloc[i,0])

            zipfile=df.iloc[i,0]
            tar = tarfile.open(zipfile)
            tmpdate=0
            rootpath = 'c:/temp/' + zipfile.split("\\")[-1][:-7]
            for file in tar.getmembers()[:]:
                if file.isdir()==False:
                    if tmpdate==0:

                        tmpdate=file.get_info()["mtime"]
                        readfile=file
                    else:
                        if file.get_info()["mtime"]>tmpdate:

                            tmpdate = file.get_info()["mtime"]
                            readfile=file

            df.iloc[i, 2] = tmpdate
            df.iloc[i,3]=readfile.name

            tar.extract(readfile, 'c:/temp/')
            f=open('c:/temp/'+readfile.name).read()
            if 'Lower Surface' in f:
                df.iloc[i,4]="TRUE"

            if 'Lower Surface' in f:
                df.iloc[i,5]="TRUE"

            tar.close()
        df.to_csv("C:/temp/iris.csv",index=None)




    # time.localtime(os.stat(file).st_mtime)
    # time.strftime("%Y-%m-%d %H:%M:%S",timedata)




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
            dbPath="Z:\\_SQLite\\AsmlBatchreport.db"
            col=['tool','date','time','lotid','jobName','jobModified','layer','doseActual','doseJob','focusActual','focusJob','focusRxActual','focusRxJob','focusRyActual','focusRyJob','aperture','illuminationMode','sigmaOutActual','sigmaOutJob','sigmaInActual','sigmaInJob','alignStrategy','markRequired','spmMarkScan','MaxDynErrCount','MaxDoseErrCount','deltaRedX','deltaRedY','deltaGreenX','deltaGreenY','batchStart','batchFinish','wfrBatchOut','wfrAccept','wfrReject','throughputWfrFirst','throughputWfrMax','throughputWfrMin','alignRecipe','tranXave','tranYave','expXave','expYave','wfrRotAve','wfrOrtAve','tranXdev','tranYdev','expXdev','expYdev','wfrRotDev','wfrOrtDev','reticleMagAve','reticleRotAve','redXave','redYave','greenXave','greenYave','reticleMagDev','reticleRotDev','redXdev','redYdev','greenXdev','greenYdev','globalLevelDzAve','globalLevelPhixAve','globalLevelPhiyAve','blueFocusAve','blueRyAve','globalLevelDzDev','globalLevelPhixDev','globalLevelPhiyDev','blueFocusDev','blueRyDev','fieldLevelDzMinAve','fieldLevelPhixMinAve','fieldLevelPhiyMinAve','fieldLevelDzMaxAve','fieldLevelPhixMaxAve','fieldLevelPhiyMaxAve','fieldLevelDzMinDev','fieldLevelPhixMinDev','fieldLevelPhiyMinDev','fieldLevelDzMaxDev','fieldLevelPhixMaxDev','fieldLevelPhiyMaxDev','fieldLevelDzMeanAve','fieldLevelPhixMeanAve','fieldLevelPhiyMeanAve','fieldLevelDzSigmaAve','fieldLevelPhixSigmaAve','fieldLevelPhiySigmaAve','fieldLevelDzMeanDev','fieldLevelPhixMeanDev','fieldLevelPhiyMeanDev','fieldLevelDzSigmaDev','fieldLevelPhixSigmaDev','fieldLevelPhiySigmaDev','MaXmin','MaYmin','MaRzMin','MaZmin','MsdXmin','MsdYmin','MsdRzMin','MsdZmin','MaXmax','MaYmax','MaRzMax','MaZmax','MsdXmax','MsdYmax','MsdRzMax','MsdZmax','MaXave','MaYave','MaRzAve','MaZave','MsdXave','MsdYave','MsdRzAve','MsdZave','MaXdev','MaYdev','MaRzDev','MaZdev','MsdXdev','MsdYdev','MsdRzDev','MsdZdev','intraFieldRxMinAve','intraFieldRxMaxAve','intraFieldRxAveAve','intraFieldRxDevAve','intraFieldRyMinAve','intraFieldRyMaxAve','intraFieldRyAveAve','intraFieldRyDevAve','intraFieldRxMinDev','intraFieldRxMaxDev','intraFieldRxAveDev','intraFieldRxDevDev','intraFieldRyMinDev','intraFieldRyMaxDev','intraFieldRyAveDev','intraFieldRyDevDev','TiltZplaneDzAve','TiltZplanePhixAve','TiltZplanePhiyAve','TiltZplaneMccAve','TiltResidualRxAve','TiltResidualRyAve','TiltZplaneDzDev','TiltZplanePhixDev','TiltZplanePhiyDev','TiltZplaneMccDev','TiltResidualRxDev','TiltResidualRyDev','doseError','Reticle','Inside_S','Inside_M','Inside_L','Outside_S','Outside_M','Outside_L','Full_S','Full_M','Full_L','focusErr','dynErr','doseErr','bqErr','chuckErr','xColorRG','xColorR','xColorG','yColorRG','yColorR','yColorG','xpaLargestOrderRedMax','xpaLargestOrderRedMin','xpaLargestOrderGreenMax','xpaLargestOrderGreenMin','xpaWorstRedWqMax','xpaWorstRedWqMin','xpaWorstGreenWqMax','xpaWorstGreenWqMin','xLargestOrderRedMax','xLargestOrderRedMin','xLargestOrderGreenMax','xLargestOrderGreenMin','xWorstRedWqMax','xWorstRedWqMin','xWorstGreenWqMax','xWorstGreenWqMin','yLargestOrderRedMax','yLargestOrderRedMin','yLargestOrderGreenMax','yLargestOrderGreenMin','yWorstRedWqMax','yWorstRedWqMin','yWorstGreenWqMax','yWorstGreenWqMin','xResidualMax','xResidualMin','xResidualAve','xResidualDev','yResidualMax','yResidualMin','yResidualAve','yResidualDev','batchType','QabovePoffsetAve','QabovePoffsetDev','blueFocusMax','blueFocusMin','blueFocusRange','markType','path']
        root = '//10.4.72.74/asml/_AsmlDownload/BatchReport/'
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

                    os.rename(zipfile,zipfile[0:47]+"Finished_"+zipfile[47:])

            if summary.shape[0]>0:
                conn = sqlite3.connect(dbPath)  #
                summary = summary.fillna("")  #
                summary.to_sql('tbl_asmlbatchreport', conn, if_exists='append', index=None)
                conn.close()

#










###################################################################################33
# if 1==2: # backup only
# #single function for multi-thread
#     def DOWNLOAD_AMP(tool):
#         df = pd.read_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidp' + tool + '.csv')
#         df=df[['prmsNo','ppid']].reset_index().drop('index',axis=1)
#         ampPath=[]
#         if df.shape[0]>0:
#             for i in range(df.shape[0]):
#                 ppid,prms = df.iloc[i,1].split('/')[5],df.iloc[i,0]
#                 idppath ='/HITACHI/DEVICE/HD/' + ppid[2:4]+'/data/' + ppid.split('$')[0]+'/'+ppid.split('$')[1].split('.')[0] + '/'
#                 ampPath.extend( [idppath + x[1:-1] for x in prms.replace(' ', '')[1:-1].split(',')] )
#             try:
#                 ftp = CD_SEM_111_NEW().LOGIN(tool)
#                 folder = '//10.4.72.74/asml/_DailyCheck/CD_SEM_Recipe/AMP/' + tool + '/'
#                 for length, amp in enumerate(ampPath):
#                     print(tool,length,amp,len(ampPath))
#                     try:
#                         dest = folder + amp.replace('/','$')
#                         source = amp
#                         file = open(dest, 'wb')
#                         ftp.retrbinary('RETR ' + source, file.write)
#                     except:
#                         pass
#                 ftp.close()
#             except:
#                 pass
#     def DOWNLOAD_IDP(tool):
#             ftp=CD_SEM_111_NEW().LOGIN(tool)
#             df = pd.read_csv(r'\\10.4.72.74\asml\_DailyCheck\CD_SEM_Recipe\PROMIS_CD_PPID.csv')
#             for i in range(df.shape[0]):
#                 if i%100==0:
#                     print(tool,i)
#                 idw,idp = df.iloc[i,0],df.iloc[i,1]
#                 try:
#                     source  = '/HITACHI/DEVICE/HD/' + idw[2:4] + '/data/' + idw + '/' + idp + '.idp'
#                     dest = '//10.4.72.74/asml/_DailyCheck/CD_SEM_Recipe/IDP/' + tool + '/' + idw+"$"+ idp +'.idp'
#                     file = open(dest, 'wb')
#                     ftp.retrbinary('RETR '+ source, file.write)
#                 except:
#                     pass
#             ftp.quit()
#     def READ_AMP(tool):
#         folder = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + tool +'/'
#         amppath = [ folder + i for i in os.listdir(folder)]
#         amppath.sort()
#         summary=[]
#
#         try:
#             for k,amp in enumerate(amppath):
#                 print(tool,k,amp,len(amppath))
#                 try:
#                     f = [i.strip() for i in open(amp) if '_dif' not in i]
#                     if len(f) == 54 or len(f) == 55:
#                         for index, i in enumerate(f):
#                             if 'comment   ' in i:
#                                 f = [i.split(":")[1].strip() for i in f[index + 1:]]
#                                 f.append(amp.split('$')[6])
#                                 f.append(amp.split('$')[7])
#                                 f.append(amp.split('$')[8])
#                         summary.append(f)
#                 except:
#                     pass
#         except:
#             pass
#         pd.DataFrame(summary).to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolAmp/'+tool+'_amp.csv',index=None)
#     def READ_IDP(tool):
#         folder='Z:/_DailyCheck/CD_SEM_Recipe/IDP/'+tool+'/'
#         filelist = [folder + i for i in os.listdir(folder) if 'idp' in i]
#         filelist.sort()
#
#         singleToolidp=[]
#
#         for  k,idppath in enumerate(filelist[0:]):
#
#             idpname=idppath
#             # idppath =  'Z:/_DailyCheck/CD_SEM_Recipe/IDP/' + tool +'/' + idppath + '.idp'
#
#             print(tool, k, idppath,len(filelist))
#             try:
#                 tmpTemplate=[]
#                 tmpCoordinate=[]
#                 tmpPrms=[]
#                 result=[]
#                 f = [i.strip() for i in open(idppath)]
#
#                 if len(f) > 25:
#                     for index, i in enumerate(f):
#                         if 'history    :' in i:
#                             result.append(i[14:].strip())
#                         if 'no_of_mpid' in i:
#                             result.append(i.split(':')[1].strip()) # structure qty
#                         if 'PRMS000' in i:
#                             tmpPrms.append(i.split(',')[9].strip())
#                         if 'template   : MS' in i:
#                             tmpTemplate.append(i.split(':')[-1].strip())  # template enabled
#
#                         if 'msr_point  :' in i:
#                             tmpCoordinate.append (  str(  int(int(i.split(',')[2]) / 1000)) +"," +  str(int(int(i.split(',')[3]) / 1000))   )
#                 # tmpCoordinate=[eval(i) for i in tmpCoordinate]
#                 result.extend([tmpTemplate,tmpPrms,list(pd.DataFrame(tmpCoordinate)[0].unique()),idpname]) # set-->wrong sequence
#             except:
#
#                 result=[]
#             if len(tmpCoordinate)>0:
#                 result.append(tool)
#                 singleToolidp.append(result)
#         pd.DataFrame(singleToolidp,columns=['history','dataNo','tmplate','prmsNo','coordinate','ppid','tool']).to_csv('c:/temp/singleToolidp'+tool+'.csv',index=None)
#         pd.DataFrame(singleToolidp,columns=['history','dataNo','tmplate','prmsNo','coordinate','ppid','tool']).to_csv('Z:/_DailyCheck/CD_SEM_Recipe/AMP/singleToolIdp/singleToolidp'+tool+'.csv',index=None)

if __name__ == "__main__":








    if 'test'=='test1':
        pass

        filepath = 'c:/usr/asm/data.8144/user_data/batch_reports/PROD/MO2927CA/A4/CL7L12.1'
        data, align, mark_residual = temp().singlefile(filepath)
        print(data)
        print(align)
        print(mark_residual)
        # temp().MainFunction()
        # \\10.4.3.130\ftpdata\excelcsvfile目录需要huangwei45写入，若huangwei45未登陆10.4.3.150，数据会丢失
        # 10.4.3.120 本地运行
        # NikonVector_NEW().MainFunction()
        # AWE_ANALYSIS_NEW().MainFunction()
        # NikonRecipeMaintain().MainFunction()
        # HOUSEKEEPING().F2()
        # AsmlBatchReport_TAR().MainFunction()
        # HOUSEKEEPING().F3()







    print('=========Routine Job Below')
    print('=========Routine Job Below')
    print('=========Routine Job Below')
    if 'task'=='task1':
        pass
        f = open('P:/_script/script/log.txt', 'a')
        f.write("\n\n===Start Time=== " + str(datetime.datetime.now())[:19])
        if 1 == 'not executed':
            try:
                # shutil.copyfile('P:/database/Nikon.mdb', '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\nikon.mdb')

                f.write('\nNIKON.MDB Copied-->skipped')
            except:
                f.write('\nNIKON.MDB Copying Failed')

            try:
                print('Running AsmlPreAlignment().MainFunction()  ')
                # AsmlPreAlignment().MainFunction()         #000013
                f.write('\n006_AsmlPreAlignment         _Succeeded(Skipped)     ' + str(datetime.now())[:19])
            except:
                f.write('\n006_AsmlPreAlignment         _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running PpcsAdo().MainFunction() ')
                # PpcsAdo().MainFunction()                  #000014
                f.write('\n007_PpcsAdo                  _Succeeded(Skipped)    ' + str(datetime.now())[:19])
            except:
                f.write('\n007_PpcsAdo                  _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running OvlOpt_FIA_LSA().MainFunction() ')
                # OvlOpt_FIA_LSA().MainFunction()           #000015
                f.write('\n008_OvlOpt_FIA_LSA           _Succeeded(skipped)    ' + str(datetime.now())[:19])
            except:
                pass
                f.write('\n008_OvlOpt_FIA_LSA           _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running QcCduOvl().MainFunction() ')
                # QcCduOvl().MainFunction()              #000016
                f.write('\n009_QcCduOvl                 _Succeeded(Skipped)    ' + str(datetime.now())[:19])
            except:
                f.write('\n009_QcCduOvl                 _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running NikonProductMetalImage().MainFunction()')
                NikonProductMetalImage().MainFunction()  # 000025
                f.write('\n016_NikonProductMetalImage   _Succeeded    ' + str(datetime.now())[:19])

            except:
                f.write('\n016_NikonProductMetalImage   _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running  CD_SEM_111().MainFunction() ')
                # CD_SEM_111().MainFunction()               #000026
                f.write('\n017_CD_SEM_111               _Succeeded(skipped)' + str(datetime.now())[:19])
            except:
                f.write('\n017_CD_SEM_111               _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running OvlCheck().MainFunction()')
                # OvlCheck().MainFunction()
                f.write('\n018_OvlCheck              _Succeeded(skipped)' + str(datetime.now())[:19])
            except:
                f.write('\n018_OvlCheck              _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running GOLDEN AMP CHECK')
                # CD_SEM_111_NEW().GOLDEN_AMP_CHECK()
                f.write('\n026_Golden_Amp_Check        _Succeeded(skipped)    ' + str(datetime.now())[:19])
            except:
                f.write('\n026_Golden_Amp_ChecK        _Failed       ' + str(datetime.now())[:19])

        if 1 == 'TO BE FIXED':
            try:
                print('Running CDU_QC_IMAGE_CD().MainFunction()')
                CDU_QC_IMAGE_CD().MainFunction()  # 000018
                f.write('\n010_CDU_QC_IMAGE_CD          _Succeeded    ' + str(datetime.now())[:19])
            except:
                f.write('\n010_CDU_QC_IMAGE_CD          _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running CDU_QC_IMAGE_PLOT().MainFunction() ')
                CDU_QC_IMAGE_PLOT().MainFunction()  # 000019
                f.write('\n011_CDU_QC_IMAGE_PLOT        _Succeeded    ' + str(datetime.now())[:19])
            except:
                f.write('\n011_CDU_QC_IMAGE_PLOT        _Failed       ' + str(datetime.now())[:19])

            try:
                print('Running NikonAdjust().MainFunction() ')
                NikonAdjust().MainFunction()  # 000021
                f.write('\n013_NikonAdjust              _Succeeded    ' + str(datetime.now())[:19])
            except:
                f.write('\n013_NikonAdjust              _Failed       ' + str(datetime.now())[:19])

        if 'Running NikonRecipeDate().MainFunction() ' == 'Running NikonRecipeDate().MainFunction() ':  # 001
            try:
                NikonRecipeDate().MainFunction()
                f.write('\n001_NikonRecipeDate              _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n001_NikonRecipeDate              _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running NikonPara().MainFunction()' == 'Running NikonPara().MainFunction()':  # 002
            try:
                NikonPara().MainFunction()
                f.write('\n002_NikonPara                    _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                pass
                f.write('\n002_NikonPara                    _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running NikonRecipeMaintain().MainFunction()' == 'Running NikonRecipeMaintain().MainFunction()':
            try:
                NikonRecipeMaintain().MainFunction()  # 000011-->xls file refreshed, MFG backlog read,PE list required
                f.write('\n003_NikonRecipeMaintain          _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n003_NikonRecipeMaintain          _Failed       ' + str(datetime.datetime.now())[:19])
                pass

        if 'CD_SEM_NO_111' == 'CD_SEM_NO_111':
            try:
                CD_SEM_NO_111().MAINFUNCTION()  # 00009
                f.write('\n004_CD_SEM_NO_111                _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n004_CD_SEM_NO_111                _Failed       ' + str(datetime.datetime.now())[:19])
                pass

        if 'Running AsmlBatchReport().MainFunction()' == 'Running AsmlBatchReport().MainFunction()':
            try:
                AsmlBatchReport_TAR().MainFunction()  # 000012
                f.write('\n005_AsmlBatchReport              _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n005_AsmlBatchReport              _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running NikonVector_NEW().MainFunction()' == 'Running NikonVector_NEW().MainFunction()':
            try:
                NikonVector_NEW().MainFunction()  # 00009
                f.write('\n006_NikonVector                  _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n006_NikonVector                  _Failed       ' + str(datetime.datetime.now())[:19])
                pass

        if 'Running ESF().MainFunction()  ' == 'Running ESF().MainFunction()  ':
            try:
                ESF().MainFunction()  # 000022 需要访问MFG数据库，其它电脑无法运行
                f.write('\n007_ESF                          _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n007_ESF                          _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running SPC99().MainFunction()' == 'Running SPC99().MainFunction()':
            try:
                SPC99().MainFunction()  # 000023 __init__ #000024 需要访问MFG数据库，其它电脑无法运行,
                f.write('\n008_SPC99                        _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n008_SPC99                        _Failed       ' + str(datetime.datetime.now())[:19])

        if 'MovePpidExcel().MainFunction()' == 'MovePpidExcel().MainFunction()':
            try:
                MovePpidExcel().MainFunction()
                f.write('\n009_MovePpidExcel                _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n009_MovePpidExcel                _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running ASML MACHINE CONSTANTS' == 'Running ASML MACHINE CONSTANTS':
            try:
                ASML_MC_CONS_LOG_SUMMARY().MainFunction()
                f.write('\n010_ASML_MC_CONST_LOG            _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n010_ASML_MC_CONST_LOG            _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running ASML ERROR LOG' == 'Running ASML ERROR LOG':
            try:
                ASML_ERROR_LOG_FOR_OPAS().MianFunction(path='//10.4.72.74/asml/_AsmlDownload/AsmlErrLog/')
                f.write('\n011_ASML_ERROR_LOG               _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n011_ASML_ERROR_LOG               _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running R2R OPAS FLOW' == 'Running R2R OPAS FLOW':
            try:
                MFG_FLOW_PPID_FOR_OPAS().MainFunction()
                f.write('\n012_R2R_OPAS_FLOW                _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n012_R2R_OPAS_FLOW                _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running OVL CHECK NEW' == 'Running OVL CHECK NEW':
            try:
                OvlCheckNew().MainFunction()
                f.write('\n013_OVL_CHECK_                   _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n013_OVL_CHECK                    _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running OvlCheck().Refresh_PPID_FROM_EXCEL' == 'Running OvlCheck().Refresh_PPID_FROM_EXCEL':
            try:
                tmp = str(datetime.datetime.now())[11:13]
                # if tmp>='20':
                #OvlCheck().refresh_ppid_from_xls()
                f.write('\n014_Refresh_PPID (skipped)       _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n014_Refresh_PPID                 _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running ESF CONSTRAINTS' == 'Running ESF CONSTRAINTS':
            try:
                AVAILABLE_TOOL().ESF()
                f.write('\n015_ESF_Tool_Avail               _Succeeded    ' + str(datetime.datetime.now())[:19])
                shutil.copyfile('Z:/_DailyCheck/ESF/ESF_TOOL_AVAILABLE.csv',
                                '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\ESF_TOOL_AVAILABLE.csv')
            except:
                f.write('\n015_ESF_Tool_Avail               _Failed       ' + str(datetime.datetime.now())[:19])

        if 'Running AWE_ANALYSIS_NEW_TAR().MainFunction() ' == 'Running AWE_ANALYSIS_NEW_TAR().MainFunction() ':
            try:
                AWE_ANALYSIS_NEW_TAR().MainFunction()  # 000020
                f.write('\n016_AWE_ANALYSIS                 _Succeeded    ' + str(datetime.datetime.now())[:19])
            except:
                f.write('\n016_AWE_ANALYSIS                 _Failed       ' + str(datetime.datetime.now())[:19])

        f.write("\n===End Time=== " + str(datetime.datetime.now())[:19] + "\n\n")
        f.close()
    print('=========Routine Job Above')
    print('=========Routine Job Above')
    print('=========Routine Job Above')
    print('=========Routine Job Above')






    if 'nouse'=='use':

        # if 1==2:
        #     pass
        #     # try:
        #     #     RunExcel().MainFunction()
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     MakeVmsFtp().MainFunction()               #000000
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     NikonRecipeDate().MainFunction()          #000007
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     NikonPara().MainFunction()                #000008
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     NikonVector().MainFunction()              #00009
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     NikonRecipeMaintain().MainFunction()      #000011-->xls file refreshed, MFG backlog read,PE list required
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     AsmlBatchReport().MainFunction()          #000012
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     AsmlPreAlignment().MainFunction()         #000013
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     PpcsAdo().MainFunction()                  #000014
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     OvlOpt_FIA_LSA().MainFunction()           #000015
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     QcCduOvl().MainFunction()()               #000016
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     CDU_QC_IMAGE_CD().MainFunction()          #000018
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     CDU_QC_IMAGE_PLOT().MainFunction()        #000019
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     AWE_ANALYSIS().MainFunction()             #000020
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     NikonAdjust().MainFunction()              #000021
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     ESF().MainFunction()                      #000022 需要访问MFG数据库，其它电脑无法运行
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     SPC99().MainFunction()                    #000023 __init__ #000024 需要访问MFG数据库，其它电脑无法运行,
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     NikonProductMetalImage().MainFunction()   #000025
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     CD_SEM_111().MainFunction()               #000026
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #
        #     #     OvlCheck().MainFunction()
        #     # except:
        #     #     pass
        #     #
        #     # try:
        #     #     MovePpidExcel().MainFunction()
        #     # except:
        #     #     pass
        #     #
        #     #
        #     # Tools().JobinImport()                     # get optimum r2r value from OPAS and gengerate csv file for import
        #
        #
        #
        #     try:
        #         pass
        #         # f1 = datetime.datetime.now()
        #         #  CD_SEM_111_GOLDEN_AMP().ALL_IDP_AMP_EXTRACTION()
        #         #  CD_SEM_111_GOLDEN_AMP().DOWNLOAD_ALL_IDW_IDP_AMP()
        #         #  CD_SEM_111_GOLDEN_AMP().uploadfile()
        #         #  CD_SEM_111_GOLDEN_AMP().ALL_AMP_ONLY_EXTRACTION()
        #
        #         # AVAILABLE_TOOL().ESF()
        #         # CD_SEM_111_GOLDEN_AMP().CD_DAILY_REWRITE_DOWNLOAD()
        #         # CD_SEM_111_GOLDEN_AMP().CD_DAILY_REWRITE_DOWNLOAD()
        #         # CD_SEM_111_NEW().MainFunction()
        #         # f2=datetime.datetime.now()
        #         # print(f1,f2,f2-f1)
        #     except:
        #         pass
        #     # https://www.runoob.com/python/python-multithreading.html
        #     # https://www.runoob.com/python/python-multithreading.html
        #     # https://www.jianshu.com/p/e33f6e31e06c
        #     # multi-thread for AMP


        # selection = input('please input "login 111 to run"  in order to check recipe')

        if 1==2:
            selection = "login 111 to run"
            if selection=="login 111 to run":

                print('============CD SEM IDP/AMP OPERATION============')
                print('OPAS REFRESH:-->  p:/cd/opas/r2rCdConfig.csv')
                if True: #download IDP,AMP
                    f1 = datetime.datetime.now()
                    print('========Refreshing CD PPID========')
                    CD_SEM_111_NEW().CD_PPID() #error  at 10.4.3.111  python34和python36，读出CSV文件，字段名差异

                    # if 1==1: #Download all
                    #     print('========IDP downloading========  -->to download all')
                    #     # if 1==True:                 #Try_Except Below is fro IDP download
                    #     #     try:
                    #     #         t0= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('SERVER',True))
                    #     #         t1= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('ALCD01',True))
                    #     #         t2= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('ALCD02',True))
                    #     #         t3= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('ALCD03',True))
                    #     #         t4= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('ALCD08',True))
                    #     #         t5= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('ALCD09',True))
                    #     #         t6= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('ALCD10',True))
                    #     #         t7= threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_IDP, args=('BLCD11',True))
                    #     #
                    #     #         t0.start()
                    #     #         t1.start()
                    #     #         t2.start()
                    #     #         t3.start()
                    #     #         t4.start()
                    #     #         t5.start()
                    #     #         t6.start()
                    #     #         t7.start()
                    #     #
                    #     #         t0.join()
                    #     #         t1.join()
                    #     #         t2.join()
                    #     #         t3.join()
                    #     #         t4.join()
                    #     #         t5.join()
                    #     #         t6.join()
                    #     #         t7.join()
                    #     #     except:
                    #     #         pass
                    #     print('========Reading IDP========  -->to download all')
                    #     # if 1==1:
                    #     #     try:  # Try_Except Below is to read IDP
                    #     #         t0 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('SERVER',True))
                    #     #         t1 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('ALCD01',True))
                    #     #         t2 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('ALCD02',True))
                    #     #         t3 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('ALCD03',True))
                    #     #         t4 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('ALCD08',True))
                    #     #         t5 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('ALCD09',True))
                    #     #         t6 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('ALCD10',True))
                    #     #         t7 = threading.Thread(target=CD_SEM_111_NEW().READ_IDP, args=('BLCD11',True))
                    #     #
                    #     #
                    #     #         t0.start()
                    #     #         t1.start()
                    #     #         t2.start()
                    #     #         t3.start()
                    #     #         t4.start()
                    #     #         t5.start()
                    #     #         t6.start()
                    #     #         t7.start()
                    #     #
                    #     #         t0.join()
                    #     #         t1.join()
                    #     #         t2.join()
                    #     #         t3.join()
                    #     #         t4.join()
                    #     #         t5.join()
                    #     #         t6.join()
                    #     #         t7.join()
                    #     #     except:
                    #     #         pass
                    #     print('========AMP downloading========  -->to download all')
                    #     # if 1==1:
                    #     #     try:
                    #     #         pass
                    #     #         t0 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('SERVER',True))
                    #     #         t1 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('ALCD01',True))
                    #     #         t2 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('ALCD02',True))
                    #     #         t3 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('ALCD03',True))
                    #     #         t4 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('ALCD08',True))
                    #     #         t5 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('ALCD09',True))
                    #     #         t6 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('ALCD10',True))
                    #     #         t7 = threading.Thread(target=CD_SEM_111_NEW().DOWNLOAD_AMP, args=('BLCD11',True))
                    #     #
                    #     #         t0.start()
                    #     #         t1.start()
                    #     #         t2.start()
                    #     #         t3.start()
                    #     #         t4.start()
                    #     #         t5.start()
                    #     #         t6.start()
                    #     #         t7.start()
                    #     #
                    #     #         t0.join()
                    #     #         t1.join()
                    #     #         t2.join()
                    #     #         t3.join()
                    #     #         t4.join()
                    #     #         t5.join()
                    #     #         t6.join()
                    #     #         t7.join()
                    #     #     except:
                    #     #         pass
                    #     print('========AMP Extracting========  -->to download all')
                    #     # if 1==1:
                    #     #     try:                        # Try_Except Below is for AMP extraction
                    #     #
                    #     #         t0= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('SERVER',True))
                    #     #         t1= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('ALCD01',True))
                    #     #         t2= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('ALCD02',True))
                    #     #         t3= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('ALCD03',True))
                    #     #         t4= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('ALCD08',True))
                    #     #         t5= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('ALCD09',True))
                    #     #         t6= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('ALCD10',True))
                    #     #         t7= threading.Thread(target=CD_SEM_111_NEW().READ_AMP, args=('BLCD11',True))
                    #     #
                    #     #         t0.start()
                    #     #         t1.start()
                    #     #         t2.start()
                    #     #         t3.start()
                    #     #         t4.start()
                    #     #         t5.start()
                    #     #         t6.start()
                    #     #         t7.start()
                    #     #
                    #     #         t0.join()
                    #     #         t1.join()
                    #     #         t2.join()
                    #     #         t3.join()
                    #     #         t4.join()
                    #     #         t5.join()
                    #     #         t6.join()
                    #     #         t7.join()
                    #     #     except:
                    #     #         pass
                    #     print('========Combining All IDP========  -->to download all')
                    #     # if True:
                    #     #     CD_SEM_111_NEW().All_IDP(allFlag=True)
                    #     print('========Generating Golden AMP')
                    #     # if True:
                    #     #     CD_SEM_111_NEW().GOLDEN_AMP(allFlag=True)


                    print('========Downloading Modified List========')
                    CD_SEM_111_NEW().Recipe_Updated_By_Layerowner() #layer owner下载的AMP优先处置作为标准设置
                    CD_SEM_111_NEW().Daily_CHECK()

                    f2 = datetime.datetime.now()

                    print(f1,f2,f2-f1)
                    # x = input('hit any key to end script')

                    # CD_SEM_111_NEW().OVER_WRITE_CD09_IDP()
            else:
                # CD_SEM_111_NEW().STANDARDIZE_AMP_PARAMETER()
                CD_SEM_111_NEW().GOLDEN_AMP_CHECK()
                # CD_SEM_111_NEW().GOLDEN_AMP(allFlag=False)
                # CD_SEM_111_NEW().Recipe_Updated_By_Layerowner()

