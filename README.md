## SchacoPDFViewer ##

![image](https://github.com/tiancai4652/SchacoPDFViewer/blob/master/1.png)   
![image](https://github.com/tiancai4652/SchacoPDFViewer/blob/master/2.png)   

主要实现了对于Excel文件转换PDF，并提供了PDF预览的界面

**主要应用的技数:**  
MahaApp.Metro  
MvvmLight  
Aspose,Spire,Office操作Excel PDF  
libmupdf进行pdf预览

### 一 操作说明 ###
![image](https://github.com/tiancai4652/SchacoPDFViewer/blob/master/1.png)   
 - 点击...选择包含Excel/Pdf文件的文件夹
 - 选择Excel转Pdf组件，包括Aspose库(不需要安装Office)，Spire库(不需要安装Office)，原生Office库
(需要安装MS Office)
 - 选择打印PDF工具和打印Excl工具
 - 点击Next

![image](https://github.com/tiancai4652/SchacoPDFViewer/blob/master/2.png)   
 - 双击Excel将会将Excel文件转换成PDF文件并输出到界面(每次双击将会重新生成并覆盖上一个PDF文件)，双击PDF文件将会将其输出到界面

### 本工具制作用途 ###

为了解决通过原生Office转excel到PDF的用户必须安装MS Office才能转换，进而测试不依赖Office的组件Aspose和Spire


### 再次吐槽一下客户Office的坑 ###

1 客户是精简版Office   
2 Office2007还需要安装XPS组件  
3 客户用不同版本的Office编辑Excel有兼容性错误  
4 Win10自带的Office需要重新安装才能转POF(即不是完整安装)


