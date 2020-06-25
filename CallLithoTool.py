# -*- coding: utf-8 -*-
import numpy

# from matplotlib.gridspec import GridSpec
# import matplotlib.pyplot as plt
from PyQt5.QtWidgets import *
import win32com.client
from PyQt5.QtCore import *

import gc
import numpy as np

from GUI_LithoTool import *
from PyQt5.QtGui  import *
import os,sys,xlrd,xlwt
import pandas as pd
import math
# import matplotlib.ticker as ticker

#TODO  content of excel macro -->MDB;

#below for wafer map
from matplotlib.patches import Circle, Rectangle,Polygon
import matplotlib.pyplot as plt
from math import sqrt as sqrt



sDateTime = QDateTime.currentDateTime().addDays(-30).toString("yyyy-MM-dd HH:mm:ss")
eDateTime = QDateTime.currentDateTime().toString("yyyy-MM-dd HH:mm:ss")
def DF2TABLE(table,df):
    try:  # 显示数据在表格中
        table.clear()
        table.setColumnCount(df.shape[1])
        table.setRowCount(df.shape[0])
        table.setHorizontalHeaderLabels(df.columns)

        # self.table.setEditTriggers(QTableWidget.NoEditTriggers)#单元格不可编辑
        # self.table.setSelectionBehavior(QTableWidget.SelectRows)  #选中列还是行，这里设置选中行
        # self.table.setSelectionMode(QTableWidget.SingleSelection) #只能选中一行或者一列
        # self.table.horizontalHeader().setStretchLastSection(True)  #列宽度占满表格(最后一个列拉伸处理沾满表格)
        # newItem.setForeground(QBrush(QColor(255, 0, 0)))
        for i in range(df.shape[0]):
            for j in range(df.shape[1]):
                newItem = QTableWidgetItem(str(df.iloc[i, j]))
                newItem.setTextAlignment(Qt.AlignCenter)
                if i % 2 == 1:
                    newItem.setBackground(QColor('lightcyan'))
                    # newItem.setBackground(QColor('Lime'))
                table.setItem(i, j, newItem)
    except:
        reply = QMessageBox.information(self, "注意", 'xls表格显示异常', QMessageBox.Yes, QMessageBox.Yes)
def MATPLOT(XY,items,flag):
    # print('001', XY.shape, flag)
    if flag:
        x1 = list(XY[5])
        x = [i for i in range(0, len(x1), math.ceil(XY.shape[0] / 50))]
        fig = plt.figure()
        gs = GridSpec(15,1)
        ax1 = plt.subplot(gs[:11,0])
        # ax1.plot(x1,list(XY[1]), color='red', linestyle=':', marker='*',label=items[0],MarkerSize=4,alpha=0.8,lw=0.8)
        ax1.plot(x1,list(XY[1]), color='red', linestyle=':', marker='*',label=items[0],MarkerSize=4,alpha=0.8,lw=0.8)
        # ax1.plot(x1,list(XY[2]), color='yellow', linestyle=':', marker='d',label=items[1],MarkerSize=4,alpha=0.2,lw=0.8)
        ax1.plot(x1,list(XY[2]), color='yellow', linestyle=':', marker='d',label=items[1],MarkerSize=4,alpha=0.8,lw=0.8)
        # ax1.plot(x1,list(XY[3]), color='blue', linestyle=':', marker='o',label=items[2],MarkerSize=4,alpha=0.8,lw=0.8)
        ax1.plot(x1,list(XY[3]), color='blue', linestyle=':', marker='o',label=items[2],MarkerSize=4,alpha=0.8,lw=0.8)
        # ax1.plot(x1,list(XY[4]), color='green', linestyle=':', marker='+',label=items[3],MarkerSize=4,alpha=0.8,lw=0.8)
        ax1.plot(x1,list(XY[4]), color='green', linestyle=':', marker='+',label=items[3],MarkerSize=4,alpha=0.8,lw=0.8)
        plt.xticks(x, x1[::math.ceil(XY.shape[0] / 50)], rotation=90)
        plt.grid(color="grey")
        plt.legend(loc='upper right', fancybox=True, framealpha=0.1, ncol=4)
        plt.margins(0, 0)

        ax2 = plt.subplot(gs[12:13, 0])
        ax2.plot(x1,[0 for i in range(len(x1))])
        plt.xticks(x, list(XY[6])[::math.ceil(XY.shape[0] / 50)], rotation=90)
        ax2.spines['right'].set_color('none')
        ax2.spines['left'].set_color('none')
        ax2.yaxis.set_major_locator(ticker.NullLocator())
        ax2.spines['top'].set_color('none')
        plt.margins(0, 0)


        ax3 = plt.subplot(gs[13:14, 0])
        ax3.plot(x1, [0 for i in range(len(x1))])
        plt.xticks(x, list(XY[7])[::math.ceil(XY.shape[0] / 50)], rotation=90)
        ax3.spines['right'].set_color('none')
        ax3.spines['left'].set_color('none')
        ax3.yaxis.set_major_locator(ticker.NullLocator())
        ax3.spines['top'].set_color('none')
        plt.margins(0, 0)

        # fig.subplots_adjust(hspace=3)

        fig.tight_layout()
        plt.show()
    else:#CD
        # print(XY.dtypes)
        XY[['Jobin', 'Feedback', 'Optimum',
         'CD_target', 'Met_Avg', '1', '2', '3', '4', '5', '6', '7', '8', '9']] \
            = XY[['Jobin', 'Feedback', 'Optimum',
         'CD_target', 'Met_Avg', '1', '2', '3', '4', '5', '6', '7', '8', '9']].astype(float)
        x1 = list(XY['Dcoll_Time'])
        x = [i for i in range(0, len(x1), math.ceil(XY.shape[0] / 50))]

        fig = plt.figure()
        gs = GridSpec(15, 1)
        ax1 = plt.subplot(gs[0:12, 0])
        ax1.plot(x1,list(XY['Jobin']),linewidth=1, color='black', linestyle='-',
                 marker='D',MarkerSize=5,label='Jobin')
        ax1.plot(x1, list(XY['Optimum']), linewidth=1, color='purple', linestyle='-',
                 marker='o',MarkerSize=5, label='Optimum')
        # ax1.plot(x1, list(XY['Feedback']), linewidth=0.5, color='yellow', linestyle=':', marker='o', MarkerSize=4,
        #          label='Optimum',alpha=0.5)#--> do not plot, in case of zero value
        plt.xticks(x, x1[::math.ceil(XY.shape[0] / 50)], rotation=90)
        ax1.set_ylim(XY['Jobin'].min()*0.95,XY['Jobin'].max()*1.05)
        plt.grid(color="grey")
        plt.legend(loc='upper left', fancybox=True, framealpha=0.1, ncol=2)
        plt.margins(0, 0)

        ax2 = ax1.twinx()
        ax2.plot(x1, XY['Met_Avg'],lw=1, color='red',linestyle=':', marker='*', MarkerSize=4, label='Avg')
        ax2.plot(x1, list(XY[['1', '2', '3', '4', '5', '6', '7', '8', '9']].T.max()),lw=0.5,color='blue',
                 linestyle=':',marker='+',MarkerSize=4,label='Max')
        ax2.plot(x1, list(XY[['1', '2', '3', '4', '5', '6', '7', '8', '9']].T.min()), lw=0.5, color='green',
                 linestyle=':', marker='x', MarkerSize=4, label='Min')

        plt.xticks(x, x1[::math.ceil(XY.shape[0] / 50)], rotation=90)
        plt.legend(loc='upper right', fancybox=True, framealpha=0.1, ncol=3)
        plt.margins(0, 0)

        ax3 = plt.subplot(gs[13:14, 0])
        ax3.plot(x1, [0 for i in range(len(x1))])
        plt.xticks(x, list(XY['Proc_EqID'])[::math.ceil(XY.shape[0] / 50)], rotation=90)
        ax3.spines['right'].set_color('none')
        ax3.spines['left'].set_color('none')
        ax3.yaxis.set_major_locator(ticker.NullLocator())
        ax3.spines['top'].set_color('none')
        plt.margins(0, 0)

        ax4 = plt.subplot(gs[14:, 0])
        ax4.plot(x1, [0 for i in range(len(x1))])
        plt.xticks(x, list(XY['LotID'])[::math.ceil(XY.shape[0] / 50)], rotation=90)
        ax4.spines['right'].set_color('none')
        ax4.spines['left'].set_color('none')
        ax4.yaxis.set_major_locator(ticker.NullLocator())
        ax4.spines['top'].set_color('none')
        plt.margins(0, 0)

        # fig.subplots_adjust(hspace=-33)
        fig.tight_layout()
        plt.show()


        # ax2 = ax1.twinx()
        # ax2.boxplot(XY[['1', '2', '3', '4', '5', '6', '7', '8', '9']])
        # plt.xticks(x, x1[::math.ceil(XY.shape[0] / 50)], rotation=90)
        # plt.margins(0, 0)

        # ax1.boxplot(XY[['1', '2', '3', '4', '5', '6', '7', '8', '9']])
        # plt.xticks(x, x1[::math.ceil(XY.shape[0] / 50)], rotation=90)
        # plt.margins(0, 0)
        # ax2 = ax1.twinx()
        # ax2.plot(ax1.get_xticks(), list(XY['Jobin']),color='red',linewidth=1)# linewidth='0.5', color='green', linestyle=':', marker='^',
                 # label='Jobin')
def CONNECT_DB(dbName,sql):
    conn = win32com.client.Dispatch(r"ADODB.Connection")
    DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + dbName
    conn.Open(DSN)
    rs = win32com.client.Dispatch(r'ADODB.Recordset')
    rs.Open(sql, conn, 1, 3)
    tmp=[]
    if rs.RecordCount == 0:
        return pd.DataFrame()
    else:
        if rs.RecordCount > 1:
        # print (rs.RecordCount,rs.fields.count)
            tmp2=[]
            for i in range(rs.fields.count):
                tmp2.append(rs.fields(i).name)

            for i in range(rs.RecordCount):
                tmp1=[]
                for j in range(rs.fields.count):
                    if j in [5,7]:
                        tmp1.append(str(rs.fields(j).value)[:-6])
                    else:
                        tmp1.append(rs.fields(j).value)
                tmp.append(tmp1)
                rs.MoveNext()
    rs,conn=None,None
    return pd.DataFrame(tmp,columns=tmp2)
def UPDATE_TECH(partid,fulltech):
    partid=partid.upper().strip()
    fulltech=fulltech.upper().strip()
    conn = win32com.client.Dispatch(r"ADODB.Connection")
    DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
    conn.Open(DSN)
    rs = win32com.client.Dispatch(r'ADODB.Recordset')
    sql = "select partid from TECH where partid like '%" + partid + "%'"
    rs.Open(sql, conn, 1, 3)
    if rs.RecordCount == 0:
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "insert into TECH (PartID,FullTech) values ('" + partid + "','" + fulltech + "')"
        conn.Execute(sql)
    else:
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        sql = "update TECH set FullTech = '" + fulltech + "' where PartID ='" + partid + "'"
        conn.Execute(sql)

    conn.close
