function printDiv(divId) {
    var printContents = document.getElementById(divId).innerHTML;
    var printWindow = window.open('', '', 'width=800,height=600');

    printWindow.document.write(`
        <html>
        <head>
            <title>Print Preview</title>
            <style>
                body { font-family: Arial, sans-serif; margin: 20px; }
                h3 { text-align: center; font-size: 20px; margin-bottom: 20px; }
                table { width: 100%; border-collapse: collapse; }
                th, td { border: 1px solid #000; padding: 8px; text-align: left; }
                th { background-color: #f4f4f4; }
                tr:nth-child(even) { background-color: #f9f9f9; }
                tr:hover { background-color: #ddd; }
                .container { padding: 20px; max-width: 800px; margin: auto; }
                @media print {
                    body { margin: 0; padding: 0; }
                    .no-print { display: none; }
                }
            </style>
        </head>
        <body>
            <div class="container">
                ${printContents}
            </div>
        </body>
        </html>
    `);

    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
}
