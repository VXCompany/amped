<script>
    import { createFieldValidator } from './validation';
    import { uriValidator, requiredValidator } from './validators.js'
    let uri = '';
    function createBookmark() {

        console.log(uri);
    }

    const [ validity, validate ] = createFieldValidator(requiredValidator(), uriValidator())

    // function validateUri () {
    //     return function foo (value) {
    //         console.log('validating')
    //       return (value !== 'uri') || 'Deze uri is ongeldig'
    //     }
    // }
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
    use:validate={uri}>
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