# -*- coding: utf-8 -*-
"""
Created on Tue Apr  3 16:24:25 2018

@author: huangwei45
"""

import ftplib
import os
import pandas as pd
import datetime
import win32com.client


class CD_SEM_CHECK:
    def __init__(self):
        pass
    def LOGIN(tool):
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
            #user是FTP用户名，pwd就是密码了
            ftp.login(user,password)
        except ftplib.error_perm:
            print('登录失败')
            ftp.quit()
            return
        print('登陆成功')
        return ftp
    def AMP_CHECK(self):
        T1 = datetime.datetime.now()
        #TODO  content of excel macro -->MDB; ALDI08:AWE
        try:
            # get CD PPID, OVL PPID, MFG TECH vs PART
            df = pd.read_excel('Z:/_DailyCheck/CD_SEM_Recipe/check/_PPID.xlsm', sheet_name='PPID')
            df = pd.merge(df[['PART', 'STAGE', 'PPID']].dropna(), df[['PART.2', 'TECH']].dropna().drop_duplicates(),
                          left_on='PART', right_on='PART.2', how='left').drop(['PART.2'], axis=1)
            IDW,IDP = [],[]
            # in cas of ROM CODE, IDW != PARTID
            for i in range(df.shape[0]):
                if '-' in df.iloc[i, 0] and df.iloc[i, 0][df.iloc[i, 0].find('-', 7, ) + 1].isdigit():
                    # print(df.iloc[i,0])
                    IDW.append(df.iloc[i, 0].split('-')[0])
                    IDP.append(df.iloc[i, 2][len(df.iloc[i, 0].split('-')[0]) + 1:])
                else:
                    IDW.append(df.iloc[i, 0])
                    IDP.append(df.iloc[i, 2][len(df.iloc[i, 0]) + 1:])
            df['IDW'] = IDW
            df['IDP'] = IDP
            IDW, IDP = None, None
        except:
            return
        try:
            # merge full tech
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
            conn.Open(DSN)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            sql = 'select * from TECH'
            rs.Open(sql, conn, 1, 3)
            tmp = []
            for i in range(rs.RecordCount):
                tmp.append([rs.fields(1).value,rs.fields(2).value])
                rs.MoveNext()
            rs=None
            conn.close
            tmp = pd.DataFrame(tmp,columns=['PartID', 'FullTech'])
            tmp = tmp.dropna().drop_duplicates()
            df = pd.merge(df, tmp, left_on='PART', right_on='PartID', how='left').drop(['PartID'], axis=1).fillna(
                '').drop_duplicates()
        except:
            return
        try:
            # merge line/space type,仅对应R2R工艺前三位，不规范命名的（18BCD）无法匹配
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
            conn.Open(DSN)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            sql = "select TechName, Layer, Type  from R2R_CD_TEMPLATE WHERE IsHasTemplate='True'"
            rs.Open(sql, conn, 1, 3)
            tmp = []
            for i in range(rs.RecordCount):
                tmp.append([rs.fields(0).value, rs.fields(1).value,rs.fields(2).value])
                rs.MoveNext()
            rs = None
            conn.close
            tmp = pd.DataFrame(tmp, columns=[ 'TechName', 'Layer', 'Type' ])
            tmp = tmp.dropna().drop_duplicates()
            df['TechName'] = [i[:3] for i in df['FullTech']]
            df['Layer'] = [i[:2] for i in df['IDP']]
            df = pd.merge(df, tmp, on=['TechName', 'Layer'], how='left').fillna('')
        except:
            return

        # merge IDP
        try:
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
            conn.Open(DSN)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            sql = 'select * from IDP'
            rs.Open(sql, conn, 1, 3)
            tmp2 = []
            for i in range(rs.fields.count):
                tmp2.append(rs.fields(i).name)
            tmp = []
            for i in range(rs.RecordCount):
                tmp1 = []
                for j in range(rs.fields.count):
                    tmp1.append(rs.fields(j).value)
                tmp.append(tmp1)
                rs.MoveNext()
            rs = None
            conn.close
            tmp = pd.DataFrame(tmp,columns=tmp2)
            tmp=tmp.fillna('').drop_duplicates()
            df = pd.merge(df,tmp,on=['IDW','IDP'],how = 'left').fillna('')
        except:
            return

        # merge AMP
        try:
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
            conn.Open(DSN)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            sql = 'select * from AMP'
            rs.Open(sql, conn, 1, 3)
            tmp2 = []
            for i in range(rs.fields.count):
                tmp2.append(rs.fields(i).name)
            tmp = []
            for i in range(rs.RecordCount):
                tmp1 = []
                for j in range(rs.fields.count):
                    tmp1.append(rs.fields(j).value)
                tmp.append(tmp1)
                rs.MoveNext()
            rs = None
            conn.close
            tmp = pd.DataFrame(tmp,columns=tmp2)
            tmp=tmp.fillna('').drop_duplicates()
            df = pd.merge(df, tmp, on=['IDW', 'IDP', 'TOOL'], how='left').fillna('')
            tmp=None
        except:
            return







        # df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/_Merge.csv',index=None)

        # full data to be checked
        act = df[(df['TEMPLATE'] == '') & (df['TechName'] != '') & (df['measurement'] != '')]

        act = act[
            ['PART', 'STAGE', 'PPID', 'TECH', 'IDW', 'IDP', 'FullTech', 'TechName', 'Layer', 'Type', 'TEMPLATE', 'X',
             'Y', 'TOOL', 'measurement', 'object', 'meas_kind', 'output_data', 'rot_correct', 'method', 'smoothing',
             'differential', 'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line', 'l_base_area', 'r_threshold',
             'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area', 'centering', 'inverse', 'assist', 'meas_point',
             'diameters', 'sum_lines_point']]

        # layer classification and  individual amp comparison
        category1 = ['' for i in range(act.shape[0])]
        golden = pd.read_excel(r'Z:\_DailyCheck\CD_SEM_Recipe\check\_amp_golden.xlsx', sheet_name='SIMPLE')
        flag1 = category1.copy()
        flag2 = category1.copy()
        flag3 = category1.copy()

        # ===
        for i in range(act.shape[0]):

            tmp1 = act.iloc[i, 7][1]  # Tech
            tmp2 = act.iloc[i, 8]  # Layer
            if tmp1.isdigit():
                if tmp1 == '1':

                    if 'GT' in tmp2 or 'GC' in tmp2 or 'PC' in tmp2:
                        category1[i] = ('018GT')
                    elif 'P0' in tmp2:
                        category1[i] = ('018P0')
                    elif 'TO' in tmp2:
                        category1[i] = ('018TO')
                    elif 'WT' in tmp2 or (tmp2[0] == 'W' and tmp2[1].isdigit()):
                        category1[i] = ('018HOLE')
                    elif 'AT' in tmp2 or 'TT' in tmp2 or (tmp2[0] == 'A' and tmp2[1].isdigit()):
                        category1[i] = ('018METAL')
                    else:
                        if act.iloc[i, 9] == 'Line':
                            category1[i] = ('018LINE')
                        elif act.iloc[i, 9] == 'Hole/Space':
                            category1[i] = ('018SPACE')

                else:
                    if 'GT' in tmp2 or 'GC' in tmp2 or 'PC' in tmp2 or 'TO' in tmp2:
                        category1[i] = ('035LINE')
                    elif 'WT' in tmp2 or (tmp2[0] == 'W' and tmp2[1].isdigit()):
                        category1[i] = ('035HOLE')
                    elif 'AT' in tmp2 or 'TT' in tmp2 or (tmp2[0] == 'A' and tmp2[1].isdigit()):
                        category1[i] = ('035METAL')
                    else:
                        if act.iloc[i, 9] == 'Line':
                            category1[i] = ('035LINE')
                        elif act.iloc[i, 9] == 'Hole/Space':
                            category1[i] = ('035SPACE')
            if category1[i] != '':

                ref = golden[golden['Golden'] == category1[i]].T.dropna().T  # benchmark algorithm
                ref = ref[ref.columns[:-1]]
                tmp3 = pd.DataFrame(act.iloc[i, :]).T[ref.columns]
                tmp3 = pd.concat([tmp3.T, ref.T], axis=1)
                tmp3.columns = ['tobechecked', 'reference']
                checked = tmp3['tobechecked'] == tmp3['reference']

                if str(ref['method'].iloc[0]) == '0' and str(ref['meas_kind'].iloc[0]) == '1':  # linear,multipot

                    flag1[i] = checked[0]  # linespace
                    f2 = True
                    for j in checked[1:17]:
                        f2 = f2 and j
                    flag2[i] = f2  # key parameter
                    f3 = True
                    for j in checked[17:]:
                        f3 = f3 and j
                    flag3[i] = f3  # smoothing,differential,line No, etc.

                    print(i, act.shape[0], flag1[i], flag2[i], flag3[i])

                elif str(ref['method'].iloc[0]) == '0' and str(ref['meas_kind'].iloc[0]) == '0':  # linear,single

                    flag1[i] = checked[0]  # linespace
                    f2 = True
                    for j in checked[1:16]:
                        f2 = f2 and j
                    flag2[i] = f2  # key parameter
                    f3 = True
                    for j in checked[16:]:
                        f3 = f3 and j
                    flag3[i] = f3
                    print(i, act.shape[0], flag1[i], flag2[i], flag3[i])

                elif str(ref['method'].iloc[0]) == '1' and str(ref['meas_kind'].iloc[0]) == '0':  # threshold,single

                    flag1[i] = checked[0]  # linespace
                    f2 = True
                    for j in checked[1:12]:
                        f2 = f2 and j
                    flag2[i] = f2  # key parameter
                    f3 = True
                    for j in checked[12:]:
                        f3 = f3 and j
                    flag3[i] = f3
                    print(i, act.shape[0], flag1[i], flag2[i], flag3[i])

                elif str(ref['method'].iloc[0]) == '1' and str(ref['meas_kind'].iloc[0]) == '2':  # hold
                    flag1[i] = checked[0]  # linespace
                    f2 = True
                    for j in checked[1:10]:
                        f2 = f2 and j
                    flag2[i] = f2  # key parameter
                    f3 = True
                    for j in checked[10:]:
                        f3 = f3 and j
                    flag3[i] = f3
                    print(i, act.shape[0], flag1[i], flag2[i], flag3[i])

        T2 = datetime.datetime.now()
        print(T1, '\n', T2)
        act['F1'] = flag1
        act['F2'] = flag2
        act['F3'] = flag3
        act.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/_Merge.csv', index=None)
        act.to_csv('Z:/_CD_SEM_DOUBLE_CHECK/_Merge.csv', index=None)
        return act






