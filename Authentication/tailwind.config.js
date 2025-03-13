/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Components/**/*.{razor,cshtml}",
        "./Pages/**/*.{razor,cshtml}",
        "../Authentication.Client/Pages/**/*.{razor,cshtml}",
        "../Authentication.Client/Pages/*.{razor,cshtml}",
        "../Authentication.Client/ReusableComponent/**/*.{razor,cshtml}",
        "../Authentication.Client/Pages/ReceiptsRegistration/**/*.{razor,cshtml}",
        "../Authentication.Client/Pages/ReceiptsRegistration/CashReceiptAndWithdrawal/**/*.{razor,cshtml}",
        "../Authentication.Client/Pages/ReceiptsRegistration/CashBuyAndSell/**/*.{razor,cshtml}",
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
