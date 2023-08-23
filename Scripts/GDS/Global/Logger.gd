extends Script
class_name Logger

static var _CS_LOGGER = load("res://Scripts/CS/Global/Logger.cs")


static func log_msg(msg: Array) -> void:
	_CS_LOGGER.new().Log(msg)
