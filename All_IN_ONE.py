#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
 __title__ = ''
 __author__ = 'HUANGWEI45'
 __mtime__ = '2019/04/17'
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
from DSRW import *
from datetime import datetime
import shutil


f = open('P:/_script/script/log.txt','a')
f.write("\n\n===Start Time=== " + str(datetime.now())[:19]  )
if 1=='not executed':
    try:
        #shutil.copyfile('P:/database/Nikon.mdb', '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\nikon.mdb')

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
        NikonProductMetalImage().MainFunction()   #000025
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
        #CD_SEM_111_NEW().GOLDEN_AMP_CHECK()
        f.write('\n026_Golden_Amp_Check        _Succeeded(skipped)    ' + str(datetime.now())[:19])
    except:
        f.write('\n026_Golden_Amp_ChecK        _Failed       ' + str(datetime.now())[:19])

if 1=='TO BE FIXED':
    try:
        print('Running CDU_QC_IMAGE_CD().MainFunction()')
        CDU_QC_IMAGE_CD().MainFunction()          #000018
        f.write('\n010_CDU_QC_IMAGE_CD          _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n010_CDU_QC_IMAGE_CD          _Failed       ' + str(datetime.now())[:19])



    try:
        print('Running CDU_QC_IMAGE_PLOT().MainFunction() ')
        CDU_QC_IMAGE_PLOT().MainFunction()        #000019
        f.write('\n011_CDU_QC_IMAGE_PLOT        _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n011_CDU_QC_IMAGE_PLOT        _Failed       ' + str(datetime.now())[:19])


    try:
        print('Running NikonAdjust().MainFunction() ')
        NikonAdjust().MainFunction()              #000021
        f.write('\n013_NikonAdjust              _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n013_NikonAdjust              _Failed       ' + str(datetime.now())[:19])





if 'Running NikonRecipeDate().MainFunction() '=='Running NikonRecipeDate().MainFunction() ': #001
    try:
        NikonRecipeDate().MainFunction()
        f.write('\n001_NikonRecipeDate              _Succeeded    '+ str(datetime.now())[:19])
    except:
        f.write('\n001_NikonRecipeDate              _Failed       ' + str(datetime.now())[:19])

if 'Running NikonPara().MainFunction()'=='Running NikonPara().MainFunction()': #002
    try:
        NikonPara().MainFunction()
        f.write('\n002_NikonPara                    _Succeeded    ' + str(datetime.now())[:19])
    except:
        pass
        f.write('\n002_NikonPara                    _Failed       ' + str(datetime.now())[:19])

if 'Running NikonRecipeMaintain().MainFunction()'=='Running NikonRecipeMaintain().MainFunction()':
    try:
        NikonRecipeMaintain().MainFunction()      #000011-->xls file refreshed, MFG backlog read,PE list required
        f.write('\n003_NikonRecipeMaintain          _Succeeded   ' + str(datetime.now())[:19])
    except:
        f.write('\n003_NikonRecipeMaintain          _Failed      ' + str(datetime.now())[:19])
        pass

if 'CD_SEM_NO_111'=='CD_SEM_NO_111':
    try:
        CD_SEM_NO_111().MAINFUNCTION()          #00009
        f.write('\n004_CD_SEM_NO_111                _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n004_CD_SEM_NO_111                _Failed       ' + str(datetime.now())[:19])
        pass

if 'Running AsmlBatchReport().MainFunction()'=='Running AsmlBatchReport().MainFunction()':
    try:
        AsmlBatchReport_TAR().MainFunction()          #000012
        f.write('\n005_AsmlBatchReport              _Succeeded   ' + str(datetime.now())[:19])
    except:
        f.write('\n005_AsmlBatchReport              _Failed      ' + str(datetime.now())[:19])

if 'Running NikonVector_NEW().MainFunction()'=='Running NikonVector_NEW().MainFunction()':
    try:
        NikonVector_NEW().MainFunction()          #00009
        f.write('\n006_NikonVector                  _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n006_NikonVector                  _Failed       ' + str(datetime.now())[:19])
        pass