def TEMPLATE_LIST(ftp):    #list tmplate name in 'MS'folder
    root_dir = r'/HITACHI/DEVICE/HD/.template/MS/'
    try:
       #得工作目录
        ftp.cwd(root_dir)
    except ftplib.error_perm:
        print('列出当前目录失败')
        ftp.quit()
        return
    tmp = ftp.nlst()
    templateList = [ root_dir + x for x in tmp if '~' not in x]
    
    #ftp.retrlines('LIST')
    return templateList
def DOWNLOAD_TEMPLATE():
    dir1 = 'Z:/_DailyCheck/CD_SEM_Recipe/Template/'
    Toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    #Toollist = ['SERVER']
    #Toollist = ['ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    for tool in Toollist:
        os.chdir(dir1 + tool) 
        try:
            ftp = LOGIN(tool)
            templateList = TEMPLATE_LIST(ftp)
            for n,folder in enumerate(templateList):
                print(n,len(templateList),folder)
                try:
                    ftp.cwd(folder)
                    file = open(folder.split('/')[-1],'wb' )
                    ftp.retrbinary('RETR PRMS0001' ,file.write) 
                except:
                    print("ERROR")
        except:
            pass
        ftp.quit()
# above is for downloading tempplate
# Below is for products IDP/PRMS0001

