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
        
    },Submit: function () {
        const form = document.getElementById('assetForm');
        if (!form) {
            window.alert('form없음');
            return;
        }
        form.submit();
    },
    RegisterFocusEventListener: function () {
        document.addEventListener('focusin', function (e) {
            if (e.target.tagName === 'INPUT' || e.target.tagName === 'TEXTAREA') {
                // 입력 요소에 포커스가 가면 Unity로 메시지 전달
                SendMessage('User', 'OnInputFocusChanged', 'true');
            }
        });

        document.addEventListener('focusout', function (e) {
            if (e.target.tagName === 'INPUT' || e.target.tagName === 'TEXTAREA') {
                // 입력 요소에서 포커스가 벗어나면 Unity로 메시지 전달
                SendMessage('User', 'OnInputFocusChanged', 'false');
            }
        });
    },
    getUserAuthority: function (){
        const userAuthority = document.getElementById('userAuthority');
        if (!userAuthority) {
            window.alert('userAuthority없음');
            return;
        }
        let authority = userAuthority.value;
        SendMessage('Manager', 'SetUserAuthority', authority);
    },
    getBd: function(){
        const bd = document.getElementById('bd');
        if (!bd) {
            window.alert('bd없음');
            return;
        }
        Sendmassage('Manager', 'LoadModel', bd.value);
    }
});