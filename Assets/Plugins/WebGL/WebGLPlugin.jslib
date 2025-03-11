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
    },
    SendImagesToJS: function(binaryImages, fileNames){
        const input = document.getElementById("imageInput"); // 인풋 태그 ID 지정
        if (!input) {
            window.alert("imageInput 아이디를 가진 인풋태그를 찾을 수 없음")
            return;
        }

        const dataTransfer = new DataTransfer(); // 가상의 파일 리스트 생성

        for (let i = 0; i < binaryImages.length; i++) {
            let binaryImage = binaryImages[i];
            let filename = fileNames[i];

            let blob = new Blob([binaryImage], { type: "image/png" });
            let file = new File([blob], filename, { type: "image/png" });

            dataTransfer.items.add(file);
        }

        input.files = dataTransfer.files; // 인풋 태그에 파일 리스트 설정
    }
});