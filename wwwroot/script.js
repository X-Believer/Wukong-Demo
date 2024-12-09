// 每个API的路径
const apiPaths = {
    1: 'https://api.example.com/endpoint1',
    2: 'https://api.example.com/endpoint2',
    3: 'https://api.example.com/endpoint3',
    4: 'https://api.example.com/endpoint4',
    5: 'https://api.example.com/endpoint5',
    6: 'https://api.example.com/endpoint6',
    7: 'https://api.example.com/endpoint7',
    8: 'https://api.example.com/endpoint8',
    9: 'https://api.example.com/endpoint9',
    10: 'https://api.example.com/endpoint10',
};

// 切换表单显示
function toggleApiForm(apiNumber) {
    for (let i = 1; i <= 10; i++) {
        const form = document.getElementById('api-form-' + i);
        form.style.display = i === apiNumber ? 'block' : 'none';
    }
}

// 调用API并显示结果
function callApi(apiNumber) {
    const input = document.getElementById('input-' + apiNumber).value;
    const outputBox = document.getElementById('output-' + apiNumber);
    const apiPath = apiPaths[apiNumber];

    // 检查是否有输入
    if (!input) {
        outputBox.innerText = "请输入有效的内容！";
        return;
    }

    // 发起 API 请求
    fetch(apiPath, {
        method: 'POST', // 或根据需要使用 'GET'
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ input: input }), // 发送数据给API
    })
        .then(response => response.json()) // 解析JSON响应
        .then(data => {
            // 假设 API 返回的响应数据格式为 { message: 'response message' }
            outputBox.innerText = `API ${apiNumber} response: ${data.message || '没有返回消息'}`;
        })
        .catch(error => {
            outputBox.innerText = `发生错误: ${error.message}`;
        });
}
