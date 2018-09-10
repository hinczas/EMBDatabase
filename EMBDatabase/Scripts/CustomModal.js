function myFunction(event) {
    var imagesrc = '#' + event.target.id;
    $('#imagepreview').attr('src', $(imagesrc).attr('src')); // here asign the image to the modal when the user click the enlarge link
    $('#imagemodal').modal('show');
}