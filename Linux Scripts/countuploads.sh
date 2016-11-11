DIR=/home/aaa/bbb/ccc/ddd/tmp/uploads
TOADDRESS=person@domain.com

COUNT=`ls "$DIR"/* 2>/dev/null | wc -l`
# no mail if less than x files are found
[ "$COUNT" -lt "1" ] && exit 0
# build mail body
(
    echo "There are $COUNT new uploads on Zack's FTP Site.  You should investigate!"
    # optional: list files with failed
    ls "$DIR"/*
) | mail -s "$COUNT New Uploaded Files" $TOADDRESS
