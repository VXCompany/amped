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
    <div class="wrapper">
        <div class="container">
            <input 
                type="input" placeholder="Url" 
                bind:value={uri}
                required
                class:field-danger={!$validity.valid}
                class:field-success={$validity.valid}
                use:validate={uri}
            />
            <button type="submit" id="add-btn">Add bookmark</button>
        </div>
    </div>
</form>

<style>
    
    .wrapper {
        margin: 3rem auto;
        max-width: 40rem;
        padding: 2rem;
    }

    .container {
        display: flex;
    }

    input {
        outline: none;
        border-width: 2px;
        padding: 0.8rem 1.1rem;
        border-top-left-radius: 15px;
        border-bottom-left-radius: 15px;
        border: solid 2px #94A3B8;
        flex-grow: 1;
        min-width: 0;
    }

    .field-danger {
        border-color: red;
    }

    .field-success {
        border-color: green;
    }

    #add-btn {
        padding: 0.8rem 1.1rem;
        border-top-right-radius: 15px;
        border-bottom-right-radius: 15px;
        background-color: #5E44CA;
        color: white;
        border: none;
        cursor: pointer;
    }

    @media screen and (max-width: 767px) {
		.container {
			flex-wrap: wrap;
		}
        input {
            border-radius: 15px;
        }
		#add-btn {
			order: 3;
			flex: 100%;
			margin-top: 1rem;
            border-radius: 15px;
		}
	}

</style>
