/*
Author: Duncan Levings
 */

var vars = {};
var map;

var Controller = function() {
    var controller = {
        self: null,
        initialize: function() {
            self = this;


            vars.lat = 0;
            vars.long = 0;
            //for testing
            vars.lat = 43.467189;
            vars.long = -79.699722;
            vars.email = $("#email");
            vars.emailC = $("#emailNew");
            vars.password = $("#password");
            vars.passwordC = $("#passwordNew");
            vars.loggedID = null;
            vars.pop = $("#pop");
            vars.popC = $("#popCreate");
            vars.tab = $("#content");
            vars.ui_btn = $('.ui-btn');
            vars.distance = 5;
            vars.price = 15;
            vars.paid_btn = $('#paid-button');
            vars.free_btn = $('#free-button');
            vars.marker = null;
            vars.myMarker = null;

            $("#image").attr("src", localStorage.getItem("img"));

            self.initalizeDatabase();
            self.bindEvents();

            $("#parking").on("pageshow", function() {
                self.initializeMap();
                self.renderPaidView();
            });

            $("#settings").on("pageshow", function() {
                self.initalizeSettings();
            });
        },

        initalizeDatabase: function() {
            var config = {
                apiKey: "AIzaSyC1sXhTcW9hAPTApNWq0fg_HX7DxTQN0GQ",
                authDomain: "parkingapp-1554153473424.firebaseapp.com",
                databaseURL: "https://parkingapp-1554153473424.firebaseio.com",
                projectId: "parkingapp-1554153473424",
                storageBucket: "parkingapp-1554153473424.appspot.com",
                messagingSenderId: "974903325003"
            };
            firebase.initializeApp(config);
            vars.db = firebase.firestore();
        },

        bindEvents: function() {
            $("#loginBtn").on('click', function() {
                self.validateLogin();
            });

            $("#createAccBtn").on('click', function() {
                self.validateCreate();
            });

            $("#saveSetting").on('click', function() {
                self.saveSettings();
            });

            $("#captureImage").on('click', function() {
                self.captureImage();
            });

            $("#free-button").on('click', function() {
                if (vars.free_btn.hasClass('active')) {
                    return;
                }
                self.renderFreeView();
            });

            $("#paid-button").on('click', function() {
                if (vars.paid_btn.hasClass('active')) {
                    return;
                }
                self.renderPaidView();
            });
        },

        validateLogin: function() {
            var email = vars.email.val().trim().toLowerCase();
            var pass = vars.password.val().trim();

            if (self.validateDataLogin(email, pass)) {
                vars.db.collection("users").where("email", "==", email).where("password", "==", pass)
                    .get()
                    .then(function(querySnapshot) {
                        if (querySnapshot.size < 1){
                            navigator.notification.alert("Incorrect Email or Password!", null, "ERROR", "Ok");
                        }
                        
                        querySnapshot.forEach(function(doc) {
                            navigator.notification.alert("Logging in...", null, "Notice", "Ok");
                            vars.loggedID = doc.id;
                            vars.distance = doc.data().distance;
                            vars.price = doc.data().price;
                            $.mobile.changePage( "#startup", { transition: "slideup"});
                        });
                    })
                    .catch(function(error) {
                        return false;
                    });
            }
        },

        validateCreate: function() {
            var email = vars.emailC.val().trim().toLowerCase();
            var pass = vars.passwordC.val().trim();

            if (self.validateDataCreate(email, pass)) {
                vars.db.collection("users").where("email", "==", email)
                    .get()
                    .then(function(querySnapshot) {
                        if (querySnapshot.size < 1)
                        {
                            vars.db.collection("users").add({
                                email: email,
                                password: pass,
                                distance: 5,
                                price: 15
                            })
                                .then(function(docRef) {
                                    navigator.notification.alert("Account successfully created!", null, "Account", "Ok");
                                    $.mobile.changePage( "#login", { transition: "slideup"});
                                })
                                .catch(function(error) {
                                    navigator.notification.alert("Something went wrong!", null, "ERROR", "Ok");
                                });
                        }
                        else
                        {
                            navigator.notification.alert("Account already exists!", null, "ERROR", "Ok");
                        }
                    })
                    .catch(function(error) {
                        return false;
                    });
            }
        },

        initalizeSettings: function() {
            $("#distance").val(vars.distance).slider().slider("refresh");
            $("#price").val(vars.price).slider().slider("refresh");
        },

        saveSettings: function() {
            var ref = vars.db.collection("users").doc(vars.loggedID);

            ref.get().then(function(doc) {
                if (doc.exists) {
                    var newDist = $("#distance").val();
                    var newPrice = $("#price").val();

                    return ref.update({
                        distance: newDist,
                        price: newPrice
                    })
                    .then(function() {
                        vars.distance = newDist;
                        vars.price = newPrice;

                        navigator.notification.alert("Settings successfully saved!", null, "Settings", "Ok");
                        $.mobile.changePage( "#startup", { transition: "slideup", changeHash: false });
                    })
                    .catch(function(error) {
                        navigator.notification.alert("Something went wrong!", null, "ERROR", "Ok");
                    });
                }
            })
            .catch(function(error) {
                return false;
            });
        },

        captureImage: function() {
            var options = { quality: 25,
                destinationType: Camera.DestinationType.FILE_URI,
                cameraDirection: Camera.Direction.FRONT,
                encodingType: Camera.EncodingType.JPEG,
                correctOrientation: true,
                allowEdit: true
            };

            navigator.camera.getPicture(function cameraSuccess(imageData) {
                navigator.notification.alert("Image saved successfully!", null, "Photo Results", "Ok");
                localStorage.clear();
                localStorage.setItem("img" ,imageData);

                $("#image").attr("src", imageData);
            }, function cameraError(errorData) {
                navigator.notification.alert("Error: " + JSON.stringify(errorData), null, "Camera Error", "Ok");

            }, options);
        },

        initializeMap: function() {
            var loc = new google.maps.LatLng(vars.lat, vars.long);

            if (map == null) {
                map = new google.maps.Map(
                    document.getElementById("map"), {center: loc, zoom: 15});
            }

            map.setCenter(loc);
        },

        renderPaidView: function() {
            vars.ui_btn.removeClass('active');
            vars.paid_btn.addClass('active');

            vars.tab.empty();
            $("#content").load("paid-view.html", function(data) {
                navigator.geolocation.getCurrentPosition(self.onLocSuccess, self.onLocError);

                var cur_date = new Date();
                var cur_month = cur_date.getMonth() + 1;
                var cur_day = cur_date.getDate();
                var date = cur_date.getFullYear() + '-' + (cur_month < 10 ? '0' : '') + cur_month + '-' + (cur_day < 10 ? '0' : '') + cur_day;
                var start_date = date + 'T' + cur_date.getHours() + ':00';
                cur_date.setHours(cur_date.getHours() + 6);
                var end_date = date + 'T' + cur_date.getHours() + ':00';

                self.queryPaidData(start_date, end_date);
            });
        },

        renderFreeView: function() {
            vars.ui_btn.removeClass('active');
            vars.free_btn.addClass('active');

            vars.tab.empty();

            $("#content").load("free-view.html", function(data) {
                navigator.geolocation.getCurrentPosition(self.onLocSuccess, self.onLocError);

                self.queryFreeData();
            });
        },

        onLocSuccess: function(position) {
            //vars.lat = position.coords.latitude;
            //vars.long = position.coords.longitude;
        },

        onLocError: function() {
            navigator.notification.alert("Error finding your location!", null, "ERROR", "Ok");
        },

        queryPaidData: function(start_date, end_date) {
            var url = [];
            url.push(
                "https://api.parkwhiz.com/v4/quotes/?q=coordinates:",
                vars.lat,
                ',',
                vars.long,
                "%20distance:",
                vars.distance,
                "&start_time=",
                start_date,
                "&end_time=",
                end_date,
                "&api_key=62d882d8cfe5680004fa849286b6ce20?sort=distance:asc"
            )

            $.get(url.join(""), function(result) {
                var quotes = $("#quote_list");
                var locInfo, locInfoPay,cords, div, locName, locDetails, locImage, locPay;

                var resultLength = result.length;
                var names = new Array(resultLength);
                for (var i = 0; i < resultLength; i++){
                    locInfo = result[i]._embedded['pw:location'];
                    locInfoPay = result[i].purchase_options[0];
                    if (locInfoPay) {
                        if (!names.includes(locInfo.address1))  {
                            if (locInfoPay.price.CAD > vars.price) {
                                continue;
                            }

                            cords = locInfo.entrances[0].coordinates;

                            div = $("<div data-role='collapsible' data-iconpos='right' " +
                                "data-lat='" + cords[0] + "' data-long='" + cords[1] + "'></div>");
                            div.addClass("item");

                            locName = $("<h2>" + locInfo.address1 + "</h2>");
                            locName.appendTo(div);

                            locDetails = $("<div class='ui-grid-a'></div>");

                            locImage = $("<div class=ui-block-a><img class='thumbImg' src="
                                + locInfo.photos[0].sizes.gallery_thumb.URL + "></div>");
                            locImage.appendTo(locDetails);

                            locPay = $("<div class=ui-block-b><h3>Price: $" + locInfoPay.price.CAD + "<br> Open: " +
                                locInfoPay.space_availability.status + "</h3>" +
                                "<button class='ui-btn direction'>Drive Here</button></div>")
                            locPay.appendTo(locDetails);

                            locDetails.appendTo(div);

                            div.clone().appendTo(quotes);

                            names[i] = locInfo.address1;
                        }
                    }
                }

                var loc = new google.maps.LatLng(vars.lat, vars.long);
                map.setCenter(loc);

                if (vars.myMarker) {
                    vars.myMarker.setMap(null);
                }

                vars.myMarker = new google.maps.Marker({
                    map: map,
                    position: loc,
                    icon: {
                        url: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
                    }
                });

                vars.tab.enhanceWithin();
                self.changeMarker();
                self.startNavigation();
            });
        },

        queryFreeData: function() {
            var service;
            var loc = new google.maps.LatLng(vars.lat, vars.long);

            var request = {
                location: loc,
                radius: vars.distance * 1000,
                rankby: google.maps.places.RankBy.DISTANCE,
                type: ['parking'],
                fields: ['photos, name, opening_hours, price_level']
            };

            if (vars.myMarker) {
                vars.myMarker.setMap(null);
            }

            vars.myMarker = new google.maps.Marker({
                map: map,
                position: loc,
                icon: {
                    url: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
                }
            });

            service = new google.maps.places.PlacesService(map);
            service.nearbySearch(request, self.callback);
        },

        callback: function(results, status) {
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                var free = $("#free_list");
                var div, locName, locDetails, locImage, image, locExtraInfo, open;

                var resultLength = results.length;
                for (var i = 0; i < resultLength; i++) {
                    div = $("<div data-role='collapsible' data-iconpos='right' " +
                        "data-lat='" + results[i].geometry.location.lat() + "' data-long='" + results[i].geometry.location.lng() + "'></div>");
                    div.addClass("item");

                    locName = $("<h2>" + results[i].name + "</h2>");
                    locName.appendTo(div);

                    locDetails = $("<div class='ui-grid-a'></div>");

                    image = "https://s3.amazonaws.com/pastperfectonline/museumlogos/missingimages/416/original/noimageavailable.png?1466201775";

                    if (results[i].photos) {
                        image = results[i].photos[0].getUrl();
                    }

                    locImage = $("<div class=ui-block-a><img class='thumbImg' src=" + image + "></div>");
                    locImage.appendTo(locDetails);

                    open = "UNKNOWN";
                    
                    if (results[i].opening_hours) {
                        if (results[i].opening_hours.open_now) {
                            open = "AVALIABLE";
                        }
                        else {
                            open = "CLOSED";
                        }
                    }

                    locExtraInfo = $("<div class=ui-block-b><h3>Open: " + open + "</h3>" +
                        "<button class='ui-btn direction'>Drive Here</button></div>")
                    locExtraInfo.appendTo(locDetails);

                    locDetails.appendTo(div);

                    div.clone().appendTo(free);
                }

                vars.tab.enhanceWithin();
                self.changeMarker();
                self.startNavigation();
            }
        },

        validateDataLogin: function(email, password) {
            var errMsg="";

            if (email.length < 1) {
                errMsg +="Email cannot be empty!<br />";
            }
            if (password.length < 1) {
                errMsg +="Password cannot be empty!<br />";
            }

            if (errMsg.length>0) {
                self.showPopup(errMsg);
                return false;
            }

            return true;
        },

        validateDataCreate: function(email, password) {
            var errMsg="";

            if (email.length < 1) {
                errMsg +="Email cannot be empty!<br />";
            }
            if (password.length < 1) {
                errMsg +="Password cannot be empty!<br />";
            }

            if (errMsg.length>0) {
                self.showPopupCreate(errMsg);
                return false;
            }
            return true;
        },

        changeMarker: function() {
            $('.item').bind("collapsibleexpand", function(e) {
                var loc = new google.maps.LatLng($(this).attr("data-lat"), $(this).attr("data-long"));
                map.setCenter(loc);
                map.setZoom(15);

                if (vars.marker) {
                    vars.marker.setMap(null);
                }

                vars.marker = new google.maps.Marker({
                    map: map,
                    position: loc
                });
            });
        },

        startNavigation: function() {
            $('.direction').on("click", function() {
                navigator.notification.alert("Starting Navigation...", null, "Google maps", "Ok");
            });
        },

        showPopup: function(message) {
            vars.pop.html('<p>'+message+'</p>').popup("open");
            setTimeout(function(){  vars.pop.popup("close"); }, 1500);
        },

        showPopupCreate: function(message) {
            vars.popC.html('<p>'+message+'</p>').popup("open");
            setTimeout(function(){  vars.popC.popup("close"); }, 1500);
        }
    }
    controller.initialize();
    return controller;
}
