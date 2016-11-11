#!/bin/sh

# Shell script that will send am email (run this via cronjob) if disk space exceeds alert
# threshold.  It uses an external mail server incase sendmail, etc is not installed.

ADMIN="zburns@katzmidas.com"
ALERT=45




function check_space_and_send_email() 
{
 while read output;
 do
  #echo $output
  usep=$(echo $output | awk '{ print $1}' | cut -d'%' -f1)
  partition=$(echo $output | awk '{print $2}')
  if [ $usep -ge $ALERT ] ; then
     echo "Running out of space \"$partition ($usep%)\" on server $(hostname), $(date)" | \
     /home/zburns/scripts/sendoutofspaceemail.sh
  fi
 done
}





df -H | grep -vE "^Filesystem|tmpfs|cdrom" | awk '{print $5 " " $6}' | check_space_and_send_email

