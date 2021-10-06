$(document).ready(() => {
    // Localization
    $('.dropdown-item-lang').click(function (e) {
        e.preventDefault();

        $.post([
            location.origin,
            'Home',
            'SetCulture'
        ].join('/'),
            {
                culture: $(this).data('lang')
            },
            () => location.reload()
        );
    });
});