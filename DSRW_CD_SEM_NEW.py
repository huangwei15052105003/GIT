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
import tarfile
import time
import datetime
# import numpy as np
# from sklearn.linear_model import LinearRegression
import shutil
# import win32com.client
# from dateutil import parser
# from PIL import Image,ImageDraw,ImageFont
# from math import isnan
# from matplotlib.gridspec import GridSpec
# import gc
import pandas as pd
# import matplotlib.pyplot as plt
# import matplotlib.dates as mdates
# import matplotlib.lines as mlines
# import re
# import matplotlib.ticker as ticker
# r'D:\HuangWeiScript\PyTaskCode\R2R_New_Part.xlsm'
# 'Z:\\_DailyCheck\\NikonShot\\transfer\\move.xls'
# os.system('z:\\_dailycheck\\ESF\\ESF.xlsm')

#below for wafer map
# from matplotlib.patches import Circle, Rectangle,Polygon
# import matplotlib.pyplot as plt
# from math import sqrt as sqrt
# import win32com.client
# import threading
# from multiprocessing import Process



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
            # amp['object'] = amp['object'].apply(lambda x: 'space' if x == 1 else ('line' if x == 0 else 'hole'))
            # amp['method'] = amp['method'].apply(lambda x: 'linear' if x == 0 else 'threshold')
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





if __name__ == "__main__":
    CD_SEM_NO_111().DOWNLOAD_FROM_SERVER()
    CD_SEM_NO_111().AMP_CHECK()
    CD_SEM_NO_111().GOLDEN_RECIPE()