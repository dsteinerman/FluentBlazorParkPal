window.applyTheme = function (theme) {
    console.log('Applying theme:', theme);
    if (theme === 'dark') {
        document.body.classList.add('dark-mode');
    } else {
        document.body.classList.remove('dark-mode');
    }
}

