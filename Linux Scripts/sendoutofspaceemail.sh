#!/usr/bin/expect

set timeout 30
proc abort {} { exit 2 }

spawn nc -C aaa.bbb.ccc.ddd 25
expect default abort "220 "
send "HELO katzmidas.com\r"
expect default abort "\n250 "
send "MAIL FROM:backup@katzmidas.com\r"
expect default abort "\n250 "
send "RCPT TO:zburns@katzmidas.com\r"
expect default abort "\n250 "
send "DATA\r"
expect default abort "\n354 "
send "From: backup@katzmidas.com\r"
send "To: zburns@katzmidas.com\r"
send "Subject: SERVER RUNNING OUT OF SPACE\r"
send "\r"
send "The BlackSheep Backup Server is Running out of Space - You better check the backup disk!\r"
send ".\r"
expect default abort "\n250 "
send "QUIT\r"
