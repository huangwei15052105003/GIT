



# -*- coding: utf-8 -*-
"""
Created on Mon Nov 27 10:25:27 2017

@author: huangwei45
"""

import os
import sys
import ftplib
import datetime
import time
import zipfile
import threading



class ASML_DOWNLOAD:
    def __init__(self):
        pass
    def ftp(self,tool):
        host = {'08': '10.4.131.63', '7D': '10.4.131.32', '82': '10.4.152.29', '83': '10.4.151.64', '85': '10.4.152.37',
                '86': '10.4.152.31', '87': '10.4.152.46', '89': '10.4.152.39', '8A': '10.4.152.42', '8B': '10.4.152.47',
                '8C': '10.4.152.48'}
        user = {'08': 'sys.3527', '7D': 'sys.8111', '82': 'sys.4666', '83': 'sys.4730', '85': 'sys.6450',
                '86': 'sys.8144', '87': 'sys.4142', '89': 'sys.6158', '8A': 'sys.5688', '8B': 'sys.4955',
                '8C': 'sys.9726'}
        try:
            ftp = ftplib.FTP(host=host[tool], user=user[tool], passwd='litho')#, timeout=999)
            print("Remote Tool: %s Is Connected" % (tool))

        except:
            print('unable to ftp remote tool: %s'% (tool))
            ftp=None
        return ftp
    def Single_Tool(self,tool,folderflag,momentflag):

        idNo = {'08': 'sys.3527', '7D': 'sys.8111', '82': 'sys.4666', '83': 'sys.4730', '85': 'sys.6450',
                '86': 'sys.8144', '87': 'sys.4142', '89': 'sys.6158', '8A': 'sys.5688', '8B': 'sys.4955',
                '8C': 'sys.9726'}

        ftp = self.ftp(tool)


        if ftp != None:


            if 'batchReport'=='batchReport': #batch report
                print('Downloading BatchReport')
                print('==Downloading BatchReport')
                root = 'user_data/batch_reports/PROD/'

                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/user_data/batch_reports/PROD/'

                ftp.cwd(root)  # 远端FTP目录

                tmp = []
                ftp.dir(tmp.append)  # list folder and file in /Prod
                partname = [i.split()[8] for i in tmp if 'drwxr' in i][2:]  # list folder only
                print(partname)
                print(len(partname))

                for n,part in enumerate(partname):
                    os.chdir('Z:\\ASMLbatch\\' + tool)  # 切换到本地下载目录
                    if not (os.path.exists(part)):
                        os.mkdir(part)  # create local part directory

                    ftp.cwd(part)  # @ remote part directory

                    tmp = []
                    ftp.dir(tmp.append)
                    layer = [i.split()[8] for i in tmp if 'drwxr' in i][2:]  # list folder only
                    print(tool,n,len(partname),layer)

                    if len(layer) > 0:  # layer exists
                        for layername in layer:
                            os.chdir('Z:\\ASMLbatch\\' + tool + '\\' + part)  # @ local part directory
                            if not (os.path.exists(layername)):
                                os.mkdir(layername)  # @ create local layer directory

                            ftp.cwd(layername)  # @ remote layer directory
                            os.chdir('Z:\\ASMLbatch\\' + tool + '\\' + part + '\\' + layername)  # @ local layer directory

                            filelist = ftp.nlst()
                            if len(filelist) > 0:

                                for filename in filelist:
                                    (print(part,layername,filename))

                                    if (os.path.isfile(filename)):
                                        newfilename = time.strftime("%Y-%m-%d_%H-%M-%S", time.localtime()) + '_' + filename
                                        fp = open(newfilename, 'wb')

                                        try:  # in case it is a folder, not a file
                                            ftp.retrbinary('RETR ' + filename, fp.write)
                                        except:
                                            pass

                                        if (os.path.isfile(newfilename)):  # to confirm copy is done, then delete remote file

                                            if not ('mom' in filename):
                                                try:
                                                    ftp.delete(filename)
                                                    print(tool, '  ', part, '   ', layername, '   ', filename,
                                                          '   renamed ,downloaded and remote file deleted')
                                                except:
                                                    pass
                                            else:
                                                if momentflag==True:
                                                    try:
                                                        ftp.delete(filename)
                                                        print(tool, '  ', part, '   ', layername, '   ', filename,
                                                              '   renamed ,downloaded and remote file deleted')
                                                    except:
                                                        pass

                                    else:
                                        fp = open(filename, 'wb')

                                        try:  # in case it is a folder, not a file
                                            ftp.retrbinary('RETR ' + filename, fp.write)
                                        except:
                                            pass

                                        if (os.path.isfile(filename)):  # to confirm copy is done, then delete remote file

                                            if not ('mom' in filename):
                                                try:
                                                    ftp.delete(filename)
                                                    print(tool, '  ', part, '   ', layername, '   ', filename,
                                                          '   downloaded and remote file deleted')
                                                except:
                                                    pass
                                            else:
                                                if momentflag==True:
                                                    try:
                                                        ftp.delete(filename)
                                                        print(tool, '  ', part, '   ', layername, '   ', filename,
                                                              '   renamed ,downloaded and remote file deleted')
                                                    except:
                                                        pass

                            ftp.cwd('..')  # back to  part directory
                            if folderflag==True:
                                try:
                                   ftp.rmd(layername)
                                except:
                                   pass

                    ftp.cwd('..')  # back to root（PROD） directory
                    if folderflag==True:
                        try:
                           ftp.rmd(part)
                        except:
                           pass
