// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function scrollToBottom() {
    window.scrollTo(0, document.body.scrollHeight);
}
function scrollToTop() {
    window.scrollTo(0, 0);
}

function scrollToBottomSmoothly() {
    const targetPosition = document.body.scrollHeight;
    const startPosition = window.pageYOffset;
    const duration = 750; // Adjust duration in milliseconds

    let start = null;

    const easeInOutQuad = (t, b, c, d) => {
        t /= d / 2;
        if (t < 1) return c / 2 * t * t + b;
        t--;
        return -c / 2 * (t * (t - 2) - 1) + b;
    };

    const animation = (currentTime) => {
        if (start === null) start = currentTime;
        const elapsed = currentTime - start;
        if (elapsed >= duration) {
            window.scrollTo(0, targetPosition);
            return;
        }
        const progress = easeInOutQuad(elapsed, startPosition, targetPosition - startPosition, duration);
        window.scrollTo(0, progress);
        requestAnimationFrame(animation);
    };

    requestAnimationFrame(animation);
}