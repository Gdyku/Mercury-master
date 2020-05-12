import React, {useState} from 'react';
import ScheduleForm from '../scheduleForm/ScheduleForm'
import MarkerForm from '../markerForm/MarkerForm'

const NewScheduleContainer = () => {
    const [step, setStep] = useState(1);

    const callback = () => {
        setStep(step + 1);
    };

    return (
        <div>
            <h1>{step}</h1>
            <div className={`${step == 1 ? "" : "hidden"}`}>
                <ScheduleForm callback={callback}/>
            </div>
            <div className={`${step == 2 ? "" : "hidden"}`}>
                <MarkerForm/>
            </div>
        </div>
    );
}

export default NewScheduleContainer;