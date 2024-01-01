#!/bin/bash
    systemctl stop 3xui-multi-protocol
    systemctl disable 3xui-multi-protocol
    rm /etc/systemd/system/3xui-multi-protocol.service -f
    systemctl daemon-reload
    systemctl reset-failed
    rm /etc/3xui-multi-protocol -rf
    rm 3xui-multi-protocol/3xui-multi-protocol -rf
    apt-get purge dotnet-sdk-7.0
    apt remove dotnet*
    apt remove aspnetcore*
    apt remove netstandard*
   rm -f /etc/apt/sources.list.d/microsoft-prod.list && apt update
    echo "Uninstalled Successfully!"