def DIGITAL_FOLDER(ftp):
    root_dir = r'/HITACHI/DEVICE/HD/'
    try:
       #得工作目录
        ftp.cwd(root_dir)
    except ftplib.error_perm:
        print('列出当前目录失败')
        #ftp.quit()
        return
    tmp = ftp.nlst()
    digitalFolder = [ x for x in tmp if (len(x)== 2 and x.isdigit())]
    digitalFolder.append('XX')
    #ftp.retrlines('LIST')
    return digitalFolder
def IDW_FOLDER_IDW_DOWNLOAD(ftp,folder1,tool):
    root_dir = r'/HITACHI/DEVICE/HD/' + folder1 + '/data'
    localDir = 'Z:/_DailyCheck/CD_SEM_Recipe/IDW/' + tool 
    idwFile = []
    idwFolder = []
    
    #idw folder and idw file 
    try:
        ftp.cwd(root_dir)
        tmp = ftp.nlst()
        
        
        idwFile = [ x for x in tmp if '.idw' in x and "lock" not in x]               
        os.chdir(localDir) 
        ftp.cwd(root_dir )
        idwFile.sort()
        for n,idw in enumerate(idwFile):
            print('IDW_DOWNLOAD',n,len(idwFile),idw,folder1,tool)
            try:
                file = open(idw,'wb' )
                ftp.retrbinary('RETR '+ idw ,file.write)  
            except:
                pass        
        
        idwFolder = [ x for x in tmp if '.idw' not in x and "lock" not in x] 
        return idwFolder
    except:
        pass
def IDP_FOLDER_IDP_DOWNLOAD(ftp,folder1,folder2,tool):
    root_dir = r'/HITACHI/DEVICE/HD/' + folder1 + '/data/' + folder2 
    localDir = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/' + tool 
    idpFile = []
    idpFolder = []
    
    #idp folder and idp file 
    try:
        ftp.cwd(root_dir)
        tmp = ftp.nlst()
        idpFile = [ x for x in tmp if '.idp' in x and "lock" not in x] 
        os.chdir(localDir) 
        ftp.cwd(root_dir )
        idpFile.sort()
        for n,idp in enumerate(idpFile):
            print('IDP_DOWNLOAD',n,len(idpFile),idp,folder2,tool)
            try:
                file = open(folder2 + "_" + idp,'wb' )
                ftp.retrbinary('RETR '+ idp ,file.write)
            except:
                pass        
            
        
        
        idpFolder = [ x for x in tmp if '.idp' not in x and "lock" not in x]       
        return idpFolder
    except:
        pass

    #download idp file 
def MAIN_IDW_IDP_AMP():
    
    Toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    #Toollist = ['SERVER']
    #Toollist = ['ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    
    for tool in Toollist:
        print(tool)

        try:
            ftp = LOGIN(tool)
            digitalFolder = DIGITAL_FOLDER(ftp)
            digitalFolder.sort()
            for folder1 in digitalFolder:
                idwFolder = IDW_FOLDER_IDW_DOWNLOAD(ftp,folder1,tool)
                idwFolder.sort()
                for folder2 in idwFolder:
                    #print(folder2)
                    idpFolder = IDP_FOLDER_IDP_DOWNLOAD(ftp,folder1,folder2,tool)
                    os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/AMP/' + tool)
                    idpFolder.sort()
                    for amp in idpFolder:
                        
                        ftp.cwd('/HITACHI/DEVICE/HD/' + folder1 + '/data/' + folder2 + '/' + amp)
                        print('AMP_DOWNLOAD',folder1,folder2,amp)
                        try:
                            file = open(folder2 + "_" + amp,'wb' )
                            ftp.retrbinary('RETR PRMS0001' ,file.write) 
                        except:
                            pass
            ftp.quit()
                
        except:
            pass
def CD_SEM_Template_Analysis():

    col= ['measurement',         'object',         'meas_kind',         'meas_point',
         'diameters',         'output_data',         'rot_correct',         'scan_rate',
         'method',         'design_rule',         'search_area',         'sum_lines',
         'sum_lines2',         'smoothing',         'differential',         'detect_start',
         'l_threshold',         'l_direction',         'l_edge_no',         'l_base_line',
         'l_base_area',         'r_threshold',         'r_direction',         'r_edge_no',
         'r_base_line',         'r_base_area',         'dummy1',         'dummy2',
         'dummy3',         'dummy4',         'sum_lines_point',         'assist',
         'xy_direction',         'centering',         'grain_area_x',         'grain_area_y',
         'grain_min',         'high_pass_filter',         'y_design_rule',         'gap_value_mark',
         'gap_shape1',         'gap_shape2',         'corner_edge',         'width_design',
         'area_design',         'number_design',         'grain_design',         'inverse',
         'method2',         'score_design',         'score_number_design',         'Path']
    
    root = 'Z:/_DailyCheck/CD_SEM_Recipe/Template/'
    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    filelist = []
    for tool in toollist:
        subfolder=root + tool + '/'
        tmp =[subfolder + i for i in  os.listdir(subfolder)]
        filelist.extend(tmp)
                
    template=[]
    for k, file in enumerate(filelist):
        print(k,len(filelist),file)
        try:                
            f = [ i.strip() for i in open(file) if '_dif' not in i]

            if len(f)==54 or len(f)==55:
                for index,i in enumerate(f):
                    if 'comment   ' in i:
                        f = [ i.split(":")[1].strip() for i in f[index+1:]]
                        f.append(file)
                template.append(f)
                        
        except:
            pass
    df = pd.DataFrame(template,columns=col)
    df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/template.csv',index=None)
