ls -Rhl %1 | grep "^-rw-rw-rw-" > %3-l.log
ls -Rhl %2 | grep "^-rw-rw-rw-" > %3-r.log
TortoiseMerge %3-l.log %3-r.log