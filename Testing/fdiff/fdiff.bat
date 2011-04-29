ls -Rhl %1 | grep "^[^d][rw-]\{3\}" > %3-l.log
ls -Rhl %2 | grep "^[^d][rw-]\{3\}" > %3-r.log
TortoiseMerge %3-l.log %3-r.log