def TEMPLATE_LINKED():
    root = 'Z:/_DailyCheck/CD_SEM_Recipe/idp/'
    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
   
    for tool in toollist[0:]:
        subfolder=root + tool + '/'
        filelist =[subfolder + i for i in  os.listdir(subfolder) if 'idp' in i]        
        tmp = []
        for n,file in enumerate(filelist):
            f = [ i.split(',')[1].strip() for i in open(file) if 'template   : MS' in i]
            if len(f)>0:
                tmp1 = file.split('/')[4:]
                tmp1.append(f[0])
                tmp.append(tmp1)
                print(tool,n, len(filelist))
      
        pd.DataFrame(tmp).to_csv('Z:/_DailyCheck/CD_SEM_Recipe/idp-template-linked'+ tool +'.csv')
def to_be_deleted_template():
    
    tmp=[]
    
    root = 'Z:/_DailyCheck/CD_SEM_Recipe/'
    
    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    
   
    
    for tool in toollist:
        full = os.listdir(root + 'template/' + tool)
        full = set(full)
        
        used = pd.read_csv('Z:/_DailyCheck/CD_SEM_Recipe/idp-template-linked'+ tool + '.csv',usecols = '2')
        used = used['2'].unique()
        tmp.append([tool,len(full),len(used),len(full)-len(used)])
        df = pd.DataFrame(tmp,columns=['Server','All Template Count','Linked Count','Delta'])
def TEMPLATE_DATE():    #list tmplate name in 'MS'folder
    root_dir = r'/HITACHI/DEVICE/HD/.template/MS/'
    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    summary = []
    for tool in toollist:
        print('Downloading ' + tool + 'Template......')
        try:
            ftp = LOGIN(tool)
         
            ftp.cwd(root_dir)
            tmp  = []
            ftp.retrlines('LIST',tmp.append)
            #ftp.sendcmd('MDTM ZZ83-TB/PRMS0001')
            ftp.quit()
                
            for i in tmp[1:]:
                if not ("~" in i):
                    summary.append( tool + ' ' + i)
        except:
            pass
        
    tmp = str(datetime.datetime.now())[:19] 
    tmp = tmp.replace(':','-')
    tmp = tmp.replace(' ','_')
    df = pd.DataFrame(summary)
    df.to_csv('Z:\\_DailyCheck\\CD_SEM_Recipe\\check\\template_date_' + tmp + '.csv')
    
    filelist = os.listdir('Z:\\_DailyCheck\\CD_SEM_Recipe\\check\\')
    
    for file in filelist:
        if tmp[0:10] in file:
            new = [i for i in open('Z:\\_DailyCheck\\CD_SEM_Recipe\\check\\' + file) ]
            new.sort()
            
            
  

    new1 = []
    for n, i in enumerate( new): 
        if len(i)>60:
            x = i[45:].strip()[4:]
            if ":"  in x[11:].strip().split(' ')[0]:
                new1.append( i.split(',')[1][:6] + ',' +
                            x[:3].strip() + ',' +
                            x[5:8].strip() + ',' +
                            x[11:].strip().split(' ')[0] + ',' +
                            x[11:].strip().split(' ')[-1])
            else:
                new1.append( i.split(',')[1][:6] + ',' +
                            x[:3].strip() + ',' +
                            x[5:8].strip() + ',' +
                            x[11:].strip().split(' ')[0][0:4] + ',' +
                            x[11:].strip().split(' ')[-1])                
                        

    reference = [ i for i in open('Z:\\_DailyCheck\\CD_SEM_Recipe\\check\\template_date_reference.csv') ]
    reference.sort()
    reference1=[]

    for n, i in enumerate(reference):        
        if len(i)>60:
            x = i[45:].strip()[4:]
            if ":"  in x[11:].strip().split(' ')[0]:
                reference1.append( i.split(',')[1][:6] + ',' +
                            x[:3].strip() +  ',' +
                            x[5:8].strip() + ',' +
                            x[11:].strip().split(' ')[0] + ',' +
                            x[11:].strip().split(' ')[-1])
            else:
                reference1.append( i.split(',')[1][:6] + ',' +
                            x[:3].strip() +  ',' +
                            x[5:8].strip() + ',' +
                            x[11:].strip().split(' ')[0][0:4] + ',' +
                            x[11:].strip().split(' ')[-1])                
                        
    
    
    

    modified =pd.DataFrame( list(set(reference1)-set(new1)))
    modified['Type']='Modified'
    modified['Date']=tmp
    modified.columns = ['Detail','Type','Date']
    name = [ ( i.split(',')[0] + ',' + i.split(',')[4]) for i in list( modified['Detail'])]
    
    
    l1 = []
    for x in name:
        try:
            l1.append([ i for i in new1 if  (x.split(',')[0] == i.split(',')[0] and  x.split(',')[1] == i.split(',')[-1])][0]) 
        except:
            l1.append('Deleted')
            
        
    modified['Revised']=l1    
    
    Flag=['' for i in  range(modified.shape[0])]
    for i in range(modified.shape[0]):
        try:
            if modified.iloc[i,0].split(',')[:3]== modified.iloc[i,3].split(',')[:3]:
                Flag[i] = "Identical Month-Day"
            else:
                Flag[i] = "Month-Day Modified"
        except:
            Flag[i] = "Check Failed"
    modified['Flag'] = Flag
        
    modified.to_csv('Z:\\_DailyCheck\\CD_SEM_Recipe\\check\\_TEMPLATE_DAILY.csv',index=None,mode='a',header=None)
    
    
    
    
    added = ( list   (set(new1)-set(reference1)        )         )
    
    added1=[]
    for n,i in enumerate(added):
        if ( i.split(',')[0] + ',' + i.split(',')[4]) in name:
            pass
        else:
            added1.append(i)
    added1 = pd.DataFrame(added1)
    added1['Type']='Added'
    added1['Date']=tmp
    added1.columns = ['Detail','Type','Date']
    added1.to_csv('Z:\\_DailyCheck\\CD_SEM_Recipe\\check\\\_TEMPLATE_DAILY.csv',index=None,mode='a',header=None)
