// 彈窗功能模組化
function setupModal() {
    const openModalButton = document.getElementById('open-modal');
    const closeModalButton = document.getElementById('close-modal');
    const modal = document.getElementById('modal');

    if (openModalButton && closeModalButton && modal) {
        openModalButton.addEventListener('click', function() {
            modal.style.display = 'block';
        });

        closeModalButton.addEventListener('click', function() {
            modal.style.display = 'none';
        });

        window.addEventListener('click', function(event) {
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        });
    } else {
        console.error('缺少彈窗相關的元素');
    }
}

// 處理疫苗搜尋功能
function setupVaccineSearch(formSelector, entrySelector) {
    document.querySelector(formSelector).addEventListener('submit', function(event) {
        event.preventDefault(); // 防止表單實際提交

        const year = document.getElementById('year-select').value;
        const month = document.getElementById('month-select').value;

        if (!year || !month) {
            alert('請選擇年份和月份');
            return;
        }

        // 查找匹配的疫苗條目
        const entries = document.querySelectorAll(entrySelector);
        let found = false;

        entries.forEach(entry => {
            const entryDate = entry.textContent.trim();
            const [entryYear, entryMonth] = entryDate.split('/');

            if (entryYear === year && entryMonth === month) {
                // 滾動到匹配的條目
                entry.scrollIntoView({ behavior: 'smooth' });
                found = true;
            }
        });

        if (!found) {
            alert('未找到匹配的疫苗記錄');
        }
    });
}

// 設置頁面功能
function setupPage() {
    // 設置彈窗
    setupModal();

    // 設置疫苗搜尋功能
    setupVaccineSearch('form#vaccine-search-form-1', '.vaccine-entry-1 .date');
    setupVaccineSearch('form#vaccine-search-form-2', '.vaccine-entry-2 .date');

    // 記住活躍的選項卡
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

        // 存儲活躍的選項卡到 localStorage
        localStorage.setItem('activeTab', pageNumber);
    }

    // 載入活躍的選項卡
    document.addEventListener('DOMContentLoaded', () => {
        const savedTab = localStorage.getItem('activeTab');
        if (savedTab) {
            showPage(parseInt(savedTab));
        } else {
            showPage(1);  // 默認顯示第1頁
        }
    });
}

// 等待 DOM 內容加載完成後執行
document.addEventListener('DOMContentLoaded', setupPage);
