mergeInto(LibraryManager.library, {
    ExecuteMethodToJS: function (methodName, data) {
        if (methodName == "FillInputObj"){
			FillInputObj(data);
		}	
		else if (methodName == "FillInputMtl"){
			FillInputMtl(data);
		}	
		else if (methodName == "FillInputBd"){
			FillInputBd(data);
		}
    }
   	SendImagesToJS: function(base64Datas, fileNames){
		
   	}
});
