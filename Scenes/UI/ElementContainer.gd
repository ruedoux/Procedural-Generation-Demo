extends VBoxContainer

@onready var container: Control = $Container
@onready var checkBox: CheckBox = $CheckBox


func _ready() -> void:
	_on_check_box_toggled(false)


func _on_check_box_toggled(toggled: bool) -> void:
	if toggled:
		container.show()
		change_checkbox_text(toggled)
	else:
		container.hide()
		change_checkbox_text(toggled)


func change_checkbox_text(toggled: bool) -> void:
	var containerName: String = ""
	if container.get_child_count() > 0:
		containerName = container.get_child(0).name

	if toggled:
		checkBox.text = containerName + " hide"
	if not toggled:
		checkBox.text = containerName + " show"
