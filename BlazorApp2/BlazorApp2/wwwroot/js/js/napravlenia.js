let slideIndex = 0;
        
function showSlides() {
    const slides = document.querySelectorAll('.slide');
    
    // Скрыть все слайды
    slides.forEach(slide => slide.classList.remove('active'));
    
    // Перейти к следующему слайду
    slideIndex++;
    
    // Вернуться к первому слайду после последнего
    if (slideIndex > slides.length) {
        slideIndex = 1;
    }
    
    // Показать текущий слайд
    slides[slideIndex - 1].classList.add('active');
    
    // Повторять каждые 10 секунд
    setTimeout(showSlides, 5000);
}

// Запустить слайд-шоу при загрузке страницы
showSlides();