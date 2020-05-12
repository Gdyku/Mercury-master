import { Map, GoogleApiWrapper, Marker, InfoWindow  } from 'google-maps-react';
import React, { useState } from 'react';
import { useDispatch } from "react-redux";
import "./MarkerForm.scss";
import { addMarkerPending } from '../../../actions';

const mapStyles = {
    width: '100%',
    height: '100%',
};

const MarkerFrom = (props) => {
    const [location, setLocation] = useState({});
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");

    const dispatch = useDispatch();

    const handleMapClick = (ref, map, ev) => {
        const location = ev.latLng;
        setLocation(location);
        map.panTo(location);
    };

    const handleSubmit = event => {
        event.preventDefault();
        dispatch(addMarkerPending({ }));

        if(props.callback) {
            props.callback();
        }
    }

    return (
        <form id="MarkerForm" onSubmit={handleSubmit}>
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
            <div className="form-group">
                <label htmlFor="title">Title</label>
                <input name="name" className="form-control" placeholder="Title" type="text" id="title" 
                    value={name} onChange={e => setName(e.target.value)}/>
            </div>
            <div className="form-group">
                <label htmlFor="description">Description</label>
                <textarea name="description" type="text" id="description" className="form-control" placeholder="Description"
                     value={description} onChange={e => setDescription(e.target.value)}/>
            </div>
            <div>
                <button className="btn btn-primary">End</button>
                <button className="btn btn-primary" type="submit">Save</button>
            </div>
        </form>
    );
}

export default GoogleApiWrapper({
    apiKey: 'AIzaSyAx0NDuObmAKZ2lYrBvqJbpLOm59A0c55I',
  })(MarkerFrom);