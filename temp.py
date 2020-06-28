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
'''
sqlite3事务总结:
在connect()中不传入 isolation_level
事务处理:
  使用connection.commit()
分析:
  智能commit状态:
    生成方式: 在connect()中不传入 isolation_level, 此时isolation_level==''
      在进行 执行Data Modification Language (DML) 操作(INSERT/UPDATE/DELETE/REPLACE)时, 会自动打开一个事务,
      在执行 非DML, 非query (非 SELECT 和上面提到的)语句时, 会隐式执行commit
      可以使用 connection.commit()方法来进行提交
    注意:
      不能和cur.execute("COMMIT")共用
  自动commit状态:
    生成方式: 在connect()中传入 isolation_level=None
      这样,在任何DML操作时,都会自动提交
    事务处理
      connection.execute("BEGIN TRANSACTION")
      connection.execute("COMMIT")
    如果不使用事务, 批量添加数据非常缓慢
数据对比:
  两种方式, 事务耗时差别不大
  count = 100000
    智能commit即时提交耗时: 0.621
    自动commit耗时: 0.601
    智能commit即时提交耗时: 0.588
    自动commit耗时: 0.581
    智能commit即时提交耗时: 0.598
    自动commit耗时: 0.588
    智能commit即时提交耗时: 0.589
    自动commit耗时: 0.602
    智能commit即时提交耗时: 0.588
    自动commit耗时: 0.622
'''
import sys
import time

class Elapse_time(object):
  '''耗时统计工具'''
  def __init__(self, prompt=''):
    self.prompt = prompt
    self.start = time.time()
  def __del__(self):
    print('%s耗时: %.3f' % (self.prompt, time.time() - self.start))
CElapseTime = Elapse_time
import sqlite3
# -------------------------------------------------------------------------------
# 测试
#
filename = 'c:/temp/test.db'
def prepare(isolation_level = ''):
  connection = sqlite3.connect(filename, isolation_level = isolation_level)
  connection.execute("create table IF NOT EXISTS people (num, age)")
  connection.execute('delete from people')
  connection.commit()
  return connection, connection.cursor()
def db_insert_values(cursor, count):
  num = 1
  age = 2 * num
  while num <= count:
    cursor.execute("insert into people values (?, ?)", (num, age))
    num += 1
    age = 2 * num
def study_case1_intelligent_commit(count):
  '''
  在智能commit状态下, 不能和cur.execute("COMMIT")共用
  '''
  connection, cursor = prepare()
  elapse_time = Elapse_time(' 智能commit')
  db_insert_values(cursor, count)
  cursor.execute("select count(*) from people")
  print (cursor.fetchone())
def study_case2_autocommit(count):
  connection, cursor = prepare(isolation_level = None)
  elapse_time = Elapse_time(' 自动commit')
  db_insert_values(cursor, count)
  cursor.execute("select count(*) from people")
  print (cursor.fetchone())
def study_case3_intelligent_commit_manual(count):
  connection, cursor = prepare()
  elapse_time = Elapse_time(' 智能commit即时提交')
  db_insert_values(cursor, count)
  connection.commit()
  cursor.execute("select count(*) from people")
  print (cursor.fetchone())
def study_case4_autocommit_transaction(count):
  connection, cursor = prepare(isolation_level = None)
  elapse_time = Elapse_time(' 自动commit')
  connection.execute("BEGIN TRANSACTION;") # 关键点
  db_insert_values(cursor, count)
  connection.execute("COMMIT;") #关键点
  cursor.execute("select count(*) from people;")
  print (cursor.fetchone())
if __name__ == '__main__':
  count = 100000
  prepare()
  for i in range(1):
      study_case1_intelligent_commit(count)  # 不提交数据

      # study_case2_autocommit(count) #非常缓慢
      # study_case3_intelligent_commit_manual(count)
      study_case4_autocommit_transaction(count)

