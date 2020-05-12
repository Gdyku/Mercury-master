import { Map, GoogleApiWrapper, Marker, InfoWindow  } from 'google-maps-react';
import React, { useState } from 'react';
import { useDispatch } from "react-redux";
import "./MapForm.scss"; 

const mapStyles = {
    width: '100%',
    height: '100%',
};

const MapFrom = (props) => {
    const [location, setLocation] = useState({});

    const dispatch = useDispatch();

    const handleMapClick = (ref, map, ev) => {
        const location = ev.latLng;
        setLocation(location);
        map.panTo(location);
    };

    const handleSubmit = () => {

    };

    return (
        <form id="MapForm" onSubmit={handleSubmit}>
            <div className="mapContainer">
                <Map
                    google={props.google}
                    zoom={8}
                    style={mapStyles}
                    initialCenter={{ lat: 47.444, lng: -122.176}}
                    onClick={handleMapClick}
                >
                    <Marker
                        name={'New location'}
                        position={location}
                    />
                </Map>
            </div>
            <div className="d-flex justify-content-between mt-4">
                <button className="btn btn-primary">Add</button>
                <button className="btn btn-danger">Remove</button>
            </div>
        </form>
    );
}

export default GoogleApiWrapper({
    apiKey: 'AIzaSyAx0NDuObmAKZ2lYrBvqJbpLOm59A0c55I',
  })(MapFrom);