// tour click
$('.tour-container').click(function () {
    // get id of current tour
    let tourId = $(this).children().first().val();
    console.log('tourId: ', tourId);

    location.href = [
        location.origin,
        'Tour',
        'ShowTour'
    ].join('/') + `?tourId=${tourId}`;
});