import React from 'react';
import TextField from '@material-ui/core/TextField';

const styles = {
    input: {
        width: "100%"
    }
}

export default props => {
    const onChange = (e) => {
        props.onChange({ name: e.target.value });
    }

    const { name, loading } = props;
    return (
        <TextField
            style={styles.input}
            disabled={loading}
            label=""
            value={name}
            margin="normal"
            variant="standard"
            onChange={onChange} 
        />
    )
}