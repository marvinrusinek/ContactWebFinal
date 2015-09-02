$ ->
	class Helper
		form : ->
			$(@).parents("form")

	h = new Helper

	$("h3.allFilters").click ->
		form = $(@).parents("form")
		$("input:checkbox", form).removeAttr("checked")
		#form.submit()
	
	$("h3.filterHeader").click ->
		form = $(@).parents("form")
		container = $(@).parents("div.filterContainer")
		$("input:checkbox", container).removeAttr("checked")
		form.submit()

	$("input.filter").click ()->
		form = $(@).parents("form")
		name = $(@).attr("name")
		container = $(@).parents("div.filterContainer")
		checked = $("input[name=#{name}]:checked", container)
		(i) -> $(@).attr(name, "#{name}[#{i}]") in checked
		notChecked = $("input[name=#{name}]:not(:checked)", container)
		(i) -> $(@).removeAttr("name") in notChecked
		form.submit()
	
