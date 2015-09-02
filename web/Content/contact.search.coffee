	class @ContactSearch
		constructor: (id, url) ->
			$ ->
				$(id).autocomplete({
					source : (req, res) -> console.log(req)
				})
	
