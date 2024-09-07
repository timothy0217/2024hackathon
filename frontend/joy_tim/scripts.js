function showPage(pageNumber) {
    // 隱藏所有分頁
    document.getElementById('page-1').classList.add('hidden');
    document.getElementById('page-2').classList.add('hidden');

    // 顯示對應分頁
    document.getElementById('page-' + pageNumber).classList.remove('hidden');

    // 更新底部導航的活動狀態
    document.getElementById('tab-1').classList.remove('active');
    document.getElementById('tab-2').classList.remove('active');
    document.getElementById('tab-' + pageNumber).classList.add('active');
}

document.getElementById("queryForm").addEventListener("submit", function(e) {
    e.preventDefault();
    document.getElementById("queryResults").classList.remove("hidden");
});
