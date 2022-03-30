<script>
import { createEventDispatcher } from 'svelte';

    import { createFieldValidator } from './validation';
    import { uriValidator, requiredValidator } from './validators.js';
    
    let uri = "";

    const api = AMPED_API_URL || 'http://localhost:5001/api';

    const dispatch = createEventDispatcher();

    async function createBookmark() {
        const response = await fetch(api + '/Bookmark/create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ uri: uri })
        });

        dispatch('created');
    }

    const [validity, validate] = createFieldValidator(
        requiredValidator(),
        uriValidator()
    );

</script>

<form on:submit|preventDefault={createBookmark}>
    <h2>Nieuwe bookmark</h2>
    <input
        type="text"
        placeholder="uri"
        bind:value={uri}
        required
        class:field-danger={!$validity.valid}
        class:field-success={$validity.valid}
        use:validate={uri}
    />
    <button>Opslaan</button>
</form>

<style>
    input {
        outline: none;
        border-width: 2px;
    }

    .field-danger {
        border-color: red;
    }

    .field-success {
        border-color: green;
    }
</style>