class CD_Maintain:
    def __init__(self):
        pass
    def openDB(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        return conn
    def idpQuery(self,dbtable,begin,end):
        conn = self.openDB()
        sql = "select * from "+ dbtable + " WHERE FLAG1='Revised' AND Conclusion=False "
        sql = sql + " AND DATE BETWEEN '" + begin + "' AND '" + end + "'"
        sql = sql + ' ORDER BY ID'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount>0:
            tmp1 = [ rs.fields(i).name for i in range(rs.fields.count)]
            tmp2=[]
            for j in range(rs.recordcount):
                tmp2.append( [ rs.fields(i).value for i in range(rs.fields.count)])
                rs.movenext
            return pd.DataFrame(tmp2,columns=tmp1)
        else:
            return pd.DataFrame()
        conn.close
    def ampQuery(self,dbtable,begin,end):
        print(dbtable,begin,end)
        conn = self.openDB()
        sql = "select ID,IDW,IDP,Date,Flag1,Reviewed,ReviewDate,KeepWrong,IsTemplate,Tech,Type,AutoCheck,Conclusion,EmployeeID from "+ dbtable + " WHERE "
        # sql = sql + " ( AutoCheck is null or AutoCheck <> 'Correct' ) "
        # sql = sql + " ( AutoCheck is null or AutoCheck <> 'Correct' ) and (Conclusion is null or Conclusion<>'正确')"
        sql = sql + " (  AutoCheck = 'Wrong' ) and (Conclusion is null or Conclusion<>'正确')"
        sql = sql + " AND DATE BETWEEN '" + begin + "' AND '" + end + "' order by id"
        # sql = sql + " AND DATE >'20190102'"
        # sql = sql + "  order by id"
        # sql ="select ID,IDW,IDP,Date,Flag1,Reviewed,ReviewDate,KeepWrong,IsTemplate,Tech,Type,AutoCheck,Conclusion from AMP where (AutoCheck is null or AutoCheck <> 'Correct') AND DATE BETWEEN '20190302' AND '20190401'"
        print(sql)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount>0:
            tmp1 = [ rs.fields(i).name for i in range(rs.fields.count)]
            tmp2=[]
            for j in range(rs.recordcount):
                tmp2.append( [ rs.fields(i).value for i in range(rs.fields.count)])
                rs.movenext
            return pd.DataFrame(tmp2,columns=tmp1)
        else:
            return pd.DataFrame()
        conn.close
    def ampUpdate1(self,ref,refCol,amp,Tech,cdType):
        print(cdType)
        if Tech[1] == '1':
            if amp['IDP'][0][:2] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT','TT']:
                x = ref[ref['Golden'].str.contains('018METAL')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            elif amp['IDP'][0][:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:
                x = ref[ref['Golden'].str.contains('018HOLE')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            elif amp['IDP'][0][:2] in ['GT', 'PC']:
                x = ref[ref['Golden'].str.contains('018GT')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            elif amp['IDP'][0][:2] in ['TO']:
                x = ref[ref['Golden'].str.contains('018TO')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            elif amp['IDP'][0][:2] in ['P0']:
                x = ref[ref['Golden'].str.contains('018TO')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            else:
                if cdType == 'Line':
                    x = ref[ref['Golden'].str.contains('018LINE')][refCol].reset_index().drop('index', axis=1).T
                    x = x[x[0] != 'None'].T
                elif cdType == 'Hole/Space':
                    x = ref[ref['Golden'].str.contains('018SPACE')][refCol].reset_index().drop('index',
                                                                                               axis=1).T
                    x = x[x[0] != 'None'].T
                else:
                    x = pd.DataFrame()
        else:
            if amp['IDP'][0][:2] in ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'AT', 'TT']:
                x = ref[ref['Golden'].str.contains('035METAL')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            elif amp['IDP'][0][:2] in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:
                x = ref[ref['Golden'].str.contains('035HOLE')][refCol].reset_index().drop('index', axis=1).T
                x = x[x[0] != 'None'].T
            else:
                if cdType == 'Line':
                    x = ref[ref['Golden'].str.contains('035LINE')][refCol].reset_index().drop('index', axis=1).T
                    x = x[x[0] != 'None'].T
                elif cdType == 'Hole/Space':
                    x = ref[ref['Golden'].str.contains('035SPACE')][refCol].reset_index().drop('index',
                                                                                               axis=1).T
                    x = x[x[0] != 'None'].T
                else:
                    x = pd.DataFrame()
        if x.shape[1] > 1:
            y = amp[x.columns]
            x1=[str(i) for i in (x.loc[0])]
            y1=[str(i) for i in (y.loc[0])]
            # if False in list((x == y).loc[0]):
            if x1!=y1:
                AutoCheck = 'Wrong'
            else:
                AutoCheck = 'Correct'
        else:
            AutoCheck = 'Pending'
        amp['AutoCheck']=AutoCheck
        amp['Type'] = cdType
        amp['Tech'] = Tech
        print(AutoCheck,cdType,Tech)

        tmp = ['ID', 'AutoCheck','TOOL', 'IDW', 'IDP', 'Date', 'Flag1', 'PPID', 'Tech', 'Type', 'Conclusion']
        tmp = ['ID', 'AutoCheck','TOOL', 'IDW', 'IDP', 'Date', 'Flag1',  'Tech', 'Type', 'Conclusion']
        amp = pd.concat([amp,x])
        amp=amp.fillna('')
        tmp.extend(x.columns)
        amp=amp[tmp]


        return AutoCheck,amp
    def cdAllinone(self):
        summary = pd.DataFrame(columns=['TECH','IDW','LAYER','CATEGORY'])
        conn = CD_Maintain().openDB()
        sql1 = "select *,mid(Tech,2,1) as category,mid(IDP,1,2) as layer from AMP where PPID=True and Tech is not null and Tech<>''"

        sql2 = "select Tech,IDW,layer from (" + sql1 + ")"
        sql = sql2 + " where ( layer like 'A%' or layer in ('W1','W2','W3','W4','W5','W6','W7','WT','TT','T1','T2')) "
        sql = sql + " and method=0 "


        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp1=[]
        for  i in range(rs.recordcount):
            tmp1.append([rs.fields(0).value,rs.fields(1).value,rs.fields(2).value])
            rs.movenext
        tmp1=pd.DataFrame(tmp1,columns=['TECH','IDW','LAYER'])
        tmp1['CATEGORY']='Threshold-->Linear'
        summary = pd.concat([summary,tmp1])


        sql = sql2 + " where not ( layer like 'A%' or layer in ('W1','W2','W3','W4','W5','W6','W7','WT','TT','T1','T2')) "
        sql = sql + " and method=1 and category='1'"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp1 = []
        for i in range(rs.recordcount):
            tmp1.append([rs.fields(0).value, rs.fields(1).value,rs.fields(2).value])
            rs.movenext
        tmp1 = pd.DataFrame(tmp1, columns=['TECH','IDW', 'LAYER'])
        tmp1['CATEGORY'] = 'Advanced:Linear-->Threshold'
        summary = pd.concat([summary, tmp1])

        sql = sql2 + " where not ( layer like 'A%' or layer in ('W1','W2','W3','W4','W5','W6','W7','WT','TT','T1','T2')) "
        sql = sql + " and method=0 and category<>'1' and category is not null and category <>''"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp1 = []
        for i in range(rs.recordcount):
            tmp1.append([rs.fields(0).value, rs.fields(1).value,rs.fields(2).value])
            rs.movenext
        tmp1 = pd.DataFrame(tmp1, columns=['TECH','IDW', 'LAYER'])
        tmp1['CATEGORY'] = 'LowEnd:Threshold-->Linear'
        summary = pd.concat([summary, tmp1])

        sql = sql2 + " where layer in ('W1','W2','W3','W4','W5','W6','W7','WT')"
        sql = sql + " and method=1 and  l_threshold<>50"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp1=[]
        for i in range(rs.recordcount):
            tmp1.append([rs.fields(0).value, rs.fields(1).value,rs.fields(2).value])
            rs.movenext
        tmp1 = pd.DataFrame(tmp1, columns=['TECH','IDW', 'LAYER'])
        tmp1['CATEGORY'] = 'Hole:Threshold--> !=50'
        summary = pd.concat([summary, tmp1])

        sql = sql2 + " where layer in ('A1','A2','A3','A4','A5','A6','A7','AT','TT','T1','T2')"
        sql = sql + " and category='1' and method=1 and (l_threshold<>15 or r_threshold<>15)"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp1 = []
        for i in range(rs.recordcount):
            tmp1.append([rs.fields(0).value, rs.fields(1).value,rs.fields(2).value])
            rs.movenext
        tmp1 = pd.DataFrame(tmp1, columns=['TECH','IDW', 'LAYER'])
        tmp1['CATEGORY'] = 'AdvacedMetal:Threshold !=15'
        summary = pd.concat([summary, tmp1])



        sql = sql2 + " where layer in ('A1','A2','A3','A4','A5','A6','A7','AT','TT','T1','T2')"
        sql = sql + " and category<>'1' and method=1 and (l_threshold<>20 or r_threshold<>20)"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp1 = []
        for i in range(rs.recordcount):
            tmp1.append([rs.fields(0).value, rs.fields(1).value,rs.fields(2).value])
            rs.movenext
        tmp1 = pd.DataFrame(tmp1, columns=['TECH','IDW', 'LAYER'])
        tmp1['CATEGORY'] = 'LowEndMetal:Threshold !=20'
        summary = pd.concat([summary, tmp1])



        summary=summary.drop_duplicates()
        summary = summary.reset_index().drop('index',axis=1)

        conn.close

        return summary


class OVL_Maintain:
    def __init__(self):
        pass
    def ovlwrongQuery(self,dbtable,begin,end):
        conn=CD_Maintain().openDB()

        # sql = "select PART,OVL as PPID,ToolType,f2 as ovlto,f3 as recipeZb,AutoCheck,RiQi,Conclusion,ReviewedDate,EmployeeID from " + dbtable + " where AutoCheck in ('Wrong','data not ready','!=16points') and (conclusion<>'正确' or conclusion is null) order by RiQi"
        # sql = "select part ,OVL as PPID,ToolType,AutoCheck,ArcherCheck,Q200Check,RiQi,Conclusion,ReviewedDate,EmployeeID ,mid(PART,3,4) as maskno from " + dbtable + " where (AutoCheck like '%Wrong%' or Autocheck like '%!=16points%') and (conclusion<>'正确' or conclusion is null) order by OVL"
        sql = "select part ,OVL as PPID,ToolType,AutoCheck,ArcherCheck,Q200Check,RiQi,Conclusion,ReviewedDate,EmployeeID ,mid(PART,3,4) as maskno from " + dbtable + " where ((AutoCheck like '%Wrong%' or Autocheck like '%!=16points%') and (conclusion<>'正确' or conclusion is null)) or conclusion = '错误' order by OVL"
        sql = "select * from (" + sql + ") order by maskno,revieweddate desc,autocheck"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount>0:
            tmp1 = [ rs.fields(i).name for i in range(rs.fields.count)]
            tmp2=[]
            for j in range(rs.recordcount):
                tmp2.append( [ rs.fields(i).value for i in range(rs.fields.count)])
                rs.movenext
            return pd.DataFrame(tmp2,columns=tmp1)
        else:
            return pd.DataFrame()
        conn.close
    def ovlPpidQuery(self,dbtable,part,ppid):
        conn=CD_Maintain().openDB()
        sql = "select ID,PART,STAGE,OVL as sPPID,ToolType,f1 as gdsZB,f2 as ovlTo,f4 as recipeZb,AutoCheck," \
              "f5 as autoChecked,RiQi,Conclusion,ReviewedDate,EmployeeID" \
              "  from " + dbtable + " where PART like '%" + part + "%' and OVL like '%" + ppid + "%'"
        print(sql)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount>0:
            tmp1 = [ rs.fields(i).name for i in range(rs.fields.count)]
            tmp2=[]
            for j in range(rs.recordcount):
                tmp2.append( [ rs.fields(i).value for i in range(rs.fields.count)])
                rs.movenext
            return pd.DataFrame(tmp2,columns=tmp1)
        else:
            return pd.DataFrame()
        conn.close
    def ovlMissingZb(self):
        conn = CD_Maintain().openDB()

        sql = "select ID,PART,STAGE,OVL as PPID, f1,f2,f4,f5,f7 from PPID where f1=True and f2=True and f4=False and f7=True"
        sql = "select ID,PART,STAGE,OVL as PPID, CONCLUSION ,REVIEWEDDATE from PPID where (f1=True and f2=True and f4=False and f7=True) or (conclusion='错误' and autocheck like '%wrong%')"
        sql = sql + 'order by conclusion'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount > 0:
            tmp1 = [rs.fields(i).name for i in range(rs.fields.count)]
            tmp2 = []
            for j in range(rs.recordcount):
                tmp2.append([rs.fields(i).value for i in range(rs.fields.count)])
                rs.movenext

            df = pd.DataFrame(tmp2, columns=tmp1)
            conn.close
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_DailyCheck/database/data.mdb'
            conn.Open(DSN)

            ovltool = [' ' for i in range(df.shape[0])]
            for i in range(df.shape[0]):
                part=df.iloc[i,1]
                layer = df.iloc[i,3][-2:]
                print(i,part,layer,df.shape[0])
                sql = "select Met_EqID from OL_ASML where PartID='" + part + "' and Layer='" + layer +"'"
                sql = sql + " order by Dcoll_Time desc"
                rs = win32com.client.Dispatch(r'ADODB.Recordset')
                rs.Open(sql, conn, 1, 3)
                if rs.recordcount>0:
                    ovltool[i]=rs.fields(0).value
                else:
                    sql = "select Met_EqID from OL_Nikon where PartID='" + part + "' and Layer='" + layer + "'"
                    sql = sql + " order by Dcoll_Time desc"
                    rs = win32com.client.Dispatch(r'ADODB.Recordset')
                    rs.Open(sql, conn, 1, 3)
                    if rs.recordcount>0:
                        ovltool[i]=rs.fields(0).value
                pass
            conn.close
            df['OVLTOOL']=ovltool
            df = df[['PART','STAGE','PPID','OVLTOOL','CONCLUSION','REVIEWEDDATE']]


            return df
        else:
            conn.close
            return pd.DataFrame()
    def negativeValue(self):
        conn = CD_Maintain().openDB()
        sql = " select PPID,EqpType,A1,A2,A3,A4,A5,A6,A7,A8 from ZB where A1 like '-%' or A2 like '-%'"
        sql = sql + " or A3 like '-%' or A4 like '-%' or A5 like '-%' or A6 like '-%' or A7 like '-%' or A8 like '-%'"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount > 0:
            tmp1 = [rs.fields(i).name for i in range(rs.fields.count)]
            tmp2 = []
            for j in range(rs.recordcount):
                tmp2.append([rs.fields(i).value for i in range(rs.fields.count)])
                rs.movenext

            df = pd.DataFrame(tmp2, columns=tmp1)
            conn.close
            return df
        else:
            return pd.DataFrame()
    def identicalZb(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)

        sql = "update zb set part=mid(ppid,1,len(ppid)-3) where  eqptype='Q200'"
        conn.execute(sql)

        sql = "select distinct part from ZB"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        part=[]
        for i in range(rs.recordcount):
            if len(rs.fields(0).value)>7:
                part.append(rs.fields(0).value)
            rs.movenext




        result = []
        raw=pd.DataFrame(columns=['PPID', 'A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8', 'TOOL',
       'LAYER'])
        raw=[]
        for no,name in enumerate(part[:]):
            print(no,name,len(part))
            sql = "select PPID,A1,A2,A3,A4,A5,A6,A7,A8,EqpType,len(PPID) as length from ZB where part =  '" + name + "'"
            sql = "select PPID,A1,A2,A3,A4,A5,A6,A7,A8,EqpType from (" + sql +") where length="+ str(len(name)+3)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)
            zb=[]
            try:
                for i in range(rs.recordcount):
                    zb.append([rs.fields(i).value for i in range(rs.fields.count)])
                    rs.movenext
                zb=pd.DataFrame(zb,columns=['PPID','A1','A2','A3','A4','A5','A6','A7','A8','TOOL'])
                zb['LAYER']=[i.split('-')[-1] for i in list(zb['PPID'])]
                tmp =zb[zb['LAYER'].str.startswith('A') | zb['LAYER'].str.startswith('W')| zb['LAYER'].str.startswith('TT')\
                    | zb['LAYER'].str.startswith('TO')| zb['LAYER'].str.startswith('SN')| zb['LAYER'].str.startswith('SI')]



                for tool in tmp['TOOL'].unique():

                    tmp1=tmp[tmp['TOOL']==tool]
                    tmp1 = tmp1.reset_index().drop('index', axis=1)
                    tmp2=tmp1.copy()
                    if tmp1.shape[0]>1:
                        tmp1=tmp1[['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8']]
                        tmp1=tmp1.applymap(lambda x: eval(x))






                        for j in range(tmp1.shape[0]-1):
                            for k in range(j+1,tmp1.shape[0]):
                                flag= (abs (tmp1.iloc[j,0]-tmp1.iloc[k,0])<0.02 and abs(tmp1.iloc[j,1]-tmp1.iloc[k,1])<0.02) \
                                    or (abs (tmp1.iloc[j,2]-tmp1.iloc[k,2])<0.02 and abs(tmp1.iloc[j,3]-tmp1.iloc[k,3])<0.02) \
                                    or (abs (tmp1.iloc[j,4]-tmp1.iloc[k,4]) < 0.02 and abs(tmp1.iloc[j,5]-tmp1.iloc[k,5]) < 0.02) \
                                    or (abs (tmp1.iloc[j,6]-tmp1.iloc[k,6])<0.02 and abs(tmp1.iloc[j,7]-tmp1.iloc[k,7])<0.02)
                                if flag==True:
                                    result.append(  [tmp2.iloc[j,0],tmp2.iloc[k,0]])
                                    raw.append(list(tmp2.loc[j]))
                                    raw.append(list(tmp2.loc[k]))

            except:
                pass

        conn.close

        return pd.DataFrame(result),pd.DataFrame(raw)

# class WAFER_MAP:
#
#     def __init__(self):
#         pass
#     def Calculate_Die_Notch_Down(self):
#         tmpcount = 0
#         stepX = eval(input(" Please Input Step X (mm): "))
#         stepY = eval(input(" Please Input Step Y (mm): "))
#         dieX = eval(input(" Please Input Die X (mm): "))
#         dieY = eval(input(" Please Input Die Y (mm): "))
#         # offX = eval(input(" Please Input Map Offset X (mm): "))
#         # offY = eval(input(" Please Input Map Offset Y (mm): "))
#         #wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
#         # part = input('Please Input Part Name: ')
#         wee=97
#         col1,row1 = int(wee // stepX),int(wee // stepY)
#         col2,row2 = int(stepX / dieX),int(stepY / dieY)
#         shotDie = stepX / dieX * stepY / dieY
#         summary=[]
#         pricision=0.1
#
#
#         #=====================================================
#         # 'This script is trying to minimize NonebyLS area at left and right sides of wafer.
#         # 'Assume: FEC is 3 mm, ZbyLS can reach 8 mm from wafer edge along X-axis
#         # '        Pre scan is 13 mm, slit scan is 6 mm per side
#         # '        Field is not exposable when scan cent is more than 99.5 mm from wafer center.
#         # 'Warning: Step size X less than 16 mm is NOT considered
#
#         # LSmapX
#         b1,b2=stepX,stepY
#         Ximg=0  #Image Shift on reticle X(size on wafer)
#         shiftX1_min,shiftX1_max,shiftX2_min,shiftX2_max="","","",""
#
#         if int(184 / b1 + 1) < 194 / b1:                        #'NonebyLS is unavoidable.
#             if 194 - int(194 / b1) * b1 + 2.5 > b1 / 2:         #'(B) NonebyLS exposable in one side when the other side field edge touch FEC
#                 K2 = 97 - (2 * int(194 / 2 / b1) + 1) * b1 / 2           # 'Field edge touch FEC
#                 if Ximg>0:
#                     XcellA1 = K2 + Ximg                         #'Field edge touch right FEC
#                 else:
#                     XcellA1 = -K2 + Ximg                        #'Field edge touch left FEC
#             else:
#                 K3 = 100 - 0.5 - int(100 / b1 + 1 / 2) * b1     #'Scan center touch wafer edge (99.5 mm from wafer center)
#                 if 100 - b1 * int(200 / b1) / 2 < 4.25:         #    '(D) Left and right NonebyLS => symmetric map
#                     K3 = (int(100 / b1) - int(100 / b1 - 1 / 2)) * b1 / 2
#                                                                     #'(C)'
#                 if Ximg>0:
#                     XcellA1=-K3+Ximg                            #'Scan center touch left wafer edge
#                 else:
#                     XcellA1=K3+Ximg                             #'Scan center touch right wafer edge
#             shiftX1_min = XcellA1
#         else:                                                    #'(A)
#             D1 = 92 - b1 * (int(184 / b1 + 1) - 1) / 2           #'Reticle center touch NonebyLS
#             D2 = b1 * (int(184 / b1 + 1) / 2) - 97               #''Field edge touch FEC
#             if D1<D2:
#                 K1=D1
#             else:
#                 K1=D2
#
#             if int(184 / b1 + 1) - int(int(184 / b1 + 1) / 2) * 2 == 1:      #'Odd columns within ZbyLS
#                 if Ximg==0:
#                     XcellA1=-K1
#                     XcellA2=K1
#                 else:
#                     XcellA1=Ximg
#                     XcellA2=Ximg-Ximg/abs(Ximg)*K1
#             else:
#                 if Ximg==0:                                                 # 'Even columns within ZbyLS
#                     XcellA1=-b1/2
#                     XcellA2=-b1/2+K1
#                     XcellB1=b1/2
#                     XcellB2=b1/2-K1
#
#                     if  XcellB1 > XcellB2:
#                         XBmin = XcellB2
#                         XBmax = XcellB1
#                     else:
#                         XBmin = XcellB1
#                         XBmax = XcellB2
#                     shiftX2_min=XBmin
#                     shiftX2_max=XBmax
#                 else:
#                     XcellA1 = Ximg - Ximg / abs(Ximg) * b1 / 2
#                     XcellA2 = Ximg - Ximg / abs(Ximg) * (b1 / 2 - K1)
#             if XcellA1 > XcellA2:
#                 XAmin = XcellA2
#                 XAmax = XcellA1
#             else:
#                 XAmin = XcellA1
#                 XAmax = XcellA2
#             shiftX1_min = XAmin
#             shiftX1_max = XAmax
#
#         print(shiftX1_min)
#         print(shiftX1_max)
#         print(shiftX2_min)
#         print(shiftX2_max)
#
#         l=[]
#         if shiftX1_max=='' and shiftX2_min=='' and shiftX2_max=='':
#             l=[shiftX1_min]
#         else:
#             try:
#                 for i in range  ( int((shiftX1_max-shiftX1_min)/pricision)+1 ):
#                     l.append(shiftX1_min + i * pricision)
#             except:
#                 pass
#             try:
#                 for i in range  ( int((shiftX2_max-shiftX2_min)/pricision)+1 ):
#                     l.append(shiftX2_min + i * pricision)
#             except:
#                 pass
#         l=[round(i,3) for i in l]
#
#         #calculate LSmapY:
#         for Xcell in l:
#             l1=[]
#             shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max = "", "", "", ""
#             E1 = (100 - Xcell + Ximg) - int((92 - Xcell + Ximg) / b1) * b1       #'Right side scan center
#             E2 = (100 + Xcell - Ximg) - int((92 + Xcell - Ximg) / b1) * b1       # 'Left side scan center
#             if E1>E2:
#                 E=E2
#             else:
#                 E=E1
#
#             H=pow(97 * 97 - pow((100 - E) , 2),0.5)
#             dH = H - 13 - 6 - 5
#             if dH <= 0:
#                 shiftY1_min,shiftY2_min = -b2/2,b2/2
#                 l1=[shiftY1_min,shiftY2_min]
#             else:
#                 if dH<b2/2:
#                     shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max= -b2/2,-b2/2+dH,b2/2-dH,b2/2
#                     for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
#                         l1.append(shiftY1_min + i *pricision)
#                     for i in range(int((shiftY2_max - shiftY2_min) / pricision) + 1):
#                         l1.append(shiftY2_min + i * 0.1)
#
#                 else:
#                     shiftY1_min, shiftY1_max= -b2/2,b2/2
#                     for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
#                         l1.append(shiftY1_min + i * pricision)
#             l1=[round(i,3) for i in l1]
#
#             for Ycell in l1:
#                 offX,offY=Xcell,Ycell
#                 # summary.append([Xcell, Ycell])
#
#
#
#
#
#
#
#
#                 totalDie = 0
#                 fullShot=0
#                 partialShot=0
#                 for i in range(-col1 - 1, col1 + 2):
#                     for j in range(-row1 - 1, row1 + 2):
#
#                         llx = i * stepX - stepX / 2 + offX
#                         lly = j * stepY - stepY / 2 + offY
#
#                         f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
#                         f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
#                         f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                         f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                         # laser mark
#                         f5 = ((lly + stepY) > 92) and ((llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
#                         f5 = not f5
#                         # notch
#                         f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
#                         f6 = not f6
#
#                         if f1 and f2 and f3 and f4 and f6:
#                             totalDie = totalDie + shotDie
#                             fullShot=fullShot+1
#                         else:
#                             if f1 or f2 or f3 or f4:
#                                 partialShotDie = 0
#                                 partialShot=partialShot+1
#                                 for k in range(0, col2):
#                                     for l in range(0, row2):
#                                         sx = llx + k * dieX
#                                         sy = lly + l * dieY
#
#                                         f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
#                                         f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
#                                         f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                         f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                         f5 = ((sy + dieY) > 92) and ((sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
#                                         f5 = not f5
#
#                                         f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
#                                         f6 = not f6
#
#                                         if f1 and f2 and f3 and f4 and f5 and f6:
#                                             partialShotDie += 1
#
#                                 totalDie = totalDie + partialShotDie
#                 print(totalDie)
#                 summary.append([Xcell, Ycell,totalDie,fullShot,partialShot,fullShot+partialShot])
#         summary = pd.DataFrame(summary,columns=['shiftX','shiftY','DieQty','FullShot','PartialShot','TotalShot'])
#         summary = summary.sort_values(by='TotalShot')
#         print(summary)
#     def Calculate_Die_Notch_Left(self):
#         tmpcount = 0
#         stepX = eval(input(" Please Input Step X (mm): "))
#         stepY = eval(input(" Please Input Step Y (mm): "))
#         dieX = eval(input(" Please Input Die X (mm): "))
#         dieY = eval(input(" Please Input Die Y (mm): "))
#         # offX = eval(input(" Please Input Map Offset X (mm): "))
#         # offY = eval(input(" Please Input Map Offset Y (mm): "))
#         #wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
#         # part = input('Please Input Part Name: ')
#         wee=97
#         col1,row1 = int(wee // stepX),int(wee // stepY)
#         col2,row2 = int(stepX / dieX),int(stepY / dieY)
#         shotDie = stepX / dieX * stepY / dieY
#         summary=[]
#         pricision=0.1
#
#
#         #=====================================================
#         # 'This script is trying to minimize NonebyLS area at left and right sides of wafer.
#         # 'Assume: FEC is 3 mm, ZbyLS can reach 8 mm from wafer edge along X-axis
#         # '        Pre scan is 13 mm, slit scan is 6 mm per side
#         # '        Field is not exposable when scan cent is more than 99.5 mm from wafer center.
#         # 'Warning: Step size X less than 16 mm is NOT considered
#
#         # LSmapX
#         b1,b2=stepX,stepY
#         Ximg=0  #Image Shift on reticle X(size on wafer)
#         shiftX1_min,shiftX1_max,shiftX2_min,shiftX2_max="","","",""
#
#         if int(184 / b1 + 1) < 194 / b1:                        #'NonebyLS is unavoidable.
#             if 194 - int(194 / b1) * b1 + 2.5 > b1 / 2:         #'(B) NonebyLS exposable in one side when the other side field edge touch FEC
#                 K2 = 97 - (2 * int(194 / 2 / b1) + 1) * b1 / 2           # 'Field edge touch FEC
#                 if Ximg>0:
#                     XcellA1 = K2 + Ximg                         #'Field edge touch right FEC
#                 else:
#                     XcellA1 = -K2 + Ximg                        #'Field edge touch left FEC
#             else:
#                 K3 = 100 - 0.5 - int(100 / b1 + 1 / 2) * b1     #'Scan center touch wafer edge (99.5 mm from wafer center)
#                 if 100 - b1 * int(200 / b1) / 2 < 4.25:         #    '(D) Left and right NonebyLS => symmetric map
#                     K3 = (int(100 / b1) - int(100 / b1 - 1 / 2)) * b1 / 2
#                                                                     #'(C)'
#                 if Ximg>0:
#                     XcellA1=-K3+Ximg                            #'Scan center touch left wafer edge
#                 else:
#                     XcellA1=K3+Ximg                             #'Scan center touch right wafer edge
#             shiftX1_min = XcellA1
#         else:                                                    #'(A)
#             D1 = 92 - b1 * (int(184 / b1 + 1) - 1) / 2           #'Reticle center touch NonebyLS
#             D2 = b1 * (int(184 / b1 + 1) / 2) - 97               #''Field edge touch FEC
#             if D1<D2:
#                 K1=D1
#             else:
#                 K1=D2
#
#             if int(184 / b1 + 1) - int(int(184 / b1 + 1) / 2) * 2 == 1:      #'Odd columns within ZbyLS
#                 if Ximg==0:
#                     XcellA1=-K1
#                     XcellA2=K1
#                 else:
#                     XcellA1=Ximg
#                     XcellA2=Ximg-Ximg/abs(Ximg)*K1
#             else:
#                 if Ximg==0:                                                 # 'Even columns within ZbyLS
#                     XcellA1=-b1/2
#                     XcellA2=-b1/2+K1
#                     XcellB1=b1/2
#                     XcellB2=b1/2-K1
#
#                     if  XcellB1 > XcellB2:
#                         XBmin = XcellB2
#                         XBmax = XcellB1
#                     else:
#                         XBmin = XcellB1
#                         XBmax = XcellB2
#                     shiftX2_min=XBmin
#                     shiftX2_max=XBmax
#                 else:
#                     XcellA1 = Ximg - Ximg / abs(Ximg) * b1 / 2
#                     XcellA2 = Ximg - Ximg / abs(Ximg) * (b1 / 2 - K1)
#             if XcellA1 > XcellA2:
#                 XAmin = XcellA2
#                 XAmax = XcellA1
#             else:
#                 XAmin = XcellA1
#                 XAmax = XcellA2
#             shiftX1_min = XAmin
#             shiftX1_max = XAmax
#
#         print(shiftX1_min)
#         print(shiftX1_max)
#         print(shiftX2_min)
#         print(shiftX2_max)
#
#         l=[]
#         if shiftX1_max=='' and shiftX2_min=='' and shiftX2_max=='':
#             l=[shiftX1_min]
#         else:
#             try:
#                 for i in range  ( int((shiftX1_max-shiftX1_min)/pricision)+1 ):
#                     l.append(shiftX1_min + i * pricision)
#             except:
#                 pass
#             try:
#                 for i in range  ( int((shiftX2_max-shiftX2_min)/pricision)+1 ):
#                     l.append(shiftX2_min + i * pricision)
#             except:
#                 pass
#         l=[round(i,3) for i in l]
#
#         #calculate LSmapY:
#         for Xcell in l:
#             l1=[]
#             shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max = "", "", "", ""
#             E1 = (100 - Xcell + Ximg) - int((92 - Xcell + Ximg) / b1) * b1       #'Right side scan center
#             E2 = (100 + Xcell - Ximg) - int((92 + Xcell - Ximg) / b1) * b1       # 'Left side scan center
#             if E1>E2:
#                 E=E2
#             else:
#                 E=E1
#
#             H=pow(97 * 97 - pow((100 - E) , 2),0.5)
#             dH = H - 13 - 6 - 5
#             if dH <= 0:
#                 shiftY1_min,shiftY2_min = -b2/2,b2/2
#                 l1=[shiftY1_min,shiftY2_min]
#             else:
#                 if dH<b2/2:
#                     shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max= -b2/2,-b2/2+dH,b2/2-dH,b2/2
#                     for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
#                         l1.append(shiftY1_min + i *pricision)
#                     for i in range(int((shiftY2_max - shiftY2_min) / pricision) + 1):
#                         l1.append(shiftY2_min + i * 0.1)
#
#                 else:
#                     shiftY1_min, shiftY1_max= -b2/2,b2/2
#                     for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
#                         l1.append(shiftY1_min + i * pricision)
#             l1=[round(i,3) for i in l1]
#
#             for Ycell in l1:
#                 offX,offY=Xcell,Ycell
#                 # summary.append([Xcell, Ycell])
#
#
#
#
#
#
#
#
#                 totalDie = 0
#                 fullShot=0
#                 partialShot=0
#                 for i in range(-col1 - 1, col1 + 2):
#                     for j in range(-row1 - 1, row1 + 2):
#
#                         llx = i * stepX - stepX / 2 + offX
#                         lly = j * stepY - stepY / 2 + offY
#
#                         f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
#                         f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
#                         f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                         f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                         # laser mark
#                         f5 = ((llx + stepX) > 92) and ((lly + stepY < 13 and lly + stepY > -13) or (lly < 13 and lly > -13))
#                         f5 = not f5
#                         # notch
#                         f6 = (llx < -94) and ((lly + stepY < 14 and lly + stepY > -14) or (lly < 14 and lly > -14))
#                         f6 = not f6
#
#                         if f1 and f2 and f3 and f4 and f6:
#                             totalDie = totalDie + shotDie
#                             fullShot=fullShot+1
#                         else:
#                             if f1 or f2 or f3 or f4:
#                                 partialShotDie = 0
#                                 partialShot=partialShot+1
#                                 for k in range(0, col2):
#                                     for l in range(0, row2):
#                                         sx = llx + k * dieX
#                                         sy = lly + l * dieY
#
#                                         f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
#                                         f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
#                                         f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                         f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                         f5 = ((sx + dieX) > 92) and ((sy + dieY < 13 and sy + dieY > -13) or (sy < 13 and sy > -13))
#                                         f5 = not f5
#
#                                         f6 = (sx < -94) and ((sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
#                                         f6 = not f6
#
#                                         if f1 and f2 and f3 and f4 and f5 and f6:
#                                             partialShotDie += 1
#
#                                 totalDie = totalDie + partialShotDie
#                 print(totalDie)
#                 summary.append([Xcell, Ycell,totalDie,fullShot,partialShot,fullShot+partialShot])
#         summary = pd.DataFrame(summary,columns=['shiftX','shiftY','DieQty','FullShot','PartialShot','TotalShot'])
#         summary = summary.sort_values(by='TotalShot')
#         print(summary)
#     def gating_notch_down(self,shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee):
#         tmpcount = 0
#         tmpcount1 = 0
#
#         for k in range(0, col2):
#             for l in range(0, row2):
#                 sx = llx + k * dieX
#                 sy = lly + l * dieY
#
#                 f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
#                 f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
#                 f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                 f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                 f5 = ((sy + dieY) > 92) and ((sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
#                 f5 = not f5
#
#                 f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
#                 f6 = not f6
#
#                 if (f1 and f2 and f3 and f4 and f5 and f6):
#                     tmpcount += 1
#
#                 tmpcount1 = tmpcount1 + len([i for i in [f1, f2, f3, f4] if i == True]) / 4
#
#         # return (tmpcount1 / shotDie < 0.15) and (tmpcount / gdw < 0.005)
#         return False
#     def Plot_Map_Notch_Down(self):
#
#
#         tmpcount = 0
#         stepX = eval(input(" Please Input Step X (um): "))/1000
#         stepY = eval(input(" Please Input Step Y (um): "))/1000
#         dieX = eval(input(" Please Input Die X (um): "))/1000
#         dieY = eval(input(" Please Input Die Y (um): "))/1000
#         offX = eval(input(" Please Input Map Offset X (um): "))/1000
#         offY = eval(input(" Please Input Map Offset Y (um): "))/1000
#         wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
#         part = input('Please Input Part Name: ')
#         gdw = 3.14*97*97/dieX/dieY
#
#
#
#
#         col1 = int(wee // stepX)
#         row1 = int(wee // stepY)
#
#         col2 = int(stepX / dieX)
#         row2 = int(stepY / dieY)
#
#         shotDie = stepX / dieX * stepY / dieY
#
#         totalDie = 0
#
#         fig = plt.figure(figsize=(10, 10))
#         ax = fig.add_subplot(111)
#
#         # ell1 = Ellipse(xy = (0.0, 0.0), width = 4, height = 8, angle = 30.0, facecolor= 'yellow', alpha=0.3)
#         # ax.add_patch(ell1)
#
#         cir1 = Circle(xy=(0, 0), radius=100, alpha=1, fill=False, edgecolor='black', linewidth=1)
#         ax.add_patch(cir1)
#         cir1 = Circle(xy=(0, 0), radius=wee, alpha=1, fill=False, edgecolor='blue', linewidth=1)
#         ax.add_patch(cir1)
#
#         for i in range(-col1 - 1, col1 + 2):
#             # for i in range(-100,100):
#             for j in range(-row1 - 1, row1 + 2):
#
#                 llx = i * stepX - stepX / 2 + offX
#                 lly = j * stepY - stepY / 2 + offY
#
#                 f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
#                 f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
#                 f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                 f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                 # laser mark
#                 f5 = ((lly + stepY) > 92) and ((llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
#                 f5 = not f5
#                 # notch
#                 f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
#                 f6 = not f6
#
#                 if f1 and f2 and f3 and f4 and f6 and f5:
#                     square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False, edgecolor='red',
#                                        linewidth=0.3)
#                     ax.add_patch(square)
#                     totalDie = totalDie + shotDie
#                 else:
#                     if f1 or f2 or f3 or f4:
#
#                         partialShotDie = 0
#
#                         flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee)
#
#                         for k in range(0, col2):
#                             for l in range(0, row2):
#                                 sx = llx + k * dieX
#                                 sy = lly + l * dieY
#
#                                 f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
#                                 f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
#                                 f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                 f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                 f5 = ((sy + dieY) > 92) and (
#                                             (sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
#                                 f5 = not f5
#
#                                 f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
#                                 f6 = not f6
#
#                                 if f1 and f2 and f3 and f4 and f5 and f6:
#                                     if flag == True:
#                                         square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='pink',
#                                                            alpha=1, fill=True, edgecolor='pink', linewidth=0.3)
#                                         ax.add_patch(square)
#
#                                     else:
#
#                                         square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
#                                                            alpha=0.3, fill=True, edgecolor='red', linewidth=0.3)
#                                         ax.add_patch(square)
#                                         partialShotDie += 1
#
#                                 else:
#                                     if f1 or f2 or f3 or f4:
#                                         square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
#                                                            alpha=0.5, fill=True, edgecolor='green', linewidth=0.3)
#                                         ax.add_patch(square)
#                             square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
#                                                edgecolor='red', linewidth=0.3)
#                             ax.add_patch(square)
#                         totalDie = totalDie + partialShotDie
#
#         square = Rectangle(xy=(-13, 92), width=26, height=8, facecolor='yellow', alpha=0.5, fill=True,
#                            edgecolor='black', linewidth=0.3)
#         ax.add_patch(square)
#
#         square = Rectangle(xy=(-14, -100), width=28, height=6, facecolor='yellow', alpha=0.5, fill=True,
#                            edgecolor='black', linewidth=0.3)
#         ax.add_patch(square)
#
#         triangle = plt.Polygon([[0, -95], [3, -100], [-3, -100]], color='purple', alpha=1)  # 顶点坐标颜色α
#         ax.add_patch(triangle)
#
#         x, y = 0, 0
#         ax.plot(x, y, 'ro')
#
#         plt.axis('scaled')
#         # ax.set_xlim(-8, 8)
#         # ax.set_ylim(-8,8)
#         plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length
#
#         plt.text(-50, 50, 'Total Die Qty: ' + str(int(totalDie)))
#         plt.text(-50, 40, 'Step Size: ' + str(stepX) + ', ' + str(stepY))
#         plt.text(-50, 30, 'Die Size ' + str(dieX) + ', ' + str(dieY))
#         plt.text(-50, 20, 'Offset Size: ' + str(offX) + ', ' + str(offY))
#         plt.text(-50, 10, 'Edge Exclusion: ' + str(100 - wee))
#         plt.text(-50, 0, 'Unit: mm ')
#         plt.text(-50, -10, 'Product: ' + part)
#         plt.text(-50, -20, 'Orientation:Notch Down ' )
#
#         plt.savefig('c:\\temp\\' + part + '1.jpg', dpi=600)
#         plt.savefig('c:\\temp\\' + part + '2.jpg', dpi=100)
#         plt.show()
#     def Plot_Map_Notch_Left(self):
#
#
#         tmpcount = 0
#         stepX = eval(input(" Please Input Step X (um): "))/1000
#         stepY = eval(input(" Please Input Step Y (um): "))/1000
#         dieX = eval(input(" Please Input Die X (um): "))/1000
#         dieY = eval(input(" Please Input Die Y (um): "))/1000
#         offX = eval(input(" Please Input Map Offset X (um): "))/1000
#         offY = eval(input(" Please Input Map Offset Y (um): "))/1000
#         wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
#         part = input('Please Input Part Name: ')
#         gdw = 3.14*97*97/dieX/dieY
#
#
#
#
#         col1 = int(wee // stepX)
#         row1 = int(wee // stepY)
#
#         col2 = int(stepX / dieX)
#         row2 = int(stepY / dieY)
#
#         shotDie = stepX / dieX * stepY / dieY
#
#         totalDie = 0
#
#         fig = plt.figure(figsize=(10, 10))
#         ax = fig.add_subplot(111)
#
#         # ell1 = Ellipse(xy = (0.0, 0.0), width = 4, height = 8, angle = 30.0, facecolor= 'yellow', alpha=0.3)
#         # ax.add_patch(ell1)
#
#         cir1 = Circle(xy=(0, 0), radius=100, alpha=1, fill=False, edgecolor='black', linewidth=1)
#         ax.add_patch(cir1)
#         cir1 = Circle(xy=(0, 0), radius=wee, alpha=1, fill=False, edgecolor='purple', linewidth=1)
#         ax.add_patch(cir1)
#
#         for i in range(-col1 - 1, col1 + 2):
#             # for i in range(-100,100):
#             for j in range(-row1 - 1, row1 + 2):
#
#                 llx = i * stepX - stepX / 2 + offX
#                 lly = j * stepY - stepY / 2 + offY
#
#                 f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
#                 f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
#                 f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                 f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
#                 # laser mark
#                 f5 = ((llx + stepX) > 92) and ((lly + stepY < 13 and lly + stepY > -13) or (lly < 13 and lly > -13))
#                 f5 = not f5
#                 # notch
#                 f6 = (llx < -94) and ((lly + stepY < 14 and lly + stepY > -14) or (lly < 14 and lly > -14))
#                 f6 = not f6
#
#                 if f1 and f2 and f3 and f4 and f6 and f5:
#                     square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False, edgecolor='red',
#                                        linewidth=0.5)
#                     ax.add_patch(square)
#                     totalDie = totalDie + shotDie
#                 else:
#                     if f1 or f2 or f3 or f4:
#                         # square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
#                         #                    edgecolor='red', linewidth=0.5)
#                         # ax.add_patch(square)
#                         partialShotDie = 0
#
#                         # flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee)
#                         flag=False
#                         for k in range(0, col2):
#                             for l in range(0, row2):
#                                 sx = llx + k * dieX
#                                 sy = lly + l * dieY
#
#                                 f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
#                                 f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
#                                 f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                 f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
#                                 f5 = ((sx + dieX) > 92) and (
#                                             (sy + dieY < 13 and sy + dieY > -13) or (sy < 13 and sy > -13))
#                                 f5 = not f5
#
#                                 f6 = (sx < -94) and ((sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
#                                 f6 = not f6
#
#                                 if f1 and f2 and f3 and f4 and f5 and f6:
#                                     if flag == True:
#                                         square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='pink',
#                                                            alpha=1, fill=True, edgecolor='pink', linewidth=0.3)
#                                         ax.add_patch(square)
#
#                                     else:
#
#                                         square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
#                                                            alpha=0.5, fill=True, edgecolor='blue', linewidth=0.3)
#                                         ax.add_patch(square)
#                                         partialShotDie += 1
#
#                                 else:
#                                     if f1 or f2 or f3 or f4:
#                                         square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
#                                                            alpha=0.5, fill=True, edgecolor='green', linewidth=0.3)
#                                         ax.add_patch(square)
#
#                             square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
#                                                edgecolor='red', linewidth=0.5)
#                             ax.add_patch(square)
#                         totalDie = totalDie + partialShotDie
#
#         square = Rectangle(xy=(92, -13), width=8, height=26, facecolor='yellow', alpha=0.5, fill=True,
#                            edgecolor='black', linewidth=0.3)
#         ax.add_patch(square)
#
#         square = Rectangle(xy=(-100, -14), width=6, height=28, facecolor='yellow', alpha=0.5, fill=True,
#                            edgecolor='black', linewidth=0.3)
#         ax.add_patch(square)
#
#         triangle = plt.Polygon([[-95, 0], [-100, 3], [100, -3]], color = 'r', alpha = 1 )#顶点坐标颜色α
#         ax.add_patch(triangle)
#
#
#
#
#         x, y = 0, 0
#         ax.plot(x, y, 'ro')
#
#         plt.axis('scaled')
#         # ax.set_xlim(-8, 8)
#         # ax.set_ylim(-8,8)
#         plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length
#
#         plt.text(-50, 50, 'Total Die Qty: ' + str(int(totalDie)))
#         plt.text(-50, 40, 'Step Size: ' + str(stepX) + ', ' + str(stepY))
#         plt.text(-50, 30, 'Die Size ' + str(dieX) + ', ' + str(dieY))
#         plt.text(-50, 20, 'Offset Size: ' + str(offX) + ', ' + str(offY))
#         plt.text(-50, 10, 'Edge Exclusion: ' + str(100 - wee))
#         plt.text(-50, 0, 'Unit: mm ')
#         plt.text(-50, -10, 'Product: ' + part)
#         plt.text(-50,-20,'Orientation: Notch Left')
#
#         plt.savefig('c:\\temp\\' + part + '1.jpg', dpi=600)
#         plt.savefig('c:\\temp\\' + part + '2.jpg', dpi=100)
#         plt.show()

class Product:
    def __init__(self):
        pass
    def addNewPart(self):
        partlist = [ i[1:-8] for i in os.listdir('P:/_NewBiasTable') if i[0]=='_' and i[-3:]=='xls' and 'Template' not in i]
        parttype = [ i[-7:-4].upper() for i in os.listdir('P:/_NewBiasTable') if i[0]=='_' and i[-3:]=='xls' and 'Template' not in i]

        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql='select distinct Part from PRODUCT'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        old=[]
        if rs.recordcount>0:
            for i in range(rs.recordcount):
                old.append(rs.fields(0).value)
                rs.movenext
        partlist =list(set(partlist) - set(old))

        sql = 'select * from PRODUCT'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)

        for k, file in enumerate(partlist):
            print(k, len(partlist),file)
            tmp=pd.read_excel('P:/Recipe/Biastable/'+file+'.xls')
            tech = tmp.iloc[5,0].strip()
            tech=tech.upper()

            rs.AddNew()  # 添加一条新记录
            rs.Fields('Part').Value = file
            rs.Fields('RiQi').Value = QDateTime.currentDateTime().toString("yyyy-MM-dd HH:mm:ss")[:10]
            rs.Fields('PartType').Value = parttype[k]
            rs.Fields('Tech').Value = tech
            rs.Fields('MaskNo').Value = file[2:6]
            rs.Update()  # 更新
        conn.close
    def summaryData(self):
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = 'select distinct Part from PRODUCT'
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        partlist = []
        if rs.recordcount > 0:
            for i in range(rs.recordcount):
                partlist.append(rs.fields(0).value)
                rs.movenext

        sql1 = "(select Part,count(part) as alllayers   from PPID group by Part order by Part )"
        sql2 = "(select  Part,count(CD) as  cdlayers from PPID where CD like '%-LN' group by Part order by Part)"
        sql3 = "(select Part,count(OVL) as ovllayers from PPID where OVL like '%-%' group by Part order by Part)"
        sql4 = "(select Part,count(AutoCheck) as ovlcorrect from PPID where AutoCheck like '%Correct%' group by Part order by Part)"
        sql5 = "(select A.Part,A.alllayers,B.cdlayers,C.ovllayers from " + sql1 + " A," + sql2+ " B,"+ sql3
        sql5 = sql5 + " C where A.Part=B.Part and A.Part=C.Part) "
        sql6 = "( select A.*,B.ovlcorrect from " + sql5 + " A left join " + sql4 + " B on A.Part=B.Part)"
        sql7 = "(select Part,PartType,Tech,RiQi,MaskNo from PRODUCT order by RiQi,MaskNo)"
        sql  = " select A.*,B.alllayers as cdovllayers,B.cdlayers,B.ovllayers,B.ovlcorrect from " + sql7 + " A left join " + sql6 + " B on A.Part=B.Part"
        sql = sql + " order by RiQi,MaskNo"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)

        col = [rs.fields(i).name for i in range(rs.fields.count)]
        print(col)


        tmp=[]
        for i in range(rs.recordcount):

            tmp.append([rs.fields(i).value for i in range(rs.fields.count)])
            rs.movenext

        tmp = pd.DataFrame(tmp,columns=col)
        tmp['cdcorrect']='pending'
        tmp['pre-R2R']='pending'
        tmp['post-R2R']='pending'
        return tmp
    def queryByPart(self,part):
        pass
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        sql = "select *  from PPID WHERE PART='" + part + "'"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)

        col = [rs.fields(i).name for i in range(rs.fields.count)]
        col =['Part','Stage','CdPpid','ToolType','ID','OvlPpid','isOvlStd','isOvlTo','isSize','isOvlZb','ovlAutoCheck','isOvlChecked','ovlRiQi','ovlConclusion','ovlReviewedDate','ovlEmpID','isCdRun','isOvlRun']




        tmp = []
        for i in range(rs.recordcount):
            tmp.append([rs.fields(i).value for i in range(rs.fields.count)])
            rs.movenext
        conn.close
        tmp = pd.DataFrame(tmp, columns=col)
        tmp=tmp[['Part','Stage','CdPpid','ToolType','OvlPpid','isCdRun','isOvlRun','isOvlStd','isOvlTo','isSize','isOvlZb','ovlAutoCheck','isOvlChecked','ovlRiQi','ovlConclusion','ovlReviewedDate','ovlEmpID']]
        # tmp=tmp.T
        # tmp=tmp.reset_index()
        # tmp.columns=['A'+str(i+1) for i in range(tmp.shape[1])]




        return tmp
class MyMainWindow(QMainWindow, Ui_MainWindow):
    def __init__(self, parent=None):
        super(MyMainWindow, self).__init__(parent)

        self.part = None
        self.f1 = False
        self.f2 = False
        self.dfF = None  #flow data
        self.dfB = None  #bias table data
        self.dfZ = None  #coordinate
        self.df  = None  #compiled data
        self.fulltech = ""
        self.SPMX = None #search Y as well
        self.SPMY = None #search X as well
        self.EGAX = None
        self.EGAY = None
        self.EGAX1 = None
        self.EGAY1 = None
        self.dic = None  # dictionary of coordinate
        self.dicKey = None
        self.nTO=None
        self.setupUi(self)
        self.initUI()
        #CD Maintain
    def initUI(self):

        self.dateTimeEdit.setCalendarPopup(True)
        self.dateTimeEdit.setDateTime(QDateTime.currentDateTime().addDays(-30))
        self.dateTimeEdit.setDisplayFormat("yyyy-MM-dd HH:mm:ss")
        self.dateTimeEdit.setMaximumDate(QDate.currentDate().addDays(1000))
        self.dateTimeEdit.setMinimumDate(QDate.currentDate().addDays(-1000))
        self.dateTimeEdit_2.setCalendarPopup(True)
        self.dateTimeEdit_2.setDateTime(QDateTime.currentDateTime())
        self.dateTimeEdit_2.setDisplayFormat("yyyy-MM-dd HH:mm:ss")
        self.dateTimeEdit_2.setMaximumDate(QDate.currentDate().addDays(1000))
        self.dateTimeEdit_2.setMinimumDate(QDate.currentDate().addDays(-1000))
        # self.exTable.horizontalHeader().setStyleSheet("background-color: yellow")
        self.exTable.horizontalHeader().setSectionResizeMode(QHeaderView.Interactive)

        self.r2rLineEdit01.setPlaceholderText("输入‘%’或工艺代码")
        self.r2rLineEdit02.setPlaceholderText("输入‘%’或Part名")
        self.r2rLineEdit03.setPlaceholderText("输入‘%’或层次名")
        self.r2rLineEdit04.setPlaceholderText("输入‘%’或设备名")

        self.exLineEdit1_12.setPlaceholderText("StepX(um)")
        self.exLineEdit1_13.setPlaceholderText("StepY(um)")
        self.exLineEdit1_29.setPlaceholderText("DieX(um)")
        self.exLineEdit1_14.setPlaceholderText("DieY(um)")
        self.exLineEdit1_31.setPlaceholderText("MapOffsetX(um)")
        self.exLineEdit1_30.setPlaceholderText("MapOffsetY(um)")
        self.exLineEdit1_32.setPlaceholderText("ImgOffsetY(um)")
        self.exLineEdit1_33.setPlaceholderText("Edge(mm,输入3）")
        self.exLineEdit1_55.setPlaceholderText("MapOffsetX-L-N(um)")
        self.exLineEdit1_56.setPlaceholderText("MapOffsetY-L-N(um)")
        self.exLineEdit1_80.setPlaceholderText("SizeX（um）")
        self.exLineEdit1_81.setPlaceholderText("SizeX-shift（um）")
        self.exLineEdit1_83.setPlaceholderText("拟合精度（0.1mm）")
        self.exLineEdit1_82.setPlaceholderText("理论最大管芯数")
        self.exLineEdit1_9.setPlaceholderText("StepX")
        self.exLineEdit1_10.setPlaceholderText('Y新品坐标')




        # singal and slot
        self.exPushButton_25.clicked.connect(self.mapCal)
        self.exPushButton_26.clicked.connect(self.mapDraw)
        self.exPushButton_27.clicked.connect(self.maxDie)
        self.exPushButton_28.clicked.connect(self.largeField)
        self.exPushButton_29.clicked.connect(self.normalField)
        self.exPushButton_30.clicked.connect(self.splitGate)
        self.exPushButton_31.clicked.connect(self.MPW)



        self.exPushButton_88.clicked.connect(self.exNAquery)
        self.exPushButton_9.clicked.connect(self.exNAupdate)
        self.exPushButton_10.clicked.connect(self.exNAinsert)
        self.exTable.itemDoubleClicked.connect(self.onExTableItemChangedDoubleClicked)
        self.exPushButton.clicked.connect(self.onExBtn)     #read excel flow
        self.exPushButton_2.clicked.connect(self.onExBtn_2) #read excel bias table
        self.exPushButton_3.clicked.connect(self.onExBtn_3) #check ASML NA/SIGMA, reticle
        self.exPushButton_4.clicked.connect(self.onExBtn_4) #Nikon Date
        # self.exPushButton_5.clicked.connect(self.onExBtn_5) #OVL Result
        # self.exPushButton_6.clicked.connect(self.onExBtn_6) #CD AMP
        # self.exPushButton_7.clicked.connect(self.onExBtn_7) #CD IDP
        # self.exPushButton_8.clicked.connect(self.onExBtn_8) #ovl data missing
        self.dateTimeEdit.dateTimeChanged.connect(self.onDateTimeChanged)
        self.dateTimeEdit_2.dateTimeChanged.connect(self.onDateTimeChanged_2)
        self.exComboBox.activated.connect(self.onExCombo)
        self.exComboBox_2.activated.connect(self.onExCombo_2)
        self.actionEX1.triggered.connect(self.onMenuEX1)
        self.actionEX2.triggered.connect(self.onMenuEX2)
        # self.actionT2.triggered.connect(self.exit)
        self.actionExit.triggered.connect(self.exit)

        #short cut
        self.scBtn01.clicked.connect(self.onScBtn01) #ESF限制
        self.scBtn02.clicked.connect(self.onScBtn02) #量测SPC
        self.scBtn03.clicked.connect(self.onScBtn03) #产品套刻
        self.scBtn04.clicked.connect(self.onScBtn04) #套刻QC
        self.scBtn05.clicked.connect(self.onScBtn05) #条宽QC
        self.scBtn06.clicked.connect(self.onScBtn06) #YMS QC
        self.scBtn07.clicked.connect(self.onScBtn07) #YMS QC
        self.scBtn08.clicked.connect(self.onScBtn08) #预对位
        self.scBtn09.clicked.connect(self.onScBtn09) #MCC
        self.scBtn10.clicked.connect(self.onScBtn10)  #Nikon调节

        self.scBtn30.clicked.connect(self.onScBtn30) #run excel
        self.scBtn31.clicked.connect(self.onScBtn31) #cd idp/amp upload
        self.scBtn32.clicked.connect(self.onScBtn32) #OVL update
        self.scBtn33.clicked.connect(self.onScBtn33) #Nikon Vector
        self.scBtn34.clicked.connect(self.onScBtn34) #Nikon Recipe Maintain
        self.scBtn35.clicked.connect(self.onScBtn35) #ASML BatchReport Extraction
        self.scBtn36.clicked.connect(self.onScBtn36) #ASML PreAlignment Plot
        self.scBtn37.clicked.connect(self.onScBtn37) #PPCS DATABASE
        self.scBtn38.clicked.connect(self.onScBtn38) #OVL OPTIMUM VALUE
        self.scBtn39.clicked.connect(self.onScBtn39) #CDU QC
        self.scBtn40.clicked.connect(self.onScBtn40) #CDU IMAGE
        self.scBtn41.clicked.connect(self.onScBtn41) #ASML AWE FILE
        self.scBtn42.clicked.connect(self.onScBtn42) #NIKON Parameter
        self.scBtn43.clicked.connect(self.onScBtn43) #ESF CONSTRAINTS
        self.scBtn44.clicked.connect(self.onScBtn44) #SPC99
        self.scBtn45.clicked.connect(self.onScBtn45) #LOGIN 111 to download ASML batchreport etc
        self.scBtn46.clicked.connect(self.onScBtn46) #LOGIN 111 to download CD file
        self.scBtn47.clicked.connect(self.onScBtn47) #FTP FILE CREATION FOR NIKON RECIPE UPLOAD
        self.scBtn48.clicked.connect(self.onScBtn48) #PLOG QC CDU/OVL

        #r2r
        self.r2rBtn01.clicked.connect(self.onR2rBtn01)#30天
        self.r2rBtn02.clicked.connect(self.onR2rBtn02)  #60天
        self.r2rBtn03.clicked.connect(self.onR2rBtn03)  #90天
        self.r2rBtn04.clicked.connect(self.onR2rBtn04)  #自选
        self.r2rBtnFilter.clicked.connect(self.onR2rBtnFilter)
        self.r2rBtnByDate.clicked.connect(self.onR2rByDate)
        self.r2rBtnByTool.clicked.connect(self.onR2rByTool)
        self.r2rBtnByPart.clicked.connect(self.onR2rByPart)
        self.r2rBtnByCompare.clicked.connect(self.onR2rCompare)
        self.r2rBtnOffset.clicked.connect(self.onR2rOffset)
        #cd
        self.idpButton1.clicked.connect(self.idpQuery)
        self.idpButton2.clicked.connect(self.idpUpdate)
        self.idpButton1_3.clicked.connect(self.ampQuery)
        self.updateButton.clicked.connect(self.ampUpdate)
        self.updateButton_3.clicked.connect(self.ampUpdate1)
        self.updateButton_3.setStyleSheet("QPushButton{color:black}"
                                  "QPushButton:hover{color:red}"
                                  "QPushButton{background-color:rgb(78,255,255)}")
                                  # "QPushButton{border:2px}"
                                  # "QPushButton{border-radius:10px}"
                                  # "QPushButton{padding:2px 4px}")
        #https: // www.cnblogs.com/XJT2018/p/9835262.html
        self.label_40.setStyleSheet("QLabel{background:lightyellow;}"
                                    "QLabel{color:rgb(49,54,59)}")
        self.label_41.setStyleSheet("QLabel{background:lightblue;}"
                                    "QLabel{color:rgb(49,54,59)}")
        self.updateButton_4.clicked.connect(self.cdAllinone)
        self.updateButton_4.setStyleSheet("QPushButton{color:black}"
                                          "QPushButton:hover{color:red}"
                                          "QPushButton{background-color:rgb(20,200,255)}")
        self.cdTab.itemDoubleClicked.connect(self.cdTabItemChangedDoubleClicked)



        #OVL
        self.ovlButton1.clicked.connect(self.ovlwrongQuery)
        self.ovlTab.itemDoubleClicked.connect(self.ovlTabItemChangedDoubleClicked)
        self.ovlButton2.clicked.connect(self.ovlQuery)
        self.ovlButton4_2.clicked.connect(self.ovlToBeDownloaded)
        self.ovlLineEdit2_3.setPlaceholderText("请输入工号更新结论")
        self.ovlButton3.clicked.connect(self.updateConclusion)
        self.ovlButton4.clicked.connect(self.updateRefZb)
        self.ovlLineEdit3.setPlaceholderText("请输入被测量层次")
        self.ovlLineEdit4.setPlaceholderText("请输入（复制）套刻坐标，按左下，右下，右上，左上顺序，单位微米，半角逗号分割")
        self.ovlLineEdit5.setPlaceholderText("设备类型LDI或LII")
        self.ovlButton4_5.clicked.connect(self.negativeValue)
        self.ovlButton4_6.clicked.connect(self.identicalZb)

        #Product
        self.partBtn1.clicked.connect(self.partRefresh)
        self.partBtn2.clicked.connect(self.summaryPartData)
        self.partTab.itemDoubleClicked.connect(self.partTabItemChangedDoubleClicked)
        self.partBtn3.clicked.connect(self.byPartQuery)
    def byPartQuery(self):
        pass
        str=self.partLineEdit1.text().strip().upper()
        tmp = Product().queryByPart(part=str)
        DF2TABLE(self.partTab, tmp)
    def partTabItemChangedDoubleClicked(self,item):
        self.nTO = (item.row())
        self.partLineEdit1.setText( self.partTab.item(self.nTO, 0).text())
        self.byPartQuery()

    def partRefresh(self):
        Product().addNewPart()

    def summaryPartData(self):
        tmp = Product().summaryData()
        DF2TABLE(self.partTab, tmp)
    def ovlwrongQuery(self):
        pass
        begin = self.onDateTimeChanged().split(' ')[0].replace('-', '')
        end = self.onDateTimeChanged_2().split(' ')[0].replace('-', '')
        tmp = OVL_Maintain().ovlwrongQuery('PPID', begin, end)
        if tmp.shape[0] > 0:
            DF2TABLE(self.ovlTab, tmp)
        else:
            pass
    def ovlQuery(self):
        pass
        part=self.ovlLineEdit1.text()
        ppid=self.ovlLineEdit2.text()
        tmp = OVL_Maintain().ovlPpidQuery('PPID',part,ppid)
        if tmp.shape[0] > 0:
            DF2TABLE(self.ovlTab, tmp)
        else:
            pass

    def ovlToBeDownloaded(self):
        pass
        tmp = OVL_Maintain().ovlMissingZb()
        # tmp=tmp.sort_values(by='PPID')
        if tmp.shape[0] > 0:
            DF2TABLE(self.ovlTab, tmp)
        else:
            pass
        tmp.to_csv('c:/temp/ZbToBeDownloaded.csv', index=None)
        try:
            tmp.to_csv('z:/ZbToBeDownloaded.csv', index=None,encoding='GBK')
        except:
            pass
        reply = QMessageBox.information(self, "注意", '数据已导出保存在\n\nc:/temp/ZbToBeDownloaded.csv', QMessageBox.Yes, QMessageBox.Yes)
    def negativeValue(self):
        tmp = OVL_Maintain().negativeValue()
        if tmp.shape[0] > 0:
            DF2TABLE(self.ovlTab, tmp)
        else:
            pass
        tmp.to_csv('c:/temp/ovlNegativeValue.csv', index=None)
        try:
            tmp.to_csv('z:/ovlNegativeValue.csv', index=None, encoding='GBK')
        except:
            pass
        reply = QMessageBox.information(self, "注意", '数据已导出保存在\n\nz:/ovlNegativeValue.csv', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def identicalZb(self):
        tmp = OVL_Maintain().identicalZb()
        if tmp[1].shape[0]>0:
            tmp[1].columns = ['PPID','X1','Y1','X2','Y2','X3','Y3','X4','Y4','TOOL','LAYER']
            DF2TABLE(self.ovlTab, tmp[1])
        tmp[1].to_csv('z:/identicalZb.csv')
        reply = QMessageBox.information(self, "注意", '数据已导出保存在\n\nz:/identicalZb.csv', QMessageBox.Yes,
                                        QMessageBox.Yes)

    def ovlTabItemChangedDoubleClicked(self,item):
        self.nTO=(item.row())
        part = self.ovlTab.item(self.nTO, 0).text()
        ppid = self.ovlTab.item(self.nTO,1).text()
        ToolType = self.ovlTab.item(self.nTO, 2).text()
        if str(part).replace('.','').replace('-','').isdigit():
            return
        self.ovlLineEdit1.setText(part)
        self.ovlLineEdit2.setText(ppid)
        self.ovlLineEdit5.setText(ToolType)
        self.refresh_ovlcheck( part, ppid,ToolType)
    def refresh_ovlcheck(self,part,ppid,ToolType):
        if ToolType not in ['LDI','LII']:
            reply= QMessageBox.information(self, "注意",
                                            '设备类型只能输入LDI或LII！！！',
                                            QMessageBox.Yes , QMessageBox.Yes)
            return


        conn = CD_Maintain().openDB()
        sql = "Select A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,Count from ZB where Ppid='" + ppid + "'"
        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
        tmprs.Open(sql, conn, 1, 3)
        if tmprs.recordcount>0:
            recipeZb = [eval(tmprs.fields(k).value) for k in range(int(eval(tmprs.fields('Count').value)))]
        else:
            reply = QMessageBox.information(self, "注意", '未查询到测试坐标数据', QMessageBox.Yes, QMessageBox.Yes)
            return

        sql = "select Ppid from BT Where Ovl_Ppid='" + ppid + "'"
        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
        tmprs.Open(sql, conn, 1, 3)
        if tmprs.recordcount > 0:
            ovlto = tmprs.fields('Ppid').value.replace(' ', '')
        else:
            reply = QMessageBox.information(self, "注意", '未在BiasTable中查询到被测量层次', QMessageBox.Yes, QMessageBox.Yes)
            return

        sql = "select StepX,StepY from STEP_SIZE where Part='" + part + "'"
        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
        tmprs.Open(sql, conn, 1, 3)
        if tmprs.recordcount > 0:
            stepx, stepy = eval(tmprs.fields('StepX').value), eval(tmprs.fields('StepY').value)
        else:
            reply = QMessageBox.information(self, "注意", '未查询到stepping seize', QMessageBox.Yes, QMessageBox.Yes)
            return

        # ToolType= self.ovlTab.item(self.nTO,2).text()

        sql = "Select LDx,LDy,RDx,RDy,RUx,RUy,LUx,LUy from OVL_STANDARD Where Part='" + part + "' and Ppid='" + ovlto + "'"
        tmprs = win32com.client.Dispatch(r'ADODB.Recordset')
        tmprs.Open(sql, conn, 1, 3)
        if tmprs.recordcount > 0:
            refZb = [eval(tmprs.fields('LDx').value), eval(tmprs.fields('LDY').value), eval(tmprs.fields('RDx').value),
                     eval(tmprs.fields('RDY').value), eval(tmprs.fields('RUx').value), eval(tmprs.fields('RUY').value),
                     eval(tmprs.fields('LUx').value), eval(tmprs.fields('LUY').value)]  # 类似MIM层次，CE命名不规范，部分ovlto下有多组坐标

        if part[-2:].upper() == "-L" and ToolType.upper() == 'LII':
            comZb=['' for i in range(8)]
            delta=['' for i in range(8)]
            delta[0] =round( ((refZb[0] / 1000 + stepy / 4) - recipeZb[0]),3)
            comZb[0] =round( (refZb[0] / 1000 + stepy / 4),3)

            delta[1] =round( ((refZb[1] / 1000 + stepx / 2) - recipeZb[1]),3)
            comZb[1] =round( (refZb[1] / 1000 + stepx / 2),3)

            delta[2] =round( ((refZb[2] / 1000 + stepy / 4) - recipeZb[2]),3)
            comZb[2] =round( (refZb[2] / 1000 + stepy / 4),3)

            delta[3] =round( ((refZb[3] / 1000 + stepx / 2) - recipeZb[3]),3)
            comZb[3] =round( (refZb[3] / 1000 + stepx / 2),3)

            delta[4] =round( ((refZb[4] / 1000 + stepy / 4) - recipeZb[4]),3)
            comZb[4] =round( (refZb[4] / 1000 + stepy / 4),3)

            delta[5] =round( ((refZb[5] / 1000 + stepx / 2) - recipeZb[5]),3)
            comZb[5] =round( (refZb[5] / 1000 + stepx / 2),3)

            delta[6] =round( ((refZb[6] / 1000 + stepy / 4) - recipeZb[6]),3)
            comZb[6] =round( (refZb[6] / 1000 + stepy / 4),3)

            delta[7] =round( ((refZb[7] / 1000 + stepx / 2) - recipeZb[7]),3)
            comZb[7] =round( (refZb[7] / 1000 + stepx / 2),3)

            tmp = pd.DataFrame([refZb,comZb,recipeZb,delta]).T
            tmp.columns=['gdsZB','calZB','recipeZB','delta']
            DF2TABLE(self.ovlTab, tmp)



        elif part[-2:].upper() == "-L" and ToolType.upper() == 'LDI':
            if len(recipeZb) == 16:
                comZb = ['' for i in range(16)]
                delta = ['' for i in range(16)]
                delta[0] = round((refZb[3] / 1000 + stepx / 2) - recipeZb[0],3)
                delta[1] = round((-refZb[2] / 1000 + stepy / 4) - recipeZb[1],3)

                delta[2] = round((refZb[5] / 1000 + stepx / 2) - recipeZb[2],3)
                delta[3] = round((-refZb[4] / 1000 + stepy / 4) - recipeZb[3],3)

                delta[4] = round((refZb[7] / 1000 + stepx / 2) - recipeZb[4],3)
                delta[5] = round((-refZb[6] / 1000 + stepy * 3 / 4) - recipeZb[5],3)

                delta[6] = round((refZb[1] / 1000 + stepx / 2) - recipeZb[6],3)
                delta[7] = round((-refZb[0] / 1000 + stepy * 3 / 4) - recipeZb[7],3)

                delta[8] = round((refZb[3] / 1000 + stepx / 2) - recipeZb[8],3)
                delta[9] = round((-refZb[2] / 1000 + stepy * 3 / 4) - recipeZb[9],3)

                delta[10] = round((refZb[5] / 1000 + stepx / 2) - recipeZb[10],3)
                delta[11] = round((-refZb[4] / 1000 + stepy * 3 / 4) - recipeZb[11],3)

                delta[12] = round((refZb[7] / 1000 + stepx / 2) - recipeZb[12],3)
                delta[13] = round((-refZb[6] / 1000 + stepy / 4) - recipeZb[13],3)

                delta[14] =round((refZb[1] / 1000 + stepx / 2) - recipeZb[14],3)
                delta[15] = round((-refZb[0] / 1000 + stepy / 4) - recipeZb[15],3)

                comZb[0] = round((refZb[3] / 1000 + stepx / 2), 3)
                comZb[1] = round((-refZb[2] / 1000 + stepy / 4) , 3)

                comZb[2] = round((refZb[5] / 1000 + stepx / 2) , 3)
                comZb[3] = round((-refZb[4] / 1000 + stepy / 4) , 3)

                comZb[4] = round((refZb[7] / 1000 + stepx / 2) , 3)
                comZb[5] = round((-refZb[6] / 1000 + stepy * 3 / 4), 3)

                comZb[6] = round((refZb[1] / 1000 + stepx / 2) , 3)
                comZb[7] = round((-refZb[0] / 1000 + stepy * 3 / 4) , 3)

                comZb[8] = round((refZb[3] / 1000 + stepx / 2) , 3)
                comZb[9] = round((-refZb[2] / 1000 + stepy * 3 / 4) , 3)

                comZb[10] = round((refZb[5] / 1000 + stepx / 2) , 3)
                comZb[11] = round((-refZb[4] / 1000 + stepy * 3 / 4) , 3)

                comZb[12] = round((refZb[7] / 1000 + stepx / 2) , 3)
                comZb[13] = round((-refZb[6] / 1000 + stepy / 4), 3)

                comZb[14] = round((refZb[1] / 1000 + stepx / 2) , 3)
                comZb[15] = round((-refZb[0] / 1000 + stepy / 4) , 3)
        else:
            pass
            comZb = ['' for i in range(8)]
            delta = ['' for i in range(8)]
            delta[0] = round((refZb[0] / 1000 + stepx / 2) - recipeZb[0],3)
            delta[1] = round((refZb[1] / 1000 + stepy / 2) - recipeZb[1],3)

            delta[2] = round((refZb[2] / 1000 + stepx / 2) - recipeZb[2],3)
            delta[3] = round((refZb[3] / 1000 + stepy / 2) - recipeZb[3],3)

            delta[4] = round((refZb[4] / 1000 + stepx / 2) - recipeZb[4],3)
            delta[5] = round((refZb[5] / 1000 + stepy / 2) - recipeZb[5],3)

            delta[6] = round((refZb[6] / 1000 + stepx / 2) - recipeZb[6],3)
            delta[7] = round((refZb[7] / 1000 + stepy / 2) - recipeZb[7],3)

            comZb[0] = round((refZb[0] / 1000 + stepx / 2) , 3)
            comZb[1] = round((refZb[1] / 1000 + stepy / 2) , 3)

            comZb[2] = round((refZb[2] / 1000 + stepx / 2) , 3)
            comZb[3] = round((refZb[3] / 1000 + stepy / 2) , 3)

            comZb[4] = round((refZb[4] / 1000 + stepx / 2), 3)
            comZb[5] = round((refZb[5] / 1000 + stepy / 2) , 3)

            comZb[6] = round((refZb[6] / 1000 + stepx / 2) , 3)
            comZb[7] = round((refZb[7] / 1000 + stepy / 2) , 3)
        tmp = pd.DataFrame([refZb,comZb,recipeZb,delta]).T
        tmp.columns=['gdsZB','calZB','recipeZB','delta']
        tmp['Part']=part
        tmp['Ppid']=ppid
        if tmp.shape[0]==16:
            tmp['Location']=['x1','y1','x2','y2','x3','y3','x4','y4','x5','y5','x6','y6','x7','y7','x8','y8']
        else:
            tmp['Location'] = ['x1', 'y1', 'x2', 'y2', 'x3', 'y3', 'x4', 'y4']
        list1 = list(tmp['delta'])
        x = [ list1[i] for i in range(len(list1)) if i%2==0]
        y = [ list1[i] for i in range(len(list1)) if i%2==1]
        rng=['' for i in range(tmp.shape[0])]
        rng[0]='RangeX: ' +str( round((pd.DataFrame(x).describe().iloc[7,0]-pd.DataFrame(x).describe().iloc[3,0]),3))
        rng[1]='RangeY: ' +str( round((pd.DataFrame(y).describe().iloc[7,0]-pd.DataFrame(y).describe().iloc[3,0]),3))
        tmp['RangeCheck']=rng
        DF2TABLE(self.ovlTab, tmp)
    def updateConclusion(self):
        employeeid=self.ovlLineEdit2_3.text()
        if employeeid not in ['11067402', '20546313', '20934636', '11067305', '20570125', '11065552', '21005745',
                              '11066347', '20785358', '20082601', '11068434', '20972339', '21023923', '11067475',
                              '11068246', '20934644', '20590158', '20068437', '20740048', '21057480', '11067846',
                              '11068114', '20217740', '11067223', '11068127', '20491831', '11067904', '11065627',
                              '20059603', '11067071', '21035183', '21057465', '20480780', '20128671', '20128633']:
            reply = QMessageBox.information(self, "注意", '工号不匹配，请重新输入', QMessageBox.Yes, QMessageBox.Yes)
            return
        f1 = self.ovlRadio1.isChecked()
        f2 = self.ovlRadio2.isChecked()
        if f1^f2 == False:
            reply = QMessageBox.information(self, "注意", '请确定结论正确与否', QMessageBox.Yes, QMessageBox.Yes)
            return
        if f1==True:
            conclusion='正确'
        else:
            conclusion='错误'
        part=self.ovlLineEdit1.text().strip().upper()
        ppid = self.ovlLineEdit2.text().strip().upper()

        reply = QMessageBox.information(self, "注意",
                                        'Part：' + part + '\n\nPPID: ' + ppid + '\n\n更新结论是: ' + conclusion +  '！！\n\n请确认！！',
                                        QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
        if reply == QMessageBox.Yes:
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
            conn.Open(DSN)
            sql = "Update PPID SET Conclusion='" + conclusion + "',ReviewedDate='" + QDateTime.currentDateTime().toString("yyyy-MM-dd HH:mm:ss")
            sql = sql + "',EmployeeID='" +employeeid + "' where PART='" + part + "' and OVL='"+ppid+"'"
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)
            if conclusion=='错误':
                pass
                # rs = win32com.client.Dispatch(r'ADODB.Recordset')
                # sql = "Update ZB SET Obselete=True where Part='" + part + "' and Ppid='" + ppid + "'"
                # sql = "Update ZB SET Obselete=True where Ppid='" + ppid + "'"
                # print(sql)
                # rs.Open(sql, conn, 1, 3)
            conn.close
    def updateRefZb(self):
        part = self.ovlLineEdit1.text().strip().upper()
        ovl_ppid = self.ovlLineEdit2.text().strip().upper()
        ppid = (ovl_ppid[-2:] + '-' + self.ovlLineEdit3.text().strip()).upper()
        ToolType=self.ovlLineEdit5.text().strip().upper()


        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
        conn.Open(DSN)
        #更新bias table中测量顺序
        sql = "select * from BT where Part='" + part + "' and Ovl_Ppid='"+ovl_ppid + "'"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount>0:
            reply = QMessageBox.information(self, "注意",
                                            'Bias Table中已定义套刻测量顺序为' +rs.fields('Ppid').value + '\n\n请确认是否需要更新！！！',
                                            QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
            if reply== QMessageBox.Yes:
                sql = "update BT set Ppid='" + ppid + "' where Part='" + part + "' and Ovl_Ppid='"+ovl_ppid+"'"
                rs = win32com.client.Dispatch(r'ADODB.Recordset')
                rs.Open(sql, conn, 1, 3)
        else:
            reply= QMessageBox.information(self, "注意",
                                            'Bias Table中拟加入新数据，请确认！！！',
                                            QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
            if reply==QMessageBox.Yes:
                rs.AddNew()  # 添加一条新记录
                rs.Fields('Path').Value = "manual revised or added @ "+ QDateTime.currentDateTime().toString("yyyy-MM-dd HH:mm:ss")
                rs.Fields('Part').Value = part
                rs.Fields('Ppid').Value = ppid
                rs.Fields('Ovl_Ppid').Value = ovl_ppid
                rs.Update()  # 更新
        #更新标准坐标

        sql = "select * from OVL_STANDARD where Part='" + part + "' and Ppid='" + ppid +"'"
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        if rs.recordcount>0:
            reply = QMessageBox.information(self, "注意", 'OVL_STANDARD表中已存在数据，未更新！！！',
                                            QMessageBox.Yes , QMessageBox.Yes)
        else:
            reply = QMessageBox.information(self, "注意", 'OVL_STANDARD表中中拟加入新数据，请确认！！！', QMessageBox.Yes | QMessageBox.No,
                                            QMessageBox.No)

            if reply == QMessageBox.Yes:
                zbstr = self.ovlLineEdit4.text().split(',')
                if len(zbstr)==8:
                    rs.AddNew()  # 添加一条新记录
                    rs.Fields('Path').Value = "manual revised or added @ " + QDateTime.currentDateTime().toString(
                        "yyyy-MM-dd HH:mm:ss")
                    rs.Fields('Part').Value = part
                    rs.Fields('Ppid').Value = ppid
                    rs.Fields('LDx').Value = zbstr[0]
                    rs.Fields('LDy').Value = zbstr[1]
                    rs.Fields('RDx').Value = zbstr[2]
                    rs.Fields('RDy').Value = zbstr[3]
                    rs.Fields('RUx').Value = zbstr[4]
                    rs.Fields('RUy').Value = zbstr[5]
                    rs.Fields('LUx').Value = zbstr[6]
                    rs.Fields('LUy').Value = zbstr[7]
                    rs.Update()  # 更新
                else:
                    reply = QMessageBox.information(self, "注意", '输入的坐标非8个，退出，未更新！！！',
                                                    QMessageBox.Yes, QMessageBox.Yes)

        conn.close
        print(part,ovl_ppid)
        self.refresh_ovlcheck(part,ovl_ppid,ToolType)




    def ampQuery(self):
        pass
        begin=self.onDateTimeChanged().split(' ')[0].replace('-','')
        end=self.onDateTimeChanged_2().split(' ')[0].replace('-','')
        tmp = CD_Maintain().ampQuery('AMP',begin,end)
        if tmp.shape[0]>0:
            DF2TABLE(self.cdTab,tmp)
        else:
            pass
    def idpQuery(self):
        pass
        begin=self.onDateTimeChanged().split(' ')[0].replace('-','')
        end=self.onDateTimeChanged_2().split(' ')[0].replace('-','')
        tmp = CD_Maintain().idpQuery('IDP',begin,end)
        if tmp.shape[0]>0:
            DF2TABLE(self.cdTab,tmp)
        else:
            pass
    def idpUpdate(self):

        try:
            ID=self.cdLineEdit1.text().strip()
            EmpolyeeID = self.cdLineEdit2.text().strip()
            IDW = self.cdTab.item(self.nTO, 0).text()
            IDP = self.cdTab.item(self.nTO, 1).text()

            if EmpolyeeID not in ['11067402','20546313','20934636',
                            '11067305','20570125','11065552','21005745',
                            '11066347','20785358','20082601','11068434',
                            '20972339','21023923','11067475','11068246',
                            '20934644','20590158','20068437','20740048',
                            '21057480','11067846','11068114','20217740',
                            '11067223','11068127','20491831','11067904',
                            '11065627','20059603','11067071','21035183',
                            '21057465','20480780','20128671','20128633']:
                reply = QMessageBox.information(self, "注意", '工号不匹配，请重新输入',
                                                QMessageBox.Yes, QMessageBox.Yes)

                return
        except:
            reply = QMessageBox.information(self, "注意", '请选择ID，更改（补充）工艺代码。。。。。', QMessageBox.Yes, QMessageBox.Yes)




        if ID.isdigit():
            if self.cdRadio1.isChecked():
                reply=QMessageBox.information(self,"注意",
                                              '选择的查询ID是：' + ID +
                                              '\n\n判断结论是： 正确！'
                                              '\n\n更新人工号是：'+ EmpolyeeID +
                                              '\n\n同一IDW，IDP下的其它数据会被表示为错误！',
                                              QMessageBox.Yes | QMessageBox.No ,QMessageBox.No)
                if reply == QMessageBox.Yes:
                    conn = CD_Maintain().openDB()
                    rs = win32com.client.Dispatch(r'ADODB.Recordset')
                    sql = "UPDATE IDP SET Reviewed=True, Conclusion=True,ReviewDate= '" + QDateTime.currentDateTime().toString(
                        "yyyy-MM-dd HH:mm:ss") + "',EmployeeID=" + EmpolyeeID + " WHERE ID=" + ID
                    rs.Open(sql, conn, 1, 3)
                    #revise others -->false
                    sql = "UPDATE IDP SET Reviewed=True, Conclusion=False,ReviewDate= '" + QDateTime.currentDateTime().toString(
                        "yyyy-MM-dd HH:mm:ss") + "',EmployeeID=" + EmpolyeeID + " WHERE ID<>" + ID
                    sql = sql + " AND IDW='" + IDW +"' AND IDP='" + IDP + "'"
                    rs = win32com.client.Dispatch(r'ADODB.Recordset')
                    rs.Open(sql, conn, 1, 3)
                    #list results
                    sql = "SELECT * FROM IDP WHERE IDW='" + IDW +"' AND IDP='" + IDP + "' ORDER BY ID"
                    rs = win32com.client.Dispatch(r'ADODB.Recordset')
                    rs.Open(sql, conn, 1, 3)
                    tmp1 = [rs.fields(i).name for i in range(rs.fields.count)]
                    tmp2=[]
                    for j in range(rs.recordcount):
                        tmp2.append( [rs.fields(i).value for i in range(rs.fields.count)])
                        if rs.recordcount !=1 or j != rs.recordcount-1:
                            rs.movenext
                    tmp = pd.DataFrame(tmp2)
                    if tmp.shape[0]==1:
                        tmp=tmp.T
                    tmp.columns=tmp1
                    conn.close
                    DF2TABLE(self.cdTab,tmp)
                else:
                    reply = QMessageBox.information(self, "注意", '退出，重新确认', QMessageBox.Yes , QMessageBox.Yes)
            else:
                conn = CD_Maintain().openDB()
                rs = win32com.client.Dispatch(r'ADODB.Recordset')
                sql = "UPDATE IDP SET Reviewed=True, Conclusion=False,ReviewDate= '" + QDateTime.currentDateTime().toString(
                    "yyyy-MM-dd HH:mm:ss") + "',EmployeeID=" + EmpolyeeID + " WHERE ID=" + ID
                rs.Open(sql, conn, 1, 3)

                sql = "SELECT * FROM IDP WHERE IDW='" + IDW + "' AND IDP='" + IDP + "' ORDER BY ID"
                rs = win32com.client.Dispatch(r'ADODB.Recordset')
                rs.Open(sql, conn, 1, 3)
                tmp1 = [rs.fields(i).name for i in range(rs.fields.count)]
                tmp2 = []
                for j in range(rs.recordcount):
                    tmp2.append([rs.fields(i).value for i in range(rs.fields.count)])
                    if rs.recordcount != 1 or j != rs.recordcount - 1:
                        rs.movenext
                tmp = pd.DataFrame(tmp2)
                if tmp.shape[0] == 1:
                    tmp = tmp.T
                tmp.columns = tmp1
                conn.close
                DF2TABLE(self.cdTab, tmp)
        else:
            reply = QMessageBox.information(self, "注意", 'ID非数字，请重新选择', QMessageBox.Yes, QMessageBox.Yes)
    def ampUpdate(self):
        try:
            Tech,cdType='',''
            ID=self.cdLineEdit3_5.text().strip()
            EmpolyeeID = self.cdLineEdit2.text().strip()
            IDW = self.cdTab.item(self.nTO, 1).text()
            IDP = self.cdTab.item(self.nTO, 2).text()
            Flag1 = self.cdTab.item(self.nTO, 4).text()
            Tech = self.cdLineEdit4_3.text()

            if self.cdRadio3.isChecked():
                cdType='Line'
            elif self.cdRadio4.isChecked():
                cdType='Hole/Space'
            else:
                pass
            if EmpolyeeID not in ['11067402','20546313','20934636',
                            '11067305','20570125','11065552','21005745',
                            '11066347','20785358','20082601','11068434',
                            '20972339','21023923','11067475','11068246',
                            '20934644','20590158','20068437','20740048',
                            '21057480','11067846','11068114','20217740',
                            '11067223','11068127','20491831','11067904',
                            '11065627','20059603','11067071','21035183',
                            '21057465','20480780','20128671','20128633'] \
                    or len(cdType.strip())==0 or len(Tech.strip())==0:
                reply = QMessageBox.information(self, "注意", '工号，工艺代码，条宽类型数据有误，请确认',
                                                QMessageBox.Yes, QMessageBox.Yes)
                return
        except:
            reply = QMessageBox.information(self, "注意", '请选择ID，更改（补充）工艺代码。。。。。', QMessageBox.Yes, QMessageBox.Yes)
            return


        if  not Tech[1].isdigit():
            reply=QMessageBox.information(self, "注意", '工艺代码第二位必须为数字，请确认', QMessageBox.Yes, QMessageBox.Yes)
            return
        else:
            reply = QMessageBox.information(self, "注意", '刷新信息是：\n\n' +
                                            '记录ID是：' + ID +
                                            '\n\n记录类型是：' + Flag1 +
                                            '\n\nIDW是：' + IDW +
                                            '\n\nIDP是：' + IDP +
                                            '\n\n工艺代码是：' + Tech +
                                            '\n\n条宽类型是：' + cdType
                                            , QMessageBox.Yes|QMessageBox.No, QMessageBox.No)

            if reply == QMessageBox.Yes:
                # OPEN DB
                conn = win32com.client.Dispatch(r"ADODB.Connection")
                DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
                conn.Open(DSN)
                # GOLDEN AMP
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
                # AMP RECORD
                print(Flag1)
                if Flag1 in ['Added', 'Default','Revised']:
                    sql = "SELECT * FROM AMP WHERE ID=" + ID
                    rs = win32com.client.Dispatch(r'ADODB.Recordset')
                    rs.Open(sql, conn, 1, 3)
                    tmp1 = [rs.fields(i).name for i in range(0,rs.fields.count)]
                    tmp2 = [rs.fields(i).value for i in range(0,rs.fields.count)]
                    amp = pd.DataFrame(tmp2).T
                    amp.columns=tmp1
                    AutoCheck =  CD_Maintain().ampUpdate1(ref,refCol,amp,Tech,cdType)
                    DF2TABLE(self.cdTab, AutoCheck[1])
                    conn.close
                else:
                    pass
            else:
                return
        #             conn = CD_Maintain().openDB()
        #             rs = win32com.client.Dispatch(r'ADODB.Recordset')
        #             sql = "UPDATE IDP SET Reviewed=True, Conclusion=True,ReviewDate= '" + QDateTime.currentDateTime().toString(
        #                 "yyyy-MM-dd HH:mm:ss") + "',EmployeeID=" + EmpolyeeID + " WHERE ID=" + ID
        #             rs.Open(sql, conn, 1, 3)
        #             print('001',sql)
        #             #revise others -->false
        #             sql = "UPDATE IDP SET Reviewed=True, Conclusion=False,ReviewDate= '" + QDateTime.currentDateTime().toString(
        #                 "yyyy-MM-dd HH:mm:ss") + "',EmployeeID=" + EmpolyeeID + " WHERE ID<>" + ID
        #             sql = sql + " AND IDW='" + IDW +"' AND IDP='" + IDP + "'"
        #             rs = win32com.client.Dispatch(r'ADODB.Recordset')
        #             print('002',sql)
        #             rs.Open(sql, conn, 1, 3)
        #             #list results
        #             sql = "SELECT * FROM IDP WHERE IDW='" + IDW +"' AND IDP='" + IDP + "' ORDER BY ID"
        #             rs = win32com.client.Dispatch(r'ADODB.Recordset')
        #             rs.Open(sql, conn, 1, 3)
        #             print('003',sql)
        #             tmp1 = [rs.fields(i).name for i in range(rs.fields.count)]
        #             tmp2=[]
        #             for j in range(rs.recordcount):
        #                 tmp2.append( [rs.fields(i).value for i in range(rs.fields.count)])
        #                 if rs.recordcount !=1 or j != rs.recordcount-1:
        #                     rs.movenext
        #             tmp = pd.DataFrame(tmp2)
        #             if tmp.shape[0]==1:
        #                 tmp=tmp.T
        #             tmp.columns=tmp1
        #             conn.close
        #             DF2TABLE(self.cdTab,tmp)
        #         else:
        #             reply = QMessageBox.information(self, "注意", '退出，重新确认', QMessageBox.Yes , QMessageBox.Yes)
        #     else:
        #         conn = CD_Maintain().openDB()
        #         rs = win32com.client.Dispatch(r'ADODB.Recordset')
        #         sql = "UPDATE IDP SET Reviewed=True, Conclusion=False,ReviewDate= '" + QDateTime.currentDateTime().toString(
        #             "yyyy-MM-dd HH:mm:ss") + "',EmployeeID=" + EmpolyeeID + " WHERE ID=" + ID
        #         rs.Open(sql, conn, 1, 3)
        #
        #         sql = "SELECT * FROM IDP WHERE IDW='" + IDW + "' AND IDP='" + IDP + "' ORDER BY ID"
        #         print(sql)
        #         rs = win32com.client.Dispatch(r'ADODB.Recordset')
        #         rs.Open(sql, conn, 1, 3)
        #         print(rs.recordcount)
        #         tmp1 = [rs.fields(i).name for i in range(rs.fields.count)]
        #         tmp2 = []
        #         for j in range(rs.recordcount):
        #             tmp2.append([rs.fields(i).value for i in range(rs.fields.count)])
        #             if rs.recordcount != 1 or j != rs.recordcount - 1:
        #                 rs.movenext
        #         tmp = pd.DataFrame(tmp2)
        #         if tmp.shape[0] == 1:
        #             tmp = tmp.T
        #         tmp.columns = tmp1
        #         conn.close
        #         DF2TABLE(self.cdTab, tmp)
        # else:
        #     reply = QMessageBox.information(self, "注意", 'ID非数字，请重新选择', QMessageBox.Yes, QMessageBox.Yes)
    def ampUpdate1(self):
        if (not self.radioResult11.isChecked()) and (not self.radioResult21.isChecked()):
            reply = QMessageBox.information(self, "注意", '未确认程序正确与否，请重新选择', QMessageBox.Yes, QMessageBox.Yes)
            return
        if self.radioResult21.isChecked():
            Conclusion='正确'
        else:
            Conclusion='错误'
        if Conclusion=='正确' and self.cdTab.item(0,2).text()=='Wrong':
            KeeyWrong=True

        reply = QMessageBox.information(self, "注意", 'ID：'+self.cdTab.item(0,0).text() +
                                        '\n\nIDW: ' + self.cdTab.item(0,3).text() +
                                        '\n\nIDP: ' + self.cdTab.item(0,4).text() +
                                        '\n\n将被标记为：' + Conclusion + '！！\n\n请确认！！',
                                        QMessageBox.Yes|QMessageBox.No, QMessageBox.No)
        if reply == QMessageBox.Yes:
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + 'z:/_database/lithotool.mdb'
            conn.Open(DSN)
            Tech = self.cdLineEdit4_3.text()
            if self.cdRadio3.isChecked():
                cdType = 'Line'
            elif self.cdRadio4.isChecked():
                cdType = 'Hole/Space'
            else:
                pass
            sql = "Update AMP SET Conclusion='" + Conclusion
            sql = sql + "',Reviewed=True,ReviewDate= '" + QDateTime.currentDateTime().toString("yyyy-MM-dd HH:mm:ss")
            sql = sql + "', Tech='"+Tech+"',Type='"+cdType
            sql = sql + "',EmployeeID=" + str(int(self.cdLineEdit2.text()))
            sql = sql + ",KeepWrong=True WHERE ID =" + self.cdTab.item(0,0).text()

            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)



            if Conclusion=='正确':
                sql =  "Update AMP SET Conclusion='错误',Reviewed=True,ReviewDate= '" + QDateTime.currentDateTime().toString("yyyy-MM-dd HH:mm:ss")
                sql = sql + "' Where ID <>" + self.cdTab.item(0,0).text()
                sql = sql + " and IDW='"+self.cdTab.item(0,3).text()
                sql = sql + "' and IDP='"+self.cdTab.item(0,4).text()+"'"
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)
            # sql = "select ID,IDW,IDP,Date,Reviewed,ReviewDate,AutoCheck,KeepWrong,Conclusion from AMP where ID = "+self.cdTab.item(0,0).text()
            sql = "select ID,IDW,IDP,Tech,Type,TOOL,Date,Reviewed,ReviewDate,AutoCheck,KeepWrong,Conclusion,EmployeeID from AMP where IDW = '"+self.cdTab.item(0,3).text()
            sql = sql + "' and IDP='"+self.cdTab.item(0,4).text()+ "'"
            print(sql)

            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)
            tmp1=[]
            if rs.recordcount==1:
                tmp = [ rs.fields(i).value for i in range(rs.fields.count)]
                tmp = pd.DataFrame(tmp).T
            else:
                for n in range(rs.recordcount):
                    tmp = [rs.fields(i).value for i in range(rs.fields.count)]
                    tmp1.append(tmp)
                    rs.movenext
                tmp=pd.DataFrame(tmp1)
            tmp.columns=['ID','IDW','IDP','Tech','Type','TOOL','Date','Reviewed','ReviewDate','AutoCheck','KeepWrong','Conclusion','EmployeeID']
            DF2TABLE(self.cdTab, tmp)
            conn.close
    def cdTabItemChangedDoubleClicked(self,item):
        self.nTO=(item.row())
        if item.column()==13:
            if self.cdTab.item(0,4).text().isdigit():
                self.cdLineEdit1.setText(item.text())
                self.cdLineEdit3_5.setText('')
                self.cdLineEdit4_3.setText('')

            else:
                reply = QMessageBox.information(self, "注意", 'ID列选择错误，重选', QMessageBox.Yes, QMessageBox.Yes)
        elif item.column() in [0,10,9]:
            if self.cdTab.item(0,0).text().isdigit():
                self.cdLineEdit3_5.setText(self.cdTab.item(item.row(), 0).text())
                self.cdLineEdit4_3.setText(self.cdTab.item(item.row(), 9).text())

                self.cdLineEdit1.setText('')
            else:
                reply = QMessageBox.information(self, "注意", 'ID列选择错误，重选', QMessageBox.Yes, QMessageBox.Yes)
        else:
            reply = QMessageBox.information(self, "注意", 'ID列选择错误，退出', QMessageBox.Yes, QMessageBox.Yes)
            return
    def cdAllinone(self):
        tmp = CD_Maintain().cdAllinone()
        if tmp.shape[0]>0:
            DF2TABLE(self.cdTab,tmp)
        else:
            pass

        tmp.to_csv('c:/temp/WrongAMP.csv',index=None)


    def onDateTimeChanged(self):
        sDateTime = self.dateTimeEdit.dateTime().toString("yyyy-MM-dd HH:mm:ss")
        return sDateTime
    def onDateTimeChanged_2(self):
        eDateTime = self.dateTimeEdit_2.dateTime().toString("yyyy-MM-dd HH:mm:ss")
        return eDateTime
    def exit(self):
        try:
            self.close()
        except:
            pass
    def onExBtn(self):
        self.part = self.exLineEdit1_2.text().strip()
        if self.exLineEdit1_2.text().strip() =="":
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请在产品名称栏未输入产品名')
            self.box.showNormal()
            return
        self.f1 = self.exRadioButton_3.isChecked()  # ->Old file
        self.f2 = self.exRadioButton_4.isChecked()  # ->New file
        if self.f1 ^ self.f2 == False:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请选择程序类型')
            self.box.showNormal()
            return
        if os.path.exists('p:/_flow/' + self.part + '.xls')==False:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请保存产品流程')
            self.box.showNormal()
            return
        if os.path.exists('P:/recipe/biastable/' + self.part + '.xls')==False:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请保存BiasTable')
            self.box.showNormal()
            return
        tmp = xlrd.open_workbook('p:/_flow/' + self.part + '.xls')
        self.exComboBox.clear()
        self.exComboBox.addItems(tmp.sheet_names())
        tmp = xlrd.open_workbook('P:/recipe/biastable/' + self.part + '.xls')
        self.exComboBox_2.clear()
        self.exComboBox_2.addItems(tmp.sheet_names())
    def onExCombo(self):# read flow
        try:
            self.dfF = pd.read_excel('p:/_flow/' + self.part + '.xls',sheet_name=self.exComboBox.currentText(),
                                     index_col=None)

            self.dfF=self.dfF.reset_index()
            self.dfF = self.dfF.fillna('')
            self.dfF.columns=[int(i) for i in range(self.dfF.shape[1])]
            for i in range(30):
                tmp = list(self.dfF.loc[i])
                if '步骤号' in tmp or 'PPID' in tmp or  'EQPTYPE' in tmp or  'MASK' in tmp:
                    self.dfF = self.dfF.drop([x for x in range(i)])
                    no1 = tmp.index("EQPTYPE")
                    no2 = tmp.index('CallName')
                    if self.dfF.iloc[0,no2-1] == '步骤号':
                        self.dfF = self.dfF[[no2-2,no1-2,no1-1,no1,no1+2,no1+3]]
                    else:
                        self.dfF = self.dfF[[no2 - 1, no1 - 2, no1 - 1, no1, no1 + 2, no1 + 3]]
                    self.dfF.columns = ['0','8','9','10','12','13']


            self.dfF = self.dfF[(self.dfF['10'].str.contains('LII') | self.dfF['10'].str.contains('LDI') )]
            tmp=[]
            for i in self.dfF.index:
                if  self.dfF.loc[i][0] =='删除':
                    tmp.append(i)
            self.dfF = self.dfF.drop(tmp)
            self.dfF = self.dfF.applymap(lambda x: str(x).strip().upper())
            self.dfF= self.dfF.reset_index().drop(columns='index')


            for i in range(self.dfF.shape[0]):
                if len(self.dfF.iloc[i,2] )==0:
                    self.dfF.iloc[i, 2]=self.dfF.iloc[i,1]
                if self.dfF.iloc[i,5]=='':
                    self.dfF.iloc[i, 5]=self.dfF.iloc[i,4]
                self.dfF.iloc[i, 1]=self.dfF.iloc[i, 2].split('.')[1]
                self.dfF.iloc[i, 4] = self.dfF.iloc[i, 2].split(';')[0]
            self.dfF.columns=['Type','Layer',"PPID",'Tool','Track','Mask']
            self.dfF = self.dfF.applymap(lambda x: str(x).strip().upper())
            self.SHOW_EXCEL(xlsdata=self.dfF)

        except:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '流程读取错误，请检查原始Excel文件')
            self.box.showNormal()

    def SHOW_EXCEL(self,xlsdata):
        try:  # 显示数据在表格中
            self.exTable.clear()
            self.exTable.setColumnCount(xlsdata.shape[1])
            self.exTable.setRowCount(xlsdata.shape[0])
            self.exTable.setHorizontalHeaderLabels(xlsdata.columns)

            # self.table.setEditTriggers(QTableWidget.NoEditTriggers)#单元格不可编辑
            # self.table.setSelectionBehavior(QTableWidget.SelectRows)  #选中列还是行，这里设置选中行
            # self.table.setSelectionMode(QTableWidget.SingleSelection) #只能选中一行或者一列
            # self.table.horizontalHeader().setStretchLastSection(True)  #列宽度占满表格(最后一个列拉伸处理沾满表格)
            # newItem.setForeground(QBrush(QColor(255, 0, 0)))
            for i in range(xlsdata.shape[0]):
                for j in range(xlsdata.shape[1]):
                    newItem = QTableWidgetItem(str(xlsdata.iloc[i, j]))
                    newItem.setTextAlignment(Qt.AlignCenter)
                    if i % 2 == 1:
                        newItem.setBackground(QColor('lightcyan'))
                        # newItem.setBackground(QColor('Lime'))
                    self.exTable.setItem(i, j, newItem)
        except:
            self.box = QMessageBox(QMessageBox.Warning, '注意', 'xls表格显示异常')
            self.box.showNormal()
    def onExCombo_2(self):# read BiasTable
        try:
            self.dfB = pd.read_excel('P:/recipe/biastable/' + self.part + '.xls',
                                     skiprows=[0,1,2],
                                     sheet_name=self.exComboBox_2.currentText())

            self.dfB.columns=[str(i) for i in range(self.dfB.shape[1])]
            self.fulltech = (self.dfB.iloc[0,0]).strip()

            UPDATE_TECH(self.part, self.fulltech)


            self.dfB = self.dfB.fillna('')
            self.dfB = self.dfB[self.dfB['0'].str.contains(self.fulltech[0:3])]
            self.dfB = self.dfB[['2','3','4','5','6','17','19']]

            # self.dfB = self.dfB.fillna('')
            self.dfB = self.dfB.applymap(lambda x: str(x).upper().strip())
            self.dfB= self.dfB.reset_index().drop(columns='index')
            self.dfB.columns=['Stage',"Layer",'Mask','MLM','Label','Tool','OvlTo']
            for i in range(self.dfB.shape[0]):
                if 'A' in self.dfB.iloc[i,5] or '26' in str(self.dfB.iloc[i,5]):
                    self.dfB.iloc[i,5]='LDI'
                else:
                    self.dfB.iloc[i,5]='LII'
            self.SHOW_EXCEL(xlsdata=self.dfB)
        except:
            self.box = QMessageBox(QMessageBox.Warning, '注意', 'BiasTable读取错误，请检查原始Excel文件')
            self.box.showNormal()
    def onExBtn_2(self):
        try:
            if self.dfF.shape[1]==6 and self.dfB.shape[1]==7:
                pass
        except:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未正确读取流程和BiasTable的EXCEL文件')
            self.box.showNormal()
            return
        self.NA_SIGMA()
        self.ALIGN_TREE()
        self.MARK_CHOICE()
        if self.f2==True:
            if os.path.exists('P:/Recipe/Coordinate/' + self.part + '.txt'):
                self.GET_COORDINATE()
            else:
                # self.box = QMessageBox(QMessageBox.Warning, '注意', '坐标文件不存在，脚本继续执行')
                # self.box.showNormal()
                reply = QMessageBox.information(self, "注意", '坐标文件不存在，脚本继续执行', QMessageBox.Yes, QMessageBox.Yes)
        self.SHOW_TABLE()
    def NA_SIGMA(self):
        pass
        self.df = pd.merge(self.dfB, self.dfF, how='left', on='Layer')
        self.df = self.df[['Stage', 'Layer', 'Mask_x', 'MLM', 'Label', 'Tool_x', 'OvlTo', 'Track']]
        self.df.columns = ['Stage', 'Layer', 'Mask', 'MLM', 'Label', 'Tool', 'OvlTo', 'Track']
        self.df=self.df.fillna(' ')
        try:  # 工艺，层次，trackrecipe--》illumination
            databasepath = r'P:\_NewBiasTable\NewProduct.mdb'
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + databasepath
            conn.Open(DSN)
            # rs = win32com.client.Dispatch(r'ADODB.Recordset')

            tmp = []
            tmp1 = self.fulltech.strip()[0:3]

            for i in range(self.df.shape[0]):
                if self.df.iloc[i, 5] == 'LII':
                    tmp.append('')
                else:


                    tmp2 = self.df.iloc[i, 7]
                    tmp3 = self.df.iloc[i, 1]
                    # print(tmp1,len(tmp1),tmp2,len(tmp2),tmp3,len(tmp3))
                    sql = "SELECT TYPE,NA,Outer,Inner From AsmlNaVsTechLayer Where "
                    sql = sql + "  Tech='" + tmp1 + "'"
                    sql = sql + " and Track='" + tmp2 + "'"
                    sql = sql + " and Layer='" + tmp3 + "'"
                    print(sql)
                    rs = win32com.client.Dispatch(r'ADODB.Recordset')
                    rs.Open(sql, conn, 1, 3)
                    # print(rs.recordcount)
                    if rs.RecordCount == 0:
                        tmp.append("未找到匹配的设置")
                    elif rs.RecordCount > 1:
                        tmp.append("多重设置，待确定")
                    elif rs.RecordCount == 1:
                        tmp4 = (rs.fields(0).value)[0:3] + '/' + rs.fields(1).value + '/' + rs.fields(2).value
                        if 'Ann' in rs.fields(0).value:
                            tmp4 = tmp4 + '/' + rs.fields(3).value
                        tmp.append(tmp4)
            self.df['Ill'] = tmp
            tmp, tmp1, tmp2, tmp3, tmp4 = None, None, None, None, None
            rs.close
            conn.close
        except:
            # self.box = QMessageBox(QMessageBox.Warning, '注意', 'NA/PC数据库读取异常')
            # self.box.showNormal()
            reply = QMessageBox.information(self, "注意", 'NA/PC数据库读取异常', QMessageBox.Yes, QMessageBox.Yes)
    def SHOW_TABLE(self):
        try:  # 显示数据在表格中
            self.exTable.setColumnCount(self.df.shape[1])
            self.exTable.setRowCount(self.df.shape[0])
            self.exTable.setHorizontalHeaderLabels(self.df.columns)
            for x in range(self.exTable.columnCount()):
                head=self.exTable.horizontalHeaderItem(x)# 获得水平方向表头的Item对象
                head.setFont(QFont("Times",10,QFont.Bold))#)  # 设置字体
                head.setForeground(QBrush(Qt.red))




                # headItem.setBackgroundColor(QColor(0, 60, 10))  # 设置单元格背景颜色
                # headItem.setTextColor(QColor(200, 111, 30))  # 设置文字颜色

            # self.table.setEditTriggers(QTableWidget.NoEditTriggers)#单元格不可编辑
            # self.table.setSelectionBehavior(QTableWidget.SelectRows)  #选中列还是行，这里设置选中行
            # self.table.setSelectionMode(QTableWidget.SingleSelection) #只能选中一行或者一列
            # self.table.horizontalHeader().setStretchLastSection(True)  #列宽度占满表格(最后一个列拉伸处理沾满表格)
            # newItem.setForeground(QBrush(QColor(255, 0, 0)))
            for i in range(self.df.shape[0]):
                for j in range(self.df.shape[1]):
                    newItem = QTableWidgetItem(str(self.df.iloc[i, j]))
                    newItem.setTextAlignment(Qt.AlignCenter)
                    if i % 2 == 1:
                        # newItem.setBackground(QColor('Lime'))
                        newItem.setBackground(QColor('lightcyan'))
                    self.exTable.setItem(i, j, newItem)
            self.exTable.resizeColumnsToContents()
            self.exTable.resizeRowsToContents()
        except:
            # self.box = QMessageBox(QMessageBox.Warning, '注意', '表格显示异常')
            # self.box.showNormal()
            reply = QMessageBox.information(self, "注意", '表格显示异常', QMessageBox.Yes, QMessageBox.Yes)
    def ALIGN_TREE(self):
        try: #定义 alignment treepass

            tree=['--']
            tmp = list(self.df['Layer'])
            if 'ZA' in tmp:
                # self.box = QMessageBox(QMessageBox.Warning, '注意', '双零层工艺，待手动设置')
                # self.box.showNormal()
                reply = QMessageBox.information(self, "注意", '双零层工艺，待手动设置', QMessageBox.Yes, QMessageBox.Yes)
            else:
                if not  'TO' in tmp:
                    # self.box = QMessageBox(QMessageBox.Warning, '注意', '无TO层次，需手动设置')
                    # self.box.showNormal()
                    reply = QMessageBox.information(self, "注意", '无TO层次，需手动设置', QMessageBox.Yes, QMessageBox.Yes)

            if 'TO' in tmp:
                self.nTO = tmp.index('TO')

                if self.nTO==0:
                    pass
                elif self.nTO == 1:
                    tree.append(self.df.iloc[0, 1])
                else:
                    for i in  range(1,self.nTO+1):

                        if self.df.iloc[i,1] != 'TO':
                            if 'ZA' in tmp[0:i]:
                                tree.append('ZA')
                            else:
                                tree.append(self.df.iloc[0, 1])
                        else:
                            if 'ZA' in tmp[0:self.nTO]:
                                tree.append('ZA')
                            elif 'TB' in tmp[0:self.nTO]:
                                tree.append('TB')
                            elif 'PX' in tmp[0:self.nTO]:
                                tree.append('PX')
                            elif 'PT' in tmp[0:self.nTO]:
                                tree.append('PT')
                            else:
                                tree.append(self.df.iloc[0, 1])
                    # self.box = QMessageBox(QMessageBox.Warning, '注意', 'TO非第1,2层次，请再确认对位顺序')
                    # self.box.showNormal()
                    reply = QMessageBox.information(self, "注意", 'TO非第1,2层次，请再确认对位顺序', QMessageBox.Yes, QMessageBox.Yes)

            else:
                self.nTO=0

            T_Nikon= {'RN':'TO','SN':'TR','TT':'W1','CP':'A1'}

            Nikon = {'W2':'A1','W3':'A2','A1':"W1",'A2':'W2','NP':'GT','PP':'GT','W4':'A3','A3':'W3'}
            H_Asml = {"W2":"W1",'W3':'W2','W4':'W3','W5':'W4','W6':'W5','W7':'W6',
                      "A1":"W1",'A2':'W2','A3':'W3','A4':'W4','A5':'W5','A6':'W6'}
            for i in range(self.nTO+1,len(tmp)):
                layer=self.df.iloc[i,1]
                if self.df.iloc[i,5] == "LII":   #NIKON
                    if self.fulltech[0] =='T':
                        try:
                            tree.append(T_Nikon[self.df.iloc[i,1]])
                            # tree.append("LII")
                        except:
                            # self.box = QMessageBox(QMessageBox.Warning, '注意', 'T_NIKON字典不完整')
                            # self.box.showNormal()
                            reply = QMessageBox.information(self, "注意", 'T_NIKON字典不完整', QMessageBox.Yes, QMessageBox.Yes)
                            tree.append('??')
                    else:
                        try:
                            tree.append(Nikon[layer])
                            # tree.append('LII')
                        except:
                            if layer=='WT':
                                tree.append(self.df.iloc[i,6])
                            elif layer == 'AT':
                                tree.append(self.df.iloc[i, 6])
                            elif layer =='TT':
                                tree.append(self.df.iloc[i, 6])
                            elif layer == 'W1':
                                tree.append(self.df.iloc[i, 6])
                            elif layer in ['CP','CF','PI','P2','PN']:
                                tree.append(self.df.iloc[i-1,2][5:7])
                            elif layer == 'CT':
                                if 'W' in self.df.iloc[i - 1, 1]:
                                    tree.append(self.df.iloc[i - 1, 2][5:7])
                                elif 'W' in self.df.iloc[i - 2, 1]:
                                    tree.append(self.df.iloc[i - 2, 2][5:7])
                                elif 'W' in self.df.iloc[i - 3, 1]:
                                    tree.append(self.df.iloc[i - 3, 2][5:7])

                            else:
                                tree.append('TO')


                else:                              #ASML
                    if self.fulltech[0] == 'T' or self.fulltech[1:2]!="1":
                        try:
                            tree.append(self.df.iloc[i, 6])
                            # tree.append('LDI')
                        except:
                            # self.box = QMessageBox(QMessageBox.Warning, '注意', '低端工艺ASML层次AlignTree设置不全')
                            # self.box.showNormal()
                            reply = QMessageBox.information(self, "注意", '低端工艺ASML层次AlignTree设置不全', QMessageBox.Yes, QMessageBox.Yes)
                            tree.append('??')
                    else:
                        try:
                            tree.append(H_Asml[layer])
                            # tree.append('LDI')
                        except:
                            if layer =='WT':
                                if 'W' in self.df.iloc[i-1,1]:
                                    tree.append(self.df.iloc[i-1, 2][5:7])
                                elif 'W' in self.df.iloc[i-2,1]:
                                    tree.append(self.df.iloc[i-2, 2][5:7])
                                elif 'W' in self.df.iloc[i-3,1]:
                                    tree.append(self.df.iloc[i-3, 2][5:7])
                                else:
                                    # self.box = QMessageBox(QMessageBox.Warning, '注意', '018工艺ASML层次设置逻辑不全')
                                    # self.box.showNormal()
                                    reply = QMessageBox.information(self, "注意",  '018工艺ASML层次设置逻辑不全', QMessageBox.Yes,
                                                                    QMessageBox.Yes)
                                    tree.append('??')
                            elif layer == 'AT':
                                tree.append(self.df.iloc[i-1, 2][5:7])
                            elif layer =='CT':
                                if 'W' in self.df.iloc[i-1,1]:
                                    tree.append(self.df.iloc[i-1, 2][5:7])
                                elif 'W' in self.df.iloc[i-2,1]:
                                    tree.append(self.df.iloc[i-2, 2][5:7])
                                elif 'W' in self.df.iloc[i-3,1]:
                                    tree.append(self.df.iloc[i-3, 2][5:7])
                            elif layer in ['OE','OV']:
                                tree.append('??')
                                # self.box = QMessageBox(QMessageBox.Warning, '注意', 'OE,OV待确认')
                                # self.box.showNormal()
                                reply = QMessageBox.information(self, "注意", 'OE,OV待确认', QMessageBox.Yes,
                                                                QMessageBox.Yes)
                            elif layer == 'W1':
                                tree.append(self.df.iloc[i, 6])
                            else:
                                tree.append('TO')
            self.df['Align']=tree

        except:
            # self.box = QMessageBox(QMessageBox.Warning, '注意', 'ALIGN_TREE函数异常，请确认')
            # self.box.showNormal()
            reply = QMessageBox.information(self, "注意", 'ALIGN_TREE函数异常，请确认', QMessageBox.Yes, QMessageBox.Yes)
    def MARK_CHOICE(self):
        try:
            tmp = ['--']
            for i in range(1,self.df.shape[0]):
                layer = self.df.iloc[i,1]
                tool = self.df.iloc[i,5]
                if tool == 'LII':
                    if self.fulltech[0]=='T':  #DMOS
                        if layer in ['SN','SP','CP','TT']:
                            tmp.append("FIA")
                        else:
                            tmp.append('LSA')
                    else:#NonDMOS
                        if 'W' in layer or 'A' in layer or layer in ['TT','CT']:
                            tmp.append("FIA")
                        else:
                            if layer in ['CP','PN','CF','PI','P2']:
                                tmp.append('LSA')
                            else:
                                if self.fulltech[1]=='1' or self.fulltech[0:3]=='M52':
                                    if layer=='RE':
                                        tmp.append('LSA')
                                    else:
                                        if i<self.nTO:
                                            tmp.append("LSA")
                                        else:
                                            tmp.append('FIA')
                                else:
                                    tmp.append("LSA")
                else:#'LDI'
                    if self.fulltech[1:2] == '1' and layer in ['W1','W2','W3','W4','W5','W6','W7','WT']:
                        tmp.append('AA157')
                    else:
                        tmp.append('AH53')
            self.df['Method']=tmp
        except:
            # self.box = QMessageBox(QMessageBox.Warning, '注意', '标记选择函数异常，请确认')
            # self.box.showNormal()
            reply = QMessageBox.information(self, "注意", '标记选择函数异常，请确认', QMessageBox.Yes, QMessageBox.Yes)
    def READ_DICT(self,rowno,list):
        for i in range(len(list[0])):
            for j in range(len(list[1])):
                if self.df.iloc[rowno, 5] == 'LDI':
                    strx = list[0][i].replace('?','X')
                    stry = list[0][i].replace('?','Y')
                else:
                    strx = list[0][i].replace('?', 'Y')
                    stry = list[0][i].replace('?', 'X')
                try:
                    self.SPMX[rowno] = self.dic[strx + list[1][j] + "_"]
                    self.SPMY[rowno] = self.dic[stry + list[1][j] + "_"] + " " + stry[:-1]
                    return
                except:
                    self.SPMX[rowno] = 'failed'
                    self.SPMY[rowno] = 'failed'
    def READ_DICT_EGA(self,rowno,list):
        for i in range(len(list[0])):
            for j in range(len(list[1])):
                for k in range(len(list[2])):
                    strx = list[0][i].replace('?','X')
                    stry = list[0][i].replace('?','Y')
                    try:
                        self.EGAX[rowno] = self.dic[strx + list[1][j] + "_" + list[2][k]]
                        self.EGAY[rowno] = self.dic[stry + list[1][j] + "_" + list[2][k]] + " " + stry[:-2]+list[2][k]
                        return
                    except:
                        self.EGAX[rowno] = 'failed'
                        self.EGAY[rowno] = 'failed'
    def READ_DICT_EGA1(self,rowno,list):
        for i in range(len(list[0])):
            for j in range(len(list[1])):
                for k in range(len(list[2])):
                    strx = list[0][i].replace('?','X')
                    stry = list[0][i].replace('?','Y')
                    try:
                        self.EGAX1[rowno] = self.dic[strx + list[1][j] + "_" + list[2][k]]
                        self.EGAY1[rowno] = self.dic[stry + list[1][j] + "_" + list[2][k]] + " " + stry[:-2]+list[2][k]
                        return
                    except:
                        self.EGAX1[rowno] = 'failed'
                        self.EGAY1[rowno] = 'failed'
    def GET_COORDINATE(self):

        tmp = [ i.strip() for i in open('P:/Recipe/Coordinate/' + self.part + '.txt') if
                "WY" in i.upper() or "WX" in i.upper() or "SPM" in i.upper() or
                "NAA157" in i.upper() or "NAH" in i.upper() or
                'FIA' in i.upper() or 'LSA' in i.upper() ]
        self.dfZ = pd.DataFrame(tmp)
        self.dfZ = self.dfZ[0].str.split('\t',expand=True)
        self.dfZ = self.dfZ[[0,1,2,4,6]]
        self.dfZ = self.dfZ[[not i for i in self.dfZ[0].str.contains('DRC')]]
        self.dfZ['key'] = self.dfZ[0] + "_" + self.dfZ[1] + "_" + self.dfZ[2]
        self.dfZ['item'] = self.dfZ[4] + ', ' + self.dfZ[6]
        l1,l2 = list(self.dfZ['key']) ,list( self.dfZ['item'])
        l11,l = [],[]
        # self.dic = dict(zip(self.dfZ['key'], self.dfZ['item']))   #零层等同一前缀下，有多个坐标未加区分，默认选第一个
        for i, tmp in enumerate(l1):
            if tmp in l11:
                pass  #重复前缀
            else:
                l11.append(tmp)
                l.append((l1[i],l2[i]))
        self.dic = dict(l)

        #确认产品是否是60um划片槽
        tmp =set([ i.strip()[0] for i in list(self.dfZ[0])])
        if len(tmp) == 1 and 'N' in tmp:
            N=True
        else:
            if 'N' not in tmp:
                N=False
            else:
                if self.fulltech[0]=='T':
                    N=True
                else:
                    N=False
            # self.box = QMessageBox(QMessageBox.Warning, '注意', '同时含有普通标记和narrow标记，DMOS产品按narrow标记产品处置；其它产品按普通标记处理，部分双零层工艺等会出错')
            # self.box.showNormal()
            reply = QMessageBox.information(self, "注意", '同时含有普通标记和narrow标记，DMOS产品按narrow标记产品处置；其它产品按普通标记处理，部分双零层工艺等会出错', QMessageBox.Yes, QMessageBox.Yes)
                # return
        #读取坐标
        self.SPMX = [ '' for i in range(self.df.shape[0])]
        self.SPMY,self.EGAX,self.EGAY,self.EGAX1,self.EGAY1 = \
            self.SPMX.copy(),self.SPMX.copy(),self.SPMX.copy(),self.SPMX.copy(),self.SPMX.copy()
        for i in range(1,self.df.shape[0]):
            no = i
            layer = self.df.iloc[i,9]
            stage = self.df.iloc[i,1]
            #DUV=DUV=DUV=DUV=DUV=DUV=DUV=DUV=DUV=DUV=
            if self.df.iloc[i,5]=='LDI':
                #80um=80um=80um=80um=80um=80um=80um=
                if N == False:
                    # print('所有层次采用AH53,stackmark backup')
                    if layer in ['TO','W2','W4','W6']:
                        dicKey = [['SPM53?2_'],[layer,'TOW246']]
                        self.READ_DICT(rowno=no,list=dicKey)
                    elif layer in ['W1','W3','W5','W7']:
                        dicKey = [['SPM53?2_'], [layer, 'W1357']]
                        self.READ_DICT(rowno=no, list=dicKey)
                    elif layer in ['ZO','ZA']:
                        dicKey = [['SPM53?2_','NAH53?_','NAA157?_','NAH325374?_'], [layer]]
                        self.READ_DICT(rowno=no, list=dicKey)
                    else:
                        dicKey = [['SPM53?2_'], [layer]]
                        self.READ_DICT(rowno=no, list=dicKey)
        #         #60um=60um=60um=60um=60um=60um=60um=
                else:
                    if stage in ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'WT']:  # print('NAA157')
                        if layer in ['TO','W2','W4','W6']:
                            dicKey = [['NAA157?_'],[layer,'TOW246']]
                            self.READ_DICT(rowno=no,list=dicKey)
                        elif layer in ['W1','W3','W5','W7']:
                            dicKey = [['NAA157?_'], [layer, 'W1357']]
                            self.READ_DICT(rowno=no, list=dicKey)
                        else:
                            dicKey = [['NAA157?_'], [layer]]
                            self.READ_DICT(rowno=no, list=dicKey)
                    elif stage in ['A1','A2','A3','A4','A5','A6','AT','CT','OE','OV']: #print("NAH325374")
                        if layer in ['TO','W2','W4','W6']:
                            dicKey = [['NAH325374?_'],[layer,'TOW246']]
                            self.READ_DICT(rowno=no,list=dicKey)
                        elif layer in ['W1','W3','W5','W7']:
                            dicKey = [['NAH325374?_'], [layer, 'W1357']]
                            self.READ_DICT(rowno=no, list=dicKey)
                        else:
                            dicKey = [['NAH325374?_'], [layer]]
                            self.READ_DICT(rowno=no, list=dicKey)
                    else:
                        dicKey = [['NAH53?_','NAH325374?_','NAA157?_'], [layer, 'TOW246']]
                        self.READ_DICT(rowno=no, list=dicKey)
            # Iline=Iline=Iline=Iline=Iline=Iline=Iline=Iline=
            else:
                if stage in  ['CP','PN','CF','PI','P2'] and self.fulltech[0]=='T':
                    dicKey = [['W?D_','W?C_','W?_','W?F_','NW?D_','NW?C_','NW?_','NW?F_'], [layer]]
                    self.READ_DICT(rowno=no, list=dicKey)
                    dicEga = [['FIA?D_', 'FIA?C_','FIA?_', 'NFIA?D_', 'NFIA?C_','NFIA?_'], [layer], ['13','13/8' '9','9/12']]
                    self.READ_DICT_EGA(rowno=no, list=dicEga)
                    dicEga = [['LSA?D_', 'LSA?_','LSA?C_', 'NLSA?D_','NLSA?_' 'NLSA?C_'], [layer], ['7','9']]
                    self.READ_DICT_EGA1(rowno=no, list=dicEga)
                elif stage in  ['CP','PN','CF','PI','P2'] and self.fulltech[0]!='T':
                    dicKey = [['W?D_', 'W?C_', 'W?_', 'W?F_', 'NW?D_', 'NW?C_', 'NW?_', 'NW?F_'], [layer]]
                    self.READ_DICT(rowno=no, list=dicKey)
                    dicEga = [['LSA?D_', 'LSA?_','LSA?C_', 'NLSA?D_','NLSA?_' 'NLSA?C_'], [layer], ['7','9']]
                    self.READ_DICT_EGA(rowno=no, list=dicEga)
                    dicEga = [['FIA?D_', 'FIA?_', 'FIA?C_', 'NFIA?D_', 'NFIA?_', 'NFIA?C_'], [layer],
                              ['13', '13/8' '9', '9/12']]
                    self.READ_DICT_EGA1(rowno=no, list=dicEga)
                elif stage =='TT':
                    dicKey = [['W?F_', 'W?C_', 'W?_', 'W?D_', 'NW?F_', 'NW?C_', 'NW?_', 'NW?D_'], [layer]]
                    self.READ_DICT(rowno=no, list=dicKey)
                    dicEga = [['FIA?C_', 'FIA?_','FIA?D_', 'NFIA?C_', 'NFIA?_','NFIA?D_'], [layer], ['13','13/8' '9','9/12']]
                    self.READ_DICT_EGA(rowno=no, list=dicEga)
                    dicEga = [['LSA?C_', 'LSA?_', 'LSA?D_', 'NLSA?C_', 'NLSA?_' 'NLSA?D_'], [layer], ['7', '9']]
                    self.READ_DICT_EGA1(rowno=no, list=dicEga)
                else:
                    if layer in ['A1', 'A2', 'A3', 'AT', 'CT','W2','W3','WT']:
                        dicKey = [['W?C_', 'W?D_', 'W?_', 'W?F_', 'NW?C_', 'NW?D_', 'NW?_', 'NW?F_'], [layer]]
                        self.READ_DICT(rowno=no, list=dicKey)
                        dicEga = [['FIA?C_', 'FIA?_','FIA?D_', 'NFIA?C_', 'NFIA?_','NFIA?D_'], [layer], ['13','13/8' '9','9/12']]
                        self.READ_DICT_EGA(rowno=no, list=dicEga)
                        dicEga = [['LSA?C_', 'LSA?_', 'LSA?D_', 'NLSA?C_', 'NLSA?_' 'NLSA?D_'], [layer], ['7', '9']]
                        self.READ_DICT_EGA1(rowno=no, list=dicEga)
                    else:
                        if self.fulltech[1] == '1' or self.fulltech[0:3] == 'M52' or self.fulltech[0] == 'T':
                            if layer == 'RE':
                                dicKey = [['W?C_', 'W?D_', 'W?_', 'W?F_', 'NW?C_', 'NW?D_', 'NW?_', 'NW?F_'], [layer]]
                                self.READ_DICT(rowno=no, list=dicKey)
                                dicEga = [['LSA?C_', 'LSA?_','LSA?D_', 'NLSA?C_','NLSA?_' 'NLSA?D_'], [layer], ['7','9']]
                                self.READ_DICT_EGA(rowno=no, list=dicEga)
                                dicEga = [['FIA?C_', 'FIA?_', 'FIA?D_', 'NFIA?C_', 'NFIA?_', 'NFIA?D_'], [layer],
                                          ['13', '13/8' '9', '9/12']]
                                self.READ_DICT_EGA1(rowno=no, list=dicEga)
                            else:
                                dicKey = [['W?C_', 'W?D_', 'W?_', 'W?F_', 'NW?C_', 'NW?D_', 'NW?_', 'NW?F_'], [layer]]
                                self.READ_DICT(rowno=no, list=dicKey)
                                dicEga = [['FIA?C_', 'FIA?_','FIA?D_', 'NFIA?C_', 'NFIA?_','NFIA?D_'], [layer], ['13', '9']]
                                self.READ_DICT_EGA(rowno=no, list=dicEga)
                                dicEga = [['LSA?C_', 'LSA?_', 'LSA?D_', 'NLSA?C_', 'NLSA?_' 'NLSA?D_'], [layer],
                                          ['7', '9']]
                                self.READ_DICT_EGA1(rowno=no, list=dicEga)
                        else:
                            dicKey = [['W?C_', 'W?D_', 'W?_', 'W?F_', 'NW?C_', 'NW?D_', 'NW?_', 'NW?F_'], [layer]]
                            self.READ_DICT(rowno=no, list=dicKey)
                            dicEga = [['LSA?C_', 'LSA?_','LSA?D_', 'NLSA?C_','NLSA?_' 'NLSA?D_'], [layer], ['7','9']]
                            self.READ_DICT_EGA(rowno=no, list=dicEga)
                            dicEga = [['FIA?C_', 'FIA?_', 'FIA?D_', 'NFIA?C_', 'NFIA?_', 'NFIA?D_'], [layer],
                                      ['13', '9']]
                            self.READ_DICT_EGA1(rowno=no, list=dicEga)

#NP, PP -->GT, LSA

        for count , value in enumerate(self.EGAY):
            if len(value) >1 and 'FIA' in value:
                tmpx,tmpy= self.EGAX[count],self.EGAY[count]
                self.EGAX[count], self.EGAY[count] = self.EGAX1[count],self.EGAY1[count]
                self.EGAX1[count], self.EGAY1[count] = tmpx,tmpy




        self.df['SPMX(WY)']=self.SPMX
        self.df['SPMY(WX)']=self.SPMY
        self.df['LSAX'] = self.EGAX
        self.df['LSAY'] = self.EGAY
        self.df['FIAX']=self.EGAX1
        self.df['FIAY']=self.EGAY1
        #print(self.df),convert larget field mark coordinate
        stepx = self.exLineEdit1_9.text().strip()
        stepy = self.exLineEdit1_10.text().strip()
        if self.part.upper()[-2:]=='-L':
            if stepx.isdigit() or stepx.replace('.','8').isdigit():
                offset = (eval(stepx))/2
                for i in range(1,self.df.shape[0]):
                    if self.df.iloc[i,5]=='LDI':
                        tmpx,tmpy=self.df.iloc[i,11],self.df.iloc[i,12]
                        self.df.iloc[i,11]=tmpy.split(' ')[1]+', '+  str(-(eval(tmpy.split(',')[0]) + offset))
                        self.df.iloc[i,12]=tmpx.split(',')[1]+', '+  str(-(eval(tmpx.split(',')[0].strip()) + offset)) + " "+ tmpy.split(' ')[2]
            else:
                reply=  QMessageBox.information(self, "注意", '大视场产品，请输入Step_X!!\n\n坐标转换未完成', QMessageBox.Yes, QMessageBox.Yes)



    def onMenuEX1(self):    # output of sheet for recipe edit
        if self.f2 == True:
            strFile ='P:/_NewBiasTable/_' + self.part.upper() + ('_New.xls')
        else:
            strFile ='P:/_NewBiasTable/_' + self.part.upper() + ('_Old.xls')

        book  = xlwt.Workbook()
        sheet = book.add_sheet('sheet1')
        # FROMAT
        style1=xlwt.XFStyle()

        fnt1 = xlwt.Font()  # 创建一个文本格式，包括字体、字号和颜色样式特性
        fnt1.name = u'Aerial'  # 设置其字体为微软雅黑
        fnt1.colour_index = 12  # 设置其字体颜色
        fnt1.bold = True
        style1.font=fnt1

        borders = xlwt.Borders()  # Create Borders
        borders.left = xlwt.Borders.THIN
        # May be: NO_LINE, THIN, MEDIUM, DASHED, DOTTED, THICK, DOUBLE, HAIR, MEDIUM_DASHED, 　　THIN_DASH_DOTTED, 　　　　　　　      #MEDIUM_DASH_DOTTED, THIN_DASH_DOT_DOTTED, MEDIUM_DASH_DOT_DOTTED,
        # SLANTED_MEDIUM_DASH_DOTTED, or 0x00 through 0x0D.
        borders.right = xlwt.Borders.THIN
        # borders.top = xlwt.Borders.DASHED
        borders.top = xlwt.Borders.THIN
        # borders.bottom = xlwt.Borders.DASHED
        borders.bottom = xlwt.Borders.THIN
        borders.left_colour = 0x40
        borders.right_colour = 0x40
        borders.top_colour = 0x40
        borders.bottom_colour = 0x40
        style1.borders = borders

        alignment1 = xlwt.Alignment()  # Create Alignment
        # May be: HORZ_GENERAL, HORZ_LEFT, HORZ_CENTER, HORZ_RIGHT,
        # HORZ_FILLED, HORZ_JUSTIFIED, HORZ_CENTER_ACROSS_SEL, HORZ_DISTRIBUTED
        alignment1.horz = xlwt.Alignment.HORZ_CENTER
        # May be: VERT_TOP, VERT_CENTER, VERT_BOTTOM, VERT_JUSTIFIED, VERT_DISTRIBUTED
        alignment1.vert = xlwt.Alignment.VERT_CENTER  # 垂直居中
        style1.alignment = alignment1  # Add Alignment to Style

        # head
        tmp = ['Stage', 'Layer', 'Mask', 'MLM', 'Label', 'Tool', 'Ovl', 'Track', 'Ill', 'Ali', 'Method', 'SPMX/WY',
               'SPMY/WX', "EGAX", 'EGAY','EGAX1','EGAY1']



        for i in range(9):
            sheet.write(0,i,tmp[i],style1)


        #content
        style = xlwt.XFStyle()
        fnt = xlwt.Font()  # 创建一个文本格式，包括字体、字号和颜色样式特性
        fnt.name = u'Aerial'  # 设置其字体为微软雅黑
        fnt.colour_index =0   # 设置其字体颜色   2 red,12 blue
        fnt.bold = False
        style.font = fnt
        alignment = xlwt.Alignment()
        alignment.horz = xlwt.Alignment.HORZ_LEFT
        alignment.vert = xlwt.Alignment.VERT_CENTER  # 垂直居中
        style.alignment = alignment
        style.borders = borders
        for i in range(self.exTable.rowCount()):
            for j in range(9):
                sheet.write(i+1,j,self.exTable.item(i,j).text(),style)
        if self.f2==True:
            sheet = book.add_sheet('sheet2')
            tmp1 = ['Layer', 'Ali', 'Method', 'SPM_X/W_Y', 'SPM_Y/W_X', "LSA_X", 'LSA_Y','FIA_X','FIA_Y']

            for i in range(len(tmp1)):
                sheet.write(0, i, tmp1[i], style1)


            for i in range(self.exTable.rowCount()):
                sheet.write(i +1, 0, self.exTable.item(i, 1).text(), style)
                sheet.write(i +1, 1, self.exTable.item(i, 9).text(), style)
                sheet.write(i +1, 2, self.exTable.item(i, 10).text(), style)
                sheet.write(i +1, 3, self.exTable.item(i, 11).text(), style)
                sheet.write(i +1, 4, self.exTable.item(i, 12).text(), style)
                sheet.write(i +1, 5, self.exTable.item(i, 13).text(), style)
                sheet.write(i +1, 6, self.exTable.item(i, 14).text(), style)
                sheet.write(i +1, 7, self.exTable.item(i, 15).text(), style)
                sheet.write(i +1, 8, self.exTable.item(i, 16).text(), style)



            # sheet.col(0).width = 256*10 #stage
            # sheet.col(1).width = 256 * 16#ill
            # sheet.col(2).width = 256 * 9#mask
            # sheet.col(3).width = 256 * 5#mlm
            # sheet.col(4).width = 256 * 16
            # sheet.col(5).width = 256 * 23
            # sheet.col(6).width = 256 * 16
            # sheet.col(7).width = 256 * 23
            #
            fnt = xlwt.Font()  # 创建一个文本格式，包括字体、字号和颜色样式特性
            fnt.name = u'Aerial'  # 设置其字体为微软雅黑
            fnt.colour_index = 2  # 设置其字体颜色   2 red,12 blue
            fnt.bold = True
            style.font = fnt

            ##以下输入注意事项
            count =2+  self.exTable.rowCount()
            sheet.write_merge(count, count, 0, 8,
                              '注意：(产品工艺代码是' + self.fulltech + ')', style)
            count += 1
            sheet.write_merge(count, count, 0, 8, '__Nikon TO/GT/PC SHIFT FOCUS-->YES;   PAD EGA SHOT-->4', style)

            count += 1
            sheet.write_merge(count, count, 0, 8, '__W1对位以bias table为准，除了C18打头的工艺需求一定对TO，用TO-SM53,(narrow mark用TO-AA157)', style)

            count += 1
            sheet.write_merge(count, count, 0, 8,
                              '__implant layer、Contact、Via-*及PAD和FU打标处不曝光（复合版及特殊产品除外)', style)

            count += 1
            sheet.write_merge(count, count, 0, 8, '__新建新品的CP层EGA改为6个shot,EGA REQUISITE SHOTS为4', style)

            count += 1
            sheet.write_merge(count, count, 0, 8, '__建ASML菜单时，ovl<45nm的layer，除TO、poly 、W1、C18 的SI 不需要改，其它层次更改如下：',style)
            count += 1
            sheet.write_merge(count, count, 0, 8, '__(续上行），Wafer alignment：Y#Reduce Reticle Alignments ：Y# Maximum Drift：10#Maximum Interval (wafers): 25', style)

            count += 1
            sheet.write_merge(count, count, 0, 8, '__40um 所有layer  Search LSA算法用11（TT层除外），CP层EGA用FIA', style)

            if 'ZA' in list(self.dfB['Layer']) or 'ZO' in list(self.dfB['Layer']) :
                count = count +1
                sheet.write_merge(count, count, 0, 8, '__(双)零层工艺，请注意对位顺序及对位坐标，以及NIKON对位方法',style)
            if '2.0' in list(self.dfB['MLM']):
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__复合版,若四合一版，Metal层的focus disable range设置为6000', style)
            if self.fulltech[1]=='1' or self.fulltech[0:3]=='M52':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__M52/.18以下工艺TO前非关键层(除RE）NIKON参数设置:EGA为FIA,Shot的数为8个，EGA Reject：Yes;', style)
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__(续上一行）EGA Limitation：0.65，EGA Requisite shot：6，Result Allowance：1.5um', style)
            if 'PI' in list(self.dfB['Layer']):
                count = count +1
                sheet.write_merge(count, count, 0, 8, '__PI层次的菜单，请将focus offset 0.2 直接加进菜单，并通知朱龙飞设置R2R',style)
            if  self.part[0:2]=='D6':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除打标处，所有层次partial shot必须都要曝光', style)
            if  self.part[0:2]=='O1':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除打标处，所有层次partial shot必须都要曝光', style)
            if  self.part[0:2]=='XV' or self.part[0:2]=='XU' or self.part[0:2]=='UF':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除了注入、孔层打标处不曝光以外其他边缘全部曝光', style)
            if self.part[0:2] == 'XV' or self.part[0:2] == 'XU' or self.part[0:2] == 'UF':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除了注入、孔层打标处不曝光以外其他边缘全部曝光', style)

            if self.fulltech[0]=='T' or self.fulltech[0]=='K':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__DOMS/IGBT 的PAD层search 、EGA都用FIA,算法41(工艺代码第一位为 “T" 或 "K")', style)

            if self.fulltech[0]=='T' or self.fulltech[0]=='K':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__DOMS/IGBT新品菜单新建时，SN/SP层次默认EGA对位方式设置为FIA', style)

            if self.fulltech[0]=='T' or self.fulltech[0]=='K':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__DOMS/IGBT新品菜单新建时，DMOS产品新建，Nikon前段对位固定使用TR层次mark', style)

            if self.fulltech[0:3]=='B52':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__B52有PX layer的,对位PX', style)

            if self.part[-1]=='M' or self.part[-1]=='N':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__M/N结尾的产品,重新模拟map,按照新品重新建立曝光菜单,无需去除打标处及边缘无效shot，全部曝光', style)





        try:
            book.save(strFile)
        except:
            # self.box = QMessageBox(QMessageBox.Warning, '注意', '文件保存失败，请确认同名文件是否打开')
            # self.box.showNormal()
            reply = QMessageBox.information(self, "注意", '文件保存失败，请确认同名文件是否打开', QMessageBox.Yes, QMessageBox.Yes)
        self.dfB,self.dfF,self.dfZ=None,None,None
        self.df,self.dic,self.dicKey=None,None,None
        gc.collect()


    def onMenuEX2(self):
        if self.dfZ.shape[0]>0:
            try:  # 显示数据在表格中
                self.exTable.clear()
                self.exTable.setColumnCount(self.dfZ.shape[1])
                self.exTable.setRowCount(self.dfZ.shape[0])
                l1 = ["ITEM" + str(i) for i in range(self.dfZ.shape[1])]
                self.exTable.setHorizontalHeaderLabels(l1)
                for i in range(self.dfZ.shape[0]):
                    for j in range(self.dfZ.shape[1]):
                        newItem = QTableWidgetItem(str(self.dfZ.iloc[i, j]))
                        newItem.setTextAlignment(Qt.AlignCenter)
                        if i % 2 == 1:
                            newItem.setBackground(QColor('lightcyan'))
                        self.exTable.setItem(i, j, newItem)
            except:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '表格显示异常')
                self.box.showNormal()
    def onExBtn_3(self):  #asml recipe check: pre-alin,na/pc,maskid
        self.part = self.exLineEdit1_2.text().strip()
        if self.exLineEdit1_2.text().strip() == "":
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请在产品名称栏未输入产品名')
            self.box.showNormal()
            return
        self.f1 = self.exRadioButton_3.isChecked()  # ->Old file
        self.f2 = self.exRadioButton_4.isChecked()  # ->New file
        if self.f1 ^ self.f2 == False:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请选择程序类型')
            self.box.showNormal()
            return
        if self.f2 == True:
            strFile ='P:/_NewBiasTable/_' + self.part.upper() + ('_New.xls')
        else:
            strFile ='P:/_NewBiasTable/_' + self.part.upper() + ('_Old.xls')
        if os.path.exists('P:/recipe/recipe/' + self.part) == False:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请保存ASML文本程序')
            self.box.showNormal()
            return




        #read ASML TEXT
        f = open('P:/recipe/recipe/' + self.part)
        df = [i.strip() for i in f if i.strip() != '']
        illumination = []
        mask = []
        illType=[]
        f1 = False
        f2 = False
        f3 = False
        # get pre-alin mode
        for index, i in enumerate(df):
            if 'Prealignment Method' in i:
                preAlign = i.split(':')[1].strip()  # print('\npreAlign', preAlign, '\n')
            if '+=================+=================+=========+========+========+' in i:
                f1 = True
                no = index
            if f1 == True and index > no and '+-----------------+-----------------+---------+--------+--------+' not in i:
                illumination.append([i.split('|')[1].strip(), i.split('|')[3].strip(), i.split('|')[4].strip(),
                                     i.split('|')[5].strip(),i.split('|')[2].strip()[-1]])



            if '+-----------------+-----------------+---------+--------+--------+' in i:
                f1 = False
                illumination = pd.DataFrame(illumination).drop_duplicates()
                illumination = illumination[illumination[0].str.len() > 1]  # ('illumination\n',illumination)

            if '+=================+=================+==========================+==============+' in i:
                f2 = True
            if f2 == True and 'SPM' not in i and '|' in i:
                mask.append([i.split('|')[1].strip(), i.split('|')[3].strip()])
                reticle = pd.DataFrame(mask)
            if '+-----------------+-----------------+--------------------------+--------------+' in i:
                f2 = False

            if '+=================+=================+==============+=================+' in i:
                f3 = True
            if f3==True and 'SPM' not in i and '|' in i:
                illType.append([i.split('|')[1].strip(),i.split('|')[3].strip()])
            if '+-----------------+-----------------+--------------+-----------------+' in i:
                f3 = False

        if self.exLineEdit1_10.text().strip().upper()=='Y':

            if os.path.exists(strFile)==False:
                self.box = QMessageBox(QMessageBox.Warning, '注意', 'P:/_NewBiasTable目录未找到文件')
                self.box.showNormal()
                return
            else:
                bt = pd.read_excel(strFile, sheet_name='sheet1', header=0)
                bt = bt.fillna('')
                bt = bt[bt['Tool'].str.contains('LDI')]
                bt=bt[['Stage','Layer','Mask','MLM','Ill']]


                try:
                    tmp = bt['Ill'].str.split('/',expand=True)
                except:
                    reply = QMessageBox.information(self, "注意", 'BiasTable Ill栏格式异常', QMessageBox.Yes,
                                                    QMessageBox.Yes)
                    return







                tmp = tmp.fillna('0')
                bt = pd.concat([bt,tmp],axis=1)

                if bt.shape[1]==8:
                    bt[3]=['0.000' for i in range(bt.shape[0])]


                bt[[1, 2, 3]] = bt[[1, 2, 3]].astype(float)
                bt[1]=bt[1].map(lambda x:('%.3f')%x)
                bt[2]=bt[2].map(lambda x:('%.3f')%x)
                bt[3]=bt[3].map(lambda x:('%.3f')%x)






                illumination[[1, 2, 3]] = illumination[[1, 2, 3]].astype(float)
                illumination[1] = illumination[1].map(lambda x:('%.3f')%x)
                illumination[2] = illumination[2].map(lambda x:('%.3f')%x)
                illumination[3] = illumination[3].map(lambda x:('%.3f')%x)


                illumination.columns=['Layer',1,2,3,'MLM']
                data = pd.merge(illumination, bt,how='left', on='Layer')


                data = data[['Stage','Layer', '1_x', '2_x',   '3_x',    'MLM_x',   0,   '1_y',   '2_y',   '3_y','MLM_y','Mask']]


                data.columns=['Stage','Layer', 'R1', 'R2',   'R3',   'R_MLM',   'B_illType',   'B1',   'B2',   'B3', 'B_MLM','B_Mask']
                mask= pd.DataFrame(mask,columns=['Layer','R_Mask'])
                data = pd.merge(data,mask,how = 'left',on ='Layer')

                illType=pd.DataFrame(illType,columns=['Layer','R_illType'])
                data = pd.merge(data, illType, how='left', on='Layer')
                data = data[['Stage', 'Layer', 'R_Mask','R_MLM',  'R_illType','R1','R2', 'R3', 'B_Mask','B_MLM','B_illType', 'B1', 'B2', 'B3' ]]



                flagNA=[]
                flagMask=[]
                flagMLM=[]
                flagType=[]
                data=data.dropna()
                for i in range(data.shape[0]):

                    flagNA.append( data.iloc[i,5].strip()==data.iloc[i,11].strip()and
                                   data.iloc[i,6].strip()==data.iloc[i,12].strip() and
                                   data.iloc[i,7].strip()==data.iloc[i,13].strip())

                    flagMask.append(data.iloc[i,2].strip()==data.iloc[i,8].strip())
                    if len(str(data.iloc[i,9]))>0:
                        flagMLM.append(str(data.iloc[i,3])[0]==str(data.iloc[i,9])[0])
                    else:
                        flagMLM.append(False)
                    flagType.append(data.iloc[i,4][0].upper()==data.iloc[i,10][0].upper())
                data['flagIllType']=flagType
                data['flagNA']=flagNA
                data['flagMask']=flagMask
                data['flagMLM']=flagMLM
                data['Pre']=preAlign
        else:
            data=pd.DataFrame(columns=['Name', 'X_x_x', 'Y_x_x', 'X_y_x', 'Y_y_x', 'X_x_y', 'Y_x_y', 'X_y_y',
       'Y_y_y', 'C1', 'C2', 'C3', 'C4'])
        #################################################
        if self.f2==True:
            stepx = self.exLineEdit1_9.text().strip()
            stepy = self.exLineEdit1_10.text().strip()
            if self.part.upper()[-2:] == '-L':
                if stepx.isdigit() or stepx.replace('.','8').isdigit():
                    offset = (eval(stepx)) / 2
                else:
                    reply = QMessageBox.information(self, "注意", '大视场产品，请输入Step_X!!\n\n坐标转换未完成', QMessageBox.Yes,
                                                    QMessageBox.Yes)
                    return
            f = open('p:/recipe/recipe/'+self.part)
            df = [i.strip() for i in f if i.strip() != '']
            zb = df[
                 df.index('+=================+=====+=====+==========+==========+============+============+') + 1:df.index(
                     '+-----------------+-----+-----+----------+----------+------------+------------+')]
            zb = pd.DataFrame(zb)
            zb = zb[0].str.split('|', expand=True)
            zb = zb[[1, 4, 5]]
            zb.columns = ['Name', 'X', 'Y']
            zb['Name'] = [i.strip().split('_')[0] for i in zb['Name']]
            zb = zb.drop_duplicates()
            zb = zb.reset_index().drop('index', axis=1)
            zbx = zb.loc[[2 * i for i in range(int(zb.shape[0] / 2))]]
            zby = zb.loc[[2 * i + 1 for i in range(int(zb.shape[0] / 2))]]
            zb = pd.merge(zbx, zby, how='outer', on=['Name'])

            zb[['X_x', 'Y_x', 'X_y', 'Y_y']] = zb[['X_x', 'Y_x', 'X_y', 'Y_y']].apply(pd.to_numeric)
            ref = [i.strip() for i in open('P:/Recipe/Coordinate/'+ self.part + '.txt') if
                   "SPM" in i.upper() or "AA157" in i.upper() or "AH" in i.upper()]
            ref = pd.DataFrame(ref)
            ref = ref[0].str.split('\t', expand=True)
            ref = ref[ref[2].str.len()<2]
            ref = ref[[0, 1, 4, 6]]

            zbx = ref[ref[0].str.endswith('X')|ref[0].str.endswith('X2')]
            zbx = zbx.reset_index().drop('index', axis=1)
            zby = ref[ref[0].str.endswith('Y')|ref[0].str.endswith('Y2')]
            zby = zby.reset_index().drop('index', axis=1)
            ref = pd.merge(zbx, zby, how='outer', left_index=True, right_index=True)
            ref=ref.dropna() #坐标文件不对，X/Y坐标未成对出现
            ref = ref.drop(['0_y', '1_y'], axis=1)
            ref[['4_x', '6_x', '4_y', '6_y']] = ref[['4_x', '6_x', '4_y', '6_y']].apply(pd.to_numeric)

            tmp = {'NAA157': 'NAAV', 'NAH325374': 'NSV', 'NAH53': 'NA', 'AA157': 'AA', 'SPM53': '', 'AH325374': 'AH'}
            for i in range(ref.shape[0]):
                if 'NAA157' in ref.iloc[i, 0]:
                    ref.iloc[i, 0] = tmp['NAA157'] + ref.iloc[i, 1][:2]
                elif 'NAH325374' in ref.iloc[i, 0]:
                    ref.iloc[i, 0] = tmp['NAH325374'] + ref.iloc[i, 1][:2]
                elif 'NAH53' in ref.iloc[i, 0]:
                    ref.iloc[i, 0] = tmp['NAH53'] + ref.iloc[i, 1][:2]
                elif 'AA157' in ref.iloc[i, 0]:
                    ref.iloc[i, 0] = tmp['AA157'] + ref.iloc[i, 1][:2]
                elif 'SPM53' in ref.iloc[i, 0]:
                    ref.iloc[i, 0] = tmp['SPM53'] + ref.iloc[i, 1][:2]
                elif 'AH325374' in ref.iloc[i, 0]:
                    ref.iloc[i, 0] = tmp['AH325374'] + ref.iloc[i, 1][:2]
            ref = ref[['0_x', '4_x', '6_x', '4_y', '6_y']]

            stepx = self.exLineEdit1_9.text()
            stepy = self.exLineEdit1_10.text()


            if self.part.strip().upper()[-2:]=='-L':

                ref['X_x'] = [ref.iloc[i, 4] / 1000 for i in range(ref.shape[0])]
                ref['Y_x'] = [-(offset + ref.iloc[i, 3]) / 1000 for i in range(ref.shape[0])]
                ref['X_y'] = [ref.iloc[i, 2] / 1000 for i in range(ref.shape[0])]
                ref['Y_y'] = [-(offset + ref.iloc[i, 1]) / 1000 for i in range(ref.shape[0])]
                ref = ref[['0_x', 'X_x', 'Y_x', 'X_y', 'Y_y']]
                ref.columns = zb.columns

            else:
                ref.columns = zb.columns
                ref['X_x'] =  ref['X_x']/1000
                ref['Y_x'] = ref['Y_x']/1000
                ref['X_y'] = ref['X_y']/1000
                ref['Y_y'] = ref['Y_y']/1000



            zb = pd.merge(zb, ref, how='left', on='Name')
            zb['C1'] = [abs(zb.iloc[i, 5] - zb.iloc[i, 1]) < 0.0001 for i in range(zb.shape[0])]
            zb['C2'] = [abs(zb.iloc[i, 6] - zb.iloc[i, 2]) < 0.0001 for i in range(zb.shape[0])]
            zb['C3'] = [abs(zb.iloc[i, 7] - zb.iloc[i, 3]) < 0.0001 for i in range(zb.shape[0])]
            zb['C4'] = [abs(zb.iloc[i, 8] - zb.iloc[i, 4]) < 0.0001 for i in range(zb.shape[0])]
            tmp=data.columns
            zb.columns=data.columns[0:zb.shape[1]]
            print(zb)
            zb=zb.round({"R1":5,'R2':5,'Layer':5,'MLM':5,'Type':5,'B1':5,'B2':5,'B3':5})
            data=pd.concat([data,zb])
            data=data[tmp]









        #####################################################################
        self.SHOW_EXCEL(xlsdata=data)
        self.exTable.resizeColumnsToContents()


        try:


            if self.f2==True:
                data.to_csv('P:/Recipe/NA_SIGMA/' + self.part + '_New.csv',index=None)
            else:
                data.to_csv('P:/Recipe/NA_SIGMA/' + self.part + '_Old.csv', index=None)

        except:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '需要权限保存比对结果')
            self.box.showNormal()
    def onExBtn_4(self): #read Nikondate
        fname,_=QFileDialog.getOpenFileName(self,'Open file','Z:\\_DailyCheck\\NikonRecipe','CSV FILE(*.csv)')
        if len(fname)>10:
            tmp = pd.read_csv(fname)
            tmp = tmp.fillna('')
            l = list((tmp.columns))
            l[0]='PartID'
            l[-1]='Dis-Connected Tool'
            tmp.columns=l
            self.SHOW_EXCEL(xlsdata=tmp)
            self.exTable.resizeColumnsToContents()
    def onExBtn_5(self): #read OVL Result
        tmp = '\\\\10.4.72.74\\litho\overlay\\Result.csv'
        df = pd.read_csv(tmp, encoding='GBK')
        df = df.fillna('')
        df = df[df['absolute-check']==False ]
        df = df[df['range-check']==False]
        df = df[['Part', 'Stage', 'OVL-PPID', 'ToolType', 'PPID', 'Count', 'Path',
            'd11', 'd12', 'd21', 'd22', 'd31', 'd32', 'd41', 'd42',
            'd51', 'd52', 'd61', 'd62', 'd71', 'd72', 'd81', 'd82',
            'absolute-check','range-check', 'run']]

        df[['Count','d11','d12', 'd21', 'd22', 'd31', 'd32', 'd41', 'd42']]\
            = df[['Count','d11','d12', 'd21', 'd22', 'd31', 'd32', 'd41', 'd42']].astype(int)

        for i in range(df.shape[0]):
            for j in range(15,23):
                if df.iloc[i,j]!="":
                    df.iloc[i,j]=int(df.iloc[i,j])

        self.SHOW_EXCEL(xlsdata=df)
        self.exTable.resizeColumnsToContents()
    def onExBtn_6(self):  # CD SEM AMP RESULT
        tmp =r'Z:\_DailyCheck\CD_SEM_Recipe\check\_AMP_CHECK.csv'
        df = pd.read_csv(tmp,encoding='GBK')
        df = df.fillna('')
        # df = df[df['Flag']==False]
        self.SHOW_EXCEL(xlsdata=df)
        self.exTable.resizeColumnsToContents()
    def onExBtn_7(self):  # CD SEM IDP RESULT
        tmp =r'Z:\_DailyCheck\CD_SEM_Recipe\check\_IDP_CHECK.csv'
        df = pd.read_csv(tmp)
        df = df.fillna('')
        df = df[df['Flag']==False]
        self.SHOW_EXCEL(xlsdata=df)
        self.exTable.resizeColumnsToContents()
    def onExBtn_8(self):  # CD SEM IDP RESULT
        tmp = '\\\\10.4.72.74\\litho\overlay\\Result.csv'
        df = pd.read_csv(tmp, encoding='GBK')
        df = df.fillna('')
        df = df[df['run'] == True]
        df = df[df['absolute-check'] == '']
        df = df[['Part','Stage','OVL-PPID','ToolType','PPID']]
        self.SHOW_EXCEL(xlsdata=df)
        self.exTable.resizeColumnsToContents()
    def onExTableItemChangedDoubleClicked(self,item):
        self.df.iloc[item.row(),item.column()] = item.text().upper().strip()
        self.GET_COORDINATE()
        self.SHOW_TABLE()

    def exNAquery(self):
        pass
        dbName = r'P:\_NewBiasTable\NewProduct.mdb'
        Tech = self.exLineEdit1_3.text()
        Layer = self.exLineEdit1_4.text()
        Track = self.exLineEdit1_5.text()
        NA = self.exLineEdit1_8.text()
        Outer = self.exLineEdit1_6.text()
        Inner = self.exLineEdit1_7.text()
        sql = "SELECT * FROM AsmlNaVsTechLayer Where Tech like '" + Tech[:3] + "%' and Layer like '%" + Layer
        sql = sql + "%' and Track like '%" + Track + "%' ORDER BY Tech,Layer,Track"
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + dbName
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        tmp = []
        if rs.RecordCount == 0:
            df=pd.DataFrame()
        else:
            if rs.RecordCount > 1:
                tmp2 = []
                for i in range(rs.fields.count):
                    tmp2.append(rs.fields(i).name)

                for i in range(rs.RecordCount):

                    tmp.append([rs.fields(i).value for i in range(rs.fields.count)])
                    rs.MoveNext()
        rs, conn = None, None
        df = pd.DataFrame(tmp,columns=tmp2)
        df=df.fillna(' ')
        DF2TABLE(self.exTable, df)
    def exNAupdate(self):
        pass
    def exNAinsert(self):
        dbName = r'P:\_NewBiasTable\NewProduct.mdb'
        Tech = self.exLineEdit1_3.text().upper()
        Layer = self.exLineEdit1_4.text().upper()
        Track = self.exLineEdit1_5.text().upper()
        NA = self.exLineEdit1_8.text()
        Outer = self.exLineEdit1_6.text()
        Inner = self.exLineEdit1_7.text()
        if  len(Inner.strip())>0:
            iType='Annular'
        else:
            iType='Conventional'
        try:
            if len(Tech.strip())!=12 or len(Layer.strip())==0 or len(Track.strip())==0 or NA[1]!='.' or Outer[1]!='.':
                reply=  QMessageBox.information(self, "注意", '请确认输入是否正确', QMessageBox.Yes, QMessageBox.Yes)
                return
        except:
            reply = QMessageBox.information(self, "注意", '请确认输入是否正确', QMessageBox.Yes, QMessageBox.Yes)
            return
        sql = "SELECT * FROM AsmlNaVsTechLayer Where Tech like '" + Tech[:3] + "%' and Layer like '%" + Layer
        sql = sql + "%' and Track like '%" + Track + "%' ORDER BY Tech,Layer,Track"
        conn = win32com.client.Dispatch(r"ADODB.Connection")
        DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + dbName
        conn.Open(DSN)
        rs = win32com.client.Dispatch(r'ADODB.Recordset')
        rs.Open(sql, conn, 1, 3)
        print(sql)
        print(rs.recordcount)
        if rs.recordcount>0:
            reply = QMessageBox.information(self, "注意", '前三位相同设置已存在，未更新', QMessageBox.Yes, QMessageBox.Yes)
        else:
            rs.AddNew()  # 添加一条新记录
            rs.Fields(1).Value = Tech.strip().upper()
            rs.Fields(2).Value = Layer.strip().upper()
            rs.Fields(3).Value = Track.strip().upper()
            rs.Fields(4).Value = Tech.strip().upper()+"_"+Layer.strip().upper()+'_'+Track.strip().upper()
            rs.Fields(5).Value = iType
            rs.Fields(6).Value = NA
            rs.Fields(7).Value = Outer
            rs.Fields(8).Value = Inner
            rs.Update()  # 更新

            rs.AddNew()  # 添加一条新记录
            rs.Fields(1).Value = Tech.strip().upper()[0:3]
            rs.Fields(2).Value = Layer.strip().upper()
            rs.Fields(3).Value = Track.strip().upper()
            rs.Fields(4).Value = Tech.strip().upper()[0:3]+"_"+Layer.strip().upper()+'_'+Track.strip().upper()
            rs.Fields(5).Value = iType
            rs.Fields(6).Value = NA
            rs.Fields(7).Value = Outer
            rs.Fields(8).Value = Inner
            rs.Update()  # 更新

            sql = "SELECT * FROM AsmlNaVsTechLayer Where Tech like '" + Tech[:3] + "%' and Layer like '%" + Layer
            sql = sql + "%' and Track like '%" + Track + "%' ORDER BY Tech,Layer,Track"
            conn = win32com.client.Dispatch(r"ADODB.Connection")
            DSN = 'PROVIDER = Microsoft.Jet.OLEDB.4.0;DATA SOURCE = ' + dbName
            conn.Open(DSN)
            rs = win32com.client.Dispatch(r'ADODB.Recordset')
            rs.Open(sql, conn, 1, 3)
            tmp = []
            if rs.RecordCount == 0:
                df = pd.DataFrame()
            else:
                if rs.RecordCount > 1:
                    tmp2 = []
                    for i in range(rs.fields.count):
                        tmp2.append(rs.fields(i).name)

                    for i in range(rs.RecordCount):
                        tmp.append([rs.fields(i).value for i in range(rs.fields.count)])
                        rs.MoveNext()

            df = pd.DataFrame(tmp, columns=tmp2)
            df = df.fillna(' ')
            DF2TABLE(self.exTable, df)
        rs, conn = None, None

    def Calculate_Die_Notch_Down(self,stepX,stepY,dieX,dieY,part,wee,Ximg,pricision):
        tmpcount = 0
        # stepX = eval(input(" Please Input Step X (mm): "))
        # stepY = eval(input(" Please Input Step Y (mm): "))
        # dieX = eval(input(" Please Input Die X (mm): "))
        # dieY = eval(input(" Please Input Die Y (mm): "))
        # offX = eval(input(" Please Input Map Offset X (mm): "))
        # offY = eval(input(" Please Input Map Offset Y (mm): "))
        #wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        # part = input('Please Input Part Name: ')
        # wee=97
        col1,row1 = int(wee // stepX),int(wee // stepY)
        col2,row2 = int(stepX / dieX),int(stepY / dieY)
        shotDie = stepX / dieX * stepY / dieY
        summary=[]
        # pricision=0.1


        #=====================================================
        # 'This script is trying to minimize NonebyLS area at left and right sides of wafer.
        # 'Assume: FEC is 3 mm, ZbyLS can reach 8 mm from wafer edge along X-axis
        # '        Pre scan is 13 mm, slit scan is 6 mm per side
        # '        Field is not exposable when scan cent is more than 99.5 mm from wafer center.
        # 'Warning: Step size X less than 16 mm is NOT considered

        # LSmapX
        b1,b2=stepX,stepY
        # Ximg=0  #Image Shift on reticle X(size on wafer)
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

        # if "old"!='old':
        #     summary['leftPartialSize'] = round((97 - stepX / 2 + summary['shiftX']) % stepX, 3)
        #     summary['rightPartialSize'] = round((97 - stepX / 2 - summary['shiftX']) % stepX, 3)
        #     # summary['miniSizeX'] = 0.5 * stepX - 5.9
        #     summary['miniSizeX'] = 0.5 * stepX - 5.8999
        #     # summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.9))
        #     #二进制小数点进度，也可以用  math.ceil( (0.5*stepX-5.9)*1000 )/1000,仅精确到um，或*10000
        #     # https://www.jb51.net/article/159505.htm
        #     summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.8999))
        #     # summary['rightFlag'] = summary['rightPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.9))
        #     summary['rightFlag'] = summary['rightPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.8999))
        #     summary = summary.sort_values(by=['TotalShot','DieQty'])
        if "new"=='new': #for 3mm coverage
            summary['leftPartialSize'] = round((99.5 - stepX / 2 + summary['shiftX']) % stepX, 3)
            summary['rightPartialSize'] = round((99.5 - stepX / 2 - summary['shiftX']) % stepX, 3)
            summary['miniSizeX'] = 0.5 * stepX - 3.3999
            # summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.9))
            #二进制小数点进度，也可以用  math.ceil( (0.5*stepX-5.9)*1000 )/1000,仅精确到um，或*10000
            # https://www.jb51.net/article/159505.htm
            summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 3.3999))
            summary['rightFlag'] = summary['rightPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 3.3999))
            summary = summary.sort_values(by=['TotalShot','DieQty'])



        return summary
    def Calculate_Die_Notch_Left(self,stepX,stepY,dieX,dieY,part,wee,Ximg,pricision):
        tmpcount = 0
        # stepX = eval(input(" Please Input Step X (mm): "))
        # stepY = eval(input(" Please Input Step Y (mm): "))
        # dieX = eval(input(" Please Input Die X (mm): "))
        # dieY = eval(input(" Please Input Die Y (mm): "))
        # offX = eval(input(" Please Input Map Offset X (mm): "))
        # offY = eval(input(" Please Input Map Offset Y (mm): "))
        #wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        # part = input('Please Input Part Name: ')
        # wee=97
        col1,row1 = int(wee // stepX),int(wee // stepY)
        col2,row2 = int(stepX / dieX),int(stepY / dieY)
        shotDie = stepX / dieX * stepY / dieY
        summary=[]
        # pricision=0.1


        #=====================================================
        # 'This script is trying to minimize NonebyLS area at left and right sides of wafer.
        # 'Assume: FEC is 3 mm, ZbyLS can reach 8 mm from wafer edge along X-axis
        # '        Pre scan is 13 mm, slit scan is 6 mm per side
        # '        Field is not exposable when scan cent is more than 99.5 mm from wafer center.
        # 'Warning: Step size X less than 16 mm is NOT considered

        # LSmapX
        b1,b2=stepX,stepY
        # Ximg=0  #Image Shift on reticle X(size on wafer)
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
        summary['leftPartialSize']=round((99.5-stepX/2+summary['shiftX'])%stepX,3)
        summary['rightPartialSize']=round((99.5-stepX/2-summary['shiftX'])%stepX,3)
        summary['miniSizeX']=0.5*stepX-3.3999
        summary['leftFlag']=summary['leftPartialSize'].apply(lambda x: True if x==0 else x>(0.5*stepX-3.3999))
        summary['rightFlag']=summary['rightPartialSize'].apply(lambda x: True if x==0 else x>(0.5*stepX-3.3999))



        summary = summary.sort_values(by=['TotalShot','DieQty'])
        return summary
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

        return (tmpcount1 / shotDie < 0.05) and (tmpcount / gdw < 0.005)
        # print(tmpcount1,shotDie,tmpcount,gdw)
        # return False
    def gating_notch_left(self,shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee):
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
                f5 = ((sx + dieX) > 92) and ((sy + dieY < 13 and sy + dieY > -13) or (sy < 13 and sy > -13))
                f5 = not f5

                f6 = (sx < -94) and ((sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
                f6 = not f6

                if (f1 and f2 and f3 and f4 and f5 and f6):
                    tmpcount += 1

                tmpcount1 = tmpcount1 + len([i for i in [f1, f2, f3, f4] if i == True]) / 4

        return (tmpcount1 / shotDie < 0.05) and (tmpcount / gdw < 0.005)
        # print(tmpcount1,shotDie,tmpcount,gdw)
        # return False
    def Plot_Map_Notch_Left(self,stepX,stepY,dieX,dieY,part,wee,offX,offY,largeFlag):

        print(stepX,stepY,dieX,dieY,offX,offY)


        tmpcount = 0
        # stepX = eval(input(" Please Input Step X (um): "))/1000
        # stepY = eval(input(" Please Input Step Y (um): "))/1000
        # dieX = eval(input(" Please Input Die X (um): "))/1000
        # dieY = eval(input(" Please Input Die Y (um): "))/1000
        # offX = eval(input(" Please Input Map Offset X (um): "))/1000
        # offY = eval(input(" Please Input Map Offset Y (um): "))/1000
        # wee = 100 - eval(input(" Please Input Edge Exclusion (mm): "))
        # part = input('Please Input Part Name: ')
        gdw = 3.14*97*97/dieX/dieY

        fullshot=0
        partialshot=0



        col1 = int(wee // stepX)
        row1 = int(wee // stepY)

        col2 = int(round(stepX / dieX,0))
        row2 = int(round(stepY / dieY,0))

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

                if f1 and f2 and f3 and f4:
                    fullshot +=1
                else:
                    if f1 or f2 or f3 or f4:
                        partialshot += 1



                if f1 and f2 and f3 and f4 and f6 and f5:
                    square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.3, fill=False, edgecolor='red',
                                       linewidth=1)
                    ax.add_patch(square)
                    totalDie = totalDie + shotDie

                else:
                    if f1 or f2 or f3 or f4:

                        # square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
                        #                    edgecolor='red', linewidth=0.5)
                        # ax.add_patch(square)
                        partialShotDie = 0

                        # flag = self.gating_notch_left(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee)
                        # flag=False
                        if part[0:2]=='D6' or part[0:2]=='XU' or part[0:2]=='XV' or part[0:2]=='UF' or part[-1]=='N' or part[-1]=="M":
                            flag=False
                        else:
                            flag = self.gating_notch_left(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,
                                                          wee)




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
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='black',
                                                           alpha=0.7, fill=True, edgecolor='pink', linewidth=0.3)
                                        ax.add_patch(square)

                                    else:

                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
                                                           alpha=0.5, fill=True, edgecolor='blue', linewidth=0.3)
                                        ax.add_patch(square)
                                        partialShotDie += 1

                                else:
                                    if f1 or f2 or f3 or f4:
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
                                                           alpha=0.5, fill=False, edgecolor='green', linewidth=0.3)
                                        ax.add_patch(square)

                            if flag == False:
                                square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.3, fill=False,
                                                   edgecolor='red', linewidth=1)
                                ax.add_patch(square)
                            else:
                                square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.05, fill=True,
                                                   edgecolor='grey', linewidth=0.3)
                                ax.add_patch(square)



                            # square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=1, fill=False,
                            #                    edgecolor='red', linewidth=0.5)
                            # ax.add_patch(square)
                        totalDie = totalDie + partialShotDie

        square = Rectangle(xy=(92, -13), width=8, height=26, facecolor='yellow', alpha=1, fill=False,
                           edgecolor='blue', linewidth=0.8)
        ax.add_patch(square)

        square = Rectangle(xy=(95, -8), width=5, height=16, facecolor='yellow', alpha=0.5, fill=True, edgecolor='yellow',
                           linewidth=0.3)
        ax.add_patch(square)

        square = Rectangle(xy=(-100, -14), width=6, height=28, facecolor='yellow', alpha=1, fill=False,
                           edgecolor='blue', linewidth=0.8)
        ax.add_patch(square)

        triangle = plt.Polygon([[-95, 0], [-100, 3], [-100, -3]], color = 'r', alpha = 0.3 )#顶点坐标颜色α
        ax.add_patch(triangle)




        x, y = 0, 0
        ax.plot(x, y, 'ro')

        # plt.xlim(-200,160)
        # plt.ylim(-160,200)


        plt.axis('scaled')
        # ax.set_xlim(-8, 8)
        # ax.set_ylim(-8,8)
        plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length

        plt.text(-30,60,'Total Shot Qty: ' + str(fullshot+partialshot) + ' Full Shot: ' + str(fullshot) +  ' Partial Shot: ' + str(partialshot) )
        plt.text(-30, 50, 'Total Die Qty: ' + str(int(totalDie)))
        self.exLineEdit1_8.setText('ASML'+ str(int(totalDie)))
        plt.text(-30, 40, 'Step Size: ' + str(round(stepX,5)) + ', ' + str(round(stepY,5)),fontweight = 'bold')
        plt.text(-30, 30, 'Die Size ' + str(round(dieX,5)) + ', ' + str(round(dieY,5)),fontweight = 'bold')
        plt.text(-30, 20, 'Offset Size: ' + str(round(offX,5)) + ', ' + str(round(offY,5)),fontweight = 'bold')
        plt.text(-30, 10, 'Edge Exclusion: ' + str(100 - wee))
        plt.text(-30, 0, 'Unit: mm ')
        plt.text(-30, -10, 'Product: ' + part)
        plt.text(-30,-20,'Orientation: Notch Left')

        tmpsize = (100-5.5-stepX/2-offX)%stepX
        plt.text (-30,-30,'Image-X Size:' + str(round(tmpsize, 5)),fontweight = 'bold')
        plt.text (-30,-40,'Image-X Center Shift:' + str(round((tmpsize - stepX) / 2, 5)),fontweight = 'bold')

        #below comment for recipe creation
        plt.text(-110, -110,
                 '9 clock partial size in 97mm radius:' + str(round((wee - stepX / 2 + offX) % stepX * 1000, 3))+'um',
                 color='blue')
        plt.text(-110, -118,
                 '3 clock partial size in 97mm radius:' + str(round((wee - stepX / 2 - offX) % stepX * 1000, 3))+'um',
                 color='blue')


        print(type(stepX))

        plt.title('DATA ONLY FOR LARGE FILED PRODUCT!!!!!-->ASML MAP--CD MAP--ASML OVL MAP:\n\nCD:StepX ' + str(stepY*1000)
                  + ',   StepY ' + str(stepX*1000)
                  + ',   OffsetX ' + str(round(-offY*1000,2))
                  + ',   OffsetY ' + str(round(offX*1000,2))
                  +'\n\nASML/DUV Layer OVL:StepX ' + str(stepX*1000)
                  + ',   StepY ' + str(stepY*1000)
                  + '\nKLA Archer Rotation: "0" degree!!,  Locking Corner Label:"A"!!', fontsize='large',fontweight = 'bold',color='red',loc='left')


        try:
            plt.savefig('P:\\recipe\\image2\\' + part + '_ASML.jpg', dpi=600)
            plt.savefig('P:\\recipe\\image\\' + part + '_ASML.jpg', dpi=600)
        except:
            pass
        plt.show()


        #y=x
        self.exLineEdit1_56.setText(self.exLineEdit1_31.text())
        #x= -（y+stepY/4)
        tmp=round( eval(self.exLineEdit1_30.text()) + stepY/4,5)
        self.exLineEdit1_55.setText(str(-tmp))



        if tmp>= -stepY/4 and tmp<=stepY/4:
            self.exLineEdit1_55.setText(str(-tmp))
        else:
            if tmp<0:
                self.exLineEdit1_55.setText(str(round(stepY/2+tmp,5)))
            else:
                self.exLineEdit1_55.setText(str(round(stepY/2-tmp,5)))
    def Plot_Map_Notch_Down_BAK(self,stepX,stepY,dieX,dieY,part,wee,offX,offY,largeFlag,laserY_Flag=True,partial_Flag=False):


        if largeFlag==True:
            part=part+'-L'
        gdw = 3.14*97*97/dieX/dieY




        col1 = int(wee // stepX)
        row1 = int(wee // stepY)

        col2 = int(round(stepX / dieX,0))
        row2 = int(round(stepY / dieY,0))

        shotDie = stepX / dieX * stepY / dieY

        totalDie = 0

        fig = plt.figure(figsize=(10, 10))
        ax = fig.add_subplot(111)


        cir1 = Circle(xy=(0, 0), radius=100, alpha=1, fill=False, edgecolor='black', linewidth=1)
        ax.add_patch(cir1)
        cir1 = Circle(xy=(0, 0), radius=wee, alpha=1, fill=False, edgecolor='blue', linewidth=1)
        ax.add_patch(cir1)
        fullshot=0
        partialshot=0
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

                if f1 and f2 and f3 and f4:
                    fullshot +=1
                else:
                    if f1 or f2 or f3 or f4:
                        partialshot += 1


                if f1 and f2 and f3 and f4 and f6 and f5:
                    square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.3, fill=False, edgecolor='red',
                                       linewidth=0.3)
                    ax.add_patch(square)
                    totalDie = totalDie + shotDie
                else:
                    if f1 or f2 or f3 or f4:

                        partialShotDie = 0

                        # flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,wee)
                        # flag=False
                        if part[0:2]=='D6' or part[0:2]=='XU' or part[0:2]=='XV' or part[0:2]=='UF' or part[-1]=='N' or part[-1]=="M":
                            flag=False
                        else:
                            flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,
                                                          wee)
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
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='black',
                                                           alpha=0.7, fill=True, edgecolor='pink', linewidth=0.3)
                                        ax.add_patch(square)

                                    else:

                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
                                                           alpha=0.3, fill=True, edgecolor='red', linewidth=0.3)
                                        ax.add_patch(square)
                                        partialShotDie += 1

                                else:
                                    if f1 or f2 or f3 or f4:
                                        square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
                                                           alpha=0.5, fill=False, edgecolor='green', linewidth=0.3)
                                        ax.add_patch(square)
                            print(flag)
                            if flag==False:
                                square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.3, fill=False,
                                                   edgecolor='red', linewidth=0.3)
                                ax.add_patch(square)
                            else:
                                square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.05, fill=True,
                                                   edgecolor='grey', linewidth=0.3)
                                ax.add_patch(square)

                        totalDie = totalDie + partialShotDie

        square = Rectangle(xy=(-13, 92), width=26, height=8, facecolor='yellow', alpha=1, fill=False,
                           edgecolor='blue', linewidth=0.8)
        ax.add_patch(square)

        square = Rectangle(xy=(-8, 95), width=16, height=5, facecolor='yellow', alpha=0.3, fill=True,
                           edgecolor='black', linewidth=0.3)
        ax.add_patch(square)


        square = Rectangle(xy=(-14, -100), width=28, height=6, facecolor='yellow', alpha=1, fill=False,
                           edgecolor='blue', linewidth=0.8)
        ax.add_patch(square)

        triangle = plt.Polygon([[0, -95], [3, -100], [-3, -100]], color='purple', alpha=0.3)  # 顶点坐标颜色α
        ax.add_patch(triangle)

        x, y = 0, 0
        ax.plot(x, y, 'ro')

        plt.axis('scaled')
        # ax.set_xlim(-8, 8)
        # ax.set_ylim(-8,8)
        plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length

        plt.text(-30, 60, 'Total Shot Qty: ' + str(fullshot + partialshot) + ' Full Shot: ' + str(
            fullshot) + ' Partial Shot: ' + str(partialshot))
        plt.text(-30, 50, 'Total Die Qty: ' + str(int(totalDie)))
        self.exLineEdit1_6.setText('NIKON' + str(int(totalDie)))
        plt.text(-30, 40, 'Step Size: ' + str(round(stepX*1000,2)) + ', ' + str(round(stepY*1000,2)),fontweight = 'bold')
        plt.text(-30, 30, 'Die Size ' + str(round(dieX*1000,2)) + ', ' + str(round(dieY*1000,2)),fontweight = 'bold')
        plt.text(-30, 20, 'Offset Size: ' + str(round(offX*1000,2)) + ', ' + str(round(offY*1000,2)),fontweight = 'bold')
        plt.text(-30, 10, 'Edge Exclusion: ' + str(round(100 - wee,1)) +'mm' )
        plt.text(-30, 0, 'Unit: um ')
        plt.text(-30, -10, 'Product: ' + part )
        plt.text(-30, -20, 'Orientation:Notch Down ' )


        if largeFlag==True or partial_Flag==True:

            tmpsize = (100 - 5.5 - stepY / 2 - offY) % stepY
            plt.text(-30,-30,'NIKON BLIND -Y:  ' + str(round(-stepY / 2, 5)*1000-32),fontweight = 'bold')
            plt.text(-30,-40,'NIKON BLIND +Y:  ' + str(round(-stepY / 2 + tmpsize, 5)*1000),fontweight = 'bold')
            plt.text(-30,-50,'NIKON BLIND SHIFT:  ' + str(round(-(tmpsize - stepY) / 2, 5)*1000),fontweight = 'bold')

        if laserY_Flag==True:
            plt.text(-30, -60, '3,9 clock optimized')
        else:
            plt.text(-30, -60, '3,9 clock not optimized')

        if part[-1]=='N' or part[-1]=='M':
            plt.text(-100, -70, 'ALL SHOTS SHOULD BE EXPOSED FOR PART NAME ENDING WITH "N"(SPLIT GATE)!!!',fontweight='bold',color='red')
            plt.text(-100, -80, 'NO PARTIAL IMAGE SET AT LASER MARK AREA',fontweight='bold',color='red')

        plt.text(-110, -110, '9 clock partial size in 97mm radius:' + str(round((wee - stepX / 2 + offX) % stepX*1000, 3))+'um',color='blue')
        plt.text(-110, -118, '3 clock partial size in 97mm radius:' + str(round((wee - stepX / 2 - offX) % stepX*1000, 3))+'um',color='blue')

        if largeFlag==True:
            plt.title('NIKON MAP FOR LARGE FIELD', fontsize='large', fontweight='bold', color='red', loc='center')
        else:
            plt.title('STANDARD FIELD MAP', fontsize='large', fontweight='bold', color='red', loc='center')

        try:
            if largeFlag==True:
                plt.savefig('P:\\recipe\\image2\\' + part + '_NIKON.jpg', dpi=600)
                plt.savefig('P:\\recipe\\image\\' + part + '_NIKON.jpg', dpi=600)
            else:
                plt.savefig('P:\\recipe\\image2\\' + part + '.jpg', dpi=600)
                plt.savefig('P:\\recipe\\image\\' + part + '.jpg', dpi=600)

        except:
            pass
        plt.show()







        map =  open('C:/temp/map_'+part+'.txt','w')

        for row in range((row1 + 2)*row2,(-row1-1)*row2,-1):
            for column in range((-col1-1)*col2,(col1+2)*col2,1):





                llx= column * dieX

                lly = row*dieY


                f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                f2 = (pow(llx + dieX, 2) + pow(lly, 2)) < pow(wee, 2)
                f3 = (pow(llx + dieX, 2) + pow(lly + dieY, 2)) < pow(wee, 2)
                f4 = (pow(llx, 2) + pow(lly + dieY, 2)) < pow(wee, 2)
                # laser mark
                f5 = ((lly + dieY) > 92) and ((llx + dieX < 13 and llx + dieX > -13) or (llx < 13 and llx > -13))
                f5 = not f5
                # notch
                f6 = (lly < -94) and ((llx + dieX < 14 and llx + dieX > -14) or (llx < 14 and llx > -14))
                f6 = not f6

                if f1 and f2 and f3 and f4:
                    if f5 and f6:
                        map.write("1")
                    else:
                        map.write('0')
                elif f1 or f2 or f3 or f4:
                    map.write('P')
                else:
                    map.write('0')

            map.write('\n')
        map.close()
    def Plot_Map_Notch_Down(self,stepX,stepY,dieX,dieY,part,wee,offX,offY,largeFlag,laserY_Flag=True,partial_Flag=False):


        if largeFlag==True:
            part=part+'-L'
        gdw = 3.14*97*97/dieX/dieY




        col1 = int(wee // stepX)
        row1 = int(wee // stepY)

        col2 = int(round(stepX / dieX,0))
        row2 = int(round(stepY / dieY,0))

        shotDie = stepX / dieX * stepY / dieY

        totalDie = 0

        fig = plt.figure(figsize=(10, 10))
        ax = fig.add_subplot(111)


        cir1 = Circle(xy=(0, 0), radius=100, alpha=1, fill=False, edgecolor='black', linewidth=1)
        ax.add_patch(cir1)
        cir1 = Circle(xy=(0, 0), radius=wee, alpha=1, fill=False, edgecolor='blue', linewidth=1)
        ax.add_patch(cir1)
        fullshot=0
        partialshot=0

        #==========
        #for txt output
        arr0= pd.DataFrame([['0' for i in range(col2)] for i in range(row2)])
        arr1= pd.DataFrame([['1' for i in range(col2)] for i in range(row2)])
        arrcol=pd.DataFrame(columns=[i for i in  range(col2)])
        arr = pd.DataFrame(columns=[i for i  in range(col2)])

        #===========



        for i in range(-col1 - 1, col1 + 2):
            arrcol = pd.DataFrame(columns=[i for i in range(col2)])

            for j in range(-row1 - 1, row1 + 2):
                arrs = pd.DataFrame([['0' for i in range(col2)] for i in range(row2)])  # draw map

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


                #===calculate shots number
                #todo notchfujn相切，但四点未在map内的需要考虑，待完成
                if f1 and f2 and f3 and f4:
                    fullshot +=1
                else:
                    if f1 or f2 or f3 or f4 :
                        partialshot += 1

                #===calculate die number
                if f1 and f2 and f3 and f4:
                    square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.3, fill=False, edgecolor='red',
                                       linewidth=1)
                    ax.add_patch(square)
                    totalDie = totalDie + shotDie
                    arr11 = arr1.copy()


                    #落在打标、notch的管芯要重算
                    for k in range(0, row2):
                        for l in range(0, col2):
                            sx = llx + l * dieX
                            sy = lly + k * dieY

                            f5 = ((sy + dieY) > 92) and (
                                        (sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
                            f6 = (sy < -94) and ((sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                            if f5 or f6:
                                totalDie = totalDie-1
                                square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey', alpha=0.5,
                                                   fill=False, edgecolor='green', linewidth=0.3)
                                ax.add_patch(square)
                                arr11.iloc[row2 - 1 - k, l] = '0'

                    arrcol = pd.concat([arr11, arrcol], axis=0)  ## draw map

                #todo  txt format revision


















                elif f1 or f2 or f3 or f4 :


                    partialShotDie=0
                    if part[0:2]=='D6' or part[0:2]=='XU' or part[0:2]=='XV' or part[0:2]=='UF' or part[-1]=='N' or part[-1]=="M":
                        flag=False
                    else:
                        flag = self.gating_notch_down(shotDie, gdw, dieX, dieY, col2, row2, llx, lly, stepX, stepY,
                                                      wee)

                    for k in range(0, row2):
                        for l in range(0, col2):
                            sx = llx + l * dieX
                            sy = lly + k * dieY

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
                                    square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='black',
                                                       alpha=0.7, fill=True, edgecolor='pink', linewidth=0.3)
                                    ax.add_patch(square)

                                    arrs = arr0.copy()  # draw map


                                else:

                                    square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='green',
                                                       alpha=0.3, fill=True, edgecolor='red', linewidth=0.3)
                                    ax.add_patch(square)
                                    partialShotDie += 1

                                    arrs.iloc[row2-1-k,l] = '1'  # draw map

                            elif f1 or f2 or f3 or f4:
                                square = Rectangle(xy=(sx, sy), width=dieX, height=dieY, facecolor='grey',
                                                   alpha=0.5, fill=False, edgecolor='green', linewidth=0.3)
                                ax.add_patch(square)
                                arrs.iloc[row2-1-k,l] = 'P'  # draw map
                            else:
                                arrs.iloc[row2 - 1 - k, l] = '0'

                        if flag==False:
                            square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.3, fill=False,
                                               edgecolor='red', linewidth=1)
                            ax.add_patch(square)
                        else:
                            square = Rectangle(xy=(llx, lly), width=stepX, height=stepY, alpha=0.05, fill=True,
                                               edgecolor='red', linewidth=1)
                            ax.add_patch(square)

                    plt.text(llx+2, lly+2, str(partialShotDie),fontweight='bold',fontsize=14)
                    if flag==True:
                        plt.text(llx + stepX/2-4, lly + stepY/2-4, "X", fontweight='bold',color='red',fontsize=20)
                    totalDie = totalDie + partialShotDie
                    print(arrs.shape, arrcol.shape)
                    arrcol = pd.concat([arrs,arrcol],axis=0) #draw map


                else:
                    arrcol = pd.concat([arr0,arrcol],axis=0)

            print('=====' + str(i) )
            print(arrcol.shape)
            print(arrcol)
            arr = pd.concat([arr,arrcol],axis=1)
            # arr=arr.fillna(' ')


        square = Rectangle(xy=(-13, 92), width=26, height=8, facecolor='yellow', alpha=1, fill=False,
                           edgecolor='blue', linewidth=0.8)
        ax.add_patch(square)

        square = Rectangle(xy=(-8, 95), width=16, height=5, facecolor='yellow', alpha=0.3, fill=True,
                           edgecolor='black', linewidth=0.3)
        ax.add_patch(square)


        square = Rectangle(xy=(-14, -100), width=28, height=6, facecolor='yellow', alpha=1, fill=False,
                           edgecolor='blue', linewidth=0.8)
        ax.add_patch(square)

        triangle = plt.Polygon([[0, -95], [3, -100], [-3, -100]], color='purple', alpha=0.3)  # 顶点坐标颜色α
        ax.add_patch(triangle)

        x, y = 0, 0
        ax.plot(x, y, 'ro')

        plt.axis('scaled')
        # ax.set_xlim(-8, 8)
        # ax.set_ylim(-8,8)
        plt.axis('equal')  # changes limits of x or y axis so that equal increments of x and y have the same length

        plt.text(-30, 60, 'Total Shot Qty: ' + str(fullshot + partialshot) + ' Full Shot: ' + str(
            fullshot) + ' Partial Shot: ' + str(partialshot))
        plt.text(-30, 50, 'Total Die Qty: ' + str(int(totalDie)))
        self.exLineEdit1_6.setText('NIKON' + str(int(totalDie)))
        plt.text(-30, 40, 'Step Size: ' + str(round(stepX*1000,2)) + ', ' + str(round(stepY*1000,2)),fontweight = 'bold')
        plt.text(-30, 30, 'Die Size ' + str(round(dieX*1000,2)) + ', ' + str(round(dieY*1000,2)),fontweight = 'bold')
        plt.text(-30, 20, 'Offset Size: ' + str(round(offX*1000,2)) + ', ' + str(round(offY*1000,2)),fontweight = 'bold')
        plt.text(-30, 10, 'Edge Exclusion: ' + str(round(100 - wee,1)) +'mm' )
        plt.text(-30, 0, 'Unit: um ')
        plt.text(-30, -10, 'Product: ' + part )
        plt.text(-30, -20, 'Orientation:Notch Down ' )


        if largeFlag==True or partial_Flag==True:

            tmpsize = (100 - 5.5 - stepY / 2 - offY) % stepY
            plt.text(-30,-30,'NIKON BLIND -Y:  ' + str(round(-stepY / 2, 5)*1000-32),fontweight = 'bold')
            plt.text(-30,-40,'NIKON BLIND +Y:  ' + str(round(-stepY / 2 + tmpsize, 5)*1000),fontweight = 'bold')
            plt.text(-30,-50,'NIKON BLIND SHIFT:  ' + str(round(-(tmpsize - stepY) / 2, 5)*1000),fontweight = 'bold')

        if laserY_Flag==True:
            plt.text(-30, -60, '3,9 clock optimized')
        else:
            plt.text(-30, -60, '3,9 clock not optimized')

        if part[-1]=='N' or part[-1]=='M':
            plt.text(-100, -70, 'ALL SHOTS SHOULD BE EXPOSED FOR PART NAME ENDING WITH "N"(SPLIT GATE)!!!',fontweight='bold',color='red')
            plt.text(-100, -80, 'NO PARTIAL IMAGE SET AT LASER MARK AREA',fontweight='bold',color='red')

        plt.text(-110, -110, '9 clock partial size in 97mm radius:' + str(round((wee - stepX / 2 + offX) % stepX*1000, 3))+'um',color='blue')
        plt.text(-110, -118, '3 clock partial size in 97mm radius:' + str(round((wee - stepX / 2 - offX) % stepX*1000, 3))+'um',color='blue')

        if largeFlag==True:
            plt.title('NIKON MAP FOR LARGE FIELD', fontsize='large', fontweight='bold', color='red', loc='center')
        else:
            plt.title('STANDARD FIELD MAP', fontsize='large', fontweight='bold', color='red', loc='center')



        try:
            if largeFlag==True:


                plt.savefig('P:\\recipe\\image2\\' + part + '_NIKON.jpg', dpi=600)
                plt.savefig('P:\\recipe\\image\\' + part + '_NIKON.jpg', dpi=600)


            else:
                plt.savefig('P:\\recipe\\image2\\' + part + '.jpg', dpi=600)
                plt.savefig('P:\\recipe\\image\\' + part + '.jpg', dpi=600)

        except:
            pass
        plt.show()

        reply = QMessageBox.question(self, '注意', 'Save Txt Map??\n\n',
                                     QMessageBox.Yes | QMessageBox.No | QMessageBox.Abort, QMessageBox.Abort)
        if reply == QMessageBox.Yes:
            try:
                arr.to_csv('c:/temp/000.csv', index=None, header=None)
                arr.to_csv('p:/recipe/image2/'+part+'.csv',index=None,header=None)
            except:
                pass














    def maxDie(self):
        stepX = eval(self.exLineEdit1_12.text()) / 1000
        stepY = eval(self.exLineEdit1_13.text()) / 1000
        dieX = eval(self.exLineEdit1_29.text()) / 1000
        dieY = eval(self.exLineEdit1_14.text()) / 1000
        part = self.exLineEdit1_2.text().strip().upper()
        if self.exLineEdit1_33.text().strip() == '':
            wee = 97
        else:
            wee = 100 - eval(self.exLineEdit1_33.text())

        if self.exLineEdit1_32.text().strip() == '':
            Ximg = 0
        else:
            Ximg = eval(self.exLineEdit1_32.text())

        if self.exLineEdit1_83.text().strip() == "":
            pricision = 0.1
        else:
            pricision = eval(self.exLineEdit1_83.text())
        if part[-2:]=='-L':
            tmp=stepX
            stepX=stepY
            stepY=2*tmp

            tmp=dieX
            dieX=dieY
            dieY=tmp

            tmpcount = 0
            col1, row1 = int(wee // stepX), int(wee // stepY)
            col2, row2 = int(round(stepX / dieX,0)), int(round(stepY / dieY,0))
            shotDie = stepX / dieX * stepY / dieY
            summary = []

            l = []
            for i in range(int(stepX / 2 / pricision) + 1):
                l.append( i * pricision)
            l = [round(i, 3) for i in l]
            l1 = []
            for i in range(int(stepY / 2 / pricision) + 1):
                l1.append(i * pricision)
            l1 = [round(i, 3) for i in l1]

            for Xcell in l:
                for Ycell in l1:
                    offX, offY = Xcell, Ycell
                    # summary.append([Xcell, Ycell])

                    totalDie = 0
                    fullShot = 0
                    partialShot = 0
                    for i in range(-col1 - 1, col1 + 2):
                        for j in range(-row1 - 1, row1 + 2):

                            llx = i * stepX - stepX / 2 + offX
                            lly = j * stepY - stepY / 2 + offY

                            f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                            f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                            f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            # laser mark
                            f5 = ((llx + stepX) > 92) and (
                                        (lly + stepY < 13 and lly + stepY > -13) or (lly < 13 and lly > -13))
                            f5 = not f5
                            # notch
                            f6 = (llx < -94) and ((lly + stepY < 14 and lly + stepY > -14) or (lly < 14 and lly > -14))
                            f6 = not f6

                            if f1 and f2 and f3 and f4 and f6:
                                totalDie = totalDie + shotDie
                                fullShot = fullShot + 1
                            else:
                                if f1 or f2 or f3 or f4:
                                    partialShotDie = 0
                                    partialShot = partialShot + 1
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

                                            f6 = (sx < -94) and (
                                                        (sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
                                            f6 = not f6

                                            if f1 and f2 and f3 and f4 and f5 and f6:
                                                partialShotDie += 1

                                    totalDie = totalDie + partialShotDie
                    print(totalDie)
                    summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
            summary = pd.DataFrame(summary,
                                   columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
            summary = summary.sort_values(by=['DieQty'])
            self.exLineEdit1_82.setText( str(list(summary['DieQty'])[-1]))
        else:
            tmpcount = 0
            col1, row1 = int(wee // stepX), int(wee // stepY)
            col2, row2 = int(round(stepX / dieX,0)), int(round(stepY / dieY,0))
            shotDie = stepX / dieX * stepY / dieY
            summary = []

            l = []
            for i in range(int(stepX / 2 / pricision) + 1):
                l.append(i * pricision)
            l = [round(i, 3) for i in l]
            l1 = []
            for i in range(int(stepY / 2 / pricision) + 1):
                l1.append(i * pricision)
            l1 = [round(i, 3) for i in l1]
            for Xcell in l:
                for Ycell in l1:
                    offX, offY = Xcell, Ycell
                    # summary.append([Xcell, Ycell])

                    totalDie = 0
                    fullShot = 0
                    partialShot = 0
                    for i in range(-col1 - 1, col1 + 2):
                        for j in range(-row1 - 1, row1 + 2):

                            llx = i * stepX - stepX / 2 + offX
                            lly = j * stepY - stepY / 2 + offY

                            f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                            f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                            f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            # laser mark
                            f5 = ((lly + stepY) > 92) and (
                                        (llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                            f5 = not f5
                            # notch
                            f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                            f6 = not f6

                            if f1 and f2 and f3 and f4 and f6:
                                totalDie = totalDie + shotDie
                                fullShot = fullShot + 1
                            else:
                                if f1 or f2 or f3 or f4:
                                    partialShotDie = 0
                                    partialShot = partialShot + 1
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

                                            f6 = (sy < -94) and (
                                                        (sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                            f6 = not f6

                                            if f1 and f2 and f3 and f4 and f5 and f6:
                                                partialShotDie += 1

                                    totalDie = totalDie + partialShotDie
                    print(totalDie)
                    summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
            summary = pd.DataFrame(summary,
                                   columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
            summary = summary.sort_values(by=['DieQty','TotalShot'],ascending=False)
            self.exLineEdit1_82.setText(str(list(summary['DieQty'])[0]))
        summary = summary.sort_values(by=[ 'TotalShot','DieQty'])
        DF2TABLE(self.exTable, summary)
    def maxDie_largeField(self): #针对大视场产品，优化后，3,9点有partial shot无法曝光，改为固定offsetX，
        stepX = eval(self.exLineEdit1_12.text()) / 1000
        stepY = eval(self.exLineEdit1_13.text()) / 1000
        dieX = eval(self.exLineEdit1_29.text()) / 1000
        dieY = eval(self.exLineEdit1_14.text()) / 1000
        part = self.exLineEdit1_2.text().strip().upper()
        if self.exLineEdit1_33.text().strip() == '':
            wee = 97
        else:
            wee = 100 - eval(self.exLineEdit1_33.text())

        if self.exLineEdit1_32.text().strip() == '':
            Ximg = 0
        else:
            Ximg = eval(self.exLineEdit1_32.text())

        if self.exLineEdit1_83.text().strip() == "":
            pricision = 0.1
        else:
            pricision = eval(self.exLineEdit1_83.text())
        if part[-2:]=='-L':
            tmp=stepX
            stepX=stepY
            stepY=2*tmp

            tmp=dieX
            dieX=dieY
            dieY=tmp

            tmpcount = 0
            col1, row1 = int(wee // stepX), int(wee // stepY)
            col2, row2 = int(round(stepX / dieX,0)), int(round(stepY / dieY,0))
            shotDie = stepX / dieX * stepY / dieY
            summary = []

            if True:#get offsetX，fixedvalue
                offX = -(97 - stepX / 2) % stepX
                if -offX > stepX / 2:
                    offX = offX + stepX  # 左侧完整die

                right = (97 - stepX / 2 - offX) % stepX
                # if right < (0.5 * stepX - 5.9):  # 如果右侧不完整shot无法曝光
                if right < (0.5 * stepX - 5.8999):  # 如果右侧不完整shot无法曝光
                    # offX = -(0.5 * stepX - 5.9 - right) + offX
                    offX = -(0.5 * stepX - 5.8999 - right) + offX
                    if offX > stepX / 2:
                        offX = offX - stepX
                    elif -offX>stepX/2:
                        offX = offX+stepX



            # l = []
            # for i in range(int(stepX / 2 / pricision) + 1):
            #     l.append( i * pricision)
            # l = [round(i, 3) for i in l]
            l = [offX]
            l1 = []
            for i in range(int(stepY / 2 / pricision) + 1):
                l1.append(i * pricision)
            l1 = [round(i, 3) for i in l1]

            for Xcell in l:
                for Ycell in l1:
                    offX, offY = Xcell, Ycell
                    # summary.append([Xcell, Ycell])

                    totalDie = 0
                    fullShot = 0
                    partialShot = 0
                    for i in range(-col1 - 1, col1 + 2):
                        for j in range(-row1 - 1, row1 + 2):

                            llx = i * stepX - stepX / 2 + offX
                            lly = j * stepY - stepY / 2 + offY

                            f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                            f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                            f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            # laser mark
                            f5 = ((llx + stepX) > 92) and (
                                        (lly + stepY < 13 and lly + stepY > -13) or (lly < 13 and lly > -13))
                            f5 = not f5
                            # notch
                            f6 = (llx < -94) and ((lly + stepY < 14 and lly + stepY > -14) or (lly < 14 and lly > -14))
                            f6 = not f6

                            if f1 and f2 and f3 and f4 and f6:
                                totalDie = totalDie + shotDie
                                fullShot = fullShot + 1
                            else:
                                if f1 or f2 or f3 or f4:
                                    partialShotDie = 0
                                    partialShot = partialShot + 1
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

                                            f6 = (sx < -94) and (
                                                        (sy + dieY < 14 and sy + dieY > -14) or (sy < 14 and sy > -14))
                                            f6 = not f6

                                            if f1 and f2 and f3 and f4 and f5 and f6:
                                                partialShotDie += 1

                                    totalDie = totalDie + partialShotDie
                    print(totalDie)
                    summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
            summary = pd.DataFrame(summary,
                                   columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
            summary = summary.sort_values(by=['DieQty'])
            self.exLineEdit1_82.setText( str(list(summary['DieQty'])[-1]))
        # else:
        #     tmpcount = 0
        #     col1, row1 = int(wee // stepX), int(wee // stepY)
        #     col2, row2 = int(round(stepX / dieX,0)), int(round(stepY / dieY,0))
        #     shotDie = stepX / dieX * stepY / dieY
        #     summary = []
        #
        #     l = []
        #     for i in range(int(stepX / 2 / pricision) + 1):
        #         l.append(i * pricision)
        #     l = [round(i, 3) for i in l]
        #     l1 = []
        #     for i in range(int(stepY / 2 / pricision) + 1):
        #         l1.append(i * pricision)
        #     l1 = [round(i, 3) for i in l1]
        #     for Xcell in l:
        #         for Ycell in l1:
        #             offX, offY = Xcell, Ycell
        #             # summary.append([Xcell, Ycell])
        #
        #             totalDie = 0
        #             fullShot = 0
        #             partialShot = 0
        #             for i in range(-col1 - 1, col1 + 2):
        #                 for j in range(-row1 - 1, row1 + 2):
        #
        #                     llx = i * stepX - stepX / 2 + offX
        #                     lly = j * stepY - stepY / 2 + offY
        #
        #                     f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
        #                     f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
        #                     f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
        #                     f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
        #                     # laser mark
        #                     f5 = ((lly + stepY) > 92) and (
        #                                 (llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
        #                     f5 = not f5
        #                     # notch
        #                     f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
        #                     f6 = not f6
        #
        #                     if f1 and f2 and f3 and f4 and f6:
        #                         totalDie = totalDie + shotDie
        #                         fullShot = fullShot + 1
        #                     else:
        #                         if f1 or f2 or f3 or f4:
        #                             partialShotDie = 0
        #                             partialShot = partialShot + 1
        #                             for k in range(0, col2):
        #                                 for l in range(0, row2):
        #                                     sx = llx + k * dieX
        #                                     sy = lly + l * dieY
        #
        #                                     f1 = (pow(sx, 2) + pow(sy, 2)) < pow(wee, 2)
        #                                     f2 = (pow(sx + dieX, 2) + pow(sy, 2)) < pow(wee, 2)
        #                                     f3 = (pow(sx + dieX, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
        #                                     f4 = (pow(sx, 2) + pow(sy + dieY, 2)) < pow(wee, 2)
        #                                     f5 = ((sy + dieY) > 92) and (
        #                                                 (sx + dieX < 13 and sx + dieX > -13) or (sx < 13 and sx > -13))
        #                                     f5 = not f5
        #
        #                                     f6 = (sy < -94) and (
        #                                                 (sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
        #                                     f6 = not f6
        #
        #                                     if f1 and f2 and f3 and f4 and f5 and f6:
        #                                         partialShotDie += 1
        #
        #                             totalDie = totalDie + partialShotDie
        #             print(totalDie)
        #             summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
        #     summary = pd.DataFrame(summary,
        #                            columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
        #     summary = summary.sort_values(by=['DieQty','TotalShot'],ascending=False)
        #     self.exLineEdit1_82.setText(str(list(summary['DieQty'])[0]))
        summary = summary.sort_values(by=[ 'TotalShot','DieQty'])
        DF2TABLE(self.exTable, summary)
        return summary
    def mapCal(self):
        # self.exLineEdit1_12.setPlaceholderText("StepX(um)")
        # self.exLineEdit1_13.setPlaceholderText("StepY(um)")
        # self.exLineEdit1_29.setPlaceholderText("DieX(um)")
        # self.exLineEdit1_14.setPlaceholderText("DieY(um)")
        # self.exLineEdit1_31.setPlaceholderText("MapOffsetX(um)")
        # self.exLineEdit1_30.setPlaceholderText("MapOffsetY(um)")
        # self.exLineEdit1_32.setPlaceholderText("ImgOffsetY(um)")
        # self.exLineEdit1_33.setPlaceholderText("Edge(mm,输入3）")
        # self.exLineEdit1_55.setPlaceholderText("MapOffsetX-L-N(um)")
        # self.exLineEdit1_56.setPlaceholderText("MapOffsetY-L-N(um)")
        # self.exLineEdit1_80.setPlaceholderText("SizeX（um）")
        # self.exLineEdit1_81.setPlaceholderText("SizeX-shift（um）")
        # self.exLineEdit1_83.setPlaceholderText("拟合精度（0.5mm）")
        # self.exLineEdit1_82.setPlaceholderText("理论最大管芯数")
        stepX = eval(self.exLineEdit1_12.text())/1000
        stepY = eval(self.exLineEdit1_13.text())/1000
        dieX = eval(self.exLineEdit1_29.text())/1000
        dieY = eval(self.exLineEdit1_14.text())/1000
        part = self.exLineEdit1_2.text().strip().upper()
        if self.exLineEdit1_33.text().strip()=='':
            wee=97
        else:
            wee = 100-eval(self.exLineEdit1_33.text())

        if self.exLineEdit1_32.text().strip()=='':
            Ximg=0
        else:
            Ximg=eval(self.exLineEdit1_32.text())

        if self.exLineEdit1_83.text().strip()=="":
            pricision=0.1
        else:
            pricision=eval(self.exLineEdit1_83.text())




        if part[-2:]=='-L':

            tmp=stepX
            stepX=stepY
            stepY=2*tmp

            tmp=dieX
            dieX=dieY
            dieY=tmp
            print(part, stepX, stepY, dieX, dieY, wee, Ximg, pricision)

            summary = self.Calculate_Die_Notch_Left(stepX,stepY,dieX,dieY,part,wee,Ximg,pricision)
            print(summary.shape)
            DF2TABLE(self.exTable, summary)

        else:
            summary = self.Calculate_Die_Notch_Down(stepX, stepY, dieX, dieY, part, wee,Ximg,pricision)
            print(summary.shape)
            DF2TABLE(self.exTable, summary)
        return summary
    def mapDraw(self):
        stepX = eval(self.exLineEdit1_12.text()) / 1000
        stepY = eval(self.exLineEdit1_13.text()) / 1000
        dieX = eval(self.exLineEdit1_29.text()) / 1000
        dieY = eval(self.exLineEdit1_14.text()) / 1000
        part = self.exLineEdit1_2.text().strip().upper()
        if self.exLineEdit1_33.text().strip() == '':
            wee = 97
        else:
            wee = 100 - eval(self.exLineEdit1_33.text())

        if self.exLineEdit1_32.text().strip() == '':
            Ximg = 0
        else:
            Ximg = eval(self.exLineEdit1_32.text())

        offX = eval(self.exLineEdit1_31.text())
        offY = eval(self.exLineEdit1_30.text())

        largeFlag=False

        if part[-2:] == '-L':
            largeFlag=True
            tmp = stepX
            stepX = stepY
            stepY = 2 * tmp

            tmp = dieX
            dieX = dieY
            dieY = tmp
            self.Plot_Map_Notch_Left( stepX, stepY, dieX, dieY, part, wee, offX, offY,largeFlag)
        else:
            partial_Flag=True
            self.Plot_Map_Notch_Down( stepX, stepY, dieX, dieY, part, wee, offX, offY,largeFlag,partial_Flag=partial_Flag)

        #calculate offset

        #laser mark area was changed from 8*26 to 6*26
        if  part[-2:] == '-L':
            tmpsize = (100-5.5-stepX/2-offX)%stepX
            self.exLineEdit1_80.setText(   str(    round( tmpsize ,5)     ) )
            self.exLineEdit1_81.setText(   str(    round( (tmpsize-stepX) /2 ,5)       )      )

        else:
            tmpsize = (100 - 5.5 - stepY / 2 - offY) % stepY
            self.exLineEdit1_80.setText(   str(    round( tmpsize ,5)     ) )
            self.exLineEdit1_81.setText(   str(    round( (tmpsize-stepY) /2 ,5)       )      )
            self.exLineEdit1_3.setText(   str(round( -stepY/2,5 )   ) )
            self.exLineEdit1_4.setText(    str( round(-stepY/2 + tmpsize,5))                           )
            self.exLineEdit1_5.setText(   str(    round( -(tmpsize-stepY) /2 ,5)       )      )
    def largeField(self): # Large Field 主函数
        largeFlag=False
        part = self.exLineEdit1_2.text().strip().upper()
        if part[-2:]=='-L':
            largeFlag=True
            summary=self.mapCal()

            summary = summary[summary['leftFlag'] == True]
            summary = summary[summary['rightFlag'] == True]  # 保证没有partial shot

            if summary.shape[0]>0:
                tmp = list(summary['TotalShot'].unique())[0]
                tmp1 = summary[summary['TotalShot'] == tmp]
                offX = tmp1.iloc[tmp1.shape[0] - 1, 0]
                offY = tmp1.iloc[tmp1.shape[0] - 1, 1]
            else:
                summary = self.maxDie_largeField()
                tmp=summary['TotalShot'].unique()[0]
                tmp1 = summary[summary['TotalShot']==tmp]
                offX = tmp1.iloc[-1,0]
                offY = tmp1.iloc[-1,1]
                #todo '边缘仍有可能3mm无法曝光，需再判断'，9点钟是完整shot，3点钟打标位置，不可是完整shot









            self.exLineEdit1_31.setText(str(offX))
            self.exLineEdit1_30.setText(str(offY))
            #ASML MAP
            self.mapDraw()

            #NIKON MAP
            stepX = eval(self.exLineEdit1_12.text()) / 1000
            stepY = eval(self.exLineEdit1_13.text()) / 1000
            dieX = eval(self.exLineEdit1_29.text()) / 1000
            dieY = eval(self.exLineEdit1_14.text()) / 1000
            part=part[:-2]

            if self.exLineEdit1_33.text().strip() == '':
                wee = 97
            else:
                wee = 100 - eval(self.exLineEdit1_33.text())

            if self.exLineEdit1_32.text().strip() == '':
                Ximg = 0
            else:
                Ximg = eval(self.exLineEdit1_32.text())

            offX = eval(self.exLineEdit1_55.text())
            offY = eval(self.exLineEdit1_56.text())
            self.Plot_Map_Notch_Down(stepX, stepY, dieX, dieY, part, wee, offX, offY,largeFlag)
            tmpsize = (100 - 5.5 - stepY / 2 - offY) % stepY
            self.exLineEdit1_80.setText(str(round(tmpsize, 5)))
            self.exLineEdit1_81.setText(str(round((tmpsize - stepY) / 2, 5)))
            self.exLineEdit1_3.setText(str(round(-stepY / 2, 5)))
            self.exLineEdit1_4.setText(str(round(-stepY / 2 + tmpsize, 5)))
            self.exLineEdit1_5.setText(str(round(-(tmpsize - stepY) / 2, 5)))




        else:
            reply = QMessageBox.information(self, "注意",
                                            '非大视场产品名，请确认',
                                            QMessageBox.Yes | QMessageBox.No, QMessageBox.No)



        # calculate die qty without optmization for Nikon Map
        tmpcount = 0
        col1, row1 = int(wee // stepX), int(wee // stepY)
        col2, row2 = int(round(stepX / dieX, 0)), int(round(stepY / dieY, 0))
        shotDie = stepX / dieX * stepY / dieY
        summary = []

        if 3.14*97*97/dieX/dieY<2000:
            pricision=0.1
        elif 3.14*97*97/dieX/dieY<5000:
            pricision=0.5
        else:
            pricision=1

        l = []
        for i in range(int(stepX / 2 / pricision) + 1):
            l.append(i * pricision)
        l = [round(i, 3) for i in l]
        l1 = []
        for i in range(int(stepY / 2 / pricision) + 1):
            l1.append(i * pricision)
        l1 = [round(i, 3) for i in l1]
        for Xcell in l:
            for Ycell in l1:
                offX, offY = Xcell, Ycell
                # summary.append([Xcell, Ycell])

                totalDie = 0
                fullShot = 0
                partialShot = 0
                for i in range(-col1 - 1, col1 + 2):
                    for j in range(-row1 - 1, row1 + 2):

                        llx = i * stepX - stepX / 2 + offX
                        lly = j * stepY - stepY / 2 + offY

                        f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                        f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                        f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        # laser mark
                        f5 = ((lly + stepY) > 92) and (
                                (llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                        f5 = not f5
                        # notch
                        f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                        f6 = not f6

                        if f1 and f2 and f3 and f4 and f6:
                            totalDie = totalDie + shotDie
                            fullShot = fullShot + 1
                        else:
                            if f1 or f2 or f3 or f4:
                                partialShotDie = 0
                                partialShot = partialShot + 1
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

                                        f6 = (sy < -94) and (
                                                (sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                        f6 = not f6

                                        if f1 and f2 and f3 and f4 and f5 and f6:
                                            partialShotDie += 1

                                totalDie = totalDie + partialShotDie
                print(totalDie)
                summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
        summary = pd.DataFrame(summary, columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
        summary = summary.sort_values(by=['DieQty', 'TotalShot'], ascending=False)
        self.exLineEdit1_82.setText(str(list(summary['DieQty'])[0]))

        Dmax= list(summary['DieQty'])[0]
        Dasml = eval(self.exLineEdit1_8.text()[4:])
        Dnikon=eval(self.exLineEdit1_6.text()[5:])

        Dasml= round((Dmax-Dasml)/Dmax*100,3)
        Dnikon = round((Dmax-Dnikon)/Dmax*100,3)



        reply = QMessageBox.information(self, "注意", '理论最大管芯数是' + str(list(summary['DieQty'])[0]) + '，请确认优化后的管芯是否有影响\n\nASML MAP的管芯损失是：' + str(Dasml) + '%\nNIKON MAP的管芯损失是：' + str(Dnikon) + '%\n管芯数小于2000的拟合步进是0.1mm，管芯小于5000的是0.5mm，管芯大于5000的是1\n\n步进较大，如有需求，请设置小步进（<0.1mm)手动模式下拟合' , QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
    def normalField(self):
        if True:#prepare date
            part = self.exLineEdit1_2.text().strip().upper()
            laserY_Flag=False
            largeFlag=False
            if part[-2:]=='-L':
                reply = QMessageBox.information(self, "注意", '大视场产品名，请确认', QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
                return

            stepX = eval(self.exLineEdit1_12.text()) / 1000
            stepY = eval(self.exLineEdit1_13.text()) / 1000
            dieX = eval(self.exLineEdit1_29.text()) / 1000
            dieY = eval(self.exLineEdit1_14.text()) / 1000
            if self.exLineEdit1_33.text().strip() == '':
                wee = 97
            else:
                wee = 100 - eval(self.exLineEdit1_33.text())

            if self.exLineEdit1_32.text().strip() == '':
                Ximg = 0
            else:
                Ximg = eval(self.exLineEdit1_32.text())
            if self.exLineEdit1_83.text().strip()=="":
                pricision=0.1
            else:
                pricision=eval(self.exLineEdit1_83.text())
                if pricision>=0.1:
                    pricision=0.1


            laserY = (95-stepY/2)%stepY

            if laserY>stepY/2:
                laserY = laserY-stepY

            col1, row1 = int(wee // stepX), int(wee // stepY)
            col2, row2 = int(stepX / dieX), int(stepY / dieY)
            shotDie = stepX / dieX * stepY / dieY
            summary = []

        if True: #calculate offsetX/offsetY
            # LSmapX
            b1, b2 = stepX, stepY
            # Ximg=0  #Image Shift on reticle X(size on wafer)
            shiftX1_min, shiftX1_max, shiftX2_min, shiftX2_max = "", "", "", ""

            if int(184 / b1 + 1) < 194 / b1:  # 'NonebyLS is unavoidable.
                if 194 - int(
                        194 / b1) * b1 + 2.5 > b1 / 2:  # '(B) NonebyLS exposable in one side when the other side field edge touch FEC
                    K2 = 97 - (2 * int(194 / 2 / b1) + 1) * b1 / 2  # 'Field edge touch FEC
                    if Ximg > 0:
                        XcellA1 = K2 + Ximg  # 'Field edge touch right FEC
                    else:
                        XcellA1 = -K2 + Ximg  # 'Field edge touch left FEC
                else:
                    K3 = 100 - 0.5 - int(100 / b1 + 1 / 2) * b1  # 'Scan center touch wafer edge (99.5 mm from wafer center)
                    if 100 - b1 * int(200 / b1) / 2 < 4.25:  # '(D) Left and right NonebyLS => symmetric map
                        K3 = (int(100 / b1) - int(100 / b1 - 1 / 2)) * b1 / 2  # '(C)'
                    if Ximg > 0:
                        XcellA1 = -K3 + Ximg  # 'Scan center touch left wafer edge
                    else:
                        XcellA1 = K3 + Ximg  # 'Scan center touch right wafer edge
                shiftX1_min = XcellA1
            else:  # '(A)
                D1 = 92 - b1 * (int(184 / b1 + 1) - 1) / 2  # 'Reticle center touch NonebyLS
                D2 = b1 * (int(184 / b1 + 1) / 2) - 97  # ''Field edge touch FEC
                if D1 < D2:
                    K1 = D1
                else:
                    K1 = D2

                if int(184 / b1 + 1) - int(int(184 / b1 + 1) / 2) * 2 == 1:  # 'Odd columns within ZbyLS
                    if Ximg == 0:
                        XcellA1 = -K1
                        XcellA2 = K1
                    else:
                        XcellA1 = Ximg
                        XcellA2 = Ximg - Ximg / abs(Ximg) * K1
                else:
                    if Ximg == 0:  # 'Even columns within ZbyLS
                        XcellA1 = -b1 / 2
                        XcellA2 = -b1 / 2 + K1
                        XcellB1 = b1 / 2
                        XcellB2 = b1 / 2 - K1

                        if XcellB1 > XcellB2:
                            XBmin = XcellB2
                            XBmax = XcellB1
                        else:
                            XBmin = XcellB1
                            XBmax = XcellB2
                        shiftX2_min = XBmin
                        shiftX2_max = XBmax
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


            l = []
            if shiftX1_max == '' and shiftX2_min == '' and shiftX2_max == '':
                l = [shiftX1_min]
            else:
                try:
                    for i in range(int((shiftX1_max - shiftX1_min) / pricision) + 1):
                        l.append(shiftX1_min + i * pricision)
                except:
                    pass
                try:
                    for i in range(int((shiftX2_max - shiftX2_min) / pricision) + 1):
                        l.append(shiftX2_min + i * pricision)
                except:
                    pass
            l = [round(i, 3) for i in l]

            # calculate LSmapY:
            for Xcell in l:
                l1 = []
                shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max = "", "", "", ""
                E1 = (100 - Xcell + Ximg) - int((92 - Xcell + Ximg) / b1) * b1  # 'Right side scan center
                E2 = (100 + Xcell - Ximg) - int((92 + Xcell - Ximg) / b1) * b1  # 'Left side scan center
                if E1 > E2:
                    E = E2
                else:
                    E = E1

                H = pow(97 * 97 - pow((100 - E), 2), 0.5)
                dH = H - 13 - 6 - 5
                if dH <= 0:
                    shiftY1_min, shiftY2_min = -b2 / 2, b2 / 2
                    l1 = [shiftY1_min, shiftY2_min]
                else:
                    if dH < b2 / 2:
                        shiftY1_min, shiftY1_max, shiftY2_min, shiftY2_max = -b2 / 2, -b2 / 2 + dH, b2 / 2 - dH, b2 / 2
                        for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
                            l1.append(shiftY1_min + i * pricision)
                        for i in range(int((shiftY2_max - shiftY2_min) / pricision) + 1):
                            l1.append(shiftY2_min + i * 0.1)

                    else:
                        shiftY1_min, shiftY1_max = -b2 / 2, b2 / 2
                        for i in range(int((shiftY1_max - shiftY1_min) / pricision) + 1):
                            l1.append(shiftY1_min + i * pricision)
                l1 = [round(i, 3) for i in l1]

                l2 = [i for i in l1 if abs(laserY-i)<=0.1]
                if len(l2)>0:
                    laserY_Flag=True
                    l1 = l2

                if laserY_Flag==True and len(l2)==0:
                    l1=[888]




                for Ycell in l1:
                    offX, offY = Xcell, Ycell
                    # summary.append([Xcell, Ycell])

                    totalDie = 0
                    fullShot = 0
                    partialShot = 0
                    for i in range(-col1 - 1, col1 + 2):
                        for j in range(-row1 - 1, row1 + 2):

                            llx = i * stepX - stepX / 2 + offX
                            lly = j * stepY - stepY / 2 + offY

                            f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                            f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                            f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                            # laser mark
                            f5 = ((lly + stepY) > 92) and (
                                        (llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                            f5 = not f5
                            # notch
                            f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                            f6 = not f6

                            if f1 and f2 and f3 and f4 and f6:
                                totalDie = totalDie + shotDie
                                fullShot = fullShot + 1
                            else:
                                if f1 or f2 or f3 or f4:
                                    partialShotDie = 0
                                    partialShot = partialShot + 1
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

                                            f6 = (sy < -94) and (
                                                        (sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                            f6 = not f6

                                            if f1 and f2 and f3 and f4 and f5 and f6:
                                                partialShotDie += 1

                                    totalDie = totalDie + partialShotDie
                    # print(totalDie)
                    summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])


            # if "oldAlgorithm"!="oldAlgorithm": # 3,9,clock, no coverage
            #     summary = pd.DataFrame(summary, columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
            #     summary['leftPartialSize'] = round((97 - stepX / 2 + summary['shiftX']) % stepX, 3)
            #     summary['rightPartialSize'] = round((97 - stepX / 2 - summary['shiftX']) % stepX, 3)
            #
            #     # summary['miniSizeX'] = 0.5 * stepX - 5.9
            #     summary['miniSizeX'] = 0.5 * stepX - 5.8999
            #
            #     # summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.9))
            #     summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.8999))
            #
            #     # summary['rightFlag'] = summary['rightPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.9))
            #     summary['rightFlag'] = summary['rightPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 5.8999))
            #
            #     summary['delta'] = [round(i - laserY,3) for i in summary['shiftY']]
            #     summary = summary.sort_values(by =['TotalShot','DieQty','delta'])
            #
            #     summary = summary[summary['shiftY']!=888]#只要有一个cellx可以运算，laserY_Flag=True,当其它cellx无法计算时，因flag=True，offsetY填入888，后续需剔除
            #
            #     summary['left3'] = [((i == 0) or (stepX - i) < 2.5) for i in summary['leftPartialSize']]  #flag for full wfr cover diameter=99.5
            #     summary['right3'] = [((i == 0) or (stepX - i) < 2.5) for i in summary['rightPartialSize']]
            #
            #     DF2TABLE(self.exTable, summary)
            if "newAlgorithm"=="newAlgorithm": # 3,9,clock,3mm edge covered
                summary = pd.DataFrame(summary, columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
                summary['leftPartialSize'] = round((99.5 - stepX / 2 + summary['shiftX']) % stepX, 3)
                summary['rightPartialSize'] = round((99.5 - stepX / 2 - summary['shiftX']) % stepX, 3)

                summary['miniSizeX'] = 0.5 * stepX - 3.3999
                summary['leftFlag'] = summary['leftPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 3.3999))
                summary['rightFlag'] = summary['rightPartialSize'].apply(lambda x: True if x == 0 else x > (0.5 * stepX - 3.3999))

                summary['delta'] = [round(i - laserY,3) for i in summary['shiftY']]
                summary = summary.sort_values(by =['TotalShot','DieQty','delta'])
                summary = summary[summary['shiftY']!=888]#只要有一个cellx可以运算，laserY_Flag=True,当其它cellx无法计算时，因flag=True，offsetY填入888，后续需剔除

                DF2TABLE(self.exTable, summary)


        # if "OLD"!="OLD":
        #     tmpSummary = summary[summary['leftFlag']==True]
        #     tmpSummary = tmpSummary[tmpSummary['rightFlag']==True]  #保证没有partial shot
        #     tmpSummary = tmpSummary[tmpSummary['left3']!=True]  #99.5 diameter cover
        #     tmpSummary = tmpSummary[tmpSummary['right3'] != True]  # 99.5 diameter cover
        #
        #
        #     if tmpSummary.shape[0]>0:
        #         summary=tmpSummary
        #         tmp = list(summary['TotalShot'].unique())[0]
        #         tmp1 = summary[summary['TotalShot'] == tmp]
        #         offX = tmp1.iloc[tmp1.shape[0] - 1, 0]
        #         offY = tmp1.iloc[tmp1.shape[0] - 1, 1]
        #     else:
        #         laserY_Flag = False  #partial shot无法曝光，默认X方向无法曝光
        #
        #
        #
        #
        #     if laserY_Flag==False:
        #         pass
        #         offY = laserY
        #         offX =  (100-stepX/2)%stepX  #chang 97-->100
        #         if offX > stepX / 2:
        #             offX = offX - stepX  #右侧完整die
        #
        #         left = (97-stepX/2+offX)%stepX
        #         # if left <(0.5 * stepX-5.9):   # 如果左侧不完整shot无法曝光
        #         if left <(0.5 * stepX-5.8999):   # 如果左侧不完整shot无法曝光
        #             # offX = 0.5 * stepX-5.9 - left + offX
        #             offX = 0.5 * stepX-5.8999 - left + offX
        #             if offX > stepX / 2:
        #                 offX = offX - stepX
        #             elif -offX > stepX / 2:
        #                 offX = offX + stepX    #left size is supposed to be checked again,skippend,revise in case .....
        #
        #
        #         # tmpsize = (100 - 5 - stepY / 2 - offY) % stepY
        #         # self.exLineEdit1_80.setText(str(round(tmpsize, 5)))
        #         # self.exLineEdit1_81.setText(str(round((tmpsize - stepY) / 2, 5)))
        #         # self.exLineEdit1_3.setText(str(round(-stepY / 2, 5)))
        #         # self.exLineEdit1_4.setText(str(round(-stepY / 2 + tmpsize, 5)))
        #         # self.exLineEdit1_5.setText(str(round(-(tmpsize - stepY) / 2, 5)))
        if "New"=="New":
            tmpSummary = summary[summary['leftFlag']==True]
            tmpSummary = tmpSummary[tmpSummary['rightFlag']==True]  #保证没有partial shot

            if tmpSummary.shape[0]>0:
                summary=tmpSummary
                tmp = list(summary['TotalShot'].unique())[0]
                tmp1 = summary[summary['TotalShot'] == tmp]
                offX = tmp1.iloc[tmp1.shape[0] - 1, 0]
                offY = tmp1.iloc[tmp1.shape[0] - 1, 1]
            else:
                laserY_Flag = False  #partial shot无法曝光，默认X方向无法曝光




            if laserY_Flag==False:
                pass
                offY = laserY
                offX =  (99.5-stepX/2)%stepX  #chang 97-->99.5
                if offX > stepX / 2:
                    offX = offX - stepX  #右侧完整die

                left = (99.5-stepX/2+offX)%stepX

                if left <(0.5 * stepX-3.3999):   # 如果左侧不完整shot无法曝光
                    offX = 0.5 * stepX-3.3999 - left + offX
                    if offX > stepX / 2:
                        offX = offX - stepX
                    elif -offX > stepX / 2:
                        offX = offX + stepX    #left size is supposed to be checked again,skippend,revise in case .....


                # tmpsize = (100 - 5 - stepY / 2 - offY) % stepY
                # self.exLineEdit1_80.setText(str(round(tmpsize, 5)))
                # self.exLineEdit1_81.setText(str(round((tmpsize - stepY) / 2, 5)))
                # self.exLineEdit1_3.setText(str(round(-stepY / 2, 5)))
                # self.exLineEdit1_4.setText(str(round(-stepY / 2 + tmpsize, 5)))
                # self.exLineEdit1_5.setText(str(round(-(tmpsize - stepY) / 2, 5)))






        # if laserY_Flag==False: #old algorithm for 97mm cover
        #     pass
        #     offY = laserY
        #     offX =  (97-stepX/2)%stepX
        #     if offX > stepX / 2:
        #         offX = offX - stepX  #右侧完整die
        #
        #     left = (97-stepX/2+offX)%stepX
        #     # if left <(0.5 * stepX-5.9):   # 如果左侧不完整shot无法曝光
        #     if left <(0.5 * stepX-5.8999):   # 如果左侧不完整shot无法曝光
        #         # offX = 0.5 * stepX-5.9 - left + offX
        #         offX = 0.5 * stepX-5.8999 - left + offX
        #         if offX > stepX / 2:
        #             offX = offX - stepX
        #         elif -offX > stepX / 2:
        #             offX = offX + stepX
        #
        #
        #     # tmpsize = (100 - 5 - stepY / 2 - offY) % stepY
        #     # self.exLineEdit1_80.setText(str(round(tmpsize, 5)))
        #     # self.exLineEdit1_81.setText(str(round((tmpsize - stepY) / 2, 5)))
        #     # self.exLineEdit1_3.setText(str(round(-stepY / 2, 5)))
        #     # self.exLineEdit1_4.setText(str(round(-stepY / 2 + tmpsize, 5)))
        #     # self.exLineEdit1_5.setText(str(round(-(tmpsize - stepY) / 2, 5)))  #old old

        self.Plot_Map_Notch_Down(stepX, stepY, dieX, dieY, part, wee, offX, offY, largeFlag,
                                     laserY_Flag=laserY_Flag)





        # calculate die qty without optmization for Nikon Map
        tmpcount = 0
        col1, row1 = int(wee // stepX), int(wee // stepY)
        col2, row2 = int(round(stepX / dieX, 0)), int(round(stepY / dieY, 0))
        shotDie = stepX / dieX * stepY / dieY
        summary = []

        if 3.14 * 97 * 97 / dieX / dieY < 2000:
            pricision = 0.1
        elif 3.14 * 97 * 97 / dieX / dieY < 5000:
            pricision = 0.5
        else:
            pricision = 1

        l = []
        for i in range(int(stepX / 2 / pricision) + 1):
            l.append(i * pricision)
        l = [round(i, 3) for i in l]
        l1 = []
        for i in range(int(stepY / 2 / pricision) + 1):
            l1.append(i * pricision)
        l1 = [round(i, 3) for i in l1]
        for Xcell in l:
            for Ycell in l1:
                offX, offY = Xcell, Ycell
                # summary.append([Xcell, Ycell])

                totalDie = 0
                fullShot = 0
                partialShot = 0
                for i in range(-col1 - 1, col1 + 2):
                    for j in range(-row1 - 1, row1 + 2):

                        llx = i * stepX - stepX / 2 + offX
                        lly = j * stepY - stepY / 2 + offY

                        f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                        f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                        f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        # laser mark
                        f5 = ((lly + stepY) > 92) and (
                                (llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                        f5 = not f5
                        # notch
                        f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                        f6 = not f6

                        if f1 and f2 and f3 and f4 and f6 and f5: #20191015, add f5 flag
                            totalDie = totalDie + shotDie
                            fullShot = fullShot + 1
                        else:
                            if f1 or f2 or f3 or f4:
                                partialShotDie = 0
                                partialShot = partialShot + 1
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

                                        f6 = (sy < -94) and (
                                                (sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                        f6 = not f6

                                        if f1 and f2 and f3 and f4 and f5 and f6:
                                            partialShotDie += 1

                                totalDie = totalDie + partialShotDie
                # print(totalDie)
                summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
        summary = pd.DataFrame(summary, columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
        summary = summary.sort_values(by=['DieQty', 'TotalShot'], ascending=False)
        self.exLineEdit1_82.setText(str(list(summary['DieQty'])[0]))

        Dmax = list(summary['DieQty'])[0]
        # Dasml = eval(self.exLineEdit1_8.text()[4:])
        Dnikon = eval(self.exLineEdit1_6.text()[5:])

        # Dasml = round((Dmax - Dasml) / Dmax * 100, 3)
        Dnikon = round((Dmax - Dnikon) / Dmax * 100, 3)

        reply = QMessageBox.information(self, "注意", '理论最大管芯数是' + str(
            list(summary['DieQty'])[0]) + '，请确认优化后的管芯是否有影响\n\nNIKON MAP的管芯损失是：' + str(
            Dnikon) + '%\n管芯数小于2000的拟合步进是0.1mm，管芯小于5000的是0.5mm，管芯大于5000的是1\n\n步进较大，如有需求，请设置小步进（<0.1mm)手动模式下拟合',
                                        QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
    def splitGate(self):
        largeFlag = False
        stepX = eval(self.exLineEdit1_12.text()) / 1000
        stepY = eval(self.exLineEdit1_13.text()) / 1000
        part = self.exLineEdit1_2.text().strip().upper()
        if part[-2:] == '-L':
            reply = QMessageBox.information(self, "注意", '大视场产品名，请确认', QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
            return

        summary = self.mapCal()
        summary['flag'] = [(summary.iloc[i,8] and summary.iloc[i,9]) for i in range(summary.shape[0])]
        bak=summary.copy()

        summary = summary[summary['flag']==True]

        if summary.shape[0]>0:



            tmp = list(summary['TotalShot'].unique())[0]
            tmp1 = summary[summary['TotalShot'] == tmp]
            offX = tmp1.iloc[tmp1.shape[0] - 1, 0]
            offY = tmp1.iloc[tmp1.shape[0] - 1, 1]
        else:
            print('Partial Shot Exists')
            summary=bak.copy()
            tmp = list(summary['TotalShot'].unique())[0]
            tmp1 = summary[summary['TotalShot'] == tmp]

            offY = tmp1.iloc[tmp1.shape[0] - 1, 1]


            offX = (99.5 - stepX / 2) % stepX
            if offX > stepX / 2:
                offX = offX - stepX  # 右侧完整die

            left = (99.5 - stepX / 2 + offX) % stepX
            if left < (0.5 * stepX - 3.3999):  # 如果左侧不完整shot无法曝光
                offX = 0.5 * stepX - 3.3999 - left + offX
                if offX > stepX / 2:
                    offX = offX - stepX
                elif -offX > stepX / 2:
                    offX = offX + stepX

        self.exLineEdit1_31.setText(str(offX))
        self.exLineEdit1_30.setText(str(offY))

        stepX = eval(self.exLineEdit1_12.text()) / 1000
        stepY = eval(self.exLineEdit1_13.text()) / 1000
        dieX = eval(self.exLineEdit1_29.text()) / 1000
        dieY = eval(self.exLineEdit1_14.text()) / 1000
        part = self.exLineEdit1_2.text().strip().upper()
        if self.exLineEdit1_33.text().strip() == '':
            wee = 97
        else:
            wee = 100 - eval(self.exLineEdit1_33.text())

        if self.exLineEdit1_32.text().strip() == '':
            Ximg = 0
        else:
            Ximg = eval(self.exLineEdit1_32.text())

        if self.exLineEdit1_83.text().strip() == "":
            pricision = 0.1
        else:
            pricision = eval(self.exLineEdit1_83.text())

        self.Plot_Map_Notch_Down( stepX, stepY, dieX, dieY, part, wee, offX, offY, largeFlag, laserY_Flag=True,
                            partial_Flag=False)



    def MPW(self):
        largeFlag = False
        part = self.exLineEdit1_2.text().strip().upper()
        if part[-2:] == '-L':
            reply = QMessageBox.information(self, "注意", '大视场产品名，请确认', QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
            return

        stepX = eval(self.exLineEdit1_12.text()) / 1000
        stepY = eval(self.exLineEdit1_13.text()) / 1000
        dieX = stepX
        dieY = stepY
        if self.exLineEdit1_33.text().strip() == '':
            wee = 97
        else:
            wee = 100 - eval(self.exLineEdit1_33.text())

        if self.exLineEdit1_32.text().strip() == '':
            Ximg = 0
        else:
            Ximg = eval(self.exLineEdit1_32.text())
        if self.exLineEdit1_83.text().strip() == "":
            pricision = 0.1
        else:
            pricision = eval(self.exLineEdit1_83.text())
            if pricision >= 0.1:
                pricision = 0.1

        offY = (95 - stepY / 2) % stepY

        if offY > stepY / 2:
            offY = offY - stepY

        col1, row1 = int(wee // stepX), int(wee // stepY)
        col2, row2 = 1, 1
        shotDie = 1
        summary = []

        l = []
        for i in range(int(stepX / 2 / pricision) + 1):
            l.append(i * pricision)
        l = [round(i, 3) for i in l]
        l1 = [offY]

        for Xcell in l:
            for Ycell in l1:
                offX, offY = Xcell, Ycell
                # summary.append([Xcell, Ycell])

                totalDie = 0
                fullShot = 0
                partialShot = 0
                for i in range(-col1 - 1, col1 + 2):
                    for j in range(-row1 - 1, row1 + 2):

                        llx = i * stepX - stepX / 2 + offX
                        lly = j * stepY - stepY / 2 + offY

                        f1 = (pow(llx, 2) + pow(lly, 2)) < pow(wee, 2)
                        f2 = (pow(llx + stepX, 2) + pow(lly, 2)) < pow(wee, 2)
                        f3 = (pow(llx + stepX, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        f4 = (pow(llx, 2) + pow(lly + stepY, 2)) < pow(wee, 2)
                        # laser mark
                        f5 = ((lly + stepY) > 92) and (
                                (llx + stepX < 13 and llx + stepX > -13) or (llx < 13 and llx > -13))
                        f5 = not f5
                        # notch
                        f6 = (lly < -94) and ((llx + stepX < 14 and llx + stepX > -14) or (llx < 14 and llx > -14))
                        f6 = not f6

                        if f1 and f2 and f3 and f4 and f6:
                            totalDie = totalDie + shotDie
                            fullShot = fullShot + 1
                        else:
                            if f1 or f2 or f3 or f4:
                                partialShotDie = 0
                                partialShot = partialShot + 1
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

                                        f6 = (sy < -94) and (
                                                (sx + dieX < 14 and sx + dieX > -14) or (sx < 14 and sx > -14))
                                        f6 = not f6

                                        if f1 and f2 and f3 and f4 and f5 and f6:
                                            partialShotDie += 1

                                totalDie = totalDie + partialShotDie
                print(totalDie)
                summary.append([Xcell, Ycell, totalDie, fullShot, partialShot, fullShot + partialShot])
        summary = pd.DataFrame(summary, columns=['shiftX', 'shiftY', 'DieQty', 'FullShot', 'PartialShot', 'TotalShot'])
        print('to check whether partial shot is exposed')
        summary['leftPartial'] = round((97 + summary['shiftX']-stepX/2)%stepX,3)
        summary['rightPartial'] = round((97 - summary['shiftX'] - stepX / 2) % stepX ,3)
        # summary['mini'] = round( 0.5*stepX-5.9,3)
        summary['mini'] = round( 0.5*stepX-5.8999,4)
        summary['flag'] = [((summary.iloc[i,6]>summary.iloc[i,8]) \
                            and \
                            (summary.iloc[i,7]>summary.iloc[i,8])) \
                           for i in range(summary.shape[0])]









        summary = summary.sort_values(by=['DieQty', 'TotalShot'], ascending=False)
        self.exLineEdit1_82.setText(str(list(summary['DieQty'])[0]))
        DF2TABLE(self.exTable, summary)

        summary = summary[summary['flag'] == True]






        if summary.shape[0]>0:
            tmp = list(summary['DieQty'].unique())[0]
            tmp1 = summary[summary['DieQty'] == tmp]
            # tmp1=tmp1.reset_index().drop('index',axis=1)
            # print(tmp1.shape)

            offX = round(tmp1.iloc[tmp1.shape[0] - 1, 0],5)
            offY = round(tmp1.iloc[tmp1.shape[0] - 1, 1],5)
            self.exLineEdit1_31.setText(str(offX))
            self.exLineEdit1_30.setText(str(offY))
            self.Plot_Map_Notch_Down( stepX, stepY, dieX, dieY, part, wee, offX, offY, largeFlag, laserY_Flag=True,
                                partial_Flag=False)
























































































#############################################################################################################










####################################################################




    def onScBtn01(self):#ESF限制
        fname, _ = QFileDialog.getOpenFileName(self, 'Open file', 'Z:\\_DailyCheck\\ESF', 'CSV FILE(*.csv)')
        if len(fname) > 10:
            tmp = pd.read_csv(fname,encoding='GBK')
            tmp = tmp.fillna('')
            DF2TABLE(table=self.scTab, df=tmp)
            self.scTab.resizeColumnsToContents()
    def onScBtn02(self):#量测spc
        # self.box = QMessageBox(QMessageBox.Warning, '注意', '仅打开目录，请自行点击目录查看量测组相关SPC')
        # self.box.showNormal()
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\SPC99')
    def onScBtn03(self):  # 产品套刻
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\OptOvl_Others\\OvlPicture')
    def onScBtn04(self):  # #套刻QC
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\OptOvl_Others\\QcOvl')
    def onScBtn05(self):  # 条宽QC
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\OptOvl_Others\\QcCdu')
    def onScBtn06(self):  # YMS
        os.startfile('\\\\10.4.72.74\\asml\\YMS 历史data存储')
    def onScBtn07(self):  # YMS
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\WeeklyRework')

        pass
    def onScBtn08(self):  # 预对位
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\PreAlign')
    def onScBtn09(self):  # MCC
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\ASML_AWE')
    def onScBtn10(self):  # NIKON调节
        os.startfile('\\\\10.4.72.74\\asml\\_DailyCheck\\NikonAdjust')
    def onScBtn30(self):
        from DSRW import RunExcel
        exec = RunExcel()
        exec.MainFunction()
        # DSRW.RunExcel.MainFunction(self)
        reply = QMessageBox.information(self, "注意", 'Excel Macro Completed', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn31(self):
        from DSRW import CD_SEM_111
        exec = CD_SEM_111()
        exec.MainFunction()
        reply = QMessageBox.information(self, "注意", 'CD IDP/AMP UPLOAD DONE', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn32(self):
        from DSRW import OvlCheck
        exec = OvlCheck()
        exec.MainFunction()
        reply = QMessageBox.information(self, "注意", 'OVL DATA REFRESHED', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn33(self):
        from DSRW import NikonVector
        NikonVector().MainFunction()
        reply = QMessageBox.information(self, "注意", 'EGA LOG EXTRACYED', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn34(self):
        from DSRW import NikonRecipeDate
        NikonRecipeDate().MainFunction()
        reply = QMessageBox.information(self, "注意", 'Nikon Revision Check Done', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn35(self):
        from DSRW import AsmlBatchReport
        AsmlBatchReport().MainFunction()
        reply = QMessageBox.information(self, "注意", 'ASML BATCH REPORT EXTRACTION DONE', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn36(self):
        from DSRW import AsmlPreAlignment
        AsmlPreAlignment().MainFunction()
        reply = QMessageBox.information(self, "注意", 'ASML Pre_Alignment Plot Done', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def onScBtn37(self):
        from DSRW import PpcsAdo
        PpcsAdo().MainFunction()
        reply = QMessageBox.information(self, "注意", 'R2R CHARTING DONE', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn38(self):
        from DSRW import OvlOpt_FIA_LSA
        OvlOpt_FIA_LSA().MainFunction()
        reply = QMessageBox.information(self, "注意", 'OVERLAY OPTIMUM VALUE PLOT DONE', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def onScBtn39(self):
        from DSRW import CDU_QC_IMAGE_CD
        CDU_QC_IMAGE_CD().MainFunction()
        reply = QMessageBox.information(self, "注意", 'GET CDU IMAGE CD', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def onScBtn40(self):
        from DSRW import CDU_QC_IMAGE_PLOT
        CDU_QC_IMAGE_PLOT().MainFunction()
        reply = QMessageBox.information(self, "注意", 'CDU IMAGE PROFILE', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def onScBtn41(self):
        from DSRW import AWE_ANALYSIS
        AWE_ANALYSIS().MainFunction()
        reply = QMessageBox.information(self, "注意", 'ASML AWE EXTRACTION DONE', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def onScBtn42(self):
        from DSRW import NikonPara
        NikonPara().MainFunction()
        reply = QMessageBox.information(self, "注意", 'NIKON PARAMETER EXTRACTION DONE', QMessageBox.Yes,
                                        QMessageBox.Yes)
    def onScBtn43(self):
        from DSRW import ESF
        ESF().MainFunction()
        reply = QMessageBox.information(self, "注意", 'ESF CONSTRAINTS CHECKED', QMessageBox.Yes, QMessageBox.Yes)

    def onScBtn44(self):
        from DSRW import SPC99
        SPC99().MainFunction()
        reply = QMessageBox.information(self, "注意", 'OFFLINE QC CHART PLOT', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn45(self):
        os.system('P:\\_Script\\FTP\\AsmlDownload.rdp')
        reply = QMessageBox.information(self, "注意", 'ASML DATA DOWNLOAD', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn46(self):
        os.system('P:\\_Script\\FTP\\CdDownload.rdp')
        reply = QMessageBox.information(self, "注意", 'CD IDP/AMP UPLOAD DONE', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn47(self):
        from DSRW import NikonRecipeMaintain
        NikonRecipeMaintain().MainFunction()
        reply = QMessageBox.information(self, "注意", 'FTP FINISHED FOR UPLOAD', QMessageBox.Yes, QMessageBox.Yes)
    def onScBtn48(self):
        from DSRW import QcCduOvl
        QcCduOvl().MainFunction()
        reply = QMessageBox.information(self, "注意", 'CDU/OVL QC PLOT DONE', QMessageBox.Yes, QMessageBox.Yes)



#############################################################################################################










####################################################################
    def r2rInit01(self):
        self.f1 = self.r2rRadioBtn01.isChecked() #CD
        self.f2 = self.r2rRadioBtn02.isChecked() #OVL

        if self.f1 ^ self.f2 == False:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '请选择抽取数据类型 CD? OVL?')
            self.box.showNormal()
            return True
    def GET_CD_DATA(self,sql):
        dbName=r'Z:\_DailyCheck\Database\data.mdb'
        df = CONNECT_DB(dbName, sql)
        if df.shape[0]>0:
            # df['Ji_Time'] = [i.strftime("%Y-%m-%d %H:%M:%S") for i in list(df['Ji_Time'])]
            # df['Dcoll_Time'] = [i.strftime("%Y-%m-%d %H:%M:%S") for i in list(df['Dcoll_Time'])]
            tmp = ['CD_AVG', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'Met_Avg']
            df[tmp] = df[tmp].astype(float)
            tmp = [11, 15, 16, 17, 18, 19, 20, 21, 22, 23]
            df['1'] = df['1'].map(lambda x: ('%.4f') % x)
            for i in range(df.shape[0]):
                for j in range(len([8, 9, 10])):
                    # df.ix[i][j] = float('%.1f' %df.ix[i][j])
                    df.iloc[i, [8, 9, 10][j]] = float('%.1f' % df.iloc[i, [8, 9, 10][j]])
                    df.iloc[i, 12] = float('%.3f' % df.iloc[i, 12])
                for j in range(len(tmp)):
                    df.iloc[i, tmp[j]] = float('%.4f' % df.iloc[i, tmp[j]])
            return df
        else:
            return pd.DataFrame()
    def GET_OVL_DATA(self,sql):
        dbName=r'Z:\_DailyCheck\Database\data.mdb'
        df = CONNECT_DB(dbName, sql)
        if df.shape[0]>0:
            tmp = [i for i in range(9,df.shape[1])]
            for i in range(df.shape[0]):
                for j in range(len(tmp)):
                    df.iloc[i, tmp[j]] = float('%.4f' % df.iloc[i, tmp[j]])
            return df
        else:
            return pd.DataFrame()
    def onR2rBtn01(self):#30
        if self.r2rInit01():
            return
        b = QDateTime.currentDateTime().addDays(-30).toString("yyyy-MM-dd HH:mm:ss")
        if self.f1==True:
            sql = 'select * from CD_data'
            sql = sql + " Where Dcoll_Time > #" + b +"#"
            df = self.GET_CD_DATA(sql)
        else:
            sql = 'select * from OL_ASML'
            sql = sql + " Where Dcoll_Time >  #" + b +"#"
            dbName = r'C:\Git\data.mdb'
            df1 = self.GET_OVL_DATA(sql)

            sql = 'select * from OL_Nikon'
            sql = sql + " Where Dcoll_Time >  #" + b +"#"
            dbName = r'C:\Git\data.mdb'
            df2= self.GET_OVL_DATA(sql)

            df2[0]= ''
            df2[1]= ''
            df2[2]= ''
            df2[3]= ''
            df2[4]= ''
            df2[5]= ''
            df2[6]= ''
            df2[7]= ''
            df2[8]= ''
            df2[9]= ''
            df2.columns = df1.columns
            # print(df2.shape, df1.shape)
            df = pd.concat([df1,df2],axis=0)




        # DF2TABLE(self.r2rTab, df)
        df1,df2=None,None
        self.box = QMessageBox(QMessageBox.Warning, '注意', '读取数据结束')
        self.box.showNormal()
    def onR2rBtn02(self):#60
        if self.r2rInit01():
            return
        b = QDateTime.currentDateTime().addDays(-60).toString("yyyy-MM-dd HH:mm:ss")
        if self.f1==True:
            sql = 'select * from CD_data'
            sql = sql + " Where Dcoll_Time > #" + b +"#"
            df = self.GET_CD_DATA(sql)
        else:
            sql = 'select * from OL_ASML'
            sql = sql + " Where Dcoll_Time >  #" + b +"#"
            dbName = r'C:\Git\data.mdb'
            df1 = self.GET_OVL_DATA(sql)

            sql = 'select * from OL_Nikon'
            sql = sql + " Where Dcoll_Time >  #" + b +"#"
            dbName = r'C:\Git\data.mdb'
            df2= self.GET_OVL_DATA(sql)

            df2[0]= ''
            df2[1]= ''
            df2[2]= ''
            df2[3]= ''
            df2[4]= ''
            df2[5]= ''
            df2[6]= ''
            df2[7]= ''
            df2[8]= ''
            df2[9]= ''
            df2.columns = df1.columns
            # print(df2.shape, df1.shape)
            df = pd.concat([df1,df2],axis=0)




        # DF2TABLE(self.r2rTab, df)
        df1,df2=None,None
        self.box = QMessageBox(QMessageBox.Warning, '注意', '读取数据结束')
        self.box.showNormal()
    def onR2rBtn03(self):#90
        import datetime
        # print(datetime.datetime.now())
        if self.r2rInit01():
            return
        b = QDateTime.currentDateTime().addDays(-90).toString("yyyy-MM-dd HH:mm:ss")
        if self.f1==True:
            sql = 'select * from CD_data'
            sql = sql + " Where Dcoll_Time > #" + b +"#"
            df = self.GET_CD_DATA(sql)
        else:
            sql = 'select * from OL_ASML'
            sql = sql + " Where Dcoll_Time >  #" + b +"#"
            dbName = r'C:\Git\data.mdb'
            df1 = self.GET_OVL_DATA(sql)

            sql = 'select * from OL_Nikon'
            sql = sql + " Where Dcoll_Time >  #" + b +"#"
            dbName = r'C:\Git\data.mdb'
            df2= self.GET_OVL_DATA(sql)

            df2[0]= ''
            df2[1]= ''
            df2[2]= ''
            df2[3]= ''
            df2[4]= ''
            df2[5]= ''
            df2[6]= ''
            df2[7]= ''
            df2[8]= ''
            df2[9]= ''
            df2.columns = df1.columns
            # print(df2.shape, df1.shape)
            df = pd.concat([df1,df2],axis=0)
        df1,df2=None,None
        # print(datetime.datetime.now())
        # print(df.shape)

        # DF2TABLE(self.r2rTab, df)
        # print(datetime.datetime.now())
        self.box = QMessageBox(QMessageBox.Warning, '注意', '读取数据结束')
        self.box.showNormal()
    def onR2rBtn04(self):
        if self.r2rInit01():
            return
        sDateTime=self.onDateTimeChanged()
        eDateTime=self.onDateTimeChanged_2()
        if self.f1 == True:
            sql = 'select * from CD_data'
            sql = sql + " Where Dcoll_Time between #" + sDateTime + "# and #" + eDateTime + "#"
            if self.r2rLineEdit01.text().strip() !="":
                sql = sql + " and tech like '%" + self.r2rLineEdit01.text().strip().upper() + "%'"
            if self.r2rLineEdit02.text().strip() !="":
                sql = sql + " and PartID like '%" + self.r2rLineEdit02.text().strip().upper() + "%'"
            if self.r2rLineEdit03.text().strip() !="":
                sql = sql + " and Layer like '%" + self.r2rLineEdit03.text().strip().upper() + "%'"
            if self.r2rLineEdit04.text().strip() !="":
                sql = sql + " and Proc_EqID like '%" + self.r2rLineEdit04.text().strip().upper() + "%'"
            df = self.GET_CD_DATA(sql)
        else:
            sql = 'select * from OL_ASML'
            sql = sql + " Where Dcoll_Time between #" + sDateTime + "# and #" + eDateTime + "#"
            if self.r2rLineEdit01.text().strip() !="":
                sql = sql + " and tech like '%" + self.r2rLineEdit01.text().strip().upper() + "%'"
            if self.r2rLineEdit02.text().strip() !="":
                sql = sql + " and PartID like '%" + self.r2rLineEdit02.text().strip().upper() + "%'"
            if self.r2rLineEdit03.text().strip() !="":
                sql = sql + " and Layer like '%" + self.r2rLineEdit03.text().strip().upper() + "%'"
            if self.r2rLineEdit04.text().strip() !="":
                sql = sql + " and Proc_EqID like '%" + self.r2rLineEdit04.text().strip().upper() + "%'"
            df1 = self.GET_OVL_DATA(sql)
            if df1.shape[0]==0:
                df1=pd.DataFrame(columns =['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID',
                                           'Dcoll_Time', 'Wafer_ID', 'TranX_jobin', 'TranX_Feedback',
                                           'TranX_Optimum', 'TranX_Met_Avg', 'TranX_Met_Value', 'TranX_OX_min',
                                           'TranX_OX_max', 'TranY_jobin', 'TranY_Feedback', 'TranY_Optimum',
                                           'TranY_Met_avg', 'TranY_Met_value', 'TranY_OY_Min', 'TranY_OY_Max',
                                           'ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_avg',
                                           'ScalX_Met_value', 'ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum',
                                           'ScalY_Met_avg', 'ScalY_Met_value', 'ORT_jobin', 'ORT_Feedback',
                                           'ORT_Optimum', 'ORT_Met_avg', 'ORT_Met_Value', 'Wrot_jobin',
                                           'Wrot_Feedback', 'Wrot_Optimum', 'Wrot_Met_avg', 'Wrot_Met_value',
                                           'Mag_jobin', 'Mag_Feedback', 'Mag_Optimum', 'Mag_Met_avg',
                                           'Mag_Met_value', 'Rot_jobin', 'Rot_Feedback', 'Rot_Optimum',
                                           'Rot_Met_avg', 'Rot_Met_value', 'ARMAG_jobin', 'ARMag_Feedback',
                                           'ARMag_Optimum', 'ARMag_Met_avg', 'ARMag_Met_value', 'ARRot_jobin',
                                           'ARRot_Feedback', 'ARRot_Optimum', 'ARRot_Met_avg', 'ARRot_Met_value'])


            sql = 'select * from OL_Nikon'
            sql = sql + " Where Dcoll_Time between #" + sDateTime + "# and #" + eDateTime + "#"
            if self.r2rLineEdit01.text().strip() != "":
                sql = sql + " and tech like '%" + self.r2rLineEdit01.text().strip().upper() + "%'"
            if self.r2rLineEdit02.text().strip() != "":
                sql = sql + " and PartID like '%" + self.r2rLineEdit02.text().strip().upper() + "%'"
            if self.r2rLineEdit03.text().strip() != "":
                sql = sql + " and Layer like '%" + self.r2rLineEdit03.text().strip().upper() + "%'"
            if self.r2rLineEdit04.text().strip() != "":
                sql = sql + " and Proc_EqID like '%" + self.r2rLineEdit04.text().strip().upper() + "%'"
        #     dbName = r'C:\Git\data.mdb'
            df2 = self.GET_OVL_DATA(sql)
            if df2.shape[0]>0:
                df2[0] = ''
                df2[1] = ''
                df2[2] = ''
                df2[3] = ''
                df2[4] = ''
                df2[5] = ''
                df2[6] = ''
                df2[7] = ''
                df2[8] = ''
                df2[9] = ''
                df2.columns = ['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID',
                                               'Dcoll_Time', 'Wafer_ID', 'TranX_jobin', 'TranX_Feedback',
                                               'TranX_Optimum', 'TranX_Met_Avg', 'TranX_Met_Value', 'TranX_OX_min',
                                               'TranX_OX_max', 'TranY_jobin', 'TranY_Feedback', 'TranY_Optimum',
                                               'TranY_Met_avg', 'TranY_Met_value', 'TranY_OY_Min', 'TranY_OY_Max',
                                               'ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_avg',
                                               'ScalX_Met_value', 'ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum',
                                               'ScalY_Met_avg', 'ScalY_Met_value', 'ORT_jobin', 'ORT_Feedback',
                                               'ORT_Optimum', 'ORT_Met_avg', 'ORT_Met_Value', 'Wrot_jobin',
                                               'Wrot_Feedback', 'Wrot_Optimum', 'Wrot_Met_avg', 'Wrot_Met_value',
                                               'Mag_jobin', 'Mag_Feedback', 'Mag_Optimum', 'Mag_Met_avg',
                                               'Mag_Met_value', 'Rot_jobin', 'Rot_Feedback', 'Rot_Optimum',
                                               'Rot_Met_avg', 'Rot_Met_value', 'ARMAG_jobin', 'ARMag_Feedback',
                                               'ARMag_Optimum', 'ARMag_Met_avg', 'ARMag_Met_value', 'ARRot_jobin',
                                               'ARRot_Feedback', 'ARRot_Optimum', 'ARRot_Met_avg', 'ARRot_Met_value']
            else:
                df2 = pd.DataFrame(columns=['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID',
                                               'Dcoll_Time', 'Wafer_ID', 'TranX_jobin', 'TranX_Feedback',
                                               'TranX_Optimum', 'TranX_Met_Avg', 'TranX_Met_Value', 'TranX_OX_min',
                                               'TranX_OX_max', 'TranY_jobin', 'TranY_Feedback', 'TranY_Optimum',
                                               'TranY_Met_avg', 'TranY_Met_value', 'TranY_OY_Min', 'TranY_OY_Max',
                                               'ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_avg',
                                               'ScalX_Met_value', 'ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum',
                                               'ScalY_Met_avg', 'ScalY_Met_value', 'ORT_jobin', 'ORT_Feedback',
                                               'ORT_Optimum', 'ORT_Met_avg', 'ORT_Met_Value', 'Wrot_jobin',
                                               'Wrot_Feedback', 'Wrot_Optimum', 'Wrot_Met_avg', 'Wrot_Met_value',
                                               'Mag_jobin', 'Mag_Feedback', 'Mag_Optimum', 'Mag_Met_avg',
                                               'Mag_Met_value', 'Rot_jobin', 'Rot_Feedback', 'Rot_Optimum',
                                               'Rot_Met_avg', 'Rot_Met_value', 'ARMAG_jobin', 'ARMag_Feedback',
                                               'ARMag_Optimum', 'ARMag_Met_avg', 'ARMag_Met_value', 'ARRot_jobin',
                                               'ARRot_Feedback', 'ARRot_Optimum', 'ARRot_Met_avg', 'ARRot_Met_value'])
            df = pd.concat([df1, df2], axis=0)

        df1, df2 = None, None
        if df.shape[0]>0:
           # DF2TABLE(self.r2rTab, df)
            self.box = QMessageBox(QMessageBox.Warning, '注意', '读取数据结束,共计'+ str(df.shape[0]) + '行')
            self.box.showNormal()
        else:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据')
            self.box.showNormal()
        self.df = df.copy()
        df=None
        self.df = self.df.sort_values(by='Dcoll_Time')
        # self.df = self.df.sort_values(by='Proc_EqID')
        self.R2R_PUT_SELECTION()
    def R2R_PUT_SELECTION(self):
        tmp = self.df['Proc_EqID'].unique()
        tmp.sort()
        self.r2rListTool.clear()
        for item in tmp:
            self.r2rListTool.addItem(item)
        tmp = self.df['Tech'].unique()
        tmp.sort()
        self.r2rListTech.clear()
        for item in tmp:
            self.r2rListTech.addItem(item)
        tmp = self.df['PartID'].unique()
        tmp.sort()
        self.r2rListPart.clear()
        for item in tmp:
            self.r2rListPart.addItem(item)
        tmp = self.df['Layer'].unique()
        tmp.sort()
        self.r2rListLayer.clear()
        self.r2rListLayer1.clear()
        self.r2rListLayer2.clear()
        for item in tmp:
            self.r2rListLayer.addItem(item)
            self.r2rListLayer1.addItem(item)
            self.r2rListLayer2.addItem(item)





        self.r2rListDate.clear()
        for item in self.df['Dcoll_Time'].unique():
            self.r2rListDate.addItem(item)
        self.r2rListDate.setSelectionMode(QAbstractItemView.ExtendedSelection)
        self.r2rListTool.setSelectionMode(QAbstractItemView.ExtendedSelection)
        self.r2rListTech.setSelectionMode(QAbstractItemView.ExtendedSelection)
        self.r2rListPart.setSelectionMode(QAbstractItemView.ExtendedSelection)
        self.r2rListLayer.setSelectionMode(QAbstractItemView.ExtendedSelection)
    def GET_Para(self):
        tmp = [self.r2rRadioTranX.isChecked(), self.r2rRadioTranY.isChecked(),
                self.r2rRadioExpX.isChecked(),   self.r2rRadioExpY.isChecked(),
                self.r2rRadioOrt.isChecked(),    self.r2rRadioRot.isChecked(),
                self.r2rRadioSMag.isChecked(),   self.r2rRadioSRot.isChecked(),
                self.r2rRadioAMag.isChecked(), self.r2rRadioARot.isChecked(),
                self.r2rRadioCD.isChecked()]
        try:
            no = tmp.index(True)
            tmp1= {0:['TranX_jobin', 'TranX_Feedback', 'TranX_Optimum', 'TranX_Met_Value'],
                   1:['TranX_jobin', 'TranX_Feedback', 'TranX_Optimum',  'TranX_Met_Value'],
                   2:['ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_value'],
                   3:['ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum', 'ScalY_Met_value'],
                   4:['ORT_jobin', 'ORT_Feedback', 'ORT_Optimum', 'ORT_Met_Value'],
                   5:['Wrot_jobin', 'Wrot_Feedback','Wrot_Optimum', 'Wrot_Met_value'],
                   6:['Mag_jobin', 'Mag_Feedback', 'Mag_Optimum',  'Mag_Met_value'],
                   7:['Rot_jobin', 'Rot_Feedback', 'Rot_Optimum', 'Rot_Met_value'],
                   8:['ARMAG_jobin','ARMag_Feedback', 'ARMag_Optimum',  'ARMag_Met_value'],
                   9:['ARRot_jobin', 'ARRot_Feedback',  'ARRot_Optimum',  'ARRot_Met_value'],
                   10:['Jobin', 'Feedback', 'Optimum','CD_target','Met_Avg',
                       '1', '2', '3', '4', '5', '6', '7', '8', '9']}
            key = tmp1[no]
            return key

        except:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未选择作图参数')
            self.box.showNormal()
    def onR2rBtnFilter(self):
        self.r2rListTool.reset()
        self.r2rListTech.reset()
        self.r2rListLayer.reset()
        self.r2rListDate.reset()
        self.r2rListPart.reset()
    def FILTER_DATA(self):

        plotdata = self.df.copy()
        #sort by tech
        text_list = self.r2rListTech.selectedItems()
        text = [i.text() for i in list(text_list)]
        if len(text)>0:
            plotdata = self.df[self.df['Tech'].isin(text)]
        if plotdata.shape[0]==0:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
            self.box.showNormal()
            return
        # by self.r2rListPart
        text_list = self.r2rListPart.selectedItems()
        text = [i.text() for i in list(text_list)]
        if len(text) > 0:
            plotdata = plotdata[plotdata['PartID'].isin(text)]
        if plotdata.shape[0] == 0:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
            self.box.showNormal()
            return
        # self.r2rListLayer
        text_list = self.r2rListLayer.selectedItems()
        text = [i.text() for i in list(text_list)]
        if len(text) > 0:
            plotdata = plotdata[plotdata['Layer'].isin(text)]
        if plotdata.shape[0] == 0:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
            self.box.showNormal()
            return
        # by tool
        text_list = self.r2rListTool.selectedItems()
        text = [i.text() for i in list(text_list)]
        if len(text)>0:
            plotdata = plotdata[plotdata['Proc_EqID'].isin(text)]
        if plotdata.shape[0] == 0:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
            self.box.showNormal()
            return
        return plotdata
    def onR2rByDate(self):
        #['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID', 'Dcoll_Time', 'Wafer_ID']
        # self.df=pd.read_csv('c:/temp/000.csv')
        # self.R2R_PUT_SELECTION()
        try:
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
        except:
            pass

        try:

            # plotdata= pd.read_csv('c:/temp/000.csv')
            # plotdata = plotdata.loc[0:200]
            # key = ['TranX_jobin', 'TranX_Feedback', 'TranX_Optimum',  'TranX_Met_Value']

            self.df = self.df.sort_values(by='Dcoll_Time')
            key = self.GET_Para()
            plotdata= self.FILTER_DATA()
            key.extend(['Dcoll_Time','Proc_EqID','LotID','PartID'])
            plotdata = plotdata[key]
            DF2TABLE(self.r2rTab, plotdata)
            plotdata['Dcoll_Time'] = [  str(n).zfill(5)+'-'+str(i) for n,i  in enumerate(list(plotdata['Dcoll_Time']))]
            if plotdata.shape[1] < 12:  # ovl
                plotdata.columns=[1,2,3,4,5,6,7,8]
                # plotdata[5]=[str(i) for i in list(plotdata[5])]
                MATPLOT(XY=plotdata, items=key,flag=self.r2rRadioBtn02.isChecked())
            else:#CD
                MATPLOT(XY=plotdata, items=key, flag=self.r2rRadioBtn02.isChecked())


        except:
            box = QMessageBox(QMessageBox.Warning, '注意', '请选择参数')
            box.showNormal()
    def onR2rByTool(self):
        try:
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
        except:
            pass
        try:
            self.df = self.df.sort_values(by='Proc_EqID')
            key = self.GET_Para()
            plotdata= self.FILTER_DATA()
            key.extend(['Dcoll_Time','Proc_EqID','LotID','PartID'])
            plotdata = plotdata[key]
            DF2TABLE(self.r2rTab, plotdata)
            plotdata['Dcoll_Time'] = [  str(n).zfill(5)+'-'+str(i) for n,i  in enumerate(list(plotdata['Dcoll_Time']))]
            if plotdata.shape[1] < 12:  # ovl
                plotdata.columns=[1,2,3,4,5,6,7,8]
                MATPLOT(XY=plotdata, items=key,flag=self.r2rRadioBtn02.isChecked())
            else:#cd
                MATPLOT(XY=plotdata, items=key, flag=self.r2rRadioBtn02.isChecked())
        except:
            box = QMessageBox(QMessageBox.Warning, '注意', '请选择参数')
            box.showNormal()
    def onR2rByPart(self):
        # ['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID', 'Dcoll_Time', 'Wafer_ID']
        # self.df=pd.read_csv('c:/temp/000.csv')
        # self.R2R_PUT_SELECTION()
        try:
            plt.clf()  # 清图。
            plt.cla()  # 清坐标轴。
            plt.close()  # 关窗口
            gc.collect()
        except:
            pass

        try:
            self.df = self.df.sort_values(by='PartID')
            key = self.GET_Para()
            plotdata = self.FILTER_DATA()
            key.extend(['Dcoll_Time', 'Proc_EqID', 'PartID','LotID'])
            plotdata = plotdata[key]
            DF2TABLE(self.r2rTab, plotdata)
            plotdata['Dcoll_Time'] = [str(n).zfill(5) + '-' + str(i) for n, i in
                                      enumerate(list(plotdata['Dcoll_Time']))]
            if plotdata.shape[1] < 12:  # ovl
                plotdata.columns = [1, 2, 3, 4, 5, 6, 7, 8]
                MATPLOT(XY=plotdata, items=key, flag=self.r2rRadioBtn02.isChecked())
            else:#cd
                print(plotdata.head(1))
                plotdata.columns=['Jobin', 'Feedback', 'Optimum', 'CD_target', 'Met_Avg',
                                  '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                  'Dcoll_Time', 'Proc_EqID', 'LotID',   'LotID1']
                print(plotdata.head(1))
                MATPLOT(XY=plotdata, items=key,flag=self.r2rRadioBtn02.isChecked())

        except:
            box = QMessageBox(QMessageBox.Warning, '注意', '请选择参数')
            box.showNormal()
    def onR2rCompare(self):
        try:
            self.r2rTab.clear()
            key = self.GET_Para()
            plotdata = self.df.copy()
            # sort by tech
            text_list = self.r2rListTech.selectedItems()
            text = [i.text() for i in list(text_list)]
            if len(text) > 0:
                plotdata = self.df[self.df['Tech'].isin(text)]
            if plotdata.shape[0] == 0:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
                self.box.showNormal()
                return

            # by self.r2rListPart
            text_list = self.r2rListPart.selectedItems()
            text = [i.text() for i in list(text_list)]
            if len(text) > 0:
                plotdata = plotdata[plotdata['PartID'].isin(text)]
            if plotdata.shape[0] == 0:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
                self.box.showNormal()
                return


            # self.r2rListLayer
            text_list = self.r2rListLayer.selectedItems()
            text = [i.text() for i in list(text_list)]
            if len(text) > 0:
                plotdata = plotdata[plotdata['Layer'].isin(text)]
            if plotdata.shape[0] == 0:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
                self.box.showNormal()
                return


            # by tool
            text_list = self.r2rListTool.selectedItems()
            text = [i.text() for i in list(text_list)]
            if len(text) > 0:
                plotdata = plotdata[plotdata['Proc_EqID'].isin(text)]
            if plotdata.shape[0] == 0:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
                self.box.showNormal()
                return

            # self.r2rListLayer1
            text_list = self.r2rListLayer1.selectedItems()
            text = [i.text() for i in list(text_list)]
            if len(text) > 0:
                tmp1 = plotdata[plotdata['Layer'].isin(text)]
            if plotdata.shape[0] == 0:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
                self.box.showNormal()
                return

            # self.r2rListLayer2
            text_list = self.r2rListLayer2.selectedItems()
            text = [i.text() for i in list(text_list)]
            if len(text) > 0:
                tmp2 = plotdata[plotdata['Layer'].isin(text)]
            if plotdata.shape[0] == 0:
                self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到数据，请重新筛选')
                self.box.showNormal()
                return

            key = self.GET_Para()

            tmp1 = tmp1[['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time',
                         'Met_EqID', 'Dcoll_Time', 'Wafer_ID',key[2]]]
            tmp1 = tmp1.drop_duplicates()
            tmp2 = tmp2[['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time',
                         'Met_EqID', 'Dcoll_Time', 'Wafer_ID',key[3]]]
            tmp2 = tmp2.drop_duplicates()
            tmp2.columns = ['Tech2', 'PartID2', 'Layer2', 'LotID2', 'Proc_EqID2', 'Ji_Time2',
                    'Met_EqID2', 'Dcoll_Time2', 'Wafer_ID', key[3]]
            tmp = pd.merge(tmp1, tmp2, how='left', on='Wafer_ID')
            tmp['Wafer_ID'] = [str(n).zfill(3) + '-' + str(i) for n, i in enumerate(list(tmp['Wafer_ID']))]
            tmp = tmp.dropna()


            print(tmp1.shape,tmp2.shape,tmp.shape)



            tmp3 = eval(self.r2RLineX.text())
            tmp4=[0 for i in range(tmp.shape[0])]
            tmp=tmp.reset_index()#tmp=tmp.drop(na)
            for i in range(tmp.shape[0]):
                tmp4[i] = tmp[key[2]][i] + tmp3 * tmp[key[3]][i]
            #     if tmp[key[3]][i]==0:
            #         tmp4[i]=0
            #     else:

            tmp['sum'] =tmp4
            DF2TABLE(self.r2rTab, tmp)
            try:
                tmp.to_csv('c:/temp/000.csv')
            except:
                pass



            tmp1,tmp2,tmp3,plotdata=None,None,None,None

            fig = plt.figure()
            ax1 = plt.subplot(111)
            ax1.plot(tmp['Wafer_ID'], tmp[key[2]], color='red', linestyle=':', marker='o',  MarkerSize=4, alpha=0.8, lw=0.8)
            ax1.plot(tmp['Wafer_ID'], tmp[key[3]], color='blue', linestyle=':', marker='d', MarkerSize=4, alpha=0.8, lw=0.8)
            ax1.plot(tmp['Wafer_ID'], tmp['sum'], color='black', linestyle=':', marker='D', MarkerSize=4, alpha=0.8, lw=0.8)
            x1 = list(tmp['Wafer_ID'])
            x = [i for i in range(0, len(x1), math.ceil(tmp.shape[0] / 50))]
            plt.xticks(x, x1[::math.ceil(tmp.shape[0] / 50)], rotation=90)
            plt.grid(color="grey")
            plt.legend(loc='upper right', fancybox=True, framealpha=0.1, ncol=3)
            plt.margins(0, 0)
            plt.show()
        except:
            box = QMessageBox(QMessageBox.Warning, '注意', 'ERROR，Pending')
            box.showNormal()
    def onR2rOffset(self): # revise data for R2R import function
        tmp = {
                '01_TranX':self.doubleSpinTx.value(),
                '02_TranY':self.doubleSpinTy.value(),
                '03_ExpansionX':self.doubleSpinEx.value(),
                '04_ExpansionY':self.doubleSpinEy.value(),
                '05_Orthogonality':self.doubleSpinOrt.value(),
                '06_Rotation':self.doubleSpinRot.value(),
                '07_ShotMag':self.doubleSpinBoxSMag.value(),
                '08_ShotRotation':self.doubleSpinSRot.value(),
                '09_AsymmetricMag':self.doubleSpinAMag.value(),
                '10_AsymmetricRot':self.doubleSpinARot.value(),
                '11_CD-DOSE':self.doubleSpinCD.value()
               }
        tmp = pd.DataFrame([tmp]).T.reset_index()
        tmp.columns=['Parameter','Value']
        DF2TABLE(table=self.r2rTab,df=tmp)

        reply = QMessageBox.question(self, '注意', 'TranX:' + str(self.doubleSpinTx.value()) + '\n\n'
                                     + 'TranY:' + str(self.doubleSpinTy.value()) + '\n\n'
                                     + 'ExpansionX:' + str(self.doubleSpinEx.value()) + '\n\n'
                                     + 'ExpansionY:' + str(self.doubleSpinEy.value()) + '\n\n'
                                     + 'Orthogonality:' + str(self.doubleSpinOrt.value()) + '\n\n'
                                     + 'Rotation:' + str(self.doubleSpinRot.value()) + '\n\n'
                                     + 'ShotMag:' + str(self.doubleSpinBoxSMag.value()) + '\n\n'
                                     + 'ShotRotation:' + str(self.doubleSpinSRot.value()) + '\n\n'
                                     + 'AsymmetricMag:' + str(self.doubleSpinAMag.value()) + '\n\n'
                                     + 'AsymmetricRot:' + str(self.doubleSpinARot.value()) + '\n\n'
                                     + 'CD-DOSE:' + str(self.doubleSpinCD.value()) + '\n\n',
                                     QMessageBox.Yes|QMessageBox.No|QMessageBox.Abort, QMessageBox.Abort)
        if reply == QMessageBox.Yes:
            pass
        else:
            return

        print('001')
        print('002')
        print('001')

        pass
        sDateTime = self.onDateTimeChanged()
        eDateTime = self.onDateTimeChanged_2()
        sql = 'select * from CD_data'
        sql = sql + " Where Dcoll_Time between #" + sDateTime + "# and #" + eDateTime + "#"
        if self.r2rLineEdit01.text().strip() != "":
            sql = sql + " and tech like '%" + self.r2rLineEdit01.text().strip().upper() + "%'"
        if self.r2rLineEdit02.text().strip() != "":
            sql = sql + " and PartID like '%" + self.r2rLineEdit02.text().strip().upper() + "%'"
        if self.r2rLineEdit03.text().strip() != "":
            sql = sql + " and Layer like '%" + self.r2rLineEdit03.text().strip().upper() + "%'"
        if self.r2rLineEdit04.text().strip() != "":
            sql = sql + " and Proc_EqID like '%" + self.r2rLineEdit04.text().strip().upper() + "%'"
        CD = self.GET_CD_DATA(sql)
        if CD.shape[0] == 0:
            # reply = QMessageBox.question(self, '消息框标题', '未筛选到CD数据，退出', QMessageBox.Yes | QMessageBox.No, QMessageBox.No)
            reply = QMessageBox.question(self, '消息框标题', '未筛选到CD数据，退出', QMessageBox.Abort,QMessageBox.Abort)
            if reply == QMessageBox.Abort:
                return





        sql = 'select * from OL_ASML'
        sql = sql + " Where Dcoll_Time between #" + sDateTime + "# and #" + eDateTime + "#"
        if self.r2rLineEdit01.text().strip() != "":
            sql = sql + " and tech like '%" + self.r2rLineEdit01.text().strip().upper() + "%'"
        if self.r2rLineEdit02.text().strip() != "":
            sql = sql + " and PartID like '%" + self.r2rLineEdit02.text().strip().upper() + "%'"
        if self.r2rLineEdit03.text().strip() != "":
            sql = sql + " and Layer like '%" + self.r2rLineEdit03.text().strip().upper() + "%'"
        if self.r2rLineEdit04.text().strip() != "":
            sql = sql + " and Proc_EqID like '%" + self.r2rLineEdit04.text().strip().upper() + "%'"
        OVL1 = self.GET_OVL_DATA(sql)
        if OVL1.shape[0] == 0:
            OVL1 = pd.DataFrame(
                columns=['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID', 'Dcoll_Time',
                         'Wafer_ID', 'TranX_jobin', 'TranX_Feedback', 'TranX_Optimum', 'TranX_Met_Avg',
                         'TranX_Met_Value', 'TranX_OX_min', 'TranX_OX_max', 'TranY_jobin', 'TranY_Feedback',
                         'TranY_Optimum', 'TranY_Met_avg', 'TranY_Met_value', 'TranY_OY_Min', 'TranY_OY_Max',
                         'ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_avg', 'ScalX_Met_value',
                         'ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum', 'ScalY_Met_avg', 'ScalY_Met_value',
                         'ORT_jobin', 'ORT_Feedback', 'ORT_Optimum', 'ORT_Met_avg', 'ORT_Met_Value', 'Wrot_jobin',
                         'Wrot_Feedback', 'Wrot_Optimum', 'Wrot_Met_avg', 'Wrot_Met_value', 'Mag_jobin',
                         'Mag_Feedback', 'Mag_Optimum', 'Mag_Met_avg', 'Mag_Met_value', 'Rot_jobin', 'Rot_Feedback',
                         'Rot_Optimum', 'Rot_Met_avg', 'Rot_Met_value', 'ARMAG_jobin', 'ARMag_Feedback',
                         'ARMag_Optimum', 'ARMag_Met_avg', 'ARMag_Met_value', 'ARRot_jobin', 'ARRot_Feedback',
                         'ARRot_Optimum', 'ARRot_Met_avg', 'ARRot_Met_value'])

        sql = 'select * from OL_Nikon'
        sql = sql + " Where Dcoll_Time between #" + sDateTime + "# and #" + eDateTime + "#"
        if self.r2rLineEdit01.text().strip() != "":
            sql = sql + " and tech like '%" + self.r2rLineEdit01.text().strip().upper() + "%'"
        if self.r2rLineEdit02.text().strip() != "":
            sql = sql + " and PartID like '%" + self.r2rLineEdit02.text().strip().upper() + "%'"
        if self.r2rLineEdit03.text().strip() != "":
            sql = sql + " and Layer like '%" + self.r2rLineEdit03.text().strip().upper() + "%'"
        if self.r2rLineEdit04.text().strip() != "":
            sql = sql + " and Proc_EqID like '%" + self.r2rLineEdit04.text().strip().upper() + "%'"
        #     dbName = r'C:\Git\data.mdb'
        OVL2 = self.GET_OVL_DATA(sql)
        if OVL2.shape[0] > 0:
            OVL2[0] = ''
            OVL2[1] = ''
            OVL2[2] = ''
            OVL2[3] = ''
            OVL2[4] = ''
            OVL2[5] = ''
            OVL2[6] = ''
            OVL2[7] = ''
            OVL2[8] = ''
            OVL2[9] = ''
            OVL2.columns = ['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID', 'Dcoll_Time',
                           'Wafer_ID', 'TranX_jobin', 'TranX_Feedback', 'TranX_Optimum', 'TranX_Met_Avg',
                           'TranX_Met_Value', 'TranX_OX_min', 'TranX_OX_max', 'TranY_jobin', 'TranY_Feedback',
                           'TranY_Optimum', 'TranY_Met_avg', 'TranY_Met_value', 'TranY_OY_Min', 'TranY_OY_Max',
                           'ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_avg', 'ScalX_Met_value',
                           'ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum', 'ScalY_Met_avg', 'ScalY_Met_value',
                           'ORT_jobin', 'ORT_Feedback', 'ORT_Optimum', 'ORT_Met_avg', 'ORT_Met_Value', 'Wrot_jobin',
                           'Wrot_Feedback', 'Wrot_Optimum', 'Wrot_Met_avg', 'Wrot_Met_value', 'Mag_jobin',
                           'Mag_Feedback', 'Mag_Optimum', 'Mag_Met_avg', 'Mag_Met_value', 'Rot_jobin',
                           'Rot_Feedback', 'Rot_Optimum', 'Rot_Met_avg', 'Rot_Met_value', 'ARMAG_jobin',
                           'ARMag_Feedback', 'ARMag_Optimum', 'ARMag_Met_avg', 'ARMag_Met_value', 'ARRot_jobin',
                           'ARRot_Feedback', 'ARRot_Optimum', 'ARRot_Met_avg', 'ARRot_Met_value']
        else:
            df2 = pd.DataFrame(
                columns=['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID', 'Dcoll_Time',
                         'Wafer_ID', 'TranX_jobin', 'TranX_Feedback', 'TranX_Optimum', 'TranX_Met_Avg',
                         'TranX_Met_Value', 'TranX_OX_min', 'TranX_OX_max', 'TranY_jobin', 'TranY_Feedback',
                         'TranY_Optimum', 'TranY_Met_avg', 'TranY_Met_value', 'TranY_OY_Min', 'TranY_OY_Max',
                         'ScalX_jobin', 'ScalX_Feedback', 'ScalX_Optimum', 'ScalX_Met_avg', 'ScalX_Met_value',
                         'ScalY_jobin', 'ScalY_Feedback', 'ScalY_Optimum', 'ScalY_Met_avg', 'ScalY_Met_value',
                         'ORT_jobin', 'ORT_Feedback', 'ORT_Optimum', 'ORT_Met_avg', 'ORT_Met_Value', 'Wrot_jobin',
                         'Wrot_Feedback', 'Wrot_Optimum', 'Wrot_Met_avg', 'Wrot_Met_value', 'Mag_jobin',
                         'Mag_Feedback', 'Mag_Optimum', 'Mag_Met_avg', 'Mag_Met_value', 'Rot_jobin', 'Rot_Feedback',
                         'Rot_Optimum', 'Rot_Met_avg', 'Rot_Met_value', 'ARMAG_jobin', 'ARMag_Feedback',
                         'ARMag_Optimum', 'ARMag_Met_avg', 'ARMag_Met_value', 'ARRot_jobin', 'ARRot_Feedback',
                         'ARRot_Optimum', 'ARRot_Met_avg', 'ARRot_Met_value'])
        OVL = pd.concat([OVL1, OVL2], axis=0)

        OVL1, OVL2 = None, None
        if OVL.shape[0] == 0:
            self.box = QMessageBox(QMessageBox.Warning, '注意', '未筛选到OVL数据')
            self.box.showNormal()
            return
        #without feedback removed
        CD = CD[CD['Feedback']>0]
        tmp =(OVL['TranY_Feedback']==0) \
            & (OVL['TranX_Feedback']==0) \
            & (OVL['ScalX_Feedback']==0) \
            & (OVL['ScalY_Feedback'] == 0)
        tmp = [not i for i in tmp]
        OVL=OVL[tmp]
        #get latest data
        print("all data:",CD.shape, OVL.shape)
        OVL = OVL.sort_values(by=['PartID', 'Layer', 'Proc_EqID','Ji_Time'], ascending=False)
        OVL = OVL.reset_index()
        tmp=[]
        for i in range(OVL.shape[0]):
            if i==0:
                tmp.append(True)
            else:
                tmp.append(  not(       (OVL['PartID'][i]==OVL['PartID'][i-1])
                                  and   (OVL['Layer'][i]==OVL['Layer'][i-1])
                                  and   (OVL['Proc_EqID'][i]==OVL['Proc_EqID'][i-1])  ))
        OVL=OVL[tmp]

        CD = CD.sort_values(by=['PartID', 'Layer', 'Proc_EqID','Ji_Time'], ascending=False)
        CD = CD.reset_index()
        tmp=[]
        for i in range(CD.shape[0]):
            if i==0:
                tmp.append(True)
            else:
                tmp.append(  not(       (CD['PartID'][i]==CD['PartID'][i-1])
                                  and   (CD['Layer'][i]==CD['Layer'][i-1])
                                  and   (CD['Proc_EqID'][i]==CD['Proc_EqID'][i-1])  ))
        CD=CD[tmp]
        print("latest data:",CD.shape,OVL.shape)

        CD = CD [['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID','Ji_Time', 'Met_EqID', 'Dcoll_Time',
                  'Jobin', 'Feedback', 'Optimum', 'CD_AVG', 'CD_target']]
        OVL = OVL[['Tech', 'PartID', 'Layer', 'LotID', 'Proc_EqID', 'Ji_Time', 'Met_EqID', 'Dcoll_Time',
                   'TranX_Feedback',  'TranY_Feedback', 'ScalX_Feedback',  'ScalY_Feedback',
                   'ORT_Feedback', 'Wrot_Feedback', 'Mag_Feedback', 'Rot_Feedback',  'ARMag_Feedback',  'ARRot_Feedback']]


        try:
            CD.to_csv('c:/temp/CD.csv')
            OVL.to_csv("c:/temp/OVL.csv")
        except:
            pass

if __name__=="__main__":
    app = QApplication(sys.argv)
    myWin = MyMainWindow()
    myWin.show()
    sys.exit(app.exec_())



#todo MA/MT/MV Linear--》Threshold