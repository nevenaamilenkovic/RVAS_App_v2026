// glasanje na postovima (upvote/downvote) bez osvezavanja stranice
//koristi se ajax(asynchronous javascript and xml)
//browser salje zahtev u pozadini ka serveru i prima odgovor od istog
//samim tim se ne vrsi osvezavanje stranice, tj korisnik ostaje na istoj stranici
//bez punog refresa cele stranice

//assinhrono-> zahtev ide u poyadini, stranica je i dalje aktivna, a kada stigne odgovor,
//js azurira "samo deo" stranice
(function () {
    //token koji se salje u telu zahteva, metoda Glasaj je post!!!
    function getAntiForgeryToken() {
        var input = document.querySelector('input[name="__RequestVerificationToken"]');
        return input ? input.value : '';
    }

    //menja izgled dugmeta za glasanje nekon sto server brati novi rez
    function setButtonState(btn, active, type) {
        btn.setAttribute('aria-pressed', active ? 'true' : 'false');
        if (type === 'up') {
            btn.classList.toggle('btn-success', active);
            btn.classList.toggle('btn-outline-success', !active);
        } else {
            btn.classList.toggle('btn-danger', active);
            btn.classList.toggle('btn-outline-danger', !active);
        }
    }

    //azurira prikaz glasova za jedan post na stranici i koristi odgovor sa servera
    //json! iz metode Glasaj (blog kontroler)
    function updateVoteUi(container, data) {
        var upBtn = container.querySelector('.post-glas-up');
        var downBtn = container.querySelector('.post-glas-down');
        var upCount = container.querySelector('.post-upvote-count');
        var downCount = container.querySelector('.post-downvote-count');

        if (upCount) upCount.textContent = data.upvotes;
        if (downCount) downCount.textContent = data.downvotes;

        setButtonState(upBtn, data.userVote === 'up', 'up');
        setButtonState(downBtn, data.userVote === 'down', 'down');
    }
    //tzv event delegation
    //ajax "slusa/osluskojue" svaki klik bilo gde na stranici aa ako je klik na dugme
    //ya glas, slje yahtev serveru itd..pa azurira prikaz
    document.addEventListener('click', function (e) {
        //closest trazi dugme, ako klik nije za glasanje nista se ne desava
        var btn = e.target.closest('.post-glas-btn');
        if (!btn) return;
        //button type button obicno ne radi nista ali ovde sprecavamo neko potencijalno nezeljeno ponasanje
        e.preventDefault();
        var container = btn.closest('.post-glasovi');
        if (!container) return;

        //kojii post dobija koji tip glasa
        //container->blok glasanja za jedan post
        //postId je Id posta iz htmla tj viewa
        var postId = container.getAttribute('data-post-id');
        //true za up , false za down
        var isUpvote = btn.getAttribute('data-vote') === 'up';
        var token = getAntiForgeryToken();

        btn.disabled = true;//dok traje req korisnik ne moze duplo da klikne
        var sibling = container.querySelector('.post-glas-btn:not([disabled])');
        if (sibling && sibling !== btn) sibling.disabled = true;

        var body = new URLSearchParams();
        body.append('postId', postId);
        body.append('isUpvote', isUpvote);
        body.append('__RequestVerificationToken', token);//zastita od laznih requestova


        //slanje ajax zahteva
        //post na metodu glasaj u kontroleru
        //telo sadrzi postid i token, kao obican submit za formu samo sto sve ide u pozadini
        fetch('/Blog/Glasaj', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': token
            },
            body: body.toString()
        })
        //ako server vrati gresku baca gresku i parsira json
            .then(function (response) {
                if (!response.ok) throw new Error('Glasanje nije uspelo');
                return response.json();
            })
            //po glasanju novo stanje glasova druga boja dugmeta
            .then(function (data) {
                updateVoteUi(container, data);
            })
            //ako nesto pukne ide window alert korisniku
            .catch(function () {
                alert('Glasanje nije uspelo. Pokusajte ponovo.');
            })
            //ukljucuje fugmad
            .finally(function () {
                container.querySelectorAll('.post-glas-btn').forEach(function (b) {
                    b.disabled = false;
                });
            });
    });

    //termin 10(3s2b)/11(3s2a) zadatak sa vezbi isti AJAX obrazac kao za postove
    function updateKomentarVoteUi(container, data) {
        var upBtn = container.querySelector('.komentar-glas-up');
        var downBtn = container.querySelector('.komentar-glas-down');
        var upCount = container.querySelector('.komentar-upvote-count');
        var downCount = container.querySelector('.komentar-downvote-count');

        if (upCount) upCount.textContent = data.upvotes;
        if (downCount) downCount.textContent = data.downvotes;

        setButtonState(upBtn, data.userVote === 'up', 'up');
        setButtonState(downBtn, data.userVote === 'down', 'down');
    }

    document.addEventListener('click', function (e) {
        var btn = e.target.closest('.komentar-glas-btn');
        if (!btn) return;

        e.preventDefault();
        var container = btn.closest('.komentar-glasovi');
        if (!container) return;

        var komentarId = container.getAttribute('data-komentar-id');
        var isUpvote = btn.getAttribute('data-vote') === 'up';
        var token = getAntiForgeryToken();

        btn.disabled = true;
        var sibling = container.querySelector('.komentar-glas-btn:not([disabled])');
        if (sibling && sibling !== btn) sibling.disabled = true;

        var body = new URLSearchParams();
        body.append('komentarId', komentarId);
        body.append('isUpvote', isUpvote);
        body.append('__RequestVerificationToken', token);

        fetch('/Blog/GlasajNaKomentar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': token
            },
            body: body.toString()
        })
            .then(function (response) {
                if (!response.ok) throw new Error('Glasanje nije uspelo');
                return response.json();
            })
            .then(function (data) {
                updateKomentarVoteUi(container, data);
            })
            .catch(function () {
                alert('Glasanje na komentaru nije uspelo. Pokusajte ponovo.');
            })
            .finally(function () {
                container.querySelectorAll('.komentar-glas-btn').forEach(function (b) {
                    b.disabled = false;
                });
            });
    });
})();
