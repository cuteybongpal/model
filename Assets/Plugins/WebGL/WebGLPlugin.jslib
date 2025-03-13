mergeInto(LibraryManager.library, {
    ExecuteMethodToJS: function (methodNamePtr, dataPtr) {
        const methodName = UTF8ToString(methodNamePtr);
        const data = UTF8ToString(dataPtr);
        if (methodName == "FillInputObj"){
            const input = document.getElementById('obj');
            if (!input)
                window.alert('objInput없음');
            input.value = data
        }    
        else if (methodName == "FillInputMtl"){
            const input = document.getElementById('mtl');
            if (!input)
                window.alert('objInput없음');
            input.value = data
        }    
        else if (methodName == "FillInputBd"){
            const input = document.getElementById('bd');
            if (!input)
                window.alert('objInput없음');
            input.value = data
        }
    },
    SendImagesToJS: function (Base64ImagesJson, fileNamesJson) {
        const input = document.getElementById("imageInput");

        let Base64Images = JSON.parse(UTF8ToString(Base64ImagesJson));
        let fileNames = JSON.parse(UTF8ToString(fileNamesJson));

        //alert(fileNames[0]);  // 정상적인 문자열 배열 출력 가능
        //alert(Base64Images[0]);

        alert(Base64Images);
        if (!input) {
            window.alert("imageInput 아이디를 가진 인풋태그를 찾을 수 없음");
            return;
        }

        const dataTransfer = new DataTransfer();
        //alert(Base64Images.items)
        for (let i = 0; i < Base64Images.length; i++) {
            let binaryString = atob(Base64Images[i]);
            let filename = UTF8ToString(fileNames[i]);

            let byteArray = new Uint8Array(binaryString.length);
            for (let j = 0; j < binaryString.length; j++) {
                byteArray[j] = binaryString.charCodeAt(j);
            }

            let blob = new Blob([byteArray], { type: "image/png" });
            let file = new File([blob], filename, { type: "image/png" });
            console.log("파일 이름: " + filename);
            dataTransfer.items.add(file);
        }            

        input.files = dataTransfer.files;
    }
});