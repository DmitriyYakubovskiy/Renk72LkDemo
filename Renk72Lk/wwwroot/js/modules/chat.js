$(document).ready(function () {
    autoScroll();

    const userId = window.chatConfig.userId;
    const bidId = window.chatConfig.userBidId;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start()
        .then(() => {
            connection.invoke("JoinBidGroup", userId, bidId);
        })
        .catch(err => console.error(err.toString()));

    connection.on("ReceiveMessage", (message) => {
        const isCurrentUser = message.userId === userId;
        let fileHtml = '';
        if (message.file) {
            fileHtml = `
                    <div class="chat-attachment">
                        <a href="${message.file.path}" class="chat-attachment__item" target="_blank">
                            <div class="chat-attachment__icon">
                                <i class="far fa-file-excel"></i>
                            </div>
                            <div class="chat-attachment__name">
                                ${message.file.name}
                            </div>
                        </a>
                    </div>
                `;
        }

        const messageHtml = `
            <div class="chat__message ${isCurrentUser ? "chat__message--revers" : ""}">
                <div>
                    ${`<button type="button" onclick="deleteMessage(${message.id})" class="btn btn-delete">x</button>`}
                </div>
                ${message.message}
                ${fileHtml}
                <div class="chat__user">
                    ${message.user.surname} ${message.user.name} ${message.user.patronymic}
                    <div class="chat__data">${new Date(message.createdAt).toLocaleString()}</div>
                </div>
            </div>
        `;

        $('.chat__window').append(messageHtml);
        autoScroll();
    });

    connection.on("MessageDeleted", (messageId) => {
        console.log(`.chat__message: has(button[onclick = "deleteMessage(${messageId})"])`);
        $(`.chat__message: has(button[onclick = "deleteMessage(${messageId})"])`).remove();
    });

    connection.on("ReceiveError", (error) => {
        showAlert(error, 'danger');
    });

    $('.form-message').on('submit', function (event) {
        event.preventDefault();
        var form = this;
        var messageText = $(form).find('.textarea-mesage').val();
        var fileInput = $(form).find('input[type="file"]')[0];
        var files = fileInput.files;

        var uploadPromises = [];

        if (!messageText && messageText.trim() === '' && !files) {
            showAlert('Нельзя отправить пустое сообщение');
            return;
        }

        if (files && files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                (function (file) {
                    let promise = uploadFile(file)
                        .then(function (response) {
                            if (response && response.id) {
                                return sendMessage(
                                    bidId,
                                    "Прикреплен файл",
                                    userId,
                                    response.id,
                                    response.fileName,
                                    response.filePath
                                );
                            } else {
                                throw new Error('Ошибка загрузки файла');
                            }
                        });
                    uploadPromises.push(promise);
                })(files[i]);
            }
        }
        if (messageText) {
            uploadPromises.push(sendMessage(bidId, messageText, userId, null));
        }

        Promise.all(uploadPromises)
            .then(function () {
                $(form).trigger('reset');
            })
            .catch(function (error) {
                console.error(error);
            });
    });

    function uploadFile(file) {
        var formData = new FormData();
        formData.append('file', file);

        return fetch('/Uploads/Upload', {
            method: 'POST',
            body: formData,
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .then(function (response) {
                if (!response.ok) {
                    return response.json().then(function (err) { throw new Error(err.error); });
                }
                return response.json();
            });
    }

    function sendMessage(bidId, message, userId, fileId, fileName, filePath) {
        return connection.invoke("SendMessage", parseInt(bidId), message, parseInt(userId), fileId, fileName, filePath)
            .catch(function (err) {
                console.error(err.toString());
                throw new Error('Ошибка отправки сообщения');
            });
    }

    window.deleteMessage = function (messageId) {
        if (confirm('Вы уверены, что хотите удалить сообщение?')) {
            connection.invoke("DeleteMessage", messageId, userId)
                .catch(err => {
                    console.error(err.toString());
                    showAlert('Ошибка удаления сообщения');
                });
        }
    };
});

function showAlert(message, type) {
    const alertHtml = `<div class="alert alert-${type} alert-chat"> ${message}</div>`;
    $('.chat__btn .btn').before(alertHtml);
    setTimeout(() => $('.alert').fadeOut().remove(), 3000);
}

function autoScroll() {
    $('.chat__window').animate({
        scrollTop: $('.chat__window')[0].scrollHeight
    }, 1000);
}