#================================================================================================================================================
            if 'batchAlignReport'=='batchAlignReport':
                print('Downloading Batch Alignment Report')
                root = 'user_data/batch_alignment_reports/PROD/'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/user_data/batch_alignment_reports/PROD/'

                try:
                    ftp.cwd(root)  # 远端FTP目录

                    tmp = []
                    ftp.dir(tmp.append)  # list folder and file in /Prod
                    partname = [i.split()[8] for i in tmp if 'drwxr' in i][2:]  # list folder only
                    # print(partname)

                    for part in partname:
                        os.chdir('Z:\\BatchAlignmentReport\\RawData\\' + tool)  # 切换到本地下载目录
                        if not (os.path.exists(part)):
                            os.mkdir(part)  # create local part directory

                        ftp.cwd(part)  # @ remote part directory

                        tmp = []
                        ftp.dir(tmp.append)
                        layer = [i.split()[8] for i in tmp if 'drwxr' in i][2:]  # list folder only
                        # print(layer)

                        if len(layer) > 0:  # layer exists
                            for layername in layer:
                                os.chdir(
                                    'Z:\\BatchAlignmentReport\\RawData\\' + tool + '\\' + part)  # @ local part directory
                                if not (os.path.exists(layername)):
                                    os.mkdir(layername)  # @ create local layer directory

                                ftp.cwd(layername)  # @ remote layer directory
                                os.chdir(
                                    'Z:\\BatchAlignmentReport\\RawData\\' + tool + '\\' + part + '\\' + layername)  # @ local layer directory

                                filelist = ftp.nlst()
                                if len(filelist) > 0:

                                    for filename in filelist:

                                        if (os.path.isfile(filename)):
                                            newfilename = time.strftime("%Y-%m-%d_%H-%M-%S",
                                                                        time.localtime()) + '_' + filename
                                            fp = open(newfilename, 'wb')
                                            try:
                                                ftp.retrbinary('RETR ' + filename, fp.write)
                                            except:
                                                pass

                                            if (os.path.isfile(
                                                    newfilename)):  # to confirm copy is done, then delete remote file

                                                try:
                                                    ftp.delete(filename)
                                                    print(tool, '  ', part, '   ', layername, '   ', filename,
                                                          '   renamed ,downloaded and remote file deleted')
                                                except:
                                                    pass
                                        else:
                                            fp = open(filename, 'wb')
                                            try:
                                                ftp.retrbinary('RETR ' + filename, fp.write)
                                            except:
                                                pass
                                            if (os.path.isfile(
                                                    filename)):  # to confirm copy is done, then delete remote file

                                                try:
                                                    ftp.delete(filename)
                                                    print(tool, '  ', part, '   ', layername, '   ', filename,
                                                          '   downloaded and remote file deleted')
                                                except:
                                                    pass

                                ftp.cwd('..')  # back to  part directory  # try:  #    ftp.rmd(layername)  # except:  #    pass

                        ftp.cwd('..')  # back to root（PROD） directory  # try:  #    ftp.rmd(part)  # except:  #    pass

                    # ftp.close()
                except:
                    pass
                    # ftp.close()
