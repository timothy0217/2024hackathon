
// Handle the vaccine search
document.querySelector('form').addEventListener('submit', function(event) {
    event.preventDefault(); // Prevent the form from actually submitting

    const year = document.getElementById('year-select').value;
    const month = document.getElementById('month-select').value;

    if (!year || !month) {
        alert('請選擇年份和月份');
        return;
    }

    // Find the matching vaccine entry by date
    const entries = document.querySelectorAll('.vaccine-entry .date');
    let found = false;

    entries.forEach(entry => {
        const entryDate = entry.textContent.trim();
        const [entryYear, entryMonth] = entryDate.split('/');

        if (entryYear === year && entryMonth === month) {
            // Scroll to the matched entry
            entry.scrollIntoView({ behavior: 'smooth' });
            found = true;
        }
    });

    if (!found) {
        alert('未找到匹配的疫苗記錄');
    }
});

// Handle the vaccine search
document.querySelector('form').addEventListener('submit', function(event) {
    event.preventDefault(); // Prevent the form from actually submitting

    const year = document.getElementById('year-select').value;
    const month = document.getElementById('month-select').value;

    if (!year || !month) {
        alert('請選擇年份和月份');
        return;
    }

    // Find the matching vaccine entry by date
    const entries = document.querySelectorAll('.vaccine-entry .date');
    let found = false;

    entries.forEach(entry => {
        const entryDate = entry.textContent.trim();
        const [entryYear, entryMonth] = entryDate.split('/');

        if (entryYear === year && entryMonth === month) {
            // Scroll to the matched entry
            entry.scrollIntoView({ behavior: 'smooth' });
            found = true;
        }
    });

    if (!found) {
        alert('未找到匹配的疫苗記錄');
    }
});

// Preserve the active tab across actions using localStorage
function showPage(pageNumber) {
    const pageContent = document.getElementById('page-content');
    pageContent.innerHTML = '';

    if (pageNumber === 1) {
        loadPage('subsidy-form.html', pageContent);
    } else if (pageNumber === 2) {
        loadPage('vaccine-timeline.html', pageContent);
    }

    document.getElementById('tab-1').classList.remove('active');
    document.getElementById('tab-2').classList.remove('active');
    document.getElementById('tab-' + pageNumber).classList.add('active');

    // Store the active tab in localStorage
    localStorage.setItem('activeTab', pageNumber);
}

// Load the active tab on page load
document.addEventListener('DOMContentLoaded', () => {
    const savedTab = localStorage.getItem('activeTab');
    if (savedTab) {
        showPage(parseInt(savedTab));
    } else {
        showPage(1);  // Default to tab 1
    }
});


