# 3xui-multi-protocol


مولتی پروتکل ساز مخصوص پنل 3xui

## نصب


از کد زیر جهت استفاده از اسکریپت استفاده کنید. با زدن کد زیر بصورت خودکار اسکریپت اجرا می شود و هر 25 ثانیه ترافیک کاربرانی را که دارای لینک سابسکریپشن یکسانی هستند را یکسان سازی می کند.

```bash
bash <(curl -Ls https://raw.githubusercontent.com/M4mmad/3xui-multi-protocol/master/install.sh --ipv4)
```

##  توجه 

این کد تنها ترافیک کاربرانی که دارای لینک سابسکریپشن یکسانی هستند را یکسان سازی می کند و هیچگونه ipLimit و یا کار دیگری را انجام نمی دهد.

برای استفاده حتما باید کلاینت ها را در inbound های مورد نظر با Subscription  id یکسان ایجاد کنید!


![image](https://github.com/M4mmad/3xui-multi-protocol/assets/61095662/196f9e7e-d248-4aed-940a-2ab8f9a13d95)


## Stop
برای کاهش حجم و یا ریست ترافیک کاربر لازم است سرویس را متوقف کنید!

برای توقف اجرای اسکریپت کافیست کد زیر رو وارد کنید.

```bash
systemctl stop 3xui-multi-protocol
```

اجرای دوباره :
```bash
systemctl start 3xui-multi-protocol
```

#حذف
```bash
bash <(curl -Ls https://raw.githubusercontent.com/M4mmad/3xui-multi-protocol/master/unistall.sh --ipv4)
```
