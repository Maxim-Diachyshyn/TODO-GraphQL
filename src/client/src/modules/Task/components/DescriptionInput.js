import React from 'react';
import TextField from '@material-ui/core/TextField';

const styles = {
    input: {
        width: "100%"
    }
};

const texts = {
    label: "Description"
}

export default props => {
    const { description, loading } = props;
    
    const onChange = e => {
        props.onChange({ description: e.target.value });
    }

    return (
        <TextField
            style={styles.input}
            disabled={loading}
            label={texts.label}
            value={description}
            margin="normal"
            variant="filled"
            multiline={true}
            rows={10}
            onChange={onChange} 
        />
    )}