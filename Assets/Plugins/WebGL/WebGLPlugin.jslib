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

        if (!input) {
            window.alert("imageInput 아이디를 가진 인풋태그를 찾을 수 없음");
            return;
        }

        const dataTransfer = new DataTransfer();

        for (let i = 0; i < Base64Images.items.length; i++) {
            let binaryString = atob(Base64Images.items[i]);
            let filename = fileNames.items[i]+".png";
            var byteArray = new Uint8Array(binaryString.length);

            for (let j = 0; j < binaryString.length; j++) {
                byteArray[j] = binaryString.charCodeAt(j);  // 바이너리 문자열의 각 문자를 숫자 코드로 변환하여 배열에 저장
            }

            let blob = new Blob([byteArray], { type: "image/png" });
            let file = new File([blob], filename, { type: "image/png" });
            dataTransfer.items.add(file);
        }
        input.files = dataTransfer.files;
        
    },
    SendThumbnailToJS: function(base64Data) {
    const input = document.getElementById("thumbnailInput");
    if (!input) {
        window.alert("thumbnailInput 아이디를 가진 인풋태그를 찾을 수 없음");
        return;
    }

    // 💡 base64Data는 C#에서 넘긴 UTF8 인코딩된 base64 문자열의 포인터라고 가정
    let stringData = UTF8ToString(base64Data); // ✅ 올바른 사용
    // ✅ base64 디코딩
    let binaryStr = atob(stringData); // 'data' → 'binaryStr'로 이름 바꿔서 더 명확하게
    let byteArray = new Uint8Array(binaryStr.length);
    for (let i = 0; i < binaryStr.length; i++) {
        byteArray[i] = binaryStr.charCodeAt(i); // ✅ 문자를 byte로 변환
    }

    // ✅ Blob → File → DataTransfer → input.files 설정
    let dataTransfer = new DataTransfer();
    let blob = new Blob([byteArray], { type: "image/png" }); // ⛏️ 'byteArray'를 사용해야 함
    let file = new File([blob], "thumbnail.png", { type: "image/png" });
    dataTransfer.items.add(file);

    input.files = dataTransfer.files;
},
    Submit: function () {
        const form = document.getElementById('assetForm');
        if (!form) {
            window.alert('form없음');
            return;
        }
        form.submit();
    }
});