if 'Running ESF().MainFunction()  '=='Running ESF().MainFunction()  ':
    try:
        ESF().MainFunction()                      #000022 需要访问MFG数据库，其它电脑无法运行
        f.write('\n007_ESF                          _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n007_ESF                          _Failed       ' + str(datetime.now())[:19])

if 'Running SPC99().MainFunction()'=='Running SPC99().MainFunction()':
    try:
        SPC99().MainFunction()                    #000023 __init__ #000024 需要访问MFG数据库，其它电脑无法运行,
        f.write('\n008_SPC99                        _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n008_SPC99                        _Failed       ' + str(datetime.now())[:19])

if 'MovePpidExcel().MainFunction()'=='MovePpidExcel().MainFunction()':
    try:
        MovePpidExcel().MainFunction()
        f.write('\n009_MovePpidExcel                _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n009_MovePpidExcel                _Failed       ' + str(datetime.now())[:19])

if 'Running ASML MACHINE CONSTANTS'=='Running ASML MACHINE CONSTANTS':
    try:
        ASML_MC_CONS_LOG_SUMMARY().MainFunction()
        f.write('\n010_ASML_MC_CONST_LOG            _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n010_ASML_MC_CONST_LOG            _Failed       ' + str(datetime.now())[:19])

if 'Running ASML ERROR LOG'=='Running ASML ERROR LOG':
    try:
        ASML_ERROR_LOG_FOR_OPAS().MianFunction(path='//10.4.72.74/asml/_AsmlDownload/AsmlErrLog/')
        f.write('\n011_ASML_ERROR_LOG               _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n011_ASML_ERROR_LOG               _Failed       ' + str(datetime.now())[:19])

if 'Running R2R OPAS FLOW'=='Running R2R OPAS FLOW':
    try:
        MFG_FLOW_PPID_FOR_OPAS().MainFunction()
        f.write('\n012_R2R_OPAS_FLOW                _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n012_R2R_OPAS_FLOW                _Failed       ' + str(datetime.now())[:19])

if 'Running OVL CHECK NEW'=='Running OVL CHECK NEW':
    try:
        OvlCheckNew().MainFunction()
        f.write('\n013_OVL_CHECK_                   _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n013_OVL_CHECK                    _Failed       ' + str(datetime.now())[:19])

if 'Running OvlCheck().Refresh_PPID_FROM_EXCEL'=='Running OvlCheck().Refresh_PPID_FROM_EXCEL':
    try:
        tmp=str(datetime.now())[11:13]
        # if tmp>='20':
        OvlCheck().refresh_ppid_from_xls()
        f.write('\n014_Refresh_PPID                 _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n014_Refresh_PPID                 _Failed       ' + str(datetime.now())[:19])

if 'Running ESF CONSTRAINTS'=='Running ESF CONSTRAINTS':
    try:
        AVAILABLE_TOOL().ESF()
        f.write('\n015_ESF_Tool_Avail               _Succeeded    ' + str(datetime.now())[:19])
        shutil.copyfile('Z:/_DailyCheck/ESF/ESF_TOOL_AVAILABLE.csv',
                        '\\\\10.4.3.130\\ftpdata\\LITHO\\ExcelCsvFile\\ESF_TOOL_AVAILABLE.csv')
    except:
        f.write('\n015_ESF_Tool_Avail               _Failed       ' + str(datetime.now())[:19])

if 'Running AWE_ANALYSIS_NEW_TAR().MainFunction() '=='Running AWE_ANALYSIS_NEW_TAR().MainFunction() ':
    try:
        AWE_ANALYSIS_NEW_TAR().MainFunction()         #000020
        f.write('\n016_AWE_ANALYSIS                 _Succeeded    ' + str(datetime.now())[:19])
    except:
        f.write('\n016_AWE_ANALYSIS                 _Failed       ' + str(datetime.now())[:19])

f.write("\n===End Time=== " + str(datetime.now())[:19] +  "\n\n")
f.close()












