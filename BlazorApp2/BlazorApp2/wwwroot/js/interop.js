window.setTokenExpirationTimer = function (minutes) {
    const expirationTime = Date.now() + minutes * 60 * 1000; // Время истечения
    localStorage.setItem('jwt-token-expiration', expirationTime); // Сохраняем время истечения

    setTimeout(() => {
        localStorage.removeItem('jwt-token'); // Удаляем токен
        localStorage.removeItem('jwt-token-expiration'); // Удаляем время истечения
        console.log('Токен удален из localStorage');
    }, minutes * 60 * 1000); // Конвертируем минуты в миллисекунды
};

// Проверка истечения токена при загрузке страницы
window.checkTokenExpiration = function () {
    const expirationTime = localStorage.getItem('jwt-token-expiration');
    if (expirationTime && Date.now() > parseInt(expirationTime)) {
        localStorage.removeItem('jwt-token'); // Удаляем токен
        localStorage.removeItem('jwt-token-expiration'); // Удаляем время истечения
        console.log('Токен удален из localStorage (проверка при загрузке)');
    }
};