def Download_Modified_List():
    os.chdir('Y:/ModifiedCdSemIDP/'  )  
    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    
    for tool in toollist:
        print(tool)
        try:
            ftp = LOGIN(tool)
         
            ftp.cwd('/dailycheck')
            
            for tmp in ftp.nlst():
                file = open(tmp,'wb' )
                ftp.retrbinary('RETR ' + tmp ,file.write)
            ftp.quit()
    


        except:
            pass
def IDP_AMP_EXTRACTION():
    root1 = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/'
    root2 = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'
    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    for tool in toollist[5:]:
        idpfilelist =[root1 + tool + '/' + i for i in  os.listdir(root1 + tool)]
        ampfilelist =[root2 + tool + '/' + i for i in  os.listdir(root2 + tool)]
        idpfilelist.sort()
        ampfilelist.sort()
        
        #extract AMP data
        amp=[]
        for k, file in enumerate(ampfilelist):
            print(k,'AMP',len(ampfilelist),file)
            try:                
                f = [ i.strip() for i in open(file) if '_dif' not in i]
    
                if len(f)==54 or len(f)==55:
                    for index,i in enumerate(f):
                        if 'comment   ' in i:
                            f = [ i.split(":")[1].strip() for i in f[index+1:]]
                            f.append(tool)
                            f.append(  file.split('/')[5][:file.split('/')[5].find('_',1)])
                            f.append(  file.split('/')[5][file.split('/')[5].find('_',1)+1:])                            
                    amp.append(f)
                            
            except:
                pass
        df = pd.DataFrame(amp)
        df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_amp.csv',index=None,header=None,mode='a')
        
        #extract IDP data
        idp = []
        for k,file in enumerate(idpfilelist):
            print(k,'IDP',len(idpfilelist),file)
            try:
                tmp = ['NA' for i in range(6)]
                f = [i.strip() for i in open(file)]
                if len(f)>25:
                    for index,i in enumerate(f):
                        if 'idw_name' in i:
                            tmp[0] = i.split(':')[1].strip()
                            if  'FEM' in tmp[0] or 'MAP' in tmp[0]:
                                break                        
                        if 'idp_name ' in i :
                            tmp[1] = i.split(':')[1].strip()
                            if tmp[1][-2:] != 'LN' or 'FEM' in tmp[1] or 'MAP' in tmp[1]:
                                break                      
                        if 'template   : MS : 1' in i:
                            tmp[2] = i.split(',')[1].strip()
                        if 'msr_point  :      1 :' in i:
                            tmp[3] = int( int(i.split(',')[2])/1000 )
                            tmp[4] = int( int(i.split(',')[3])/1000 )
                            tmp[5] = tool
                            idp.append(tmp)
                            break                                                
            except:
                pass
            
            
        df = pd.DataFrame(idp)
        df.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_idp.csv',index=None,header=None,mode='a')
def READ_AMP(amppath,tool):
    try:                
        f = [ i.strip() for i in open(amppath) if '_dif' not in i]
    
        if len(f)==54 or len(f)==55:
            for index,i in enumerate(f):
                if 'comment   ' in i:
                    f = [ i.split(":")[1].strip() for i in f[index+1:]]
                    f.append(tool)
                    f.append(  amppath.split('/')[6][:amppath.split('/')[6].find('_',1)])
                    f.append(  amppath.split('/')[6][amppath.split('/')[6].find('_',1)+1:])
    except:
        pass
    return f
def READ_IDP(idppath,tool):
    try:
        tmp = ['NA' for i in range(6)]
        f = [i.strip() for i in open(idppath)]
        if len(f)>25:
            for index,i in enumerate(f):
                if 'idw_name' in i:
                    tmp[0] = i.split(':')[1].strip()
                    if  'FEM' in tmp[0] or 'MAP' in tmp[0]:
                        break                        
                if 'idp_name ' in i :
                    tmp[1] = i.split(':')[1].strip()
                    if tmp[1][-2:] != 'LN' or 'FEM' in tmp[1] or 'MAP' in tmp[1]:
                        break                      
                if 'template   : MS : 1' in i:
                    tmp[2] = i.split(',')[1].strip()
                if 'msr_point  :      1 :' in i:
                    tmp[3] = int( int(i.split(',')[2])/1000 )
                    tmp[4] = int( int(i.split(',')[3])/1000 )
                    tmp[5] = tool
                    break                                                
    except:
        pass    
    #tmp = pd.DataFrame(tmp).T
    #tmp.columns=['IDW',	'IDP',	'TEMPLATE',	'X',	'Y',	'TOOL']
    return tmp
def AMP_COMPARE(ampdb,ampcol,ampcolsimple,newamp,str1):
    tmp = ampdb[ (ampdb['IDW']==newamp[-2]) & (ampdb['IDP']==newamp[-1]) & (ampdb['TOOL']==newamp[-3] ) ][ampcol]
    
    df = pd.DataFrame(columns=ampcol)
    if tmp.shape[0] == 0:
        print("NewFile")
        
        newamp.append(str1)
        pd.DataFrame(newamp).T.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_amp.csv',index=None,header=None,mode='a')
        return df
    else:
        #print("Revised File")
        newamp = pd.DataFrame(newamp).T
        newamp.columns = ampcol[:-1]
        newamp =list( newamp[ampcolsimple[:-1]].iloc[0])
        tmp =list( tmp[ampcolsimple[:-1]].iloc[0])
                    
                    
    
        tmp =[str(i) for i in  tmp]
        if tmp==newamp:
            pass
            print("Identical Algorithm" )
            return df
        else:
            df = pd.DataFrame([tmp,newamp],columns = ampcolsimple[:-1])
            df['type']=['old','new']
            df['Date']=str1
            return df