# ================================================================================================================================================
            if 'AWE'=='AWE':
                print('Downloading AWE')
                fullname = []
                root = 'user_data/AW/PROD/'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/user_data/AW/PROD/'


                try:
                    ftp.cwd(root)  # 远端FTP目录

                    tmp = []
                    ftp.dir(tmp.append)  # list folder and file in /Prod
                    partname = [i.split()[8] for i in tmp if 'drwxr' in i][2:]  # list folder only
                    # print(partname)

                    for part in partname:

                        os.chdir('Z:\\AsmlAweFile\\RawData\\' + tool)  # 切换到本地下载目录
                        if not (os.path.exists(part)):
                            os.mkdir(part)  # create local part directory

                        ftp.cwd(part)  # @ remote part directory

                        tmp1 = []
                        ftp.dir(tmp1.append)
                        layer = [i.split()[8] for i in tmp1 if 'drwxr' in i][2:]  # list folder only
                        # print(layer)

                        if len(layer) > 0:  # layer exists
                            for layername in layer:
                                os.chdir('Z:\\AsmlAweFile\\RawData\\' + tool + '\\' + part)  # @ local part directory
                                if not (os.path.exists(layername)):
                                    os.mkdir(layername)  # @ create local layer directory

                                ftp.cwd(layername)  # @ remote layer directory
                                os.chdir(
                                    'Z:\\AsmlAweFile\\RawData\\' + tool + '\\' + part + '\\' + layername)  # @ local layer directory

                                filelist = ftp.nlst()
                                if len(filelist) > 0:

                                    for filename in filelist:

                                        if (os.path.isfile(filename)):
                                            newfilename = time.strftime("%Y-%m-%d_%H-%M-%S",
                                                                        time.localtime()) + '_' + filename
                                            fp = open(newfilename, 'wb')
                                            try:
                                                ftp.retrbinary('RETR ' + filename, fp.write)
                                            except:
                                                pass
                                            if (os.path.isfile(
                                                    newfilename)):  # to confirm copy is done, then delete remote file

                                                fullname.append(os.getcwd() + '\\' + newfilename)

                                                # zip = zipfile.ZipFile(os.getcwd() + '\\' + newfilename +'.zip','w',zipfile.ZIP_DEFLATED)
                                                # zip.write(os.getcwd() + '\\' + newfilename )
                                                # zip.close()

                                                # zip file can be opened in notebook, but corrupted in test manager

                                                try:
                                                    ftp.delete(filename)
                                                    print(tool, '  ', part, '   ', layername, '   ', filename,
                                                          '   renamed ,downloaded and remote file deleted')
                                                except:
                                                    pass
                                        else:
                                            fp = open(filename, 'wb')
                                            try:
                                                ftp.retrbinary('RETR ' + filename, fp.write)
                                            except:
                                                pass
                                            if (os.path.isfile(
                                                    filename)):  # to confirm copy is done, then delete remote file

                                                fullname.append(os.getcwd() + '\\' + filename)

                                                # zip = zipfile.ZipFile(os.getcwd() + '\\' + filename +'.zip','w',zipfile.ZIP_DEFLATED)
                                                # zip.write(os.getcwd() + '\\' + filename )
                                                # zip.close()

                                                # zip file can be opened in notebook, but corrupted in test manager

                                                # str1 = 'P:\\huangwei\\haozip\\haozipc a ' + os.getcwd() + '\\' + filename +'.zip' + ' ' + os.getcwd() + '\\' + filename
                                                # print(str1)
                                                # os.system(str1)  -->unable to execute it, but ok with DOS commend

                                                try:
                                                    ftp.delete(filename)
                                                    print(tool, '  ', part, '   ', layername, '   ', filename,
                                                          '   downloaded and remote file deleted')
                                                except:
                                                    pass

                                ftp.cwd(
                                    '..')  # back to  part directory  # try:  #    ftp.rmd(layername)  # except:  #    pass

                        ftp.cwd('..')  # back to root（PROD） directory  # try:  #    ftp.rmd(part)  # except:  #    pass


                except:
                    pass

# ================================================================================================================================================
            if 'Matching'=='Matching':
                print('Downloading Matching')
                matchinglog = []

                root = 'TM/MetrologyCalibration/MachineMatching/Intrafield/XYMO.log'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/.TM/MetrologyCalibration/MachineMatching/Intrafield/XYMO.log/'
                ftp.cwd(root)
                ftp.dir(matchinglog.append)
                matchinglog = [i.split()[8] for i in matchinglog if '-rw-' in i]
                matchinglog = [i for i in matchinglog if not ('tgs' in i)]
                os.chdir('Z:\\AsmlTM\\AsmlMachingMatchingLog\\' + tool)

                for filename in matchinglog:
                    if not (os.path.isfile(filename)):
                        fp = open(filename, 'wb')
                        ftp.retrbinary('RETR ' + filename, fp.write)

                print(tool + ' matching log downloaded')
