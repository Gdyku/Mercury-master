import React, { useState } from 'react';
import MapForm from '../map-form/MapForm'
import "./ScheduleContainer.scss";
import TextField from '@material-ui/core/TextField';

const ScheduleContainer = () => {
    const [step, setStep] = useState(1);

    return (
        <section>
            <TextField required id="standard-required" label="Title" />
            <div id="s-container" className="d-flex flex-row mt-4">
                <article className="pr-2">
                    <MapForm />
                </article>
                <article className="pl-2">
                    <div class="form-group">
                        <TextField required id="standard-required" label="Description" multiline/>
                    </div>
                </article>
            </div>
        </section>
    );
}

export default ScheduleContainer;