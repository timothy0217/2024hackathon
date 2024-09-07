function showPage(pageNumber) {
    const pageContent = document.getElementById('page-content');

    // Clear the content first
    pageContent.innerHTML = '';

    if (pageNumber === 1) {
        loadPage('subsidy-form.html', pageContent);
    } else if (pageNumber === 2) {
        loadPage('vaccine-timeline.html', pageContent);
    }

    // Update active tab
    document.getElementById('tab-1').classList.remove('active');
    document.getElementById('tab-2').classList.remove('active');
    document.getElementById('tab-' + pageNumber).classList.add('active');
}

// Reusable function to load content from a file using Fetch API
function loadPage(page, container) {
    fetch(page)
        .then(response => response.text())
        .then(data => {
            container.innerHTML = data;
        })
        .catch(error => console.error('Error loading page:', error));
}

// Load the first tab by default
document.addEventListener('DOMContentLoaded', () => {
    showPage(1);
});
