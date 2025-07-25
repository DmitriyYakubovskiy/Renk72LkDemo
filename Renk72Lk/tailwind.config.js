/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Views/**/*.cshtml',
        './Pages/**/*.cshtml',
        './**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                page: '#f8f9fa',
            }
        },
    },
    plugins: [],
}