# openbsd-miniroot-patcher
If you don't have dd, you can use this utility to modify miniroot.img to install on ROC-RK3399-PC (Renegade Elite)

HowTo Use:
MinirootPatcher.exe -m x:\path_to_minirootXX.img -i x:\path_to_idbloader.img -u X:\path_to_u-boot.itb

The utility will write idbloader.img and u-boot.itb to minirootXX.img at the correct (for ROC-RK3399-PC) offsets.
Then you can write miniroot.img to the SD card and boot from it.
