var countryApp = countryApp || {
    Constants: {
        BaseUrl: 'https://localhost:7031/'
    },
    Message: new function () {
        let imageUrl = "/images/logos/logo-hexagon.png";
        let width = '40%';
        var calculateSwalWidth = function () {
            let windowWidth = $(window).width();
            if (windowWidth < 800 && windowWidth > 650) {
                width = '30%';
            }
            if (windowWidth < 650) {
                width = '40%';
            }
        };

        this.Loading = function (data) {
            calculateSwalWidth();
            var lang = document.cookie.indexOf("CurrentLanguage=ES") != -1;

            let modalTitle = data && data.title ? data.title : "";
            let modalMessage = data && data.message
                ? "<h4>" + data.message + "</h4>"
                : lang
                    ? "<h5>procesando solicitud...</h5>"
                    : "<h5>processing request...</h5>";

            let modalTimer = data && data.duration && data.duration > 0 ? data.duration : 0;

            Swal.fire({
                title: modalTitle,
                imageUrl: countryApp.Constants.BaseUrl + imageUrl,
                html: modalMessage,
                width: width,
                showCloseButton: false,
                showCancelButton: false,
                showConfirmButton: false,
                onOpen: () => {
                    Swal.showLoading();
                },
                allowEscapeKey: false,
                allowOutsideClick: false,
                timer: modalTimer
            });
        };

        this.Close = function () {
            Swal.close();
        };
    }
};

(function () {
    var showloader = function () {

        var loader = document.querySelector('.loader');
        var overlay = document.getElementById('overlayer');

        function fadeOut(el) {
            el.style.opacity = 1;
            (function fade() {
                if ((el.style.opacity -= .1) < 0) {
                    el.style.display = "none";
                } else {
                    requestAnimationFrame(fade);
                }
            })();
        };

        setTimeout(function () {
            fadeOut(loader);
            fadeOut(overlay);
        }, 200);
    };
    showloader();
})();

function createVueInstance(countryList)
{
    const app = Vue.createApp({
        data() {
            return {
                countries: JSON.parse(countryList),
                filteredCountries:null,
                pagination: {
                    countryCount: 0,
                    itemsPerPage: 21,
                    pageRange: 2,
                    marginPages: 1,
                    currentPage: 1
                },
                details: {
                    country:null,
                    region: {
                        name: '',
                        subRegions: null,
                        population: 0,
                        countries: null
                    },
                    subRegion: {
                        name: '',
                        region: '',
                        population: 0,
                        countries: null
                    },
                },
                search: ''
            }
        },
        mounted: function () {
            this.filteredCountries = this.countries;
        },
        methods: {
            countryDetails: function (code) {
                this.details.country = null;
                const countryInfo = this.countries.filter(function (item) { return item.code == code });
                if (countryInfo.length > 0)
                    this.details.country = countryInfo[0];

                if (this.details.country != null)
                    $('#countryDetailModal').modal('show');
            },
            regionDetails: function(name) {

                const countries = this.countries.filter(function (item) { return item.region == name });
                if (countries.length > 0) {

                    const totalPopulation = countries.reduce(function (total, currentValue) {
                        return total + currentValue.population;
                    }, 0);

                    const subRegions = countries.map(item => item.subRegion).filter((value, index, self) => self.indexOf(value) === index && value != '' && value != null)

                    this.details.region.name = name;
                    this.details.region.subRegions = subRegions;
                    this.details.region.population = totalPopulation;
                    this.details.region.countries = countries;
                   
                    if (this.details.region.countries != null)
                        $('#regionDetailModal').modal('show');
                }
            },
            subregionDetails: function(name) {
                const countries = this.countries.filter(function (item) { return item.subRegion == name });
                if (countries.length > 0) {

                    const totalPopulation = countries.reduce(function (total, currentValue) {
                        return total + currentValue.population;
                    }, 0);

                    this.details.subRegion.name = name;
                    this.details.subRegion.region = countries[0].region;
                    this.details.subRegion.population = totalPopulation;
                    this.details.subRegion.countries = countries;

                    if (this.details.subRegion.countries != null)
                        $('#subRegionDetailModal').modal('show');
                }
            },
            clickCallback: function (pageNum) {
                this.pagination.currentPage = Number(pageNum);
            }
        },
        computed: {
            getCountries: function () {

                if (this.search != '') {
                    this.filteredCountries = this.countries
                        .filter(
                            (entry) => this.countries.length
                                ? Object.keys(this.countries[0])
                                    .some(key => ('' + entry[key]).toLowerCase().includes(this.search.toLowerCase()))
                                : true
                        );

                    this.pagination.currentPage = 1;
                }

                if (this.search == '' || this.filteredCountries == null)
                    this.filteredCountries = this.countries;

                let current = this.pagination.currentPage * this.pagination.itemsPerPage;
                let start = current - this.pagination.itemsPerPage;
                var output = this.filteredCountries.slice(start, current);
                 
                return output;
            },
            getPageCount: function () {
                return Math.ceil(this.filteredCountries.length / this.pagination.itemsPerPage);
            }
        },
        components: {
            paginate: VuejsPaginateNext,
        },
    }).use("VuejsPaginateNext");

    app.mount("#app");
}

$(document).on('click', '.loading', function () {
    countryApp.Message.Loading();
});