# ================================================================================================================================================
            if 'OrtScaling'=='OrtScaling':
                print('Downloading OrtScaling')
                AsmlNonOrthogonalityAndScalinglog = []

                root = 'TM/MetrologyCalibration/InitialSetup/StageGrid/WaferStage/NonOrthogonalityandScaling/GROO.log'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/.TM/MetrologyCalibration/InitialSetup/StageGrid/WaferStage/NonOrthogonalityandScaling/GROO.log'
                ftp.cwd(root)
                ftp.dir(AsmlNonOrthogonalityAndScalinglog.append)
                AsmlNonOrthogonalityAndScalinglog = [i.split()[8] for i in AsmlNonOrthogonalityAndScalinglog if
                                                     '-rw-' in i]
                AsmlNonOrthogonalityAndScalinglog = [i for i in AsmlNonOrthogonalityAndScalinglog if not ('tgs' in i)]

                os.chdir('Z:\\AsmlTM\\AsmlNonOrthogonalityAndScaling\\' + tool)

                for filename in AsmlNonOrthogonalityAndScalinglog:
                    if not (os.path.isfile(filename)):
                        fp = open(filename, 'wb')
                        ftp.retrbinary('RETR ' + filename, fp.write)
                        print(filename + ' downloading.....')
                print(tool + ' NonOrthogonalityAndScaling log downloaded')
# ================================================================================================================================================
            if 'SBO'=='SBO':
                print('Downloading SBO')
                AsmlNonOrthogonalityAndScalinglog = []

                root = 'TM/Alignment/OffAxis/Calibration/LASOOASC.log'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/.TM/Alignment/OffAxis/Calibration/LASOOASC.log'
                ftp.cwd(root)
                ftp.dir(AsmlNonOrthogonalityAndScalinglog.append)
                AsmlNonOrthogonalityAndScalinglog = [i.split()[8] for i in AsmlNonOrthogonalityAndScalinglog if
                                                     '-rw-' in i]
                AsmlNonOrthogonalityAndScalinglog = [i for i in AsmlNonOrthogonalityAndScalinglog if not ('tgs' in i)]

                os.chdir('Z:\\AsmlTM\\ShiftBewteenOrders\\' + tool)

                for filename in AsmlNonOrthogonalityAndScalinglog:
                    if not (os.path.isfile(filename)):
                        fp = open(filename, 'wb')
                        ftp.retrbinary('RETR ' + filename, fp.write)
                        print(filename + ' downloading.....')
                print(tool + ' ShiftBewteenOrders log downloaded')
# ================================================================================================================================================
            if 'dynamic'=='dynamic':
                print('Downloading Dynamic')
                AsmlDynamicXYLog = []
                root = 'TM/MetrologyCalibration/DynamicSetup/XYPlane/XYDO.log'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/.TM/MetrologyCalibration/DynamicSetup/XYPlane/XYDO.log'
                ftp.cwd(root)
                ftp.dir(AsmlDynamicXYLog.append)
                AsmlDynamicXYLog = [i.split()[8] for i in AsmlDynamicXYLog if '-rw-' in i]
                AsmlDynamicXYLog = [i for i in AsmlDynamicXYLog if not ('tgs' in i)]

                os.chdir('Z:\\AsmlTM\\DynamicXY\\' + tool)

                for filename in AsmlDynamicXYLog:
                    if not (os.path.isfile(filename)):
                        fp = open(filename, 'wb')
                        ftp.retrbinary('RETR ' + filename, fp.write)
                        print(filename + ' downloading.....')
                print(tool + ' DynamicXY log downloaded')
