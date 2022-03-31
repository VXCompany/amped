<script>
import { createEventDispatcher } from 'svelte';

    import { createFieldValidator } from './validation';
    import { uriValidator, requiredValidator } from './validators.js';
    
    let uri = "";

    const dispatch = createEventDispatcher();

    async function createBookmark() {
        const response = await fetch(AMPED_API_URL + '/Bookmark/create', {
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
    <button class="btn">Opslaan</button>
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

    .btn {
		background-color: #5E44CA;
		color: white;
		padding: 0.5rem 1.2rem;
		border-radius: 15px ;
		text-decoration: none;
	}

    input[type="text"]{
      border-radius:15px;
      -moz-border-radius:15px;
      -webkit-border-radius:15px;
    }

</style>