def IDP_COMPARE(idpdb,newidp,str1):
    tmp = idpdb[ (idpdb['IDW']==newidp[0]) & (idpdb['IDP']==newidp[1]) & (idpdb['TOOL']==newidp[5] ) ]

    if tmp.shape[0] == 0:
        pass      
        print("Compare:NewFile")
        newidp.append(str1)
        pd.DataFrame(newidp).T.to_csv('Z:/_DailyCheck/CD_SEM_Recipe/check/_idp.csv',index=None,header=None,mode='a')
        newidp=[]
        return newidp
    else:
        #print("Revised File")
        tmp = tmp.fillna('NA')
        tmp = list(tmp.iloc[0])
        tmp = tmp[:-1] #remove date
        if tmp==newidp:
            pass
            print("Compare:Identical Mark Coordinate" )
            newidp=[]
            return newidp
        else:
            print("Compare:Mark Coordinate Changed")
            newidp.extend(tmp[3:5])
           
            return newidp
def Modified_IDP_AMP_DOWNLOAD():

    
    root = 'y:/ModifiedCdSemIDP/'
    toollist = ['DATAS1','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    str1 = str(datetime.datetime.now()).replace('-','')[0:8]
    #str1='20190122'
    filelist = [ root + i for i in os.listdir(root) if str1 in i and '-IDP-' in i]
    
    for tool in toollist[:]:
        print(tool)
        try:
            file = [ i for i in filelist if tool in i][0]
            idplist = [i.strip()  for i in  open(file)]
            #os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/IDP/temp')
            if tool == 'DATAS1':
                tool = 'SERVER'
            ftp = LOGIN(tool)
            #download IDP  ===========================================================================
            os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/IDP/NEW/' + tool)
            for idpfile in idplist:
                try:
                    print('downloading IDP_' + idpfile + '.........')
                    ftp.cwd(idpfile.replace('/','%',6).split('/')[0].replace('%','/'))
                    tmp = open(idpfile.replace('/','%',6).split('%')[-1][:-3].replace('/','_') + 'IDP','wb' )
                    ftp.retrbinary('RETR ' + idpfile.replace('/','%',6).split('/')[1],tmp.write)
                    
                except:
                    pass
                
            #download AMP ==================================================================
            os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/AMP/NEW/' + tool)  
            for idpfile in idplist:
                try:
                    print('downloading AMP_' + idpfile + '.........')
                    ftp.cwd(idpfile[:-4])
                    tmp = open(idpfile.replace('/','%',6).split('%')[-1][:-4].replace('/','_'),'wb' )
                    ftp.retrbinary('RETR PRMS0001' ,tmp.write)
                except:
                    pass                
            ftp.quit()                
    

        except:
            tmp = open( root + 'log.txt','a')
            tmp.write('\n'+ str1 + "_list of " + tool + ' is not available\n')
            tmp.close()
def Modified_IDP_AMP_CHECK():
    
    idpdb = pd.read_csv('Z:\_DailyCheck\CD_SEM_Recipe\check\_IDP.csv')
  
    ampdb = pd.read_csv('Z:\_DailyCheck\CD_SEM_Recipe\check\_AMP.csv')
    ampcol = ampdb.columns 
    ampcolsimple = list(pd.read_excel('Z:\_DailyCheck\CD_SEM_Recipe\check\_AMP_CHECK.xlsx').columns[:-1])
    ampcolsimple.extend(['TOOL','IDW','IDP','Date'])
    idpSummary = []
    
    tmp = ampcolsimple.copy()[:-1]
    tmp.extend(['type','Date'])


    ampSummary = pd.DataFrame(columns = tmp )
    
    root = 'y:/ModifiedCdSemIDP/'
    toollist = ['DATAS1','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    str1 = str(datetime.datetime.now()).replace('-','')[0:8]
    #str1='20190131'
    filelist = [ root + i for i in os.listdir(root) if str1 in i and '-IDP-' in i]
    
    for tool in toollist[:]:
        print(tool)
        try:          
            file = [ i for i in filelist if tool in i][0]
            idplist = [i.strip()  for i in  open(file)]
            #os.chdir('Z:/_DailyCheck/CD_SEM_Recipe/IDP/temp')
            if tool == 'DATAS1':
                tool = 'SERVER'
            
            #Read IDP and Compare  ===========================================================================


            for idpfile in idplist:
                print('\n')                
                idppath = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/NEW/' + tool + '/' + idpfile.replace('/','%',6).split('%')[-1][:-3].replace('/','_') + 'IDP'
                newidp = READ_IDP(idppath,tool)
                print(newidp)
  
                if newidp[0] != 'NA':
                    #print("to compare")
                    #newidp[4]=888
                    idpcompareresult = IDP_COMPARE(idpdb,newidp,str1)
                    if len(idpcompareresult) >2: #mark coordinate chagned
                        x = [idpfile]
                        x.extend(idpcompareresult)
                        x.append( abs(int(x[4])-int(x[7]))<20 and abs(int(x[5])-int(x[8]))<20)
                        idpSummary.append(x)
                        
                

            #Read AMP and Compare  ===========================================================================                                  
            for idpfile in idplist:                
                amppath = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/NEW/' + tool + '/' + idpfile.replace('/','%',6).split('%')[-1][:-4].replace('/','_') 
                newamp = READ_AMP(amppath,tool)
                print(newamp)
                
                if len(newamp) == 54 :
                    #newamp[0]=888
                    ampcompareresult =  AMP_COMPARE(ampdb,ampcol,ampcolsimple,newamp,str1)
                    if ampcompareresult.shape[0]>0: #amp algorithm chagned                                                
                        ampSummary = pd.concat([ampSummary,ampcompareresult],axis=0)
                

            

        except:
            pass
            
    ampSummary['Date']=str1
    if ampSummary.shape[0]>0:
        ampSummary.to_csv('Z:\_DailyCheck\CD_SEM_Recipe\check\_AMP_CHECK.csv',mode= 'a' ,index=None,header=None)
        #ampSummary.to_excel('Z:\_DailyCheck\CD_SEM_Recipe\check\_AMP_CHECK.xlsx',index=None,mode='a',header=None)
    print('ampSummary Rows:' + str( ampSummary.shape[0]) )
    print('idpSummary Rows:' + str(len(idpSummary)))
        
    if len(idpSummary)>0:
        idpSummary = pd.DataFrame(idpSummary)
        idpSummary['Date']=str1
        idpSummary.columns = ['Recipe','IDW','IDP','TEMPLATE','New_X','New_Y','Tool','Old_X','Old_Y','Flag','Date']
        idpSummary.to_csv('Z:\_DailyCheck\CD_SEM_Recipe\check\_IDP_CHECK.csv',index=None,mode='a',header=None)
def FILE_OPERATION():
    
# copy checked result for owners  
    str1 = str(datetime.datetime.now()).replace('-','')[0:8]
    dir1 = 'Z:/_CD_SEM_DOUBLE_CHECK/'
    dir2 = 'Z:/_DailyCheck/CD_SEM_Recipe/check/'
    l1 = ['_AMP_CHECK.csv','_IDP_CHECK.csv','_TEMPLATE_DAILY.csv']
    for file in l1:
        try:
            sourceFile = dir2 + file
            targetFile = dir1 + file
            print(sourceFile,targetFile)
            open(targetFile, "wb").write(open(sourceFile, "rb").read())
        except:
            pass
'''
# move downloaded file

    sourceDirIdp = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/NEW/'
    targetDirIdp = 'Z:/_DailyCheck/CD_SEM_Recipe/IDP/'
    
    sourceDirAmp = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/NEW/'
    targetDirAmp = 'Z:/_DailyCheck/CD_SEM_Recipe/AMP/'
    
    
    

    toollist = ['SERVER','ALCD01','ALCD02','ALCD03','ALCD08','ALCD09','ALCD10','BLCD11']
    
    # MOVE DOWNLOADED IDP/AMP file 
    for tool in toollist[:]:
        pass

        sourceFolderIdp = sourceDirIdp + tool + '/'
        targetFolderIdp = targetDirIdp + tool + '/'
        sourceFolderAmp = sourceDirAmp + tool + '/'
        targetFolderAmp = targetDirAmp + tool + '/'

       
        for file in os.listdir(sourceFolderIdp):
            print("Moving IDP File " + tool + " " + file )
            
            try:
                if os.path.exists(targetFolderIdp + file):
                    os.remove(targetFolderIdp + file)
                    os.rename( sourceFolderIdp + file, targetFolderIdp + file)
                else:
                    os.rename( sourceFolderIdp + file, targetFolderIdp + file)
            except:
                pass
            
        
        for file in os.listdir(sourceFolderAmp):
            print("Moving AMP File " + tool + " " + file )
            try:
                if os.path.exists(targetFolderAmp + file):
                    os.remove(targetFolderAmp + file)
                    os.rename( sourceFolderAmp + file, targetFolderAmp + file)
                else:
                    os.rename( sourceFolderAmp + file, targetFolderAmp + file)
            except:
                pass

'''
def READ_ALL_BIAS_TABLE():
    
    path = 'p:/recipe/biastable/'
    data = pd.DataFrame(columns=[0,3,7,'PART'])
    filelist = [ path + i for i in os.listdir(path) if '.xl' in i or '.XL' in i]
    for k,file in enumerate(filelist[0:]):
        print(k,len(filelist),file)
        try:
            df = pd.read_excel(file,skiprows=[0,1,2,3],header=None)[[0,3,7]].dropna()
            df['PART']=file.split('/')[3].split('.')[0]
            data = pd.concat([data,df],axis=0)
        except:
            pass


    data.columns = ['FullTech','Layer','Type','PART']
    data.to_csv('c:/temp/0001.csv',index=None)
    # data not correct
def dummy():
    
    #sumarize AMP from actual recipe
    
    act = df[['PART', 'STAGE', 'PPID', 'TECH', 'IDW', 'IDP', 'FullTech', 'TechName',
       'Layer', 'Type', 'TEMPLATE', 'X', 'Y', 'TOOL', 'measurement', 'object',
       'meas_kind', 'meas_point', 'diameters', 'output_data', 'rot_correct',
       'scan_rate', 'method', 'design_rule', 'search_area', 'sum_lines',
       'sum_lines2', 'smoothing', 'differential', 'detect_start',
       'l_threshold', 'l_direction', 'l_edge_no', 'l_base_line', 'l_base_area',
       'r_threshold', 'r_direction', 'r_edge_no', 'r_base_line', 'r_base_area',
        'sum_lines_point', 'assist',
       'xy_direction', 'centering', 'grain_area_x', 'grain_area_y',
       'grain_min', 'high_pass_filter', 'y_design_rule', 'gap_value_mark',
       'gap_shape1', 'gap_shape2', 'corner_edge', 'width_design',
       'area_design', 'number_design', 'grain_design', 'inverse', 'method2',
       'score_design', 'score_number_design']]
   
    act = df[['TechName','Layer', 'TEMPLATE',  'measurement', 'object','meas_kind', 'method']]
   
       
    
    act = act[ act['TEMPLATE']=='' ] 
    act = act[ act['TechName'] != '']
    act = act[ act['measurement'] != '']
    act = act.drop_duplicates()
    
    act['Key'] =[act.iloc[i,0] + '_' +  act.iloc[i,1] for i in range(act.shape[0])]
    
    tmp = act['Key'].groupby(act['Key']).count()
    tmp =pd.DataFrame( [ [tmp.index[i],tmp[i]] for i in range(len(tmp)) if tmp[i]>1],columns=['Key','Count'])
    act = pd.merge(act,tmp,on=['Key'],how = 'left').fillna(1)
    act['Count']=act['Count'].astype('int8')
    tmp = act[act['Count']>1]
    tmp.to_csv('c:/temp/001.csv')
    act = act.sort_values(by = ['Count','TechName','Layer'])
    act.to_csv('c:/temp/001.csv')
def FTP_RENAME():
    
    #rename IDP and AMP
    tool='SERVER'
    ftp = LOGIN(tool)
   
    ftp.cwd('/HITACHI/DEVICE/HD/ZZ/data/G52873AA')

    try:
        ftp.rename('/HITACHI/DEVICE/HD/ZZ/data/G52873AA/ZZ-LN.idp' ,'/HITACHI/DEVICE/HD/ZZ/data/G52873AA/ZZ-LN20190128.idp')
    except:
        pass
        print('error')
    
    try:
        ftp.rename('/HITACHI/DEVICE/HD/ZZ/data/G52873AA/ZZ-LN' ,'/HITACHI/DEVICE/HD/ZZ/data/G52873AA/ZZ-LN20190128')        
    except:
        pass    
        print('error')
    
    ftp.close()
    
    
    #delete MS
    str1 = r'/HITACHI/DEVICE/HD/.template/MS/'
    df = pd.read_csv(r'Z:\_DailyCheck\CD_SEM_Recipe\check\_TEMPLATE_DAILY.csv')
    df = df[df['Type']=='Added']['Detail']
    df = pd.DataFrame([[i.split(',')[0],i.split(',')[-1]] for i in df],columns = ['Tool','MS']).drop_duplicates()
    df = df.sort_values(by='Tool')
    
    for n in range(df.shape[0]):
        if n == 0:
            print(n,df.iloc[n,0],df.iloc[n,1])
            try:
                ftp = LOGIN (df.iloc[n,0])
                if '_TO_BE_DELETE' not in df.iloc[n,1]:
                    ftp.rename( str1 + df.iloc[n,1],str1 + '_TO_BE_DELETED_' + df.iloc[n,1])  
                    print('RENAME OF ' + df.iloc[n,0] + " " + df.iloc[n,1] + ' DONE' )
            except:
                pass
                print('FAILD')
        else:
            print(n,df.iloc[n,0],df.iloc[n,1])
            try:
                if df.iloc[n,0]==df.iloc[n-1,0]:
                    if '_TO_BE_DELETE' not in df.iloc[n,1]:
                        ftp.rename( str1 + df.iloc[n,1],str1 + '_TO_BE_DELETED_' + df.iloc[n,1])
                        print('RENAME OF ' + df.iloc[n,0] + " " + df.iloc[n,1] + ' DONE' )
                else:
                    ftp.close()
                    ftp = LOGIN(df.iloc[n,0])
                    if '_TO_BE_DELETE' not in df.iloc[n,1]:
                        ftp.rename( str1 + df.iloc[n,1],str1 + '_TO_BE_DELETED_' + df.iloc[n,1])
                        print('RENAME OF ' + df.iloc[n,0] + " " + df.iloc[n,1] + ' DONE' )
            except:
                pass
                print('FAILD')
    ftp.close()
def DELETE_TEMPLATE():

    
    
    df = pd.read_csv(r'Z:\_DailyCheck\CD_SEM_Recipe\check\_TEMPLATE_DAILY.csv')    
    df = df[ (df['Type']=='Added')  & (df['Detail'].str.contains('_TO_BE_DELETED_'))]
    df = [[i.split(',')[0],i.split(',')[-1]] for i in df['Detail']]
    df = pd.DataFrame(df,columns=['Tool','Recipe']).drop_duplicates()
    df = df.sort_values(by='Tool').reset_index()
    df = df.drop('index',axis=1)
    for i in range(df.shape[0]):
        print(df.loc[i:i])
        if i ==0:
            pass
            #ftp = LOGIN(df.iloc[i,0])
            recipe = df.iloc[i,1]
            #to delete recipe
       
        else:
            if df.iloc[i-1,0] == df.iloc[-1,0]:
                pass
                recipe = df.iloc[i,1]
                #to delete recipe
               
            else:
                #ftp.quit()
                #ftp = LOGIN(df.iloc[i,0])
                recipe = df.iloc[i,1]
                #to delete recipe
                
    ftp.quit()
def DELETE_TEMPLATE_SUB(ftp,recipe):
    root = '/HITACHI/DEVICE/HD/.template/MS/'
    folder = '/.IMMS0001.jpeg/'
  
    f2 = '.IMMS0001.jpeg/index.txt'
    
    try:    
        ftp.delete( root + recipe + '/.IMMS0001.jpeg/cond.txt')
    except:
        pass
    try:
        ftp.delete( root + recipe + '/.IMMS0001.jpeg/index.txt')
    except:
        pass
    try:
        ftp.rmd(root + recipe + '/.IMMS0001.jpeg/')
    except:
        pass
    try:
        ftp.delete(root + recipe + '/ENMP0001')
    except:
        pass
    try:
        ftp.delete(root + recipe + '/IMMS0001')
    except:
        pass
    try:
        ftp.delete(root + recipe + '/IMMS0001.jpeg')
    except:
        pass
    try:
        ftp.delete(root + recipe + '/PRMP0001')
    except:
        pass
    try:
        ftp.delete(root + recipe + '/PRMS0001')
    except:
        pass
    try:
        ftp.delete(root + recipe + '/ms_temp.txt')
    except:
        pass
    try:
        ftp.rmd(root + recipe)
    except:
        pass
if __name__ == '__main__':
    pass    
    #DOWNLOAD_TEMPLATE()  #download all template        
    #MAIN_IDW_IDP_AMP() #download idw,idp,amp    
    #CD_SEM_Template_Analysis()    
    #TEMPLATE_LINKED()        
    #IDP_AMP_EXTRACTION()

    #Download_Modified_List()  #list of files revised yesterday
    #Modified_IDP_AMP_DOWNLOAD() #download idp/amp file
    #Modified_IDP_AMP_CHECK()    # check idp/amp file
    #TEMPLATE_DATE()  #check modification of template
    #FILE_OPERATION() # copy results of check
    
    AMP_CHECK()