# ================================================================================================================================================
            if 'errorLog'=='errorLog':
                print('Downloading Error Log')
                pass
                errLog=[]
                root = 'ER/'
                root = '/usr/asm/data.' + idNo[tool].split('.')[1] + '/.ER/'

                ftp.cwd(root)
                ftp.dir(errLog.append)
                errLog = [i.split()[8] for i in errLog if '-rw-' in i]
                errLog = [ i for i in errLog if
                           i=='ER_event_log'
                           or i=='ER_event_log.old'
                           or ('_ER_' in i and '.gz' in i) ]
                os.chdir('Z:/AsmlErrLog/' + tool)

                old = os.listdir('Z:/AsmlErrLog/' + tool)
                print(errLog)
                errLog=list(set(errLog)-set(old))
                errLog.extend(['ER_event_log','ER_event_log.old'])
                errLog = list(set(errLog))
                print(errLog)



                for filename in errLog:
                    fp = open(filename, 'wb')
                    ftp.retrbinary('RETR ' + filename, fp.write)
                    print ("%s is beding downloaded" %(filename))

            if 'PreAlign'=='PreAlign':
                print('Downloading Wafer Handling')
                tmp=[]
                root = '/usr/asm/sys.' + idNo[tool].split('.')[1] + '/WH/'
                ftp.cwd(root)
                os.chdir('Z:/AsmlSysData/WH/' + tool)
                fp = open('WH_Machine_Const_'+ (str(datetime.datetime.now())[0:13]),'wb')
                ftp.retrbinary('RETR WH_machine.const.txt'  , fp.write)

            if 'Coordinate'=='Coordinate':
                print('Downloading Coordinate')
                tmp=[]
                root = '/usr/asm/sys.' + idNo[tool].split('.')[1] + '/CD/'
                ftp.cwd(root)
                os.chdir('Z:/AsmlSysData/CD/' + tool)
                fp = open('CD_Machine_Const_'+ (str(datetime.datetime.now())[0:13]),'wb')
                ftp.retrbinary('RETR CD_machine.const.txt'  , fp.write)

            if 'IL'=='IL':
                print('Downloading IL')
                tmp=[]
                root = '/usr/asm/sys.' + idNo[tool].split('.')[1] + '/IL/'
                ftp.cwd(root)
                os.chdir('Z:/AsmlSysData/IL/' + tool)
                fp = open('IL_Machine_Const_'+ (str(datetime.datetime.now())[0:13]),'wb')
                ftp.retrbinary('RETR IL_machine.const.txt'  , fp.write)

            if 'MC_event_log'=='MC_event_log':
                print('Downloading MC_event_log')
                root = '/usr/asm/sys.' + idNo[tool].split('.')[1] + '/MC/'
                ftp.cwd(root)
                os.chdir('Z:/AsmlSysData/MC')
                fp = open('MC_Machine_Const_' + tool, 'wb')
                ftp.retrbinary('RETR MC_event_log', fp.write)
                fp = open('MC_Machine_Const_old_' + tool, 'wb')
                ftp.retrbinary('RETR MC_event_log.old', fp.write)





#================================================================================================================================================
            print('ALSD%s,completed' %(tool))
            ftp.close()









if __name__ == "__main__":

    t1=datetime.datetime.now()
    folderflag=False
    momentflag=False

    # if 1==2:
    #     try:
    #         t0= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('82',folderflag,momentflag))
    #         t1= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('83',folderflag,momentflag))
    #         t2= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('85',folderflag,momentflag))
    #         t3= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('86',folderflag,momentflag))
    #         t4= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('87',folderflag,momentflag))
    #         t5= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('89',folderflag,momentflag))
    #         t6= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('8A',folderflag,momentflag))
    #         t7= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('8B',folderflag,momentflag))
    #         t8= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('8C',folderflag,momentflag))
    #         t9= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('7D',folderflag,momentflag))
    #         t10= threading.Thread(target=ASML_DOWNLOAD().Single_Tool,args=('08',folderflag,momentflag))
    #
    #         t0.start()
    #         t1.start()
    #         t2.start()
    #         t3.start()
    #         t4.start()
    #         t5.start()
    #         t6.start()
    #         t7.start()
    #         t8.start()
    #         t9.start()
    #         t10.start()
    #
    #         t0.join()
    #         t1.join()
    #         t2.join()
    #         t3.join()
    #         t4.join()
    #         t5.join()
    #         t6.join()
    #         t7.join()
    #         t8.join()
    #         t9.join()
    #         t10.join()
    #     except:
    #         pass

    if 1==1:
        for tool in ['82','83','85','86','87','89','8A','8B','8C','7D','08']:
        # for tool in ['82']:
            ASML_DOWNLOAD().Single_Tool(tool,folderflag,momentflag)

    t2 = datetime.datetime.now()
    print(str(t2